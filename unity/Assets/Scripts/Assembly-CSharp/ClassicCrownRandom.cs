// Gameplay-only port of 1.4.7.1 CrownRandom. UI/reward persistence belongs to
// GameManager; this owns only the native candidate order and reward draw.
public static class ClassicCrownRandom
{
	public enum CrownType { NOTHING, BOX, CROWN }
	public enum RewardType { BALL, SHIELD }
	private static readonly System.Random random = new System.Random();
	private static CrownType[] pool;
	private static int nextIndex;
	public static bool Enabled { get { return ExpansionGameplay.Enabled && (ExpansionGameplay.SourceNumber == 19 || ExpansionGameplay.SourceNumber == 21 || ExpansionGameplay.SourceNumber == 22); } }

	public static void StartRun()
	{
		if (!Enabled) return;
		if (pool == null)
		{
			pool = new CrownType[10];
			for (int i = 0; i < 10; i++) pool[i] = i < 3 ? CrownType.CROWN : (i < 5 ? CrownType.BOX : CrownType.NOTHING);
		}
		nextIndex = 0;
		for (int i = 9; i > 0; i--)
		{
			int j = random.Next(0, i + 1);
			CrownType value = pool[i]; pool[i] = pool[j]; pool[j] = value;
		}
		int ordinal = random.Next(1, 4);
		int last = random.Next(0, 5) != 0 ? 9 : 8;
		int crowns = 0;
		for (int i = 0; i < 10; i++)
		{
			if (pool[i] != CrownType.CROWN || ++crowns != ordinal) continue;
			CrownType value = pool[i]; pool[i] = pool[last]; pool[last] = value;
			break;
		}
	}

	public static CrownType GetCrownTypeOnCreate(ref int index)
	{
		index = nextIndex;
		// Native maps have exactly ten candidates. Fail loudly on invalid input,
		// rather than silently manufacturing extra crowns or consuming gem slots.
		if (pool == null || nextIndex >= 10) throw new System.InvalidOperationException("Invalid native crown candidate sequence");
		return pool[nextIndex++];
	}
	public static CrownType GetCrownType(int index) { return index >= 10 ? CrownType.NOTHING : pool[index]; }
	public static RewardType GetRewardType() { return random.Next(0, 5) == 0 ? RewardType.SHIELD : RewardType.BALL; }
}
