using System.Collections.Generic;
using UnityEngine;

public class BatchAnimator : MonoBehaviour
{
	private const string classCode = "BCT";

	private Dictionary<MathUtils.IntPair, floatTriBool> riserAnims;

	private static BatchAnimator instance;

	public static BatchAnimator Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("BatchController").AddComponent<BatchAnimator>();
			}
			return instance;
		}
	}

	public bool RiserBatched(int boardNum, int rowNum)
	{
		if (riserAnims == null)
		{
			riserAnims = new Dictionary<MathUtils.IntPair, floatTriBool>();
			return false;
		}
		if (riserAnims.ContainsKey(new MathUtils.IntPair(boardNum, rowNum)))
		{
			return true;
		}
		return false;
	}

	public floatTriBool GetBatchedRiser(int boardNum, int rowNum)
	{
		return riserAnims[new MathUtils.IntPair(boardNum, rowNum)];
	}

	public void BatchRiser(int boardNum, int rowNum, float riserPart0Height, float riserPart1Height, float riserPart2Height, bool allRisen)
	{
		BatchRiser(boardNum, rowNum, new floatTriBool(riserPart0Height, riserPart1Height, riserPart2Height, allRisen));
	}

	public void BatchRiser(int boardNum, int rowNum, floatTriBool animValues)
	{
		if (riserAnims == null)
		{
			riserAnims = new Dictionary<MathUtils.IntPair, floatTriBool>();
		}
		MathUtils.IntPair key = new MathUtils.IntPair(boardNum, rowNum);
		if (riserAnims.ContainsKey(key))
		{
			Debug.LogError("Error BRS_RAE - attempt to redundantly batch already existing riser anim values for tile index: " + key.ToString());
		}
		else
		{
			riserAnims.Add(key, animValues);
		}
	}

	private void LateUpdate()
	{
		if (riserAnims.Count > 0)
		{
			riserAnims.Clear();
		}
	}
}
