using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PortraitViewport : MonoBehaviour
{
	public const float ReferenceAspect = 9f / 16f;
	private Camera view;
	private float originalFov;
	private float originalSize;
	private int width, height;
	private float topInset, bottomInset;
	private readonly List<Edge> edges = new List<Edge>();
	private Transform background;
	private Vector3 backgroundScale;

	private class Edge
	{
		public Transform offset;
		public float depth, direction;
	}

#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern float RollingSky_GetSafeInset(int edge);
#endif

	public static PortraitViewport Attach(Camera camera)
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer) return null;
		PortraitViewport viewport = camera.GetComponent<PortraitViewport>();
		if (viewport != null) return viewport;
		viewport = camera.gameObject.AddComponent<PortraitViewport>();
		viewport.view = camera;
		viewport.originalFov = camera.fieldOfView;
		viewport.originalSize = camera.orthographicSize;
		viewport.background = camera.transform.Find("Background");
		if (viewport.background != null) viewport.backgroundScale = viewport.background.localScale;
		viewport.Refresh();
		return viewport;
	}

	public void Pin(Transform target, bool top)
	{
		Transform offset = new GameObject(target.name + " Screen Edge").transform;
		offset.SetParent(target.parent, false);
		target.SetParent(offset, false);
		edges.Add(new Edge { offset = offset, depth = view.transform.InverseTransformPoint(target.position).z,
			direction = top ? 1f : -1f });
		width = 0;
		Refresh();
	}

	private void LateUpdate()
	{
		Refresh();
	}

	private void Refresh()
	{
		if (view == null || Screen.width == 0 || Screen.height == 0) return;
		float top = 0f, bottom = 0f;
#if UNITY_WEBGL && !UNITY_EDITOR
		top = RollingSky_GetSafeInset(0);
		bottom = RollingSky_GetSafeInset(1);
#endif
		if (width == Screen.width && height == Screen.height && top == topInset && bottom == bottomInset) return;
		width = Screen.width;
		height = Screen.height;
		topInset = top;
		bottomInset = bottom;
		float expansion = Mathf.Max(1f, ReferenceAspect * height / width);
		float tangent = Mathf.Tan(originalFov * Mathf.Deg2Rad * 0.5f);
		view.fieldOfView = 2f * Mathf.Atan(tangent * expansion) * Mathf.Rad2Deg;
		view.orthographicSize = originalSize * expansion;
		if (background != null)
			background.localScale = new Vector3(backgroundScale.x, backgroundScale.y * expansion, backgroundScale.z);
		foreach (Edge edge in edges)
		{
			float halfHeight = view.orthographic ? originalSize : tangent * edge.depth;
			float inset = edge.direction > 0f ? top : bottom;
			float distance = halfHeight * (expansion - 1f - 2f * expansion * inset) * edge.direction;
			edge.offset.localPosition = edge.offset.parent.InverseTransformVector(view.transform.up * distance);
		}
	}
}
