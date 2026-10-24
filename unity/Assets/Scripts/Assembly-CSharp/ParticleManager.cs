using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	private static Transform defaultTarget;

	private static Transform explosionsParent;

	private static bool initialized;

	public static void Initialize(Transform target)
	{
		defaultTarget = target;
		initialized = true;
	}

	public static void Detonate(PrefabName explosionType)
	{
		DetonateExplosion(explosionType, defaultTarget, null);
	}

	public static void Detonate(PrefabName explosionType, GameObject target)
	{
		if (target != null)
		{
			DetonateExplosion(explosionType, target.transform, null);
		}
	}

	public static void Detonate(PrefabName explosionType, Transform target)
	{
		DetonateExplosion(explosionType, target, null);
	}

	public static void Detonate(PrefabName explosionType, Color explosionColor)
	{
		DetonateExplosion(explosionType, defaultTarget, explosionColor);
	}

	public static void Detonate(PrefabName explosionType, GameObject target, Color explosionColor)
	{
		if (target != null)
		{
			DetonateExplosion(explosionType, target.transform, explosionColor);
		}
	}

	public static void Detonate(PrefabName explosionType, Transform target, Color explosionColor)
	{
		DetonateExplosion(explosionType, target, explosionColor);
	}

	private static void DetonateExplosion(PrefabName explosionType, Transform target, Color? explosionColor)
	{
		if (initialized)
		{
			if (ExplosionIsValid(explosionType))
			{
				Transform geo = BufferManager.GetGeo(PrefabType.Particles, explosionType);
				TransformUtils.Align(geo, target);
				ParticleRecycler particleRecycler = geo.gameObject.GetComponent<ParticleRecycler>();
				if (particleRecycler == null)
				{
					particleRecycler = geo.gameObject.AddComponent<ParticleRecycler>();
				}
				particleRecycler.Initialize(explosionType);
				if (explosionsParent == null)
				{
					explosionsParent = new GameObject("_Explosions").transform;
				}
				geo.parent = explosionsParent;
				ParticleEmitter[] componentsInChildren = geo.gameObject.GetComponentsInChildren<ParticleEmitter>();
				if (componentsInChildren != null)
				{
					ParticleEmitter[] array = componentsInChildren;
					foreach (ParticleEmitter particleEmitter in array)
					{
						particleEmitter.emit = true;
					}
				}
				Firework[] componentsInChildren2 = geo.gameObject.GetComponentsInChildren<Firework>();
				if (componentsInChildren2 != null)
				{
					Firework[] array2 = componentsInChildren2;
					foreach (Firework firework in array2)
					{
						firework.emit = true;
					}
				}
				bool flag = explosionType == PrefabName.PickupBig;
				ParticleAnimator[] componentsInChildren3 = geo.GetComponentsInChildren<ParticleAnimator>();
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					if (flag)
					{
						componentsInChildren3[k].colorAnimation = MaterialManager.GetPickupParticleColors();
					}
					else
					{
						componentsInChildren3[k].colorAnimation = MaterialManager.GetEnergyParticleColors();
					}
				}
			}
			else
			{
				Debug.LogError(string.Concat("Error DTN_UFE - attempt to detonate undefined explosion type: ", explosionType, (!(target != null)) ? null : (" at " + target.name)));
			}
		}
		else
		{
			Debug.LogError(ErrorStrings.FunctionNotInitialized("Detonate", "ParticleManager"));
		}
	}

	private static bool ExplosionIsValid(PrefabName explosionType)
	{
		return BufferManager.Prefabs[PrefabType.Particles].ContainsKey(explosionType);
	}
}
