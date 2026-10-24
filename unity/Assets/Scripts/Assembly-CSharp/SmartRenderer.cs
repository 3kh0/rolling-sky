using UnityEngine;

public class SmartRenderer : MonoBehaviour
{
	public static Renderer GetRenderer(Transform componentTransform)
	{
		Renderer renderer = null;
		if (componentTransform == null)
		{
			Debug.LogError(ErrorStrings.ValueNull(componentTransform, "componentTransform"));
		}
		else
		{
			renderer = componentTransform.GetComponent<Renderer>();
			if (renderer == null)
			{
				renderer = componentTransform.GetComponentInChildren<Renderer>();
				if (renderer == null)
				{
					Debug.LogError(ErrorStrings.UnableToFind<Renderer>(componentTransform.name));
				}
			}
		}
		return renderer;
	}

	public static void SetUV(Transform rendererTransform, Vector2 newUV, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			SetUV(rendererTransform.GetComponent<Renderer>(), newUV, textureName);
		}
	}

	public static void SetUV(Renderer renderer, Vector2 newUV, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			renderer.sharedMaterial.SetTextureOffset(textureName, newUV);
		}
	}

	public static void SetU(Transform rendererTransform, float newU, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			SetU(rendererTransform.GetComponent<Renderer>(), newU, textureName);
		}
	}

	public static void SetU(Renderer renderer, float newU, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(newU, textureOffset.y));
		}
	}

	public static void SetV(Transform rendererTransform, float newV, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			SetU(rendererTransform.GetComponent<Renderer>(), newV, textureName);
		}
	}

	public static void SetV(Renderer renderer, float newV, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(textureOffset.x, newV));
		}
	}

	public static void ShiftUV(Transform rendererTransform, Vector2 uvShift, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			ShiftUV(rendererTransform.GetComponent<Renderer>(), uvShift, textureName);
		}
	}

	public static void ShiftUV(Renderer renderer, Vector2 uvShift, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, textureOffset + uvShift);
		}
	}

	public static void ShiftU(Transform rendererTransform, float uShift, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			ShiftU(rendererTransform.GetComponent<Renderer>(), uShift, textureName);
		}
	}

	public static void ShiftU(Renderer renderer, float uShift, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(textureOffset.x + uShift, textureOffset.y));
		}
	}

	public static void ShiftV(Transform rendererTransform, float vShift, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			ShiftV(rendererTransform.GetComponent<Renderer>(), vShift, textureName);
		}
	}

	public static void ShiftV(Renderer renderer, float vShift, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(textureOffset.x, textureOffset.y + vShift));
		}
	}

	public static void SetUShiftV(Transform rendererTransform, Vector2 newUandVShift, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			SetUShiftV(rendererTransform.GetComponent<Renderer>(), newUandVShift, textureName);
		}
	}

	public static void SetUShiftV(Renderer renderer, Vector2 newUandVShift, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(newUandVShift.x, textureOffset.y + newUandVShift.y));
		}
	}

	public static void SetVShiftU(Transform rendererTransform, Vector2 newVandUShift, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			SetVShiftU(rendererTransform.GetComponent<Renderer>(), newVandUShift, textureName);
		}
	}

	public static void SetVShiftU(Renderer renderer, Vector2 newVandUShift, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			Vector2 textureOffset = renderer.sharedMaterial.GetTextureOffset(textureName);
			renderer.sharedMaterial.SetTextureOffset(textureName, new Vector2(textureOffset.x + newVandUShift.x, newVandUShift.y));
		}
	}

	public static Vector2 GetUV(Transform rendererTransform, string textureName = "_MainTex")
	{
		if (DoesExist(rendererTransform))
		{
			return GetUV(rendererTransform.GetComponent<Renderer>(), textureName);
		}
		return Vector2.zero;
	}

	public static Vector2 GetUV(Renderer renderer, string textureName = "_MainTex")
	{
		if (DoesExist(renderer))
		{
			return renderer.sharedMaterial.GetTextureOffset(textureName);
		}
		return Vector2.zero;
	}

	private static bool DoesExist<ObjectType>(ObjectType objToVerify)
	{
		return objToVerify != null;
	}
}
