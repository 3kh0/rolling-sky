using UnityEngine;

public partial class TileAnimator : MonoBehaviour
{
	private class AnimatedTile
	{
		public MathUtils.IntTrio index;

		public Tile.AnimType animType;

		public MathUtils.Tri altAnim;

		public Direction animDir;

		public AnimatedTile(MathUtils.IntTrio tileIndex, Tile.AnimType tileAnimType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection)
		{
			index = tileIndex;
			animType = tileAnimType;
			altAnim = tileUsesAlternateAnim;
			animDir = tileAnimDirection;
		}

		public void SetAnimType(Tile.AnimType newAnimType)
		{
			animType = newAnimType;
		}

		public override string ToString()
		{
			if (altAnim.IsTrue)
			{
				return string.Format("Tile: {0} = {1}-alternate", index.z, animType);
			}
			return string.Format("Tile: {0} = {1}", index.z, animType);
		}

		public AnimatedTile GetCopy(Tile.AnimType newAnimType)
		{
			return new AnimatedTile(index, newAnimType, altAnim, animDir);
		}
	}

	private class AnimatedRow
	{
		private ArrayUtils.List<AnimatedTile> animatedTiles;

		private ArrayUtils.List<bool?> animationCycles;

		private ArrayUtils.List<float?> animationOffsets;

		private float? distance;

		public bool IsPlayed;
		public bool IsReset;

		public int SegmentNum { get; private set; }

		public int RowNum { get; private set; }

		public float RowPosMid { get; private set; }

		public float RowPos { get; private set; }

		public bool IsEmpty
		{
			get
			{
				return animatedTiles.IsEmpty;
			}
		}

		public bool IsNotEmpty
		{
			get
			{
				return animatedTiles.IsNotEmpty;
			}
		}

		public int Length
		{
			get
			{
				return animatedTiles.Length;
			}
		}

		public float Distance
		{
			get
			{
				if (!distance.HasValue)
				{
					distance = GetDistance();
				}
				return distance.Value;
			}
		}

		public AnimatedTile this[int index]
		{
			get
			{
				return animatedTiles[index];
			}
		}

		public AnimatedRow(MathUtils.IntTrio index, bool ignoreIndex)
		{
			StorePosition(index, ignoreIndex);
			animatedTiles = new ArrayUtils.List<AnimatedTile>(5);
			animationCycles = new ArrayUtils.List<bool?>(5);
			animationOffsets = new ArrayUtils.List<float?>(5);
		}

		public void Recycle(MathUtils.IntTrio index, bool ignoreIndex)
		{
			StorePosition(index, ignoreIndex);
		}

		public void Add(MathUtils.IntTrio index, Tile.AnimType animType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection)
		{
			animatedTiles.Add(new AnimatedTile(index, animType, tileUsesAlternateAnim, tileAnimDirection));
			animationCycles.Add(null);
			animationOffsets.Add(null);
		}

		public void Add(MathUtils.IntTrio index, Tile.AnimType animType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection, float? animationOffset)
		{
			animatedTiles.Add(new AnimatedTile(index, animType, tileUsesAlternateAnim, tileAnimDirection));
			animationOffsets.Add(animationOffset);
			animationCycles.Add(null);
		}

		public void Add(MathUtils.IntTrio index, Tile.AnimType animType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection, float? animationOffset, bool? animationCycle)
		{
			animatedTiles.Add(new AnimatedTile(index, animType, tileUsesAlternateAnim, tileAnimDirection));
			animationOffsets.Add(animationOffset);
			animationCycles.Add(animationCycle);
		}

		public void Remove(int index)
		{
			animatedTiles.RemoveAt(index);
			animationCycles.RemoveAt(index);
			animationOffsets.RemoveAt(index);
		}

		public void Reset()
		{
			animatedTiles.Reset();
			animationCycles.Reset();
			animationOffsets.Reset();
		}

		public void OnRowEnd()
		{
			if (distance.HasValue)
			{
				distance = null;
			}
			IsPlayed = false;
			IsReset = false;
		}

		public bool JustSwitchedCycle(int tileNum, bool newCycle)
		{
			bool result = animationCycles[tileNum].HasValue && animationCycles[tileNum].Value != newCycle;
			animationCycles[tileNum] = newCycle;
			return result;
		}

		public bool FirstCheck(int tileNum)
		{
			bool result = !animationCycles[tileNum].HasValue;
			animationCycles[tileNum] = true;
			return result;
		}

		public bool SecondCheck(int tileNum)
		{
			bool result = animationCycles[tileNum].HasValue && animationCycles[tileNum].Value;
			animationCycles[tileNum] = false;
			return result;
		}

		public bool? GetCyclePure(int tileNum)
		{
			return animationCycles[tileNum];
		}

		public bool HasOffset(int tileNum)
		{
			return animationOffsets[tileNum].HasValue;
		}

		public bool HasNotOffset(int tileNum)
		{
			return !HasOffset(tileNum);
		}

		public float GetOffset(int tileNum)
		{
			return animationOffsets[tileNum].Value;
		}

		public void SetOffset(int tileNum, float offset)
		{
			animationOffsets[tileNum] = offset;
		}

		public float? GetOffsetPure(int tileNum)
		{
			return animationOffsets[tileNum];
		}

		public override string ToString()
		{
			string text = ((!IsEmpty) ? null : "EMPTY - ");
			return string.Format("{0}Row: {1}, {2} @ {3} - {4}", text, SegmentNum, RowNum, RowPos, animatedTiles.ToString());
		}

		private float GetDistance()
		{
			return RowPos - BallPos.y;
		}

