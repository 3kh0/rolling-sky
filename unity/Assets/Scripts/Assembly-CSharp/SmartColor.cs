using UnityEngine;

public class SmartColor
{
	public Color color;

	public static SmartColor Black = new SmartColor(0, 0, 0);

	public static SmartColor Gray = new SmartColor(128, 128, 128);

	public static SmartColor White = new SmartColor(255, 255, 255);

	public static SmartColor Red = new SmartColor(255, 0, 0);

	public static SmartColor Green = new SmartColor(0, 255, 0);

	public static SmartColor Blue = new SmartColor(0, 0, 255);

	public static SmartColor Cyan = new SmartColor(0, 255, 255);

	public static SmartColor Yellow = new SmartColor(255, 255, 0);

	public static SmartColor Magenta = new SmartColor(255, 0, 255);

	public static SmartColor Null = new SmartColor(0, 0, 0, 0);

	public float red
	{
		get
		{
			return color.r;
		}
	}

	public float green
	{
		get
		{
			return color.g;
		}
	}

	public float blue
	{
		get
		{
			return color.b;
		}
	}

	public float alpha
	{
		get
		{
			return color.a;
		}
	}

	public Color rgb
	{
		get
		{
			return color;
		}
	}

	public float r
	{
		get
		{
			return color.r;
		}
	}

	public float g
	{
		get
		{
			return color.g;
		}
	}

	public float b
	{
		get
		{
			return color.b;
		}
	}

	public float a
	{
		get
		{
			return color.a;
		}
	}

	public bool HasAlpha
	{
		get
		{
			return alpha < 1f;
		}
	}

	public bool HasNotAlpha
	{
		get
		{
			return !HasAlpha;
		}
	}

	public SmartColor(int value)
	{
		ConfigureColorFloat(value, value, value, 255f, 255f);
	}

	public SmartColor(int value, int alpha)
	{
		ConfigureColorFloat(value, value, value, alpha, 255f);
	}

	public SmartColor(int red, int green, int blue)
	{
		ConfigureColorFloat(red, green, blue, 255f, 255f);
	}

	public SmartColor(int red, int green, int blue, int alpha)
	{
		ConfigureColorFloat(red, green, blue, alpha, 255f);
	}

	public SmartColor(float value)
	{
		ConfigureColorFloat(value, value, value, 1f);
	}

	public SmartColor(float value, float alpha)
	{
		ConfigureColorFloat(value, value, value, alpha);
	}

	public SmartColor(float red, float green, float blue)
	{
		ConfigureColorFloat(red, green, blue, 1f);
	}

	public SmartColor(float red, float green, float blue, float alpha)
	{
		ConfigureColorFloat(red, green, blue, alpha);
	}

	public SmartColor(int value, float alpha)
	{
		ConfigureColorFloat(red, green, blue, alpha * 255f, 255f);
	}

	public SmartColor(int red, int green, int blue, float alpha)
	{
		ConfigureColorFloat(red, green, blue, alpha * 255f, 255f);
	}

	public SmartColor(float value, int alpha)
	{
		ConfigureColorFloat(value, value, value, (float)alpha / 255f);
	}

	public SmartColor(float red, float green, float blue, int alpha)
	{
		ConfigureColorFloat(red, green, blue, (float)alpha / 255f);
	}

	public SmartColor(Color color)
	{
		ConfigureColorFloat(color);
	}

	public SmartColor(Color color1, Color color2)
	{
		ConfigureColorFloat(Color.Lerp(color1, color2, 0.5f));
	}

	public SmartColor(Color color1, Color color2, float weight)
	{
		ConfigureColorFloat(Color.Lerp(color1, color2, weight));
	}

	public SmartColor(SmartColor color1, SmartColor color2)
	{
		ConfigureColorFloat(Color.Lerp(color1.color, color2.color, 0.5f));
	}

	public SmartColor(SmartColor color1, SmartColor color2, float weight)
	{
		ConfigureColorFloat(Color.Lerp(color1.color, color2.color, weight));
	}

	public SmartColor(Color color1, SmartColor color2)
	{
		ConfigureColorFloat(Color.Lerp(color1, color2.color, 0.5f));
	}

	public SmartColor(Color color1, SmartColor color2, float weight)
	{
		ConfigureColorFloat(Color.Lerp(color1, color2.color, weight));
	}

	public SmartColor(SmartColor color1, Color color2)
	{
		ConfigureColorFloat(Color.Lerp(color1.color, color2, 0.5f));
	}

	public SmartColor(SmartColor color1, Color color2, float weight)
	{
		ConfigureColorFloat(Color.Lerp(color1.color, color2, weight));
	}

	private void ConfigureColorFloat(Color colorInput)
	{
		ConfigureColorFloat(colorInput.r, colorInput.g, colorInput.b, colorInput.a);
	}

