using UnityEngine;

public partial class Ball
{
	public Transform CollapsingForceGroup;
	public ParticleSystem ShockWave;
	public Transform ShieldQuad;
	public AudioSource ShieldSound;
	public AudioSource CrownSound;
	public AudioSource BoxSound;
	private FrameAnim.Frame ballCollapsingForce;
	private bool collapsingForceBound;
	public static bool breakEnable;
	public static int breakNum = -1;

	private void ResetExpansionPower()
	{
		ResetLaterPower();
		Reset1782Power();
		// Freshly attached donor shields start visible in the prefab. Native
		// Ball.Initialize hides them, after the new Ball bindings exist.
		HideShield();
		breakEnable = false;
		breakNum = -1;
		if (CollapsingForceGroup != null)
		{
			if (!collapsingForceBound) ballCollapsingForce = new FrameAnim.Frame(CollapsingForceGroup);
			collapsingForceBound = true;
			ballCollapsingForce.Hide();
		}
	}
	private void UpdateExpansionPower()
	{
		if (!breakEnable) return;
		if (breakNum == -1) breakNum = TilePosition.y + 11;
		// The donor uses equality, including its segment-row boundary quirk.
		if (TilePosition.y == breakNum)
		{
			breakNum = -1;
			breakEnable = false;
			HideCollapsingForce();
		}
	}
	public void ShowCollapsingForce()
	{
		if (!DeviceQualityChecker.QualityIsNotPour()) return;
		if (!collapsingForceBound)
		{
			if (CollapsingForceGroup == null) throw new System.InvalidOperationException("Missing native Ball.CollapsingForceGroup binding");
			ballCollapsingForce = new FrameAnim.Frame(CollapsingForceGroup);
			collapsingForceBound = true;
		}
		ballCollapsingForce.Show();
	}
	public void HideCollapsingForce()
	{
		if (DeviceQualityChecker.QualityIsNotPour() && collapsingForceBound) ballCollapsingForce.Hide();
		if (successiveJumpBound && IsDead) ballSuccessiveJump.Hide();
	}
	public void ShowShockWave()
	{
		if (DeviceQualityChecker.QualityIsNotPour())
		{
			if (ShockWave == null) throw new System.InvalidOperationException("Missing native Ball.ShockWave binding");
			ShockWave.Play();
		}
	}
	public void ShowShield()
	{
		if (ShieldQuad == null) throw new System.InvalidOperationException("Missing native Ball.ShieldQuad binding");
		TransformUtils.Show(ShieldQuad);
	}
	public void HideShield()
	{
		if (ShieldQuad != null) TransformUtils.Hide(ShieldQuad);
	}
}
