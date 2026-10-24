using UnityEngine;

// 1.5.6.1 GameManager.InGameSmoothDeltaTime. Feed this at the beginning of
// GameManager.Update, even while paused/in menus, and clear on run initialize.
// Earlier revisions continue using Unity's smoothDeltaTime unchanged.
public static class ClassicGameplayClock
{
	private static readonly MathUtils.Buffer samples = new MathUtils.Buffer(20);
	public static float DeltaTime { get { return samples.GetAverage(); } }
	public static void UpdateFrame() { samples.Add(Time.deltaTime); }
	public static void Reset() { samples.Clear(); }
}