	private void ConfigureColorFloat(float redInput, float greenInput, float blueInput, float alphaInput, float? divider = null)
	{
		float num = redInput;
		float num2 = greenInput;
		float num3 = blueInput;
		float num4 = alphaInput;
		if (divider.HasValue)
		{
			num /= divider.Value;
			num2 /= divider.Value;
			num3 /= divider.Value;
			num4 /= divider.Value;
		}
		color = new Color(num, num2, num3, num4);
	}

	public Color Desaturate()
	{
		return Desaturate(1f);
	}

	public Color Desaturate(float ammount)
	{
		color = GetDesaturated(ammount);
		return color;
	}

	public Color Saturate(float ammount)
	{
		color = GetSaturated(ammount);
		return color;
	}

	public Color Darken(float ammount)
	{
		color = GetDarkened(ammount);
		return color;
	}

	public Color Lighten(float ammount)
	{
		color = GetLightened(ammount);
		return color;
	}

	public void MakeOpaque()
	{
		SetAlphaTo(1f);
	}

	public void MakeInvisible()
	{
		SetAlphaTo(0f);
	}

	public void SetAlphaTo(int alphaValue)
	{
		SetAlphaTo((float)alphaValue / 255f);
	}

	public void SetAlphaTo(float alphaWeight)
	{
		color.a = alphaWeight;
	}

	public Color Get()
	{
		return color;
	}

	public SmartColor GetCopy()
	{
		return new SmartColor(color);
	}

	public Color GetSaturated(float ammount)
	{
		return GetSaturated(color, ammount);
	}

	public Color GetSaturated(float ammount, bool clamp)
	{
		return GetSaturated(color, ammount, clamp);
	}

	public Color GetSaturated()
	{
		return GetSaturated(1f);
	}

	public SmartColor GetSaturatedCopy()
	{
		return GetSaturatedCopy(1f);
	}

	public SmartColor GetSaturatedCopy(float ammount)
	{
		return new SmartColor(GetSaturated(ammount));
	}

	public SmartColor GetSaturatedCopy(float ammount, bool clamp)
	{
		return new SmartColor(GetSaturated(ammount, clamp));
	}

	public Color GetSaturated(bool clamp)
	{
		return GetSaturated(1f, clamp);
	}

	public SmartColor GetSaturatedCopy(bool clamp)
	{
		return GetSaturatedCopy(1f, clamp);
	}

	public Color GetDesaturated(float ammount)
	{
		return GetDesaturated(color, ammount);
	}

	public Color GetDesaturated()
	{
		return GetDesaturated(1f);
	}

	public SmartColor GetDesaturatedCopy(float ammount)
	{
		return new SmartColor(GetDesaturated(ammount));
	}

	public SmartColor GetDesaturatedCopy()
	{
		return GetDesaturatedCopy(1f);
	}

	public Color GetLightened(float ammount)
	{
		return GetLightened(color, ammount);
	}

	public SmartColor GetLightenedCopy(float ammount)
	{
		return new SmartColor(GetLightened(ammount));
	}

	public Color GetDarkened(float ammount)
	{
		return GetDarkened(color, ammount);
	}

	public SmartColor GetDarkenedCopy(float ammount)
	{
		return new SmartColor(GetDarkened(ammount));
	}

	public Color GetBurnt(float ammount)
	{
		return GetDarkened(GetSaturated(ammount), ammount);
	}

	public SmartColor GetBurntCopy(float ammount)
	{
		return new SmartColor(GetBurnt(ammount));
	}

	public float GetLuminosity()
	{
		return GetLuminosity(color);
	}

	public Color GetBlended(Color blendedColor, float blendPercent)
	{
		return Color.Lerp(color, blendedColor, blendPercent);
	}

	public Color GetBlended(SmartColor blendedColor, float blendPercent)
	{
		return GetBlended(blendedColor.color, blendPercent);
	}

	public SmartColor GetBlendedCopy(Color blendedColor, float blendPercent)
	{
		return new SmartColor(GetBlended(blendedColor, blendPercent));
	}

	public SmartColor GetBlendedCopy(SmartColor blendedColor, float blendPercent)
	{
		return new SmartColor(GetBlended(blendedColor.color, blendPercent));
	}

	public SmartColor GetHalf()
	{
		return new SmartColor(GetHalfAsColor());
	}

	public SmartColor GetHalf(bool halveAlpha)
	{
		return new SmartColor(GetHalfAsColor(halveAlpha));
	}

	public SmartColor GetMult(float multiplier)
	{
		return new SmartColor(GetMultAsColor(multiplier));
	}

	public SmartColor GetMult(float multiplier, bool multiplyAlpha)
	{
		return new SmartColor(GetMultAsColor(multiplier, multiplyAlpha));
	}

