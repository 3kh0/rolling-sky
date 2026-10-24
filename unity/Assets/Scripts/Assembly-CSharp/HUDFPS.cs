using UnityEngine;

public class HUDFPS : MonoBehaviour
{
	public float updateInterval = 0.5f;

	public bool colorFps = true;

	private float accum;

	private float frames;

	private float timeleft;

	private TextMesh text;

	private void Start()
	{
		Debug.LogError("HDFP: ERROR: HUDFOS.cs script called!!!  This script should NOT be being called under any circumstances as it is considered unused and utterly redundant!");
		text = base.gameObject.GetComponent<TextMesh>();
		if (text == null)
		{
			Debug.Log(string.Format("ERROR: HUDFPS.cs needs a TextMesh component to run.  Please add a TextMesh to gameObject {0}", base.gameObject.name));
			base.enabled = false;
		}
		else
		{
			timeleft = updateInterval;
		}
	}

	private void Update()
	{
		timeleft -= Time.smoothDeltaTime;
		accum += Time.timeScale / Time.smoothDeltaTime;
		frames += 1f;
		if (!(timeleft <= 0f))
		{
			return;
		}
		float num = accum / frames;
		string text = string.Format("{0:00.00} FPS", num);
		this.text.text = text;
		if (colorFps)
		{
			if (num > 30f)
			{
				this.text.color = Color.green;
			}
			else if (num > 10f)
			{
				this.text.color = Color.yellow;
			}
			else
			{
				this.text.color = Color.red;
			}
		}
		timeleft = updateInterval;
		accum = 0f;
		frames = 0f;
	}
}
