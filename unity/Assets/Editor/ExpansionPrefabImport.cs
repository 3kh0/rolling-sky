using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// Separate manifest and entry point: never swaps the pinned levels 6-8 manifest.
// Reuse its Windows-verified checks without modifying that accepted importer.
public static class ExpansionPrefabImport
{
    private const string Root = "Assets/Classic1471/Expansion/";
    private const string SupplementRoot = "Assets/Classic1471/ExpansionSupplement/";
    private const string LaterRoot = "Assets/Classic1561/Expansion/";
    private const string NewerRoot = "Assets/Classic1782/Expansion/";
    private const string CircusRoot = "Assets/Classic1792/Expansion/";
    private const string SunnyRoot = "Assets/Classic325/Expansion/";

    [Serializable] public class Manifest : ClassicPrefabImport.Manifest
    {
        public NativeCheck[] nativeChecks;
        public BallBinding[] ballBindings;
        public ShaderReference[] shaderReferences;
    }
    [Serializable] public class BallBinding { public string field, type, resource, relativePath; }
    [Serializable] public class ShaderReference { public string builtin; }
    [Serializable] public class PropsBinding { public string path, canonicalPath, resource, name, guid; }
    [Serializable] public class NativeCheck
    {
        public string path, type;
        public int sourceId;
        public NativeField[] fields;
    }
    [Serializable] public class NativeField { public string name; public int value; public bool boolean; }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException("Expansion1471: " + message);
    }

    private static object AcceptedCheck(string name, params object[] args)
    {
        MethodInfo method = typeof(ClassicPrefabImport).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
        Require(method != null, "Accepted validator no longer supplies " + name);
        try { return method.Invoke(null, args); }
        catch (TargetInvocationException error)
        {
            throw new InvalidOperationException("Expansion1471 / " + name, error.InnerException ?? error);
        }
    }

    private static Manifest ReadManifest(string root)
    {
        Manifest result = JsonUtility.FromJson<Manifest>(File.ReadAllText(root + "ImportManifest.json"));
        Require(result != null && result.prefabs != null && result.nativeChecks != null, "Incomplete manifest");
        Require(Application.unityVersion == result.targetUnity, "Use Unity " + result.targetUnity);
        Require(result.unsupported != null && result.unsupported.Length == 0, "Unsupported source objects remain");
        return result;
    }

    [MenuItem("Tools/Classic 1.4.7.1/Import and validate expansion assets")]
    public static void ImportAndValidate()
    {
        ImportSet(Root);
        ImportSet(SupplementRoot);
        Validate();
    }

    [MenuItem("Tools/Classic 1.5.6.1/Import and validate expansion assets")]
    public static void ImportLaterAndValidate()
    {
        ImportSet(LaterRoot);
        PropsBinding props = JsonUtility.FromJson<PropsBinding>(File.ReadAllText(LaterRoot + "PropsBinding.json"));
        AssetDatabase.ImportAsset(props.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
        ValidateLater();
    }

    public static void ImportAllAndValidate()
    {
        ImportAndValidate();
        ImportLaterAndValidate();
        ImportNewerAndValidate();
        ImportCircusAndValidate();
        ImportSunnyAndValidate();
    }

    public static void ImportNewerAndValidate()
    {
        ImportSet(NewerRoot);
        ValidateSet(NewerRoot, "Expansion1782");
        ValidatePropsBinding(NewerRoot);
    }

    public static void ImportCircusAndValidate()
    {
        ImportSet(CircusRoot);
        ValidateCircus();
    }

    public static void ValidateCircus()
    {
        ValidateSet(CircusRoot, "Expansion1792");
        ValidatePropsBinding(CircusRoot);
    }

    public static void ImportSunnyAndValidate()
    {
        ImportSet(SunnyRoot);
        ValidateSunny();
    }

    public static void ValidateSunny()
    {
        ValidateSet(SunnyRoot, "Expansion325");
    }

    private static void ImportSet(string root)
    {
        Manifest manifest = ReadManifest(root);
        foreach (ClassicPrefabImport.Dependency dependency in manifest.dependencies.OrderBy(x => x.type == "Material" ? 1 : 0))
            AssetDatabase.ImportAsset(dependency.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
        foreach (ClassicPrefabImport.PrefabRecord prefab in manifest.prefabs)
            AssetDatabase.ImportAsset(prefab.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
    }

    [MenuItem("Tools/Classic 1.4.7.1/Validate imported expansion assets")]
    public static void Validate()
    {
        ValidateSet(Root, "Expansion1471");
        ValidateSet(SupplementRoot, "Expansion1471-supplement");
    }

    [MenuItem("Tools/Classic 1.5.6.1/Validate imported expansion assets")]
    public static void ValidateLater()
    {
        ValidateSet(LaterRoot, "Expansion1561");
        ValidatePropsBinding();
    }

    public static void ValidatePropsBinding()
    {
        ValidatePropsBinding(LaterRoot);
    }

    private static void ValidatePropsBinding(string root)
    {
        PropsBinding record = JsonUtility.FromJson<PropsBinding>(File.ReadAllText(root + "PropsBinding.json"));
        Require(Application.unityVersion == "2017.4.40f1", "Props check requires Unity 2017.4.40f1");
        Material material = Resources.Load<Material>(record.resource);
        Material canonical = AssetDatabase.LoadAssetAtPath<Material>(record.canonicalPath);
        Require(material != null && canonical != null && material != canonical && material.name == record.name,
            "Missing/wrong canonical Props resource");
        Require(AssetDatabase.GetAssetPath(material) == record.path && AssetDatabase.AssetPathToGUID(record.path) == record.guid,
            "Wrong Props resource asset identity");
        // Unity can rename the imported donor asset to its collision-safe file
        // stem. The live Resources alias must retain the native runtime name.
        Require(canonical.name == record.name || canonical.name == Path.GetFileNameWithoutExtension(record.canonicalPath),
            "Unexpected imported donor Props name");
        Require(material.shader == canonical.shader && MaterialValuesJson(material) == MaterialValuesJson(canonical),
            "Imported Props differs from the packaged donor material");
        Require(!(bool)AcceptedCheck("ShaderHasError", material.shader), "Props shader compiler error");
        Directory.CreateDirectory("Library");
        File.WriteAllText("Library/" + (root == CircusRoot ? "Expansion1792" : root == NewerRoot ? "Expansion1782" : "Expansion1561") + "-props-validation.json", JsonUtility.ToJson(record, true));
        Debug.Log("PASS: Resources.Load<Material>(" + record.resource + ") matches native packaged Props values and shader");
    }

    private static string MaterialValuesJson(Material material)
    {
        string json = EditorJsonUtility.ToJson(material);
        const string nameField = "\"m_Name\"\\s*:\\s*\"[^\"]*\"";
        Require(Regex.Matches(json, nameField).Count == 1, "Unexpected imported Material JSON name schema");
        // Names are checked separately above. Keep every other serialized field,
        // including shader/texture identities, keywords, flags and all values.
        return Regex.Replace(json, nameField, "\"m_Name\":\"\"");
    }

    private static readonly string[] LegacySubEmitterFields = {
        "subEmitterBirth", "subEmitterBirth1", "subEmitterCollision",
        "subEmitterCollision1", "subEmitterDeath", "subEmitterDeath1"
    };

    private static Dictionary<string, Object> CheckLegacySubEmitters(ParticleSystem particle,
        ClassicPrefabImport.ComponentCheck check, string path, Dictionary<int, Object> objects)
    {
        Dictionary<string, Object> migrated = new Dictionary<string, Object>();
        ClassicPrefabImport.Reference[] references = check.references.Where(
            r => r.field.StartsWith("SubModule.", StringComparison.Ordinal)).ToArray();
        if (references.Length == 0) return migrated;

        string label = path + " / ParticleSystem " + check.sourceId + " / SubModule";
        // Read only the immutable source YAML. Existing manifests record non-null
        // pointers, but not the six null slots or the module's enabled flag.
        // Do not reserialize a prefab just to make a validation path exist.
        MatchCollection documents = Regex.Matches(File.ReadAllText(path),
            @"(?ms)^--- !u!198 &" + check.sourceId + @"\r?\nParticleSystem:\r?\n(?<body>.*?)(?=^--- |\z)");
        Require(documents.Count == 1, label + " missing/duplicate native particle document");
        MatchCollection modules = Regex.Matches(documents[0].Groups["body"].Value,
            @"(?m)^  SubModule:\r?\n(?<body>(?:    [^\r\n]*\r?\n)+)");
        Require(modules.Count == 1, label + " missing/duplicate native SubModule");
        string[] lines = modules[0].Groups["body"].Value.TrimEnd('\r', '\n').Split('\n');
        Require(lines.Length == 7 && Regex.IsMatch(lines[0], @"^    enabled: [01]\r?$"),
            label + " unsupported native fixed-slot schema");
        bool enabled = lines[0].TrimEnd('\r').EndsWith("1", StringComparison.Ordinal);
        ParticleSystem.SubEmittersModule module = particle.subEmitters;
        Require(module.enabled == enabled, label + " enabled flag changed");

        // Unity 2017.4.40f1 SubModuleUI and ParticleSystemBindings define the
        // replacement array as subEmitters[].{emitter,type,properties}. Check it
        // through both SerializedObject and the version-matched public API.
        SerializedObject serialized = new SerializedObject(particle);
        SerializedProperty array = serialized.FindProperty("SubModule.subEmitters");
        Require(array != null && array.isArray, label + " missing migrated subEmitters array");
        Require(array.arraySize == module.subEmittersCount && module.subEmittersCount == references.Length,
            label + " migrated entry count differs: expected " + references.Length + ", got " + module.subEmittersCount);
        int index = 0;
        for (int slot = 0; slot < LegacySubEmitterFields.Length; slot++)
        {
            string field = "SubModule." + LegacySubEmitterFields[slot];
            Match pointer = Regex.Match(lines[slot + 1], "^    " + LegacySubEmitterFields[slot] + @": \{fileID: ([0-9]+)\}\r?$");
            Require(pointer.Success, label + " unsupported native slot: " + field);
            int sourceId = int.Parse(pointer.Groups[1].Value);
            ClassicPrefabImport.Reference[] records = references.Where(r => r.field == field).ToArray();
            if (sourceId == 0)
            {
                Require(records.Length == 0, label + " originally null slot has a manifest reference: " + field);
                continue;
            }
            Require(records.Length == 1 && !records[0].sourceNull && records[0].localId == sourceId && records[0].path == path,
                label + " native slot differs from the manifest: " + field);
            Object expected;
            Require(objects.TryGetValue(sourceId, out expected) && expected is ParticleSystem,
                label + " missing/wrong native sub-emitter target: " + field);
            SerializedProperty entry = array.GetArrayElementAtIndex(index);
            SerializedProperty emitter = entry.FindPropertyRelative("emitter");
            SerializedProperty type = entry.FindPropertyRelative("type");
            SerializedProperty properties = entry.FindPropertyRelative("properties");
            Require(emitter != null && emitter.propertyType == SerializedPropertyType.ObjectReference &&
                type != null && (type.propertyType == SerializedPropertyType.Integer || type.propertyType == SerializedPropertyType.Enum) &&
                properties != null && (properties.propertyType == SerializedPropertyType.Integer || properties.propertyType == SerializedPropertyType.Enum),
                label + " unsupported migrated entry schema at " + index);
            ParticleSystem actual = module.GetSubEmitterSystem(index);
            Require(actual == expected && emitter.objectReferenceValue == expected,
                label + " migrated target/order changed at " + index + " (" + field + ")");
            // The six fixed slots are Birth0/1, Collision0/1, Death0/1.
            Require((int)module.GetSubEmitterType(index) == slot / 2 && type.intValue == slot / 2,
                label + " migrated event type changed at " + index + " (" + field + ")");
            // The read-only 2017.4.40f1 Windows dump verifies properties=0 on
            // all six migrated links. It is not a field authored by the donor.
            Require(module.GetSubEmitterProperties(index) == ParticleSystemSubEmitterProperties.InheritNothing && properties.intValue == 0,
                label + " migrated inheritance changed at " + index + " (" + field + "): " + properties.intValue);
            migrated.Add(field, actual);
            index++;
        }
        Require(index == references.Length, label + " unrecognized/duplicate native SubModule reference");
        return migrated;
    }

    private static void CheckAudioSource(ClassicPrefabImport.AudioRecord record)
    {
        // Validate the recovered PCM source separately from Unity's platform
        // conversion. A WebGL rate change must not conceal changed source data.
        using (BinaryReader reader = new BinaryReader(File.OpenRead(record.path)))
        {
            Require(new string(reader.ReadChars(4)) == "RIFF", "Not a RIFF source: " + record.path);
            uint size = reader.ReadUInt32();
            Require(size + 8L == reader.BaseStream.Length && new string(reader.ReadChars(4)) == "WAVE",
                "Invalid source WAV length/header: " + record.path);
            bool formatSeen = false, dataSeen = false;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Require(reader.BaseStream.Length - reader.BaseStream.Position >= 8, "Truncated WAV chunk: " + record.path);
                string chunk = new string(reader.ReadChars(4));
                uint length = reader.ReadUInt32();
                long end = reader.BaseStream.Position + length;
                Require(end + (length & 1) <= reader.BaseStream.Length, "Truncated WAV data: " + record.path);
                if (chunk == "fmt ")
                {
                    Require(!formatSeen && length >= 16, "Missing/duplicate WAV format: " + record.path);
                    Require(reader.ReadUInt16() == 1 && reader.ReadUInt16() == record.channels &&
                        reader.ReadUInt32() == record.frequency && reader.ReadUInt32() == record.frequency * record.channels * 2 &&
                        reader.ReadUInt16() == record.channels * 2 && reader.ReadUInt16() == 16,
                        "Source PCM format/channels/rate changed: " + record.path);
                    formatSeen = true;
                }
                else if (chunk == "data")
                {
                    Require(!dataSeen && length == (long)record.frames * record.channels * 2,
                        "Source PCM frame count changed: " + record.path);
                    dataSeen = true;
                }
                reader.BaseStream.Position = end + (length & 1);
            }
            Require(formatSeen && dataSeen, "Source WAV lacks format/data: " + record.path);
        }
    }

    internal static void CheckAudio(ClassicPrefabImport.AudioRecord record)
    {
        CheckAudioSource(record);
        int frequency = record.frequency;
        int frames = record.frames;
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL)
        {
            // Unity 2017.4 WebGL forces AAC at 44100 Hz even with PreserveSampleRate.
            // Require that measured conversion explicitly, not any nearby rate or
            // duration. The original WAV and its importer metadata remain unchanged.
            AudioImporter importer = AssetImporter.GetAtPath(record.path) as AudioImporter;
            Require(importer != null, "Missing audio importer: " + record.path);
            SerializedObject serialized = new SerializedObject(importer);
            SerializedProperty setting = serialized.FindProperty("m_Output.outputSettings.sampleRateSetting");
            SerializedProperty rate = serialized.FindProperty("m_Output.outputSettings.sampleRateOverride");
            SerializedProperty format = serialized.FindProperty("m_Output.outputSettings.compressionFormat");
            Require(setting != null && rate != null && format != null &&
                setting.intValue == (int)AudioSampleRateSetting.OverrideSampleRate && rate.intValue == 44100 &&
                format.intValue == (int)AudioCompressionFormat.AAC,
                "Unexpected WebGL audio output settings: " + record.path);
            frequency = 44100;
            frames = (int)((long)record.frames * frequency / record.frequency);
        }
        AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(record.path);
        // Retain the accepted gate's measured float-duration truncation rule,
        // now applied to the exact converted frame count rather than the source.
        float duration = (float)((double)frames / frequency);
        int reportedFrames = (int)((double)duration * frequency);
        Require(clip != null && clip.channels == record.channels && clip.frequency == frequency &&
            clip.length == duration && (clip.samples == frames || clip.samples == reportedFrames),
            "Audio samples/channels/rate differ: " + record.path);
    }

    private static void ValidateSet(string root, string label)
    {
        Manifest manifest = ReadManifest(root);
        ClassicPrefabImport.Report report = new ClassicPrefabImport.Report { unityVersion = Application.unityVersion };
        HashSet<Shader> shaders = new HashSet<Shader>();
        foreach (ClassicPrefabImport.PrefabRecord record in manifest.prefabs)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(record.path);
            Require(prefab != null && prefab.name == record.name, "Missing/wrong prefab " + record.path);
            Require(Resources.Load<GameObject>(record.resource) == prefab, "Resources.Load failed: " + record.resource);
            Dictionary<int, ClassicPrefabImport.Node> definitions = record.nodes.ToDictionary(x => x.transformId);
            Dictionary<int, Object> objects = new Dictionary<int, Object>();
            ClassicPrefabImport.Node rootNode = record.nodes.Single(x => x.parent == 0);
            AcceptedCheck("CheckNode", prefab.transform, rootNode, definitions, objects, record.path, report);
            Require(prefab.GetComponentsInChildren<Transform>(true).Length == record.nodes.Length,
                "Hierarchy node count differs: " + record.path);
            foreach (ClassicPrefabImport.ComponentCheck check in record.checks)
            {
                Object component = objects[check.sourceId];
                SerializedObject serialized = new SerializedObject(component);
                Dictionary<string, Object> migrated = check.type == "ParticleSystem"
                    ? CheckLegacySubEmitters((ParticleSystem)component, check, record.path, objects)
                    : new Dictionary<string, Object>();
                foreach (ClassicPrefabImport.Reference reference in check.references)
                {
                    Object actual;
                    if (!migrated.TryGetValue(reference.field, out actual))
                    {
                        SerializedProperty property = serialized.FindProperty(reference.field);
                        Require(property != null && property.propertyType == SerializedPropertyType.ObjectReference,
                            record.path + " / " + check.type + " missing reference field " + reference.field);
                        actual = property.objectReferenceValue;
                    }
                    if (reference.sourceNull)
                    {
                        Require(string.IsNullOrEmpty(reference.path) && reference.localId == 0,
                            record.path + " has an invalid source-null record");
                        Require(actual == null,
                            record.path + " originally null reference changed: " + reference.field);
                    }
                    else
                    {
                        Object expected = reference.localId != 0 ? objects[reference.localId] :
                            reference.path == "builtin:Cube.fbx" ? Resources.GetBuiltinResource<Mesh>("Cube.fbx") :
                            reference.path == "builtin:New-Plane.fbx" ? Resources.GetBuiltinResource<Mesh>("New-Plane.fbx") :
                            reference.path == "builtin:Quad.fbx" ? Resources.GetBuiltinResource<Mesh>("Quad.fbx") :
                            AssetDatabase.LoadMainAssetAtPath(reference.path);
                        Require(expected != null && actual == expected,
                            record.path + " reference changed: " + reference.field + " -> " + reference.path);
                    }
                    report.references++;
                }
                if (check.type == "ParticleSystem")
                {
                    AcceptedCheck("CheckParticle", (ParticleSystem)component, check.particle, record.path);
                    report.particles++;
                }
            }
            foreach (NativeCheck check in manifest.nativeChecks.Where(x => x.path == record.path))
            {
                Object component = objects[check.sourceId];
                Require(component.GetType().Name == check.type, "Native component type changed: " + record.path);
                SerializedObject serialized = new SerializedObject(component);
                foreach (NativeField field in check.fields)
                {
                    SerializedProperty property = serialized.FindProperty(field.name);
                    Require(property != null, "Native field missing: " + record.path + "/" + check.type + "/" + field.name);
                    if (property.propertyType == SerializedPropertyType.Boolean)
                        Require(property.boolValue == (field.value != 0), "Native flag changed: " + field.name);
                    else
                    {
                        Require(property.propertyType == SerializedPropertyType.Integer || property.propertyType == SerializedPropertyType.Enum,
                            "Unexpected native property type: " + field.name);
                        Require(property.intValue == field.value, "Native value changed: " + field.name);
                    }
                }
            }
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
                foreach (Material material in renderer.sharedMaterials)
                    if (material != null)
                    {
                        Require(material.shader != null, "Missing shader: " + record.path);
                        shaders.Add(material.shader);
                    }
            foreach (Animation animation in prefab.GetComponentsInChildren<Animation>(true))
                foreach (AnimationState state in animation)
                    Require(state.clip != null && state.clip.legacy && state.clip.length > 0,
                        record.path + " has a missing/empty/non-legacy animation " + state.name);
            report.prefabs++;
        }
        ClassicPrefabImport.MeshFile meshes = JsonUtility.FromJson<ClassicPrefabImport.MeshFile>(File.ReadAllText(root + "MeshChecks.json"));
        foreach (ClassicPrefabImport.MeshCheck mesh in meshes.meshes) AcceptedCheck("CheckMesh", mesh, report);
        foreach (ClassicPrefabImport.AudioRecord record in manifest.audio)
        {
            CheckAudio(record);
            report.audioClips++;
        }
        foreach (ClassicPrefabImport.Dependency dependency in manifest.dependencies)
        {
            Object asset = AssetDatabase.LoadMainAssetAtPath(dependency.path);
            Require(asset != null, "Missing dependency " + dependency.path);
            if (dependency.type == "Texture2D")
            {
                Texture2D texture = asset as Texture2D;
                Require(texture != null && texture.width == dependency.width && texture.height == dependency.height &&
                    texture.mipmapCount == dependency.mipCount, "Texture dimensions/mips differ: " + dependency.path);
            }
            if (dependency.type == "Cubemap")
            {
                Cubemap texture = asset as Cubemap;
                Require(texture != null && texture.width == dependency.width && texture.height == dependency.height &&
                    texture.mipmapCount == dependency.mipCount, "Cubemap dimensions/mips differ: " + dependency.path);
            }
            if (dependency.type == "Avatar")
                Require(asset is Avatar && ((Avatar)asset).isValid, "Invalid imported avatar " + dependency.path);
            if (dependency.type == "Shader")
            {
                Require(asset is Shader, "Wrong shader asset type: " + dependency.path);
                if (root == SunnyRoot)
                {
                    Require(AssetImporter.GetAtPath(dependency.path) is ShaderImporter,
                        "Sunny needs rebuildable shader source, not a platform-baked Shader asset: " + dependency.path);
                    Require(((Shader)asset).isSupported, "Sunny shader has no supported GPU program: " + dependency.path);
                }
                shaders.Add((Shader)asset);
            }
        }
        if (manifest.ballBindings != null)
            foreach (BallBinding binding in manifest.ballBindings)
            {
                GameObject prefab = Resources.Load<GameObject>(binding.resource);
                Require(prefab != null, "Missing Ball binding resource " + binding.field);
                Transform node = string.IsNullOrEmpty(binding.relativePath) ? prefab.transform : prefab.transform.Find(binding.relativePath);
                Require(node != null && node.GetComponent(binding.type) != null,
                    "Missing native Ball binding component " + binding.field + " -> " + binding.resource + "/" + binding.relativePath);
            }
        if (manifest.shaderReferences != null)
            foreach (ShaderReference reference in manifest.shaderReferences)
                if (!string.IsNullOrEmpty(reference.builtin))
                {
                    Shader shader = Shader.Find(reference.builtin);
                    Require(shader != null, "Missing Unity built-in fallback: " + reference.builtin);
                    shaders.Add(shader);
                }
        foreach (Shader shader in shaders)
            Require(!(bool)AcceptedCheck("ShaderHasError", shader), "Shader compiler errors: " + shader.name);
        report.status = "PASS: expansion native hierarchy/references/meshes/particle core curves/audio dimensions/Animator/MeshCollider/Avatar checks. Full gameplay and graphics parity remain separate gates.";
        Directory.CreateDirectory("Library");
        File.WriteAllText("Library/" + label + "-validation.json", JsonUtility.ToJson(report, true));
        Debug.Log(label + " validation: " + JsonUtility.ToJson(report));
    }
}
