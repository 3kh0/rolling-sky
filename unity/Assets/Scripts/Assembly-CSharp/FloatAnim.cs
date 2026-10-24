using System;
using UnityEngine;

public class FloatAnim
{
	private const float Pi = (float)Math.PI;

	private const float HalfPi = (float)Math.PI / 2f;

	private const float TwoPi = (float)Math.PI * 2f;

	private const float defaultReboundAmmount = 0.25f;

	public static float Smooth(float value)
	{
		return Smooth(value, false, true, 1);
	}

	public static void Smooth(ref float value)
	{
		value = Smooth(value);
	}

	public static float Smooth(float value, bool smoothStart, bool smoothEnd)
	{
		return Smooth(value, smoothStart, smoothEnd, 1);
	}

	public static void Smooth(ref float value, bool smoothStart, bool smoothEnd)
	{
		value = Smooth(value, smoothStart, smoothEnd);
	}

	public static float Smooth(float value, bool smoothStart, bool smoothEnd, int power)
	{
		value = PowerSmooth(value, smoothStart, smoothEnd, power);
		if (smoothStart && smoothEnd)
		{
			value = (MathUtils.Sin((value - 0.5f) * (float)Math.PI) + 1f) / 2f;
		}
		else if (smoothStart)
		{
			value = MathUtils.Sin((value - 1f) * ((float)Math.PI / 2f)) + 1f;
		}
		else if (smoothEnd)
		{
			value = MathUtils.Sin(value * ((float)Math.PI / 2f));
		}
		return value;
	}

	public static void Smooth(ref float value, bool smoothStart, bool smoothEnd, int power)
	{
		value = Smooth(value, smoothStart, smoothEnd, power);
	}

	public static float PowerSmooth(float value, int power)
	{
		return PowerSmooth(value, false, true, power);
	}

	public static void PowerSmooth(ref float value, int power)
	{
		value = PowerSmooth(value, power);
	}

	public static float PowerSmooth(float value, bool smoothStart, bool smoothEnd, int power)
	{
		if (power > 1)
		{
			if (smoothStart && smoothEnd)
			{
				if (value < 0.5f)
				{
					value *= 2f;
					PowerSmooth(ref value, true, false, power);
					value /= 2f;
				}
				else
				{
					value = value * 2f - 1f;
					PowerSmooth(ref value, false, true, power);
					value = (value + 1f) / 2f;
				}
			}
			else if (smoothStart)
			{
				value = MathUtils.Power(value, power);
			}
			else if (smoothEnd)
			{
				value = 1f - MathUtils.Power(1f - value, power);
			}
		}
		return value;
	}

	public static void PowerSmooth(ref float value, bool smoothStart, bool smoothEnd, int power)
	{
		value = PowerSmooth(value, smoothStart, smoothEnd, power);
	}

	public static float Wave01(float value)
	{
		return Wave(value, 0f, 1f, 0f);
	}

	public static void Wave01(ref float value)
	{
		value = Wave01(value);
	}

	public static float Wave01(float value, float waveLength)
	{
		return Wave(value / waveLength, 0f, 1f, 0f);
	}

	public static void Wave01(ref float value, float waveLength)
	{
		value = Wave01(value, waveLength);
	}

	public static float Wave01(float value, float waveLength, int arcStrength)
	{
		float num = Wave01(value, waveLength);
		num = 1f - num;
		num = MathUtils.Power(num, arcStrength);
		return 1f - num;
	}

	public static void Wave01(ref float value, float waveLength, int arcStrength)
	{
		value = Wave01(value, waveLength, arcStrength);
	}

	public static float Wave11(float value)
	{
		return Wave(value, -1f, 1f, 0f);
	}

	public static void Wave11(ref float value)
	{
		value = Wave11(value);
	}

	public static float Wave(float value, float minValue, float maxValue)
	{
		return Wave(value, minValue, maxValue, minValue);
	}

	public static void Wave(ref float value, float minValue, float maxValue)
	{
		value = Wave(value, minValue, maxValue);
	}

