using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// No runtime component or dependency. The exporter writes native versioned YAML;
// Unity's serializer performs the 5.4 -> 2017 migration, not a prefab approximation.
public static class ClassicPrefabImport
{
    private const string Root = "Assets/Classic1471/";

    [Serializable] public class Manifest
    {
        public string targetUnity;
        public PrefabRecord[] prefabs;
        public Dependency[] dependencies;
        public AudioRecord[] audio;
        public string[] unsupported;
    }
    [Serializable] public class PrefabRecord
    {
        public string path, resource, name;
        public Node[] nodes;
        public ComponentCheck[] checks;
    }
    [Serializable] public class Node
    {
        public int sourceId, transformId, parent, layer;
        public string name, tag;
        public bool active;
        public Vector3 position, scale;
        public Quaternion rotation;
        public int[] children, componentIds;
        public string[] componentTypes;
    }
    [Serializable] public class ComponentCheck
    {
        public int sourceId;
        public string type;
        public Reference[] references;
        public ParticleCheck particle;
    }
    [Serializable] public class Reference { public string field, path; public int localId; public bool sourceNull; }
    [Serializable] public class ParticleCheck
    {
        public float duration, speed;
        public ParticleCurve lifetime, startSpeed, size, rate;
        public bool loop, prewarm, playOnAwake, local, emission;
        public int maxParticles, rateMode;
    }
    [Serializable] public class ParticleCurve
    {
        public float scalar;
        public int minMaxState;
        public SourceCurve minCurve, maxCurve;
    }
    [Serializable] public class SourceCurve { public SourceKey[] m_Curve; }
    [Serializable] public class SourceKey { public float time, value, inSlope, outSlope; }
    [Serializable] public class Dependency { public string path, type; public int width, height, mipCount; }
    [Serializable] public class AudioRecord { public string path; public int channels, frequency, frames; }
    [Serializable] public class MeshFile { public MeshCheck[] meshes; }
    [Serializable] public class MeshCheck
    {
        public string path;
        public Channel[] channels;
        public SubMesh[] submeshes;
        public float[] bindPoses;
    }
    [Serializable] public class Channel { public string name; public int dimension; public float[] values; }
    [Serializable] public class SubMesh { public int topology; public int[] indices; }
    [Serializable] public class Report
    {
        public string unityVersion;
        public int prefabs, nodes, meshes, meshValues, references, audioClips, particles;
        public string status;
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException("Classic1471: " + message);
    }

    private static void Near(float actual, float expected, string message)
    {
        Require(Mathf.Abs(actual - expected) <= 0.000002f * Mathf.Max(1, Mathf.Abs(expected)),
            message + ": expected " + expected + ", got " + actual);
    }

    private static Manifest ReadManifest()
    {
        Manifest result = JsonUtility.FromJson<Manifest>(File.ReadAllText(Root + "ImportManifest.json"));
        Require(Application.unityVersion == result.targetUnity, "Use Unity " + result.targetUnity);
        Require(result.unsupported != null && result.unsupported.Length == 0, "Unsupported donor objects remain");
        return result;
    }

