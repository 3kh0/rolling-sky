using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
	public enum GUICorner
	{
		CORNER_TOP_LEFT = 0,
		CORNER_TOP_RIGHT = 1,
		CORNER_BOTTOM_LEFT = 2,
		CORNER_BOTTOM_RIGHT = 3
	}

	public static string hashKey = "101";

	public static string MD5Key
	{
		get
		{
			return hashKey;
		}
	}

	public static Color ConvertColorRGBtoDecimal(float red, float green, float blue)
	{
		float r = red / 255f;
		float g = green / 255f;
		float b = blue / 255f;
		return new Color(r, g, b, 1f);
	}

	public static bool HasInternetConnection()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return false;
		}
		return true;
	}

	public static void SetTransformRelativeToCorner(GUICorner theCorner, float xPaddingScreenPixels, float yPaddingScreenPixels, Transform theTransform, Camera theCamera)
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		float z = theTransform.position.z;
		switch (theCorner)
		{
		case GUICorner.CORNER_TOP_LEFT:
			vector = theCamera.ScreenToWorldPoint(new Vector3(0f, theCamera.pixelHeight, theCamera.nearClipPlane));
			vector = new Vector3(vector.x + xPaddingScreenPixels, vector.y - yPaddingScreenPixels, vector.z);
			break;
		case GUICorner.CORNER_TOP_RIGHT:
			vector = theCamera.ScreenToWorldPoint(new Vector3(theCamera.pixelWidth, theCamera.pixelHeight, theCamera.nearClipPlane));
			vector = new Vector3(vector.x - xPaddingScreenPixels, vector.y - yPaddingScreenPixels, vector.z);
			break;
		case GUICorner.CORNER_BOTTOM_LEFT:
			vector = theCamera.ViewportToWorldPoint(new Vector3(0f, 0f, theCamera.nearClipPlane));
			vector = new Vector3(vector.x + xPaddingScreenPixels, vector.y + yPaddingScreenPixels, vector.z);
			break;
		case GUICorner.CORNER_BOTTOM_RIGHT:
			vector = theCamera.ScreenToWorldPoint(new Vector3(theCamera.pixelWidth, 0f, theCamera.nearClipPlane));
			vector = new Vector3(vector.x - xPaddingScreenPixels, vector.y + yPaddingScreenPixels, vector.z);
			break;
		}
		theTransform.position = new Vector3(vector.x, vector.y, z);
	}

	public static void RegisterAppEventWithParameters(string eventName, string parameterName, string parameterValue)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add(parameterName, parameterValue);
	}
}