	public SmartColor GetAlphaMult(float alphaMultiplier)
	{
		return new SmartColor(GetAlphaMultAsColor(alphaMultiplier));
	}

	public Color GetHalfAsColor()
	{
		return GetHalfAsColor(false);
	}

	public Color GetHalfAsColor(bool halveAlpha)
	{
		return GetMultAsColor(0.5f, halveAlpha);
	}

	public Color GetMultAsColor(float multiplier)
	{
		return GetMultAsColor(multiplier, false);
	}

	public Color GetMultAsColor(float multiplier, bool multiplyAlpha)
	{
		return new Color(r * multiplier, g * multiplier, b * multiplier, a * ((!multiplyAlpha) ? 1f : multiplier));
	}

	public Color GetAlphaMultAsColor(float alphaMultiplier)
	{
		return new Color(r, g, b, a * alphaMultiplier);
	}

	public static float GetAverage(Color color)
	{
		return GetLuminosity(color) / 3f;
	}

	public static Color GetDesaturated(Color color)
	{
		float average = GetAverage(color);
		return new Color(average, average, average);
	}

	public static Color GetDesaturated(Color color, float ammount)
	{
		float average = GetAverage(color);
		float num = Lerp(color.r, average, ammount);
		float num2 = Lerp(color.g, average, ammount);
		float num3 = Lerp(color.b, average, ammount);
		return new Color(num, num2, num3);
	}

	public static Color GetSaturated(Color color)
	{
		return GetSaturated(color, 1f, true);
	}

	public static Color GetSaturated(Color color, bool clamp)
	{
		return GetSaturated(color, 1f, clamp);
	}

	public static Color GetSaturated(Color color, float ammount)
	{
		return GetSaturated(color, ammount, true);
	}

	public static Color GetSaturated(Color color, float ammount, bool clamp)
	{
		float average = GetAverage(color);
		float num = Diverge(color.r, average, ammount, clamp);
		float num2 = Diverge(color.g, average, ammount, clamp);
		float num3 = Diverge(color.b, average, ammount, clamp);
		return new Color(num, num2, num3);
	}

	public static Color GetLightened(Color color, float ammount)
	{
		if (ammount == 0f)
		{
			return color;
		}
		if (ammount < 0f)
		{
			return GetDarkened(color, 0f - ammount);
		}
		return Color.Lerp(color, Color.white, ammount);
	}

	public static Color GetDarkened(Color color, float ammount)
	{
		if (ammount == 0f)
		{
			return color;
		}
		if (ammount < 0f)
		{
			return GetLightened(color, 0f - ammount);
		}
		float num = 1f - ammount;
		float num2 = color.r * num;
		float num3 = color.g * num;
		float num4 = color.b * num;
		return new Color(num2, num3, num4);
	}

	public static float GetLuminosity(Color color)
	{
		return color.r + color.g + color.b;
	}

	public static float GetLuminosity(SmartColor color)
	{
		return color.GetLuminosity();
	}

	public static Color GetLightest(Color firstColor, Color secondColor)
	{
		float luminosity = GetLuminosity(firstColor);
		float luminosity2 = GetLuminosity(secondColor);
		return (!(luminosity >= luminosity2)) ? secondColor : firstColor;
	}

	public static SmartColor GetLightest(SmartColor firstColor, SmartColor secondColor)
	{
		float luminosity = GetLuminosity(firstColor);
		float luminosity2 = GetLuminosity(secondColor);
		return (!(luminosity >= luminosity2)) ? secondColor : firstColor;
	}

	public static Color GetBlended(Color firstColor, Color secondColor, float blendPercent)
	{
		return Color.Lerp(firstColor, secondColor, blendPercent);
	}

	public static SmartColor GetBlended(SmartColor firstColor, SmartColor secondColor, float blendPercent)
	{
		return firstColor.GetBlendedCopy(secondColor, blendPercent);
	}

	public override string ToString()
	{
		if (HasAlpha)
		{
			return string.Format("{0:0.0}, {1:0.0}, {2:0.0}, {3:0.0}", red * 255f, green * 255f, blue * 255f, alpha * 255f);
		}
		return string.Format("{0:0.0}, {1:0.0}, {2:0.0}", red * 255f, green * 255f, blue * 255f);
	}

	private static float Lerp(float firstValue, float secondValue, float weight)
	{
		if (weight == 0f)
		{
			return firstValue;
		}
		if (weight == 1f)
		{
			return secondValue;
		}
		return firstValue * (1f - weight) + secondValue * weight;
	}

	private static float Diverge(float value, float comparisonValue, float weight, bool clamp)
	{
		float value2 = ((weight == 0f) ? value : ((weight != 1f) ? (value + (value - comparisonValue) * weight) : (value * 2f - comparisonValue)));
		if (clamp)
		{
			MathUtils.Clamp01(ref value2);
		}
		return value2;
	}
}