	public static float Wave(float value, float minValue, float maxValue, float startingValue)
	{
		if (minValue == maxValue)
		{
			return minValue;
		}
		float valueShift;
		float valueDivider;
		float startShift;
		TryCalcWaveValuesFor(minValue, maxValue, 0f, out valueShift, out valueDivider, out startShift);
		return WaveManual(value, valueShift, valueDivider, startShift);
	}

	public static void Wave(ref float value, float minValue, float maxValue, float startingValue)
	{
		value = Wave(value, minValue, maxValue, startingValue);
	}

	public static float WaveSmoothed(float value, bool smoothOnlyForZero)
	{
		return (!smoothOnlyForZero) ? WaveSmoothed(value) : WaveSmoothedForZero(value);
	}

	public static float WaveSmoothed(float value)
	{
		float num = MathUtils.Abs(value) % 2f;
		bool flag = (num > 1f) ^ (value < 0f);
		float num2 = Wave01(value);
		if (flag)
		{
			num2 *= -1f;
		}
		return num2;
	}

	private static float WaveSmoothedForZero(float value)
	{
		float num = MathUtils.Abs(value) % 2f;
		bool flag = num < 0.5f || num > 1.5f;
		bool flag2 = (num > 1f) ^ (value < 0f);
		float num2;
		if (flag)
		{
			num2 = Wave01(value);
			if (flag2)
			{
				num2 *= -1f;
			}
		}
		else
		{
			num2 = Wave11(value / 2f);
		}
		return num2;
	}

	public static float Pulse01(float value)
	{
		return MathUtils.Sin(MathUtils.Clamp01(value) * (float)Math.PI);
	}

	public static void Pulse01(ref float value)
	{
		value = Pulse01(value);
	}

	public static float Pulse01(float value, float waveLength)
	{
		return Pulse01(value / waveLength);
	}

	public static void Pulse01(ref float value, float waveLength)
	{
		value = Pulse01(value / waveLength);
	}

	public static float Pulse11(float value)
	{
		return MathUtils.Sin(value * ((float)Math.PI * 2f));
	}

	public static void Pulse11(ref float value)
	{
		value = Pulse11(value);
	}

	public static float Pulse11(float value, float waveLength)
	{
		return Pulse11(value / waveLength);
	}

	public static void Pulse11(ref float value, float waveLength)
	{
		value = Pulse11(value / waveLength);
	}

	public static float Pulses(float value, float pulses)
	{
		return Wave01(value / pulses);
	}

	public static float Ribbon(float value)
	{
		return Ribbon(value, 0.5f);
	}

	public static float Ribbon(float value, float smoothPoint)
	{
		if (MathUtils.IsNotBetween(smoothPoint, 0f, 1f))
		{
			smoothPoint = 0.5f;
		}
		if (value > 1f)
		{
			value %= 1f;
		}
		value = ((!(value < smoothPoint)) ? WaveSection(value, 0.75f, 1f, smoothPoint, 1f, 1f - smoothPoint, 1f) : WaveSection(value, 0f, 0.25f, 0f, smoothPoint, smoothPoint, 0f));
		return value;
	}

	public static float SteppedWave01(float value, int steps)
	{
		if (steps <= 1)
		{
			return Wave01(value / 2f);
		}
		float num = steps;
		value *= num;
		float remainder;
		float num2 = MathUtils.MultiplesAsFloat(1f, value, out remainder);
		return Wave01(remainder / 2f) / num + num2 / num;
	}

	public static float PulseWeighted(float value, float pulseCrestsAt)
	{
		return CalculatePulseWeighted(value, pulseCrestsAt, 0.5f);
	}

	public static float PulseWeighted(float value, float pulseCrestsAt, float smoothCrestDistance)
	{
		return CalculatePulseWeighted(value, pulseCrestsAt, smoothCrestDistance);
	}