		private void StorePosition(MathUtils.IntTrio index, bool verifyIndex)
		{
			SegmentNum = index.x;
			RowNum = index.y;
			if (verifyIndex)
			{
				RowPos = (float)RowNum + SegmentTracker.GetSegmentOffset(SegmentNum);
				RowPosMid = RowPos + 0.5f;
			}
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugAdding = false;

	private const bool debugRemoving = false;

	private const bool debugEmpty = false;

	private const bool debugReplacement = false;

	public static Vector3 BallPos;

	private static bool initialized;

	private static ArrayUtils.List<AnimatedRow> animatedRows = new ArrayUtils.List<AnimatedRow>(36);

	private static ArrayUtils.List<int> completedTiles = new ArrayUtils.List<int>(5);

	private static void AnimateTile(int animRowNum, int animTileNum)
	{
		if (GameManager.TileBoard.IsNotNull(animatedRows[animRowNum][animTileNum].index) && GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].IsInUse)
		{
			if (AnimateExpansionTile(animRowNum, animTileNum)) return;
			switch (animatedRows[animRowNum][animTileNum].animType)
			{
			case Tile.AnimType.Pickup:
				SpinPickup(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PickupCollect:
				CollectPickup(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Jump:
				JumpTile(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Riser:
				RiseRiser(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Tree:
				RiseTree(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PalmTree:
				RisePalmTree(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Pillar:
				RisePillar(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Tower:
				RiseTower(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Pyramid:
				RisePyramid(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Crusher:
				RiseCrusher(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Turbine:
				SpinTurbine(animRowNum, animTileNum);
				break;
			case Tile.AnimType.BinaryHammer:
				PoundHammer(animRowNum, animTileNum);
				break;
			case Tile.AnimType.BinarySlasher:
				SpinSlasher(animRowNum, animTileNum);
				break;
			case Tile.AnimType.BinarySlasherMini:
				SpinSlasherMini(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Floater:
				FloatFloater(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Laser:
				FireLaser(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Spotlight:
				WaveSpotlight(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Save:
				CreateSaver(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Roller:
				RollRoller(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Slider:
				SlideSlider(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Mover:
				MoveMover(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Faller:
				FallFaller(animRowNum, animTileNum);
				break;
			case Tile.AnimType.CrusherFaller:
				RiseCrusher(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PickupSlider:
				SlideSlider(animRowNum, animTileNum);
				SpinPickup(animRowNum, animTileNum);
				break;
			case Tile.AnimType.RiserSlider:
				SlideSlider(animRowNum, animTileNum);
				RiseRiser(animRowNum, animTileNum);
				break;
			case Tile.AnimType.TreeSlider:
				SlideSlider(animRowNum, animTileNum);
				RiseTree(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PalmTreeSlider:
				SlideSlider(animRowNum, animTileNum);
				RisePalmTree(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PillarSlider:
				SlideSlider(animRowNum, animTileNum);
				RisePillar(animRowNum, animTileNum);
				break;
			case Tile.AnimType.TowerSlider:
				SlideSlider(animRowNum, animTileNum);
				RiseTower(animRowNum, animTileNum);
				break;
			case Tile.AnimType.PyramidSlider:
				SlideSlider(animRowNum, animTileNum);
				RisePyramid(animRowNum, animTileNum);
				break;
			case Tile.AnimType.TurbineSlider:
				SlideSlider(animRowNum, animTileNum);
				SpinTurbine(animRowNum, animTileNum);
				break;
			case Tile.AnimType.LaserSlider:
				SlideSlider(animRowNum, animTileNum);
				FireLaser(animRowNum, animTileNum);
				break;
			case Tile.AnimType.SpotlightSlider:
				SlideSlider(animRowNum, animTileNum);
				WaveSpotlight(animRowNum, animTileNum);
				break;
			case Tile.AnimType.Wyrm:
			case Tile.AnimType.PickupFloater:
			case Tile.AnimType.Star:
			case Tile.AnimType.StarFloater:
			case Tile.AnimType.StarSlider:
			case Tile.AnimType.StarCollect:
			case Tile.AnimType.Speeder:
				break;
			default:
				AnimateClassicTile(animRowNum, animTileNum);
				break;
			}
		}
		else
		{
			string arg = ((!GameManager.TileBoard.IsNotNull(animatedRows[animRowNum][animTileNum].index)) ? "NULL" : string.Format("InUse: {0}", GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].IsInUse));
			Debug.LogWarning(string.Format("TLAN: Attempt to animate invalid Tile index {0}. Removing tile from animation loop ({1})", animatedRows[animRowNum][animTileNum].index, arg));
			completedTiles.Add(animTileNum);
		}
	}

	public static void UpdateAnimation()
	{
		if (!animatedRows.IsNotEmpty)
		{
			return;
		}
		for (int i = 0; i < animatedRows.Length; i++)
		{
			if (!animatedRows[i].IsNotEmpty)
			{
				continue;
			}
			for (int j = 0; j < animatedRows[i].Length; j++)
			{
				AnimateTile(i, j);
			}
			if (completedTiles.IsNotEmpty)
			{
				for (int num = completedTiles.Length - 1; num >= 0; num--)
				{
					RemoveAnimTile(i, completedTiles[num]);
				}
				completedTiles.Reset();
			}
			animatedRows[i].OnRowEnd();
		}
	}

	public static void AddSpecial(Tile.AnimType tileAnimType)
	{
		Add(new MathUtils.IntTrio(-1, -1, -1), tileAnimType, new MathUtils.Tri(false), Direction.Null, null, false);
	}

	public static void Add(MathUtils.IntTrio tileIndex, Tile.AnimType tileAnimType)
	{
		Add(tileIndex, tileAnimType, new MathUtils.Tri(false), Direction.Null, null, true);
	}

	public static void Add(MathUtils.IntTrio tileIndex, Tile.AnimType tileAnimType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection)
	{
		Add(tileIndex, tileAnimType, tileUsesAlternateAnim, tileAnimDirection, null, true);
	}

	public static void Add(MathUtils.IntTrio tileIndex, Tile.AnimType tileAnimType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection, float? tileAnimOffset)
	{
		Add(tileIndex, tileAnimType, tileUsesAlternateAnim, tileAnimDirection, tileAnimOffset, true);
	}

	private static void Add(MathUtils.IntTrio tileIndex, Tile.AnimType tileAnimType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection, float? tileAnimOffset, bool verifyIndex)
	{
		bool flag = false;
		int? num = null;
		int index = -1;
		for (int i = 0; i < animatedRows.Length; i++)
		{
			if (animatedRows[i].IsEmpty)
			{
				if (!num.HasValue)
				{
					num = i;
				}
			}
			else if (animatedRows[i].SegmentNum == tileIndex.x && animatedRows[i].RowNum == tileIndex.y)
			{
				index = i;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			if (num.HasValue)
			{
				animatedRows[num.Value].Recycle(tileIndex, verifyIndex);
				index = num.Value;
			}
			else
			{
				animatedRows.Add(new AnimatedRow(tileIndex, verifyIndex));
				index = animatedRows.LastIndex;
			}
		}
		bool flag2 = true;
		if (animatedRows[index].IsNotEmpty)
		{
			for (int j = 0; j < animatedRows[index].Length; j++)
			{
				if (MathUtils.IntTrio.AreEqual(animatedRows[index][j].index, tileIndex))
				{
					flag2 = false;
					if (tileAnimType == Tile.AnimType.PickupCollect || (ExpansionGameplay.Enabled && tileAnimType == Tile.AnimType.StarCollect))
					{
						GameManager.TileBoard[animatedRows[index][j].index].ScorePickup();
					}
					else if ((tileAnimType == Tile.AnimType.Faller || tileAnimType == Tile.AnimType.Jump) && (animatedRows[index][j].animType == Tile.AnimType.Pickup || (ExpansionGameplay.Enabled && animatedRows[index][j].animType == Tile.AnimType.Star)))
					{
						GameManager.TileBoard[animatedRows[index][j].index].ScorePickup();
						Alter(animatedRows[index][j].index, tileAnimType, tileUsesAlternateAnim, tileAnimDirection, tileAnimOffset);
					}
					else if (tileAnimType == Tile.AnimType.Faller && animatedRows[index][j].animType == Tile.AnimType.Crusher)
					{
						Alter(animatedRows[index][j].index, Tile.AnimType.CrusherFaller);
					}
					else if (ExpansionGameplay.Enabled && (animatedRows[index][j].animType == Tile.AnimType.MoveJumpLeft || animatedRows[index][j].animType == Tile.AnimType.MoveJumpRight))
					{
						Alter(animatedRows[index][j].index, tileAnimType);
					}
					else
					{
						Debug.LogWarning(string.Format("TLAN: WARNING: Tried to add multiple coppies of tile {0} to animatedRows. New animType: {1}, old animType {2}", tileIndex, tileAnimType, animatedRows[index][j].animType));
					}
					break;
				}
			}
		}
		if (flag2)
		{
			animatedRows[index].Add(tileIndex, tileAnimType, tileUsesAlternateAnim, tileAnimDirection, tileAnimOffset);
		}
	}

	public static void Alter(MathUtils.IntTrio tileIndex, Tile.AnimType newAnimType)
	{
		AlterTile(tileIndex, newAnimType, null, null, null);
	}

	public static void Alter(MathUtils.IntTrio tileIndex, Tile.AnimType newAnimType, MathUtils.Tri tileUsesAlternateAnim, Direction tileAnimDirection, float? tileAnimOffset)
	{
		AlterTile(tileIndex, newAnimType, tileUsesAlternateAnim, tileAnimDirection, tileAnimOffset);
	}

	private static void AlterTile(MathUtils.IntTrio tileIndex, Tile.AnimType newAnimType, MathUtils.Tri? tileUsesAlternateAnim, Direction? tileAnimDirection, float? tileAnimOffset)
	{
		bool flag = false;
		int num = -1;
		for (int i = 0; i < animatedRows.Length; i++)
		{
			if (animatedRows[i].IsNotEmpty && animatedRows[i].SegmentNum == tileIndex.x && animatedRows[i].RowNum == tileIndex.y)
			{
				num = i;
				flag = true;
				break;
			}
		}
		if (flag)
		{
			bool flag2 = true;
			if (animatedRows[num].IsNotEmpty)
			{
				for (int j = 0; j < animatedRows[num].Length; j++)
				{
					if (MathUtils.IntTrio.AreEqual(animatedRows[num][j].index, tileIndex))
					{
						Direction tileAnimDirection2 = ((!tileAnimDirection.HasValue) ? animatedRows[num][j].animDir : tileAnimDirection.Value);
						MathUtils.Tri tileUsesAlternateAnim2 = ((!tileUsesAlternateAnim.HasValue) ? animatedRows[num][j].altAnim : tileUsesAlternateAnim.Value);
						float? animationOffset = ((!tileAnimOffset.HasValue) ? animatedRows[num].GetOffsetPure(j) : new float?(tileAnimOffset.Value));
						animatedRows[num].Remove(j);
						animatedRows[num].Add(tileIndex, newAnimType, tileUsesAlternateAnim2, tileAnimDirection2, animationOffset);
						break;
					}
				}
			}
			if (!flag2)
			{
				Debug.LogError(string.Format("TLAN: ERROR: Was unable to find animated tile {0} in found animated row {1} to alter its AnimType to {2}\n{3}", tileIndex, num, newAnimType, ListAnimatedTiles()));
			}
		}
		else
		{
			Debug.LogError(string.Format("TLAN: ERROR: Was unable to find animated tile {0} to alter its AnimType to {1}\n{2}", tileIndex, newAnimType, ListAnimatedTiles()));
		}
	}

	public static void TryRemove(MathUtils.IntTrio tileIndex)
	{
		TryRemove(tileIndex, false);
	}

	public static void TryRemove(MathUtils.IntTrio tileIndex, bool allowReplacement)
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < animatedRows.Length; i++)
		{
			if (!animatedRows[i].IsNotEmpty || animatedRows[i].SegmentNum != tileIndex.x || animatedRows[i].RowNum != tileIndex.y)
			{
				continue;
			}
			for (int j = 0; j < animatedRows[i].Length; j++)
			{
				if (animatedRows[i][j].index.z == tileIndex.z)
				{
					RemoveAnimTile(i, j);
					flag = true;
					break;
				}
			}
			break;
		}
	}

	public static void Clear()
	{
		for (int i = 0; i < animatedRows.Length; i++)
		{
			if (animatedRows[i].IsNotEmpty)
			{
				if (ExpansionGameplay.Enabled && ExpansionGameplay.SourceNumber == 17)
				{
					for (int j = 0; j < animatedRows[i].Length; j++)
					{
						Animation animation = GameManager.TileBoard[animatedRows[i][j].index].newAnimTrasFromAnimation;
						if (animation != null) { animation.Sample(); animation.gameObject.SetActive(false); }
					}
				}
				animatedRows[i].Reset();
			}
		}
	}

	public static string ListAnimatedTiles()
	{
		string text;
		if (animatedRows.IsEmpty)
		{
			text = "Null";
		}
		else
		{
			text = animatedRows[0].ToString();
			for (int i = 1; i < animatedRows.Length; i++)
			{
				text = text + "\n" + animatedRows[i].ToString();
			}
		}
		return text;
	}

	private static void SpinPickup(int animRowNum, int animTileNum)
	{
		if (GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform == null)
		{
			Debug.LogWarning(string.Format("TLAN: DEBUG: Unable to find Pickup to spin on tile {0} (anim tile index {1} with animation {2}, tile info {3})", GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index], animatedRows[animRowNum][animTileNum].index, animatedRows[animRowNum][animTileNum].animType, GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].type));
		}
		else
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.Rotate(Vector3.up * 20f * Time.deltaTime);
		}
	}

	private static void CollectPickup(int animRowNum, int animTileNum)
	{
		if (GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform == null)
		{
			Debug.LogWarning(string.Format("TLAN: DEBUG: Unable to find Pickup to collect on tile {0} (anim tile index {1} with animation {2}, tile type {3})", GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index], animatedRows[animRowNum][animTileNum].index, animatedRows[animRowNum][animTileNum].animType, GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].type));
			return;
		}
		Vector3 vector = new Vector3(BallPos.x, BallPos.y, BallPos.z + 0.5f);
		float num = Vector3.Distance(vector, GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.position);
		if (num <= 0.375f)
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].ScorePickup();
			completedTiles.Add(animTileNum);
			return;
		}
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.Rotate(Vector3.up * 20f * Time.deltaTime);
		float num2 = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.position.y - animatedRows[animRowNum].RowPosMid;
		float t = MathUtils.Max(num2 / 3f, 0.1f);
		Vector3 position = Vector3.Lerp(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.position, vector, t);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PickupTransform.position = position;
	}

	private static void JumpTile(int animRowNum, int animTileNum)
	{
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(0f - animatedRows[animRowNum].Distance, 0f, 2f, out wasBelow, out wasAbove);
		float num = FloatAnim.Pulse01(value);
		float newZ = -0.5f * num;
		TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].GeoTransform, newZ, true);
		if (wasAbove)
		{
			completedTiles.Add(animTileNum);
		}
	}

	private static void RiseRiser(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 5.65f, 2.65f, out wasBelow, out wasAbove);
		int num = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length;
		for (int i = 0; i < num; i++)
		{
			float num2 = (float)num - (float)(i + 1);
			if (i == 0)
			{
				num2 += 1.4444447f;
			}
			float num3 = MathUtils.ToPercent01(value, 0.225f * num2, 1f);
			float newY = num3 * 0.625f;
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], newY, true);
		}
		if (wasAbove)
		{
			completedTiles.Add(animTileNum);
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
		}
	}

	private static void RiseTree(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		float minimum;
		float maximum;
		float num;
		float num2;
		float valueToOvershootTo;
		if (animatedRows[animRowNum][animTileNum].altAnim.IsSpecialTrue)
		{
			minimum = 12.65f;
			maximum = 2.65f;
			num = 0.85f;
			num2 = 0.075f;
			valueToOvershootTo = 1.6666666f;
		}
		else if (animatedRows[animRowNum][animTileNum].altAnim.IsFalse)
		{
			minimum = 12.65f;
			maximum = 2.65f;
			num = 0.4f;
			num2 = 0.3f;
			valueToOvershootTo = 1.1666666f;
		}
		else
		{
			minimum = 12.65f;
			maximum = 2.65f;
			num = 0.4f;
			num2 = 0.3f;
			valueToOvershootTo = 1.1666666f;
		}
		bool wasBelow;
		bool wasAbove;
		float num3 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, minimum, maximum, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		int num4 = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length;
		if (animatedRows[animRowNum][animTileNum].altAnim.IsSpecialTrue)
		{
			float num5 = FloatAnim.ImpactEarly(MathUtils.Clamp01(num3), 0.125f);
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num5 * num, true);
			if (num3 >= 0.25f)
			{
				float num6 = FloatAnim.Overshoot(MathUtils.ToPercent01(num3, 0.25f, 0.4f), valueToOvershootTo);
				for (int i = 0; i < num4; i++)
				{
					TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], num6 * num2, true);
				}
				if (animatedRows[animRowNum].FirstCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
			}
		}
		else
		{
			float num7 = 1f / (float)(num4 + 2);
			for (int j = 0; j <= num4; j++)
			{
				bool flag = j == num4;
				float num8 = j;
				if (flag)
				{
					bool wasWithin;
					float num9 = FloatAnim.ImpactEarly(MathUtils.ToPercent01(num3, num7 * num8, 1f, out wasWithin), 0.1f);
					if (wasWithin)
					{
						TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num9 * num, true);
						if (animatedRows[animRowNum].FirstCheck(animTileNum))
						{
							GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
						}
					}
				}
				else
				{
					float num9 = FloatAnim.Overshoot(MathUtils.ToPercent01(num3, num7 * num8, num7 * (num8 + 1f)), valueToOvershootTo);
					TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j], num9 * num2, true);
				}
			}
		}
		if (wasAbove)
		{
			completedTiles.Add(animTileNum);
		}
	}

	private static void RisePalmTree(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 6f, 4f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			if (animatedRows[animRowNum].FirstCheck(animTileNum))
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
			}
			float num = 0.1f;
			for (int i = 0; i < 10; i++)
			{
				float num2 = i;
				float num3 = MathUtils.ToPercent01(value, num * num2, num * (num2 + 1f));
				float num4 = num3;
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], num4 * 0.2f, true);
			}
		}
		float value2 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 6f, 2.25f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			float num5 = FloatAnim.Impact(value2, 0.1f);
			float z = num5 * 120f;
			for (int j = 10; j < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; j++)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j].localEulerAngles = new Vector3(0f, 0f, z);
			}
			if (wasAbove)
			{
				completedTiles.Add(animTileNum);
			}
		}
	}

	private static void RisePyramid(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		float num;
		float num2;
		float shakeForceMax;
		if (animatedRows[animRowNum][animTileNum].altAnim.IsSpecialTrue)
		{
			num = 3f;
			num2 = 0.25f;
			shakeForceMax = 1f;
			float num3 = 1f;
		}
		else if (animatedRows[animRowNum][animTileNum].altAnim.IsTrue)
		{
			num = 2f;
			num2 = 0.25f;
			shakeForceMax = 0.75f;
			float num3 = 0.75f;
		}
		else
		{
			num = 1.25f;
			num2 = 0.1875f;
			shakeForceMax = 0.25f;
			float num3 = 0.5f;
		}
		float num4 = num2 * 1.3f;
		bool wasBelow;
		bool wasAbove;
		float num5 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 10f, 1f, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		int num6 = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length;
		if (wasAbove)
		{
			for (int i = 1; i < num6; i++)
			{
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], num2, true);
			}
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, 0f, true);
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.localEulerAngles = Vector3.zero;
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
			ShakeCamera(shakeForceMax);
			completedTiles.Add(animTileNum);
			return;
		}
		float num7 = FloatAnim.PulseWeighted(num5, 0.85f);
		float newY = num7 * num;
		TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newY, true);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.localEulerAngles = new Vector3(0f, (1f - num5) * 0f, 0f);
		if (num5 < 0.4f)
		{
			float value = num5 / 0.4f;
			float num8 = 1f / (float)(num6 - 1);
			for (int j = 1; j < num6; j++)
			{
				float num9 = j;
				float num10 = MathUtils.ToPercent01(value, num8 * num9, num8 * (num9 + 1f));
				float num11 = num10;
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j], num11 * num4, true);
			}
		}
		else if (num5 < 0.8f)
		{
			float num12 = MathUtils.ToPercent(num5, 0.4f, 0.8f);
			float percent = num12;
			float newY2 = MathUtils.FromPercent(percent, num4, num2);
			for (int k = 1; k < num6; k++)
			{
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[k], newY2, true);
			}
		}
		else if (animatedRows[animRowNum].FirstCheck(animTileNum))
		{
			for (int l = 1; l < num6; l++)
			{
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[l], num2, true);
			}
		}
		if (ExpansionGameplay.Enabled && DeviceQualityChecker.QualityIsHigh())
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].ShadowTransform.localScale =
				Vector3.one * MathUtils.FromPercent(1f - num7, 0.7f, 1f);
		}
	}

	private static void RiseTower(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 8.65f, 2.65f, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		int num = 4;
		float num2 = 0.2f;
		for (int i = 0; i < num; i++)
		{
			bool flag = i == num - 1;
			float num3 = i;
			float num4 = ((!flag) ? 1f : 0.5f);
			bool wasBelow2;
			bool wasAbove2;
			float num5 = MathUtils.ToPercent01(value, num2 * num3, num2 * (num3 + num4), out wasBelow2, out wasAbove2);
			if (wasBelow2)
			{
				continue;
			}
			float percent = FloatAnim.ImpactLate(num5, 0.1f);
			float minimum;
			float maximum;
			if (flag)
			{
				minimum = 0.125f;
				maximum = 0.25f;
			}
			else
			{
				minimum = 0.01f;
				maximum = 0.5f;
			}
			float newY = MathUtils.FromPercent(percent, minimum, maximum);
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], newY, true);
			if (flag)
			{
				if (animatedRows[animRowNum].FirstCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				float num6 = 0f - (1f - num5);
				for (int j = num; j < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; j++)
				{
					float num7 = ((j >= 8) ? ((j >= 12) ? ((j >= 16) ? 0f : 30f) : 20f) : 35f);
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j].localEulerAngles = new Vector3(0f, 0f, num7 * num6);
				}
			}
		}
		float xDistance = 0f - (GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[16].position.x - GameManager.BallPosition.x);
		float yDistance = 0f - animatedRows[animRowNum].Distance;
		float lookAtAngle = TransformUtils.GetLookAtAngle(xDistance, yDistance);
		lookAtAngle = MathUtils.Clamp(lookAtAngle, -90f, 90f);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[16].localEulerAngles = new Vector3(0f, lookAtAngle, 0f);
	}

	private static void RisePillar(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled && animatedRows[animRowNum].Distance > 15f) return;
		float offset = animatedRows[animRowNum].GetOffset(animTileNum);
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 13f, 3f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			if (wasAbove)
			{
				float value2 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 0.5f, -3.5f, out wasBelow, out wasAbove);
				if (!wasBelow)
				{
					float num = FloatAnim.Smooth(value2, true, false);
					float num2 = 4f - offset - 4f * (1f - num);
					if (num2 >= 0f || wasAbove)
					{
						TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, 0f, true);
						completedTiles.Add(animTileNum);
					}
					else
					{
						TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num2, true);
					}
				}
			}
			else
			{
				float reboundAmmount = 0.0125f;
				float num3 = FloatAnim.ImpactLate(value, reboundAmmount);
				float num4 = 4f - offset - 4f * num3;
				if (num4 < 0f)
				{
					TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num4, true);
				}
			}
		}
		if (!animatedRows[animRowNum][animTileNum].altAnim.IsSpecialTrue)
		{
			return;
		}
		float num5 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 15f, 5f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			if (wasAbove)
			{
				if (animatedRows[animRowNum].SecondCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlayAltSound();
					ShakeCamera(0.8f);
					int x = animatedRows[animRowNum][animTileNum].index.x;
					int y = animatedRows[animRowNum][animTileNum].index.y;
					int z = animatedRows[animRowNum][animTileNum].index.z;
					for (int i = 0; i < (ExpansionGameplay.Enabled ? 8 : 15); i++)
					{
						MathUtils.IntTrio intTrio = new MathUtils.IntTrio(x, y - i, z);
						if (GameManager.TileBoard.IsValid(intTrio))
						{
							if (GameManager.TileBoard[intTrio] == null)
							{
								GameManager.TileBoard[intTrio] = LevelConstructor.CreateFakeGroundTile(animatedRows[animRowNum][animTileNum].index, intTrio);
							}
							GameManager.TileBoard[intTrio].SetAsDangerous(0.5f, null, Tile.DangerType.Heat);
						}
					}
				}
			}
			else
			{
				if (animatedRows[animRowNum].FirstCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				float num6 = FloatAnim.Smooth(num5, true, false);
				float newX = num6 * 0.075f * FloatAnim.Wave(num5 * 15f, -1f, 1f, 0f);
				TransformUtils.SetX(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newX, true);
			}
		}
		float pan = GetPan(animRowNum, animTileNum, 3f);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AudioPlayer.panStereo = pan;
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAudioPlayer.panStereo = pan;
	}

	private static void RiseCrusher(int animRowNum, int animTileNum)
	{
		if (ExpansionGameplay.Enabled) { RiseExpansionCrusher(animRowNum, animTileNum); return; }
		float animOffset = ((!animatedRows[animRowNum][animTileNum].altAnim.IsTrue) ? (-0.1725f) : (-0.6f));
		float distanceCycle = GetDistanceCycle(animRowNum, 12f, animOffset);
		float num = FloatAnim.PulseWeighted(distanceCycle, 0.85f) * 2f - 1f;
		bool wasBelow;
		bool wasAbove;
		float num2 = MathUtils.Clamp01(num, out wasBelow, out wasAbove);
		if (animatedRows[animRowNum].JustSwitchedCycle(animTileNum, wasBelow) && SegmentTracker.CurrentSegmentID.Type != SegmentType.Tutorial && num2 <= 0f)
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
			ShakeCamera(0.5f, animRowNum);
		}
		float newY = num2 * 2f;
		TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newY, true);
		float num3 = ((!(num >= -0.25f) || !(num <= 1f)) ? 0.05f : FloatAnim.Pulse01(MathUtils.ToPercent(num, -0.25f, 1f)));
		num3 *= 1000f;
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.Rotate(Vector3.up * num3 * Time.deltaTime);
		float newZ = 0.2f * num2;
		for (int i = 0; i < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; i++)
		{
			TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], newZ, true);
		}
		if (DeviceQualityChecker.QualityIsHigh())
		{
			float percent = 1f - num2;
			float num4 = MathUtils.FromPercent(percent, 0.7f, 1f);
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].ShadowTransform.localScale = Vector3.one * num4;
		}
	}

	private static void SpinTurbine(int animRowNum, int animTileNum)
	{
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 10f, 3f, out wasBelow, out wasAbove);
		float num;
		if (wasBelow)
		{
			num = 0.05f;
		}
		else if (wasAbove)
		{
			num = 1f;
			if (animatedRows[animRowNum].FirstCheck(animTileNum))
			{
				for (int i = 0; i < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; i++)
				{
					TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i], -0.5f, true);
				}
			}
		}
		else
		{
			num = FloatAnim.Smooth(value, true, false);
			float newZ = -0.5f * num;
			for (int j = 0; j < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; j++)
			{
				TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j], newZ, true);
			}
		}
		num = num * 1100f * Time.deltaTime;
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.Rotate(Vector3.up * num);
	}

	private static void RiseWyrm(int animRowNum, int animTileNum)
	{
		float num = 3.5f;
		float minimum = 12f;
		float num2 = 7f;
		int power = 2;
		float maximum = 1f;
		int power2 = 3;
		float shakeForceMax = 0.5f;
		float num3 = 0.5f;
		float num4 = 0.75f;
		float num5 = 45f;
		int num6 = 8;
		bool wasBelow;
		bool wasAbove;
		float num7 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, minimum, num2, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		if (wasAbove)
		{
			if (animatedRows[animRowNum].SecondCheck(animTileNum))
			{
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num, true);
			}
			float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, num2, maximum, out wasBelow, out wasAbove);
			float num8 = FloatAnim.PowerSmooth(value, true, false, power2);
			float num9 = num8 * 90f;
			if (animatedRows[animRowNum][animTileNum].animDir == Direction.Right)
			{
				num9 *= -1f;
			}
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.localEulerAngles = new Vector3(0f, 0f, num9);
			for (int i = 0; i < num6; i++)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i].localEulerAngles = new Vector3(0f, 0f, (1f - num8) * num5);
			}
			if (wasAbove)
			{
				ShakeCamera(shakeForceMax, animRowNum);
				completedTiles.Add(animTileNum);
			}
		}
		else
		{
			if (animatedRows[animRowNum].SecondCheck(animTileNum))
			{
			}
			float num10 = FloatAnim.PowerSmooth(num7, false, true, power);
			float newY = num10 * num;
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newY, true);
			float num11 = num7 / num4;
			if (num11 > 1f)
			{
				num11 = 1f;
			}
			float num12 = num11;
			for (int j = 0; j < num6; j++)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j].localEulerAngles = new Vector3(0f, 0f, num12 * num5);
			}
		}
	}

	private static void PoundHammer(int animRowNum, int animTileNum)
	{
		float num = 6f;
		if (!animatedRows[animRowNum][animTileNum].altAnim.IsTrue)
		{
			num += 1f;
		}
		float distanceCycle = GetDistanceCycle(animRowNum, LevelDesigner.BeatLength, num);
		float num2 = distanceCycle % 0.5f * 2f;
		bool flag = distanceCycle >= 0.5f;
		float num3;
		if (flag)
		{
			num2 = 1f - num2;
			num3 = FloatAnim.Smooth(num2, false, true, 3);
		}
		else
		{
			num3 = FloatAnim.Smooth(num2, true, false, 3);
		}
		Vector3 localEulerAngles = new Vector3(0f, 0f, 146f * num3);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.localEulerAngles = localEulerAngles;
		if (animatedRows[animRowNum].JustSwitchedCycle(animTileNum, flag))
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
			ShakeCamera(0.5f, animRowNum);
		}
	}

	private static void SpinSlasher(int animRowNum, int animTileNum)
	{
		SpinSpinner(animRowNum, animTileNum, 4f, 0f, 1f);
	}

	private static void SpinSlasherMini(int animRowNum, int animTileNum)
	{
		SpinSpinner(animRowNum, animTileNum, 4f, 0f, 0.75f);
	}

	private static void SpinSpinner(int animRowNum, int animTileNum, float animPeriod, float animOffset, float soundVolume)
	{
		if (animatedRows[animRowNum][animTileNum].altAnim.IsTrue)
		{
			animOffset += 1f;
		}
		float distanceCycle = GetDistanceCycle(animRowNum, animPeriod, animOffset);
		bool newCycle = distanceCycle < 0.25f || distanceCycle > 0.75f;
		if (animatedRows[animRowNum].JustSwitchedCycle(animTileNum, newCycle))
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
		}
		distanceCycle = FloatAnim.SteppedWave01(distanceCycle, 2);
		if (!animatedRows[animRowNum][animTileNum].altAnim.IsTrue)
		{
			distanceCycle *= -1f;
		}
		Vector3 localEulerAngles = new Vector3(0f, 360f * distanceCycle, 0f);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.localEulerAngles = localEulerAngles;
	}

	private static void FloatFloater(int animRowNum, int animTileNum)
	{
		bool wasBelow;
		bool wasAbove;
		float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 10f, 3f, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		if (wasAbove)
		{
			if (animatedRows[animRowNum].FirstCheck(animTileNum))
			{
				TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, -2f, true);
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.eulerAngles = new Vector3(90f, 0f, 0f);
			}
			float num = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 2f, 1f, out wasBelow, out wasAbove);
			if (wasBelow)
			{
				return;
			}
			if (wasAbove)
			{
				if (wasAbove)
				{
					completedTiles.Add(animTileNum);
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.eulerAngles = new Vector3(90f, 0f, 0f);
					if (DeviceQualityChecker.QualityIsHigh())
					{
						GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].ShadowAltTransform.localScale = Vector3.one * 1f;
					}
				}
				return;
			}
			float num2 = FloatAnim.ImpactLate(1f - num);
			Vector3 localEulerAngles = new Vector3(70.45f * num2, 0f, 0f);
			for (int i = 0; i < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; i++)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[i].localEulerAngles = localEulerAngles;
			}
			float num3 = 7.5f * FloatAnim.Pulse01(num);
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.eulerAngles = new Vector3(90f + num3, 0f, 0f);
			if (DeviceQualityChecker.QualityIsHigh())
			{
				float percent = num;
				float num4 = MathUtils.FromPercent(percent, 0.46f, 1f);
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].ShadowAltTransform.localScale = Vector3.one * num4;
			}
		}
		else
		{
			float num5 = FloatAnim.Smooth(value, true, true, 2);
			TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, -2f * num5, true);
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.eulerAngles = new Vector3(270f + num5 * 180f, 0f, 0f);
		}
	}

	private static void SlideSlider(int animRowNum, int animTileNum)
	{
		PacePacer(animRowNum, animTileNum, false, 0.0125f);
	}

	private static void RollRoller(int animRowNum, int animTileNum)
	{
		PacePacer(animRowNum, animTileNum, true, 0f);
	}

	private static void PacePacer(int animRowNum, int animTileNum, bool roll, float animOffset)
	{
		float distance = animatedRows[animRowNum].Distance;
		if (animatedRows[animRowNum].HasNotOffset(animTileNum))
		{
			int num = animatedRows[animRowNum][animTileNum].index.z;
			if (animatedRows[animRowNum][animTileNum].animDir == Direction.Left)
			{
				num = 5 - (num + 1);
			}
			float offset;
			switch (num)
			{
			case 0:
				offset = -0.2f;
				break;
			case 1:
				offset = -0.1f;
				break;
			case 2:
				offset = 0f;
				break;
			case 3:
				offset = 0.1f;
				break;
			case 4:
				offset = 0.2f;
				break;
			default:
				offset = 0f;
				Debug.LogError(string.Format("TLAN: ERROR: Received unexpected tileIndex of {0} for animated row number {1} {2}", num, animatedRows[animRowNum][animTileNum].index.x, animatedRows[animRowNum][animTileNum].index.y));
				break;
			}
			animatedRows[animRowNum].SetOffset(animTileNum, offset);
		}
		float value = distance / 20f - animatedRows[animRowNum].GetOffset(animTileNum) + animOffset;
		if (animatedRows[animRowNum][animTileNum].animDir == Direction.Left)
		{
			MathUtils.Inverse(ref value);
		}
		float num2 = FloatAnim.Wave11(value);
		float newX = 2.5f - 2f * num2;
		if (GameManager.TileBoard.IsInvalid(animatedRows[animRowNum][animTileNum].index))
		{
			Debug.LogWarning(string.Format("TLAN: ERROR: Invalid slider tile {0} ({1}, {2})", animatedRows[animRowNum][animTileNum].index, animRowNum, animTileNum));
			completedTiles.Add(animTileNum);
			return;
		}
		TransformUtils.SetX(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SliderTransform, newX);
		if (roll)
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.localEulerAngles = new Vector3(0f, 0f, num2 * 180f);
		}
	}

	private static void FireLaser(int animRowNum, int animTileNum)
	{
		bool wasBelow;
		bool wasAbove;
		float num = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 15f, 5f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			if (wasAbove)
			{
				if (animatedRows[animRowNum].SecondCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlayAltSound();
					ParticleEmitter component = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.GetComponent<ParticleEmitter>();
					component.emit = false;
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.GetComponent<ParticleAnimator>().rndForce = new Vector3(100f, 0f, 100f);
					ShakeCamera(0.8f);
					int x = animatedRows[animRowNum][animTileNum].index.x;
					int y = animatedRows[animRowNum][animTileNum].index.y;
					int z = animatedRows[animRowNum][animTileNum].index.z;
					for (int i = 0; (float)i < 15f; i++)
					{
						MathUtils.IntTrio intTrio = new MathUtils.IntTrio(x, y - i, z);
						if (GameManager.TileBoard.IsValid(intTrio))
						{
							if (GameManager.TileBoard[intTrio] == null)
							{
								GameManager.TileBoard[intTrio] = LevelConstructor.CreateFakeGroundTile(animatedRows[animRowNum][animTileNum].index, intTrio);
							}
							GameManager.TileBoard[intTrio].SetAsDangerous(0.5f, null, Tile.DangerType.Heat);
						}
					}
				}
				float value = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 5f, 4.5f, out wasBelow, out wasAbove);
				float num3;
				if (wasAbove)
				{
					float value2 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 4.5f, 0f, out wasBelow, out wasAbove);
					float newZ = FloatAnim.Smooth(MathUtils.Inverse(value2), true, true);
					TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newZ, true);
					if (wasAbove)
					{
						if (GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].IsAdditionSwitched())
						{
							GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
						}
						float value3 = MathUtils.ToPercent01(animatedRows[animRowNum].Distance, 0f, -1f, out wasBelow, out wasAbove);
						float num2 = FloatAnim.Smooth(MathUtils.Inverse(value3), true, false);
						num3 = num2;
						if (!wasBelow && wasAbove)
						{
							completedTiles.Add(animTileNum);
						}
					}
					else
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = FloatAnim.Smooth(value, false, true);
					TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, num3, true);
				}
				Vector3 localEulerAngles = new Vector3(0f, -90f * num3, 0f);
				float newX = MathUtils.Inverse(num3) * 0.05f;
				for (int j = 0; j < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; j++)
				{
					TransformUtils.SetX(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j], newX, true);
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[j].localEulerAngles = localEulerAngles;
				}
			}
			else
			{
				ParticleEmitter component2 = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAnimTransform.GetComponent<ParticleEmitter>();
				if (animatedRows[animRowNum].FirstCheck(animTileNum))
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
					component2.emit = true;
				}
				float num4 = FloatAnim.Smooth(num, true, false);
				for (int k = 0; k < GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms.Length; k++)
				{
					TransformUtils.SetX(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimPartTransforms[k], 0.05f * num4, true);
				}
				float newY = num4 * 0.075f * FloatAnim.Wave(num * 15f, -1f, 1f, 0f);
				TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform, newY, true);
				float maxEmission = (component2.minEmission = 50f * num4);
				component2.maxEmission = maxEmission;
			}
		}
		float pan = GetPan(animRowNum, animTileNum, 3f);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AudioPlayer.panStereo = pan;
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AltAudioPlayer.panStereo = pan;
	}

	private static void WaveSpotlight(int animRowNum, int animTileNum)
	{
		float animOffset = 0f;
		float distanceCycle = GetDistanceCycle(animRowNum, LevelDesigner.BeatLength * 2f, animOffset);
		float num = FloatAnim.Wave11(distanceCycle);
		Vector3 localEulerAngles = new Vector3(0f, 0f, 30f * num);
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.localEulerAngles = localEulerAngles;
		if (animatedRows[animRowNum].Distance <= 15f && animatedRows[animRowNum].FirstCheck(animTileNum))
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].SwitchAddition();
		}
	}

	private static void CreateSaver(int animRowNum, int animTileNum)
	{
	}

	private static void MoveMover(int animRowNum, int animTileNum)
	{
		float value = BallPos.y - animatedRows[animRowNum].GetOffset(animTileNum);
		bool wasBelow;
		bool wasAbove;
		float value2 = MathUtils.ToPercent01(value, 0f, 1f, out wasBelow, out wasAbove);
		if (!wasBelow)
		{
			if (animatedRows[animRowNum].FirstCheck(animTileNum))
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
			}
			float num = FloatAnim.Impact(value2);
			Vector3 position = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].RootTransform.position;
			switch (animatedRows[animRowNum][animTileNum].animDir)
			{
			case Direction.Top:
				position.y += num;
				break;
			case Direction.Bottom:
				position.y -= num;
				break;
			case Direction.Left:
				position.x -= num;
				break;
			case Direction.Right:
				position.x += num;
				break;
			}
			TransformUtils.Set(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].GeoTransform, position);
			if (wasAbove)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].FinishMove();
				completedTiles.Add(animTileNum);
			}
		}
	}

	private static void FallFaller(int animRowNum, int animTileNum)
	{
		if (animTileNum >= animatedRows[animRowNum].Length)
		{
			Debug.LogWarning(string.Format("TLAN: Attempt to access invalid faller tile {0} in animated Row {1}, which only has {2} animated tiles currently", animTileNum, animRowNum, animatedRows[animRowNum].Length));
			completedTiles.Add(animTileNum);
			return;
		}
		float num = BallPos.y - animatedRows[animRowNum].GetOffset(animTileNum);
		if (num >= 1f && GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].IsNotFallen)
		{
			GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].FallSection();
		}
		bool wasBelow;
		bool wasAbove;
		float num2 = MathUtils.ToPercent01(num, 0f, 0.5f, out wasBelow, out wasAbove);
		if (wasBelow)
		{
			return;
		}
		float newZ;
		float x;
		if (wasAbove)
		{
			float value = MathUtils.ToPercent01(num, 0.5f, 4.5f, out wasBelow, out wasAbove);
			float num3 = FloatAnim.Smooth(value, true, false);
			newZ = num3 * 10f;
			x = num3 * 90f + 0.5f;
			if (wasAbove)
			{
				x = 90f;
				completedTiles.Add(animTileNum);
			}
		}
		else
		{
			float num4 = num2 * FloatAnim.Wave11(num2 * 2f);
			newZ = (0f - num4) * 0.05f;
			x = (0f - num4) * 0.5f;
		}
		GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].RootTransform.localEulerAngles = new Vector3(x, 0f, 0f);
		TransformUtils.SetZ(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].GeoTransform, newZ, true);
	}

	private static void RemoveAnimTile(int animRowNum, int animTileNum, bool allowReplacement = true)
	{
		Tile.AnimType? animType = null;
		if (allowReplacement)
		{
			if (ExpansionGameplay.Enabled && animatedRows[animRowNum][animTileNum].animType == Tile.AnimType.StarSlider) animType = Tile.AnimType.Slider;
			switch (animatedRows[animRowNum][animTileNum].animType)
			{
			case Tile.AnimType.RiserSlider:
			case Tile.AnimType.TowerSlider:
			case Tile.AnimType.TreeSlider:
			case Tile.AnimType.PalmTreeSlider:
			case Tile.AnimType.LaserSlider:
			case Tile.AnimType.SpotlightSlider:
			case Tile.AnimType.PickupSlider:
				animType = Tile.AnimType.Slider;
				break;
			}
		}
		if (animType.HasValue)
		{
			animatedRows[animRowNum][animTileNum].SetAnimType(animType.Value);
		}
		else
		{
			animatedRows[animRowNum].Remove(animTileNum);
		}
	}

	private static float GetDistanceCycle(int animRowNum, float animTileNum, float animOffset)
	{
		bool alternateCycle;
		return GetDistanceCycle(animRowNum, animTileNum, animOffset, out alternateCycle);
	}

	private static float GetDistanceCycle(int animRowNum, float period, float animOffset, out bool alternateCycle)
	{
		float num = animatedRows[animRowNum].Distance - animOffset * period;
		float num2 = num / period;
		float num3 = num2 % 4f;
		alternateCycle = num3 <= 2f;
		num2 /= 2f;
		float result;
		if (num2 < 0f)
		{
			result = 0f - num2 % 1f;
		}
		else
		{
			result = 1f - num2 % 1f;
			alternateCycle = !alternateCycle;
		}
		return result;
	}

	private static void ShakeCamera(float shakeForceMax, int? animRowNum = null)
	{
		float num = ((!animRowNum.HasValue) ? shakeForceMax : ((1f - MathUtils.Abs(animatedRows[animRowNum.Value].Distance) / 20f) * shakeForceMax));
		if (num > 0f)
		{
			num = FloatAnim.Smooth(num, true, false);
			CameraControl.ShakeCamera(num);
		}
	}

	private static bool AnimsCanCombine(Tile.AnimType firstTestType, Tile.AnimType secondTestType, Tile.AnimType firstTargetType, Tile.AnimType secondTargetType)
	{
		return (firstTestType == firstTargetType && secondTestType == secondTargetType) || (firstTestType == secondTargetType && secondTestType == firstTargetType);
	}

	private static float GetVolume(int animRowNum, int animTileNum, float distance, float distanceMin, float distanceMax)
	{
		return MathUtils.ToPercent01(distance, distanceMax, distanceMin);
	}

	private static float GetPan(int animRowNum, int animTileNum, float panRange)
	{
		float num = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AnimTransform.position.x - BallPos.x;
		if (num < 0f - panRange)
		{
			return 0f - panRange;
		}
		if (num > panRange)
		{
			return panRange;
		}
		return num / panRange;
	}
}