    [MenuItem("Tools/Classic 1.4.7.1/Import and validate levels 6-8 assets")]
    public static void ImportAndValidate()
    {
        Manifest manifest = ReadManifest();
        // Dependencies first, prefab references second. Never reserialize first-five assets.
        foreach (Dependency dependency in manifest.dependencies.OrderBy(x => x.type == "Material" ? 1 : 0))
            AssetDatabase.ImportAsset(dependency.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
        foreach (PrefabRecord prefab in manifest.prefabs)
            AssetDatabase.ImportAsset(prefab.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
        Validate();
    }

    [MenuItem("Tools/Classic 1.4.7.1/Validate imported levels 6-8 assets")]
    public static void Validate()
    {
        Manifest manifest = ReadManifest();
        Report report = new Report { unityVersion = Application.unityVersion };
        foreach (PrefabRecord record in manifest.prefabs)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(record.path);
            Require(prefab != null && prefab.name == record.name, "Missing/wrong prefab " + record.path);
            Require(Resources.Load<GameObject>(record.resource) == prefab, "Resources.Load failed: " + record.resource);
            Dictionary<int, Node> definitions = record.nodes.ToDictionary(x => x.transformId);
            Dictionary<int, Object> objects = new Dictionary<int, Object>();
            Node root = record.nodes.Single(x => x.parent == 0);
            CheckNode(prefab.transform, root, definitions, objects, record.path, report);
            Require(prefab.GetComponentsInChildren<Transform>(true).Length == record.nodes.Length,
                "Hierarchy node count differs: " + record.path);
            foreach (ComponentCheck check in record.checks)
            {
                Object component = objects[check.sourceId];
                SerializedObject serialized = new SerializedObject(component);
                foreach (Reference reference in check.references)
                {
                    SerializedProperty property = serialized.FindProperty(reference.field);
                    Require(property != null && property.propertyType == SerializedPropertyType.ObjectReference,
                        record.path + " / " + check.type + " missing reference field " + reference.field);
                    if (reference.sourceNull)
                    {
                        Require(reference.field.StartsWith("m_Materials.Array.data[", StringComparison.Ordinal)
                            && string.IsNullOrEmpty(reference.path) && reference.localId == 0,
                            record.path + " has an invalid source-null material record");
                        Require(property.objectReferenceValue == null,
                            record.path + " / " + check.type + " originally null material changed: " + reference.field);
                    }
                    else
                    {
                        Object expected = reference.localId != 0 ? objects[reference.localId] : AssetDatabase.LoadMainAssetAtPath(reference.path);
                        Require(expected != null && property.objectReferenceValue == expected,
                            record.path + " / " + check.type + " reference changed: " + reference.field + " -> " + reference.path);
                    }
                    report.references++;
                }
                if (check.type == "ParticleSystem")
                {
                    CheckParticle((ParticleSystem)component, check.particle, record.path);
                    report.particles++;
                }
            }
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
                foreach (Material material in renderer.sharedMaterials)
                    // Null slots must match the original references checked above.
                    // In particular, Tile.DisplayAddition binds shadow materials at runtime.
                    if (material != null)
                        Require(material.shader != null && !ShaderHasError(material.shader),
                            record.path + " has a missing shader or shader compiler error");
            foreach (Animation animation in prefab.GetComponentsInChildren<Animation>(true))
                foreach (AnimationState state in animation)
                    Require(state.clip != null && state.clip.legacy && state.clip.length > 0,
                        record.path + " has a missing/empty/non-legacy animation " + state.name);
            report.prefabs++;
        }
        MeshFile meshes = JsonUtility.FromJson<MeshFile>(File.ReadAllText(Root + "MeshChecks.json"));
        foreach (MeshCheck mesh in meshes.meshes) CheckMesh(mesh, report);
        foreach (AudioRecord record in manifest.audio)
        {
            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(record.path);
            // Unity 2017 can report samples by truncating a float duration. The
            // 23039/23040/23041-frame controls and imported FSB headers confirm
            // this under-report without lost frames. Require the exact duration,
            // not a blanket +/-1 allowance that could hide actual truncation.
            float duration = (float)((double)record.frames / record.frequency);
            int reportedFrames = (int)((double)duration * record.frequency);
            Require(clip != null && clip.channels == record.channels && clip.frequency == record.frequency &&
                clip.length == duration && (clip.samples == record.frames || clip.samples == reportedFrames),
                "Audio samples/channels/rate differ: " + record.path);
            report.audioClips++;
        }
        foreach (Dependency dependency in manifest.dependencies)
        {
            Require(AssetDatabase.LoadMainAssetAtPath(dependency.path) != null, "Missing dependency " + dependency.path);
            if (dependency.type == "Texture2D")
            {
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(dependency.path);
                Require(texture != null && texture.width == dependency.width && texture.height == dependency.height &&
                    texture.mipmapCount == dependency.mipCount, "Texture dimensions/mips differ: " + dependency.path);
            }
        }
        report.status = "PASS: hierarchy/order/transforms/component types/references/mesh channels/submeshes/skin/bind poses/audio dimensions/particle core settings. Visual gameplay and all particle curves still require runtime comparison.";
        Directory.CreateDirectory("Library");
        File.WriteAllText("Library/Classic1471-validation.json", JsonUtility.ToJson(report, true));
        Debug.Log("Classic1471 validation: " + JsonUtility.ToJson(report));
    }

    private static void CheckNode(Transform transform, Node node, Dictionary<int, Node> definitions,
        Dictionary<int, Object> objects, string path, Report report)
    {
        GameObject go = transform.gameObject;
        string label = path + " / " + node.name;
        Require(go.name == node.name && go.activeSelf == node.active && go.layer == node.layer && go.tag == node.tag,
            "Name/active/layer/tag mismatch: " + label);
        for (int i = 0; i < 3; i++)
        {
            Near(transform.localPosition[i], node.position[i], label + " position");
            Near(transform.localScale[i], node.scale[i], label + " scale");
        }
        // Angle(q, q) can report 0.04 degrees for identical float quaternions
        // whose rounded squared norm is slightly below one. Compare the stored
        // components at the same tolerance as other transforms, allowing q == -q.
        Quaternion rotation = transform.localRotation;
        float sign = Quaternion.Dot(rotation, node.rotation) < 0f ? -1f : 1f;
        for (int i = 0; i < 4; i++) Near(rotation[i] * sign, node.rotation[i], label + " rotation");
        Component[] components = go.GetComponents<Component>();
        Require(components.Length == node.componentTypes.Length, "Component count mismatch " + label);
        objects.Add(node.sourceId, go);
        for (int i = 0; i < components.Length; i++)
        {
            Require(components[i] != null && components[i].GetType().Name == node.componentTypes[i], "Missing/reordered component " + label);
            objects.Add(node.componentIds[i], components[i]);
        }
        Require(transform.childCount == node.children.Length, "Child count mismatch " + label);
        for (int i = 0; i < node.children.Length; i++)
            CheckNode(transform.GetChild(i), definitions[node.children[i]], definitions, objects, path, report);
        report.nodes++;
    }

    private static void CheckParticle(ParticleSystem particle, ParticleCheck expected, string label)
    {
        ParticleSystem.MainModule main = particle.main;
        Near(main.duration, expected.duration, label + " particle duration");
        Near(main.simulationSpeed, expected.speed, label + " particle speed");
        // Prewarm is effective only on looping systems; Unity migration clears
        // the donor Turret's inert prewarm=true/loop=false combination.
        Require(main.loop == expected.loop && main.prewarm == (expected.prewarm && expected.loop) && main.playOnAwake == expected.playOnAwake &&
            main.maxParticles == expected.maxParticles, "Particle main flags changed: " + label);
        Require(main.simulationSpace == (expected.local ? ParticleSystemSimulationSpace.Local : ParticleSystemSimulationSpace.World),
            "Particle simulation space changed: " + label);
        CheckCurve(main.startLifetime, expected.lifetime, label + " lifetime");
        CheckCurve(main.startSpeed, expected.startSpeed, label + " speed");
        CheckCurve(main.startSize, expected.size, label + " size");
        Require(particle.emission.enabled == expected.emission, "Particle emission flag changed: " + label);
        CheckCurve(expected.rateMode == 0 ? particle.emission.rateOverTime : particle.emission.rateOverDistance,
            expected.rate, label + " emission");
    }

    private static void CheckCurve(ParticleSystem.MinMaxCurve actual, ParticleCurve expected, string label)
    {
        Require((int)actual.mode == expected.minMaxState, label + " curve mode changed");
        switch (actual.mode)
        {
            case ParticleSystemCurveMode.Constant:
                Near(actual.constant, expected.scalar, label + " constant");
                break;
            case ParticleSystemCurveMode.TwoConstants:
                // Unity 5.4 stores both endpoints as normalized one-key curves.
                // Their order is intentional: Tetris speed runs from 3 to 1.
                Require(expected.minCurve.m_Curve.Length == 1 && expected.maxCurve.m_Curve.Length == 1,
                    label + " random constants do not have single-key source curves");
                Near(actual.constantMin, expected.scalar * expected.minCurve.m_Curve[0].value, label + " minimum");
                Near(actual.constantMax, expected.scalar * expected.maxCurve.m_Curve[0].value, label + " maximum");
                break;
            case ParticleSystemCurveMode.Curve:
            case ParticleSystemCurveMode.TwoCurves:
                Near(actual.curveMultiplier, expected.scalar, label + " multiplier");
                CheckCurveKeys(actual.curveMax, expected.maxCurve, label + " upper curve");
                if (actual.mode == ParticleSystemCurveMode.TwoCurves)
                    CheckCurveKeys(actual.curveMin, expected.minCurve, label + " lower curve");
                break;
            default:
                throw new InvalidOperationException(label + " unsupported particle curve mode");
        }
    }

    private static void CheckCurveKeys(AnimationCurve actual, SourceCurve expected, string label)
    {
        Require(actual != null && actual.length == expected.m_Curve.Length, label + " key count changed");
        for (int i = 0; i < actual.length; i++)
        {
            Keyframe key = actual[i];
            SourceKey source = expected.m_Curve[i];
            Near(key.time, source.time, label + " time");
            Near(key.value, source.value, label + " value");
            Near(key.inTangent, source.inSlope, label + " incoming tangent");
            Near(key.outTangent, source.outSlope, label + " outgoing tangent");
        }
    }

    private static bool ShaderHasError(Shader shader)
    {
        // Verified against the installed 2017.4.40f1 editor: ShaderHasError
        // does not exist; GetShaderErrors returns internal ShaderError records.
        MethodInfo query = typeof(ShaderUtil).GetMethod("GetShaderErrors", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        Require(query != null, "Editor lacks GetShaderErrors; shader validation cannot be claimed");
        Array messages = query.Invoke(null, new object[] { shader }) as Array;
        Require(messages != null, "Editor returned an unreadable shader diagnostic list");
        bool failed = false;
        foreach (object message in messages)
        {
            FieldInfo warning = message.GetType().GetField("warning");
            FieldInfo text = message.GetType().GetField("message");
            Require(warning != null && warning.FieldType == typeof(int) && text != null,
                "Unknown Unity shader diagnostic representation");
            int kind = (int)warning.GetValue(message);
            Require(kind == 0 || kind == 1, "Unknown Unity shader diagnostic kind: " + kind);
            string detail = shader.name + ": " + text.GetValue(message);
            if (kind == 0) { failed = true; Debug.LogError(detail); }
            else Debug.LogWarning(detail);
        }
        return failed;
    }

    private static void CheckMesh(MeshCheck expected, Report report)
    {
        Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(expected.path);
        Require(mesh != null && mesh.subMeshCount == expected.submeshes.Length, "Mesh/submesh count mismatch: " + expected.path);
        // Mesh getters are available in editor mode even for non-readable player meshes.
        foreach (Channel channel in expected.channels)
        {
            List<float> actual = new List<float>();
            if (channel.name == "Vertices" || channel.name == "Normals")
            {
                // Unity exposes normals as Vector3 even when the original vertex
                // buffer has a fourth padding component. Never drop nonzero data.
                bool paddedNormal = channel.name == "Normals" && channel.dimension == 4;
                if (paddedNormal)
                    for (int i = 3; i < channel.values.Length; i += 4)
                        Require(channel.values[i] == 0f, "Nonzero normal padding " + expected.path);
                foreach (Vector3 value in channel.name == "Vertices" ? mesh.vertices : mesh.normals)
                {
                    for (int c = 0; c < 3; c++) actual.Add(value[c]);
                    if (paddedNormal) actual.Add(0f);
                }
            }
            else if (channel.name == "Tangents")
                foreach (Vector4 value in mesh.tangents)
                    for (int c = 0; c < 4; c++) actual.Add(value[c]);
            else if (channel.name == "Colors")
                foreach (Color value in mesh.colors)
                    for (int c = 0; c < 4; c++) actual.Add(value[c]);
            else if (channel.name.StartsWith("UV", StringComparison.Ordinal))
            {
                List<Vector4> uvs = new List<Vector4>();
                mesh.GetUVs(int.Parse(channel.name.Substring(2)), uvs);
                foreach (Vector4 value in uvs)
                    for (int c = 0; c < channel.dimension; c++) actual.Add(value[c]);
            }
            else if (channel.name == "BoneIndices" || channel.name == "BoneWeights")
                foreach (BoneWeight value in mesh.boneWeights)
                    if (channel.name == "BoneIndices") actual.AddRange(new float[] { value.boneIndex0, value.boneIndex1, value.boneIndex2, value.boneIndex3 });
                    else actual.AddRange(new float[] { value.weight0, value.weight1, value.weight2, value.weight3 });
            else throw new InvalidOperationException("Unsupported mesh channel " + channel.name);
            Require(actual.Count == channel.values.Length, "Mesh channel size mismatch " + expected.path + " / " + channel.name);
            for (int i = 0; i < actual.Count; i++) Near(actual[i], channel.values[i], expected.path + " / " + channel.name + "[" + i + "]");
            report.meshValues += actual.Count;
        }
        for (int s = 0; s < expected.submeshes.Length; s++)
        {
            Require((int)mesh.GetTopology(s) == expected.submeshes[s].topology, "Mesh topology mismatch " + expected.path);
            Require(mesh.GetIndices(s).SequenceEqual(expected.submeshes[s].indices), "Mesh index order mismatch " + expected.path);
        }
        Require(mesh.bindposes.Length * 16 == expected.bindPoses.Length, "Bind pose count mismatch " + expected.path);
        for (int i = 0; i < mesh.bindposes.Length; i++)
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++) Near(mesh.bindposes[i][r, c], expected.bindPoses[i * 16 + r * 4 + c], "Bind pose " + expected.path);
        report.meshes++;
    }
}
