using UnityEngine;

public partial class Tile
{
	private static readonly int[] expansionSoundCounts = new int[System.Enum.GetValues(typeof(DangerType)).Length];
	private static readonly float[] expansionSoundTimes = new float[expansionSoundCounts.Length];
	private static int expansionMoverSoundCount;
	private static float expansionMoverSoundTime;

	private bool CanPlayExpansionSound()
	{
		bool limited = IsMover || IsMoverAuto;
		switch (dangerType)
		{
		case DangerType.Crusher: case DangerType.CrusherSafe:
		case DangerType.Binary: case DangerType.BinaryOffset: case DangerType.BinaryMini: case DangerType.BinaryMiniOffset:
		case DangerType.TallTree: case DangerType.TallTreeSlider: case DangerType.PillarLaser: case DangerType.Laser: case DangerType.LaserSlider:
			limited = true; break;
		}
		if (!limited) return true;
		float time = Time.realtimeSinceStartup;
		if (IsMover)
		{
			if (expansionMoverSoundCount < 5) { expansionMoverSoundTime = time; expansionMoverSoundCount++; return true; }
			if (time - expansionMoverSoundTime > 0.1f) { expansionMoverSoundTime = time; expansionMoverSoundCount = 1; return true; }
			return false;
		}
		int index = (int)dangerType;
		if (expansionSoundCounts[index] < 2) { expansionSoundTimes[index] = time; expansionSoundCounts[index]++; return true; }
		if (time - expansionSoundTimes[index] > 0.15f) { expansionSoundTimes[index] = time; expansionSoundCounts[index] = 1; return true; }
		return false;
	}
}
