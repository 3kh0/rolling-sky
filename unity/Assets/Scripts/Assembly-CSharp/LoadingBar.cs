using UnityEngine;

public class LoadingBar : MonoBehaviour
{
	public float EstimatedLoadingTime = 2f;

	private float loadingTimer;

	private bool loaded;

	private bool valid;

	private Renderer[] renderers;

	private void Start()
	{
		renderers = base.transform.GetComponentsInChildren<Renderer>();
		if (renderers.Length == 0)
		{
			Debug.LogError(string.Format("LDBR: ERROR: LoadingBar.cs was unable to find any renderers on {0} or it's children", base.gameObject.name));
			valid = false;
			return;
		}
		valid = true;
		ShowLoadingPercent(0f);
		loadingTimer = EstimatedLoadingTime;
		loaded = false;
	}

	private void Update()
	{
		if (!valid)
		{
			return;
		}
		if (loaded)
		{
			Application.LoadLevel(0);
			return;
		}
		loadingTimer -= Time.smoothDeltaTime;
		float loadingPercent;
		if (loadingTimer <= 0f)
		{
			loadingPercent = 1f;
			loaded = true;
		}
		else
		{
			loadingPercent = 1f - loadingTimer / EstimatedLoadingTime;
		}
		ShowLoadingPercent(loadingPercent);
	}

	private void ShowLoadingPercent(float loadingPercent)
	{
		Renderer[] array = renderers;
		foreach (Renderer renderer in array)
		{
			renderer.material.SetFloat("_Ammount", loadingPercent);
		}
	}
}