	private static float CalculatePulseWeighted(float value, float? pulseCrestsAt, float? smoothCrestDistance)
	{
		if (!pulseCrestsAt.HasValue || pulseCrestsAt.Value == 0.5f || pulseCrestsAt.Value >= 1f || pulseCrestsAt.Value <= 0f)
		{
			return (0f - MathUtils.Cos(value * ((float)Math.PI * 2f)) + 1f) / 2f;
		}
		smoothCrestDistance = MathUtils.Clamp01(smoothCrestDistance.Value);
		float radians = ((value < pulseCrestsAt.Value) ? ((!(pulseCrestsAt.Value < 0.5f) || !(smoothCrestDistance.Value > 0f)) ? (value * (float)Math.PI / pulseCrestsAt.Value) : MathUtils.Lerp(value * (float)Math.PI / pulseCrestsAt.Value, (1f + (value - pulseCrestsAt.Value) / (1f - pulseCrestsAt.Value)) * (float)Math.PI, MathUtils.ToPercent(value, (1f - smoothCrestDistance.Value) * pulseCrestsAt.Value, pulseCrestsAt.Value))) : ((!(pulseCrestsAt.Value > 0.5f) || !(smoothCrestDistance.Value > 0f)) ? ((1f + (value - pulseCrestsAt.Value) / (1f - pulseCrestsAt.Value)) * (float)Math.PI) : MathUtils.Lerp(value * (float)Math.PI / pulseCrestsAt.Value, (1f + (value - pulseCrestsAt.Value) / (1f - pulseCrestsAt.Value)) * (float)Math.PI, MathUtils.ToPercent(value, pulseCrestsAt.Value, pulseCrestsAt.Value + (1f - pulseCrestsAt.Value) * smoothCrestDistance.Value))));
		return (0f - MathUtils.Cos(radians) + 1f) / 2f;
	}

	public static float Overshoot(float value, float valueToOvershootTo, float whenToPass1 = 0.5f)
	{
		if (MathUtils.IsNotWithin01(whenToPass1))
		{
			whenToPass1 = 0.5f;
		}
		float num = PulseWeighted(value, whenToPass1);
		value = ((!(value < whenToPass1)) ? ((valueToOvershootTo - 1f) * num + 1f) : (valueToOvershootTo * num));
		return value;
	}

	public static float Impact(float value)
	{
		return Impact(value, 0.25f, 0.5f);
	}

	public static float ImpactEarly(float value)
	{
		return Impact(value, 0.25f, 0.25f);
	}

	public static float ImpactLate(float value)
	{
		return Impact(value, 0.25f, 0.75f);
	}

	public static float Impact(float value, float reboundAmmount)
	{
		return Impact(value, reboundAmmount, 0.5f);
	}

	public static float ImpactEarly(float value, float reboundAmmount)
	{
		return Impact(value, reboundAmmount, 0.25f);
	}

	public static float ImpactLate(float value, float reboundAmmount)
	{
		return Impact(value, reboundAmmount, 0.75f);
	}

	private static float Impact(float value, float reboundAmmount, float whenToFirstHit1)
	{
		if (MathUtils.IsNotWithin01(reboundAmmount))
		{
			reboundAmmount = 0.25f;
		}
		float num = 0.25f / whenToFirstHit1;
		float num2 = 0f - whenToFirstHit1;
		float num3 = 1f;
		float num4 = 1f;
		float num5 = 1f / reboundAmmount;
		if (value > whenToFirstHit1)
		{
			float num6 = MathUtils.Power(3f, (int)(whenToFirstHit1 * 4f) - 1) / 6f;
			num = num6 / whenToFirstHit1;
			num3 = 0f - num5;
			num4 = 0f - num5;
		}
		return (MathUtils.Sin((value + num2) * ((float)Math.PI * 2f) * num) + num3) / num4;
	}

	public static float Blip(float value, float restRatio)
	{
		return Pulse01(ToPeriods(value, restRatio));
	}

	public static float Bleep(float value, float restRatio)
	{
		return Wave01(ToPeriods(value, restRatio));
	}

	private static float ToPeriods(float value, float restRatio)
	{
		float num = restRatio + 1f;
		value = value % 1f * num;
		if (value > 1f)
		{
			value = 1f;
		}
		return value;
	}

	public static float WaveSection(float value, float waveStartPercent, float waveEndPercent, float outputStartPercent, float outputEndPercent)
	{
		return WaveSection(value, waveStartPercent, waveEndPercent, outputStartPercent, outputEndPercent, 1f, 0f);
	}

