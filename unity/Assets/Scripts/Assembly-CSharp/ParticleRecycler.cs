using UnityEngine;

public class ParticleRecycler : MonoBehaviour
{
	private static float defaultDestructionTime = 3f;

	private TimePeices.Ticker destructionTimer;

	private bool initialized;

	private bool destructionOrdered;

	private PrefabName prefabName;

	private ParticleEmitter[] emitters;

	private Firework[] fireworks;

	public void Initialize(PrefabName prefabName)
	{
		this.prefabName = prefabName;
		if (destructionTimer == null)
		{
			ParticleEmitter[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleEmitter>();
			float length;
			if (componentsInChildren == null)
			{
				Firework[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<Firework>();
				if (componentsInChildren2 == null)
				{
					length = defaultDestructionTime;
				}
				else
				{
					float? num = null;
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						float totalAnimTime = componentsInChildren2[i].TotalAnimTime;
						if (!num.HasValue || totalAnimTime > num.Value)
						{
							num = totalAnimTime;
						}
					}
					length = ((!num.HasValue) ? defaultDestructionTime : num.Value);
				}
			}
			else
			{
				float? num2 = null;
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					float maxEnergy = componentsInChildren[j].maxEnergy;
					if (!num2.HasValue || maxEnergy > num2.Value)
					{
						num2 = maxEnergy;
					}
				}
				length = ((!num2.HasValue) ? defaultDestructionTime : num2.Value);
			}
			destructionTimer = new TimePeices.Ticker(length);
		}
		initialized = true;
		destructionOrdered = false;
	}

	private void Update()
	{
		if (!initialized || destructionOrdered || !destructionTimer.Tick())
		{
			return;
		}
		destructionOrdered = true;
		destructionTimer.ResetAll();
		if (emitters != null)
		{
			ParticleEmitter[] array = emitters;
			foreach (ParticleEmitter particleEmitter in array)
			{
				particleEmitter.emit = false;
			}
		}
		BufferManager.GiveGeo(givenGeoPrefabId: new PrefabID(PrefabType.Particles, prefabName), givenGeo: base.transform, hideGivenGeo: true);
		initialized = false;
	}
}
