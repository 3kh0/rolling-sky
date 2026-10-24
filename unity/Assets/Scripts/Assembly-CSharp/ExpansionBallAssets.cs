using UnityEngine;

// Attach the recovered donor subtrees to the corresponding accepted Ball
// parents, without modifying the first-eight prefab or replacing its geometry.
// Parent paths and local TRS are recorded in expansion-import/*/ball-attachments.json.
public static class ExpansionBallAssets
{
	public static void Initialize(Ball ball)
	{
		if (!ExpansionGameplay.Enabled) return;
		Transform sounds = ball.transform.Find("Offset/Sounds");
		if (sounds == null || ball.DisplayGroup == null)
			throw new System.InvalidOperationException("Missing original ball attachment parent");
		if (ball.CollapsingForceGroup == null)
		{
			ball.CollapsingForceGroup = Attach("Classic1471/Prefabs/Tx_White Lines", ball.DisplayGroup);
			ball.ShockWave = Required<ParticleSystem>(ball.CollapsingForceGroup.Find("tx_chongjibo"));
		}
		if (ball.ShieldQuad == null) ball.ShieldQuad = Attach("Classic1471/Prefabs/tx_hudun", ball.DisplayGroup);
		if (ball.ShieldSound == null) ball.ShieldSound = Required<AudioSource>(Attach("Classic1471/Prefabs/Shield", sounds));
		if (ball.CrownSound == null) ball.CrownSound = Required<AudioSource>(Attach("Classic1471/Prefabs/Crown", sounds));
		if (ball.BoxSound == null) ball.BoxSound = Required<AudioSource>(Attach("Classic1471/Prefabs/Box", sounds));
		if (!ExpansionGameplay.Later) return;
		if (ball.SuccessiveJumpGroup == null)
		{
			// The source has this additional identity-transform parent between
			// DisplayGroup and the exported TwistJumper subtree.
			Transform twist = new GameObject("TwistJump").transform;
			twist.SetParent(ball.DisplayGroup, false);
			ball.SuccessiveJumpGroup = Attach("Classic1561/Prefabs/TwistJumper", twist);
		}
		if (ball.SuccessiveJumperSound == null) ball.SuccessiveJumperSound = Required<AudioSource>(Attach("Classic1561/Prefabs/SuccessiveJumperSound", sounds));
		if (ball.AccelerateSound == null) ball.AccelerateSound = Required<AudioSource>(Attach("Classic1561/Prefabs/AccelerateSound", sounds));
		if (ball.DecelerationSound == null) ball.DecelerationSound = Required<AudioSource>(Attach("Classic1561/Prefabs/DecelerationSound", sounds));
	}

	private static Transform Attach(string path, Transform parent)
	{
		GameObject prefab = Resources.Load<GameObject>(path);
		if (prefab == null) throw new System.InvalidOperationException("Missing original ball subtree: " + path);
		Transform child = Object.Instantiate(prefab).transform;
		BufferManager.RestoreClassicAnimationNames(child.gameObject);
		child.name = prefab.name;
		child.SetParent(parent, false);
		child.localPosition = prefab.transform.localPosition;
		child.localRotation = prefab.transform.localRotation;
		child.localScale = prefab.transform.localScale;
		return child;
	}

	private static T Required<T>(Transform root) where T : Component
	{
		T component = root == null ? null : root.GetComponent<T>();
		if (component == null) throw new System.InvalidOperationException("Missing original ball component: " + typeof(T).Name);
		return component;
	}
}