	public static float WaveSection(float value, float waveStartPercent, float waveEndPercent, float outputStartPercent, float outputEndPercent, float amplitude)
	{
		return WaveSection(value, waveStartPercent, waveEndPercent, outputStartPercent, outputEndPercent, amplitude, 0f);
	}

	public static float WaveSection(float value, float waveStartPercent, float waveEndPercent, float outputStartPercent, float outputEndPercent, float amplitude, float valueShift)
	{
		if (waveStartPercent != outputStartPercent || waveEndPercent != outputEndPercent)
		{
			value -= outputStartPercent;
			float num = waveEndPercent - waveStartPercent;
			float num2 = outputEndPercent - outputStartPercent;
			float num3 = num2 / num;
			value /= num3;
			value += waveStartPercent;
		}
		value = MathUtils.Sin(value * ((float)Math.PI * 2f));
		value *= amplitude;
		value += valueShift;
		return value;
	}

	public static bool TryCalcWaveValuesFor(float minValue, float maxValue, float startingValue, out float valueShift, out float valueDivider, out float startShift)
	{
		bool flag = minValue != maxValue && maxValue - minValue != 0f;
		if (flag)
		{
			if (minValue > maxValue)
			{
				MathUtils.Switch(ref minValue, ref maxValue);
			}
			valueDivider = 2f / (maxValue - minValue);
			valueShift = minValue + 1f;
			startShift = GetWaveStartShift(startingValue, valueShift, valueDivider);
		}
		else
		{
			valueDivider = 0f;
			valueShift = 0f;
			startShift = 0f;
		}
		return flag;
	}

	public static float WaveManual(float value, float valueShift, float valueDivider, float startShift, float interval = 1f)
	{
		return (MathUtils.Sin((value + startShift) * ((float)Math.PI * 2f) * interval) + valueShift) / valueDivider;
	}

	public static float GetWaveStartShift(float desiredStartingValue)
	{
		return GetWaveStartShift(desiredStartingValue, 0f, 1f, 1f);
	}

	public static float GetWaveStartShift(float desiredStartingValue, float valueShift)
	{
		return GetWaveStartShift(desiredStartingValue, valueShift, 1f, 1f);
	}

	public static float GetWaveStartShift(float desiredStartingValue, float valueShift, float valueDivider)
	{
		return GetWaveStartShift(desiredStartingValue, valueShift, valueDivider, 1f);
	}

	public static float GetWaveStartShift(float desiredStartingValue, float valueShift, float valueDivider, float interval)
	{
		return MathUtils.ArcSin(desiredStartingValue * valueDivider - valueShift) / (interval * ((float)Math.PI * 2f));
	}

	public static bool AnimElapsed(ref float timer, float maxTime, out float animPercent)
	{
		return AnimElapsed(ref timer, maxTime, out animPercent, false, false);
	}

	public static bool AnimElapsed(ref float timer, float maxTime, out float animPercent, bool inversePercent)
	{
		return AnimElapsed(ref timer, maxTime, out animPercent, false, false, inversePercent);
	}

	public static bool AnimElapsed(ref float timer, float maxTime, out float animPercent, bool smoothPercentStart, bool smoothPercentEnd)
	{
		return AnimElapsed(ref timer, maxTime, out animPercent, smoothPercentStart, smoothPercentEnd, false);
	}

	public static bool AnimElapsed(ref float timer, float maxTime, out float animPercent, bool smoothPercentStart, bool smoothPercentEnd, bool inversePercent)
	{
		timer -= Time.smoothDeltaTime;
		bool flag = timer <= 0f;
		if (flag)
		{
			timer = 0f;
			animPercent = ((!inversePercent) ? 1f : 0f);
		}
		else
		{
			animPercent = timer / maxTime;
			if (smoothPercentStart || smoothPercentEnd)
			{
				animPercent = Smooth(timer, smoothPercentEnd, smoothPercentStart);
			}
			if (!inversePercent)
			{
				animPercent = 1f - animPercent;
			}
		}
		return flag;
	}
}
