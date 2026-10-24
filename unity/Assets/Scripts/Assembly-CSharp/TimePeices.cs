using UnityEngine;

public class TimePeices : MonoBehaviour
{
	public abstract class ClasstypeTimePeices
	{
		protected bool isRandomized;

		protected MathUtils.Range rangeLength;

		protected bool canOverflow;

		public float Length { get; protected set; }

		public float TimeElapsed { get; protected set; }

		public float TimeLeft
		{
			get
			{
				return Length - TimeElapsed;
			}
		}

		public float PercentElapsed
		{
			get
			{
				return TimeElapsed / Length;
			}
		}

		public float PercentLeft
		{
			get
			{
				return TimeLeft / Length;
			}
		}

		public bool HasElapsed { get; protected set; }

		protected void InitializeTimePeice(float length)
		{
			InitializeTimePeice(length, false);
		}

		protected void InitializeTimePeice(float length, bool canOverflow)
		{
			Length = length;
			TimeElapsed = 0f;
			this.canOverflow = canOverflow;
		}

		protected void InitializeRandomTimePeice(float minLength, float maxLength)
		{
			rangeLength = new MathUtils.Range(minLength, maxLength);
			isRandomized = true;
			InitializeTimePeice(rangeLength.Random);
		}

		public void Reset()
		{
			if (isRandomized)
			{
				Length = rangeLength.Random;
			}
			TimeElapsed = 0f;
			HasElapsed = false;
		}

		public void SetLength(float newLength)
		{
			SetLength(newLength, true);
		}

		public void SetLength(float newLength, bool resetNow)
		{
			Length = newLength;
			if (resetNow)
			{
				Reset();
			}
		}

		protected void UpdateTime()
		{
			if (HasElapsed && !canOverflow)
			{
				return;
			}
			TimeElapsed += Time.smoothDeltaTime;
			if (TimeElapsed >= Length)
			{
				if (!canOverflow)
				{
					TimeElapsed = Length;
				}
				HasElapsed = true;
			}
		}
	}

	public class Timer : ClasstypeTimePeices
	{
		public Timer(float length)
		{
			InitializeTimePeice(length);
		}

		public Timer(float minLength, float maxLength)
		{
			InitializeRandomTimePeice(minLength, maxLength);
		}

		protected Timer()
		{
		}

		public void Update()
		{
			UpdateTime();
		}

		public bool UpdateAndCheckHasElapsed()
		{
			UpdateTime();
			return base.HasElapsed;
		}

		public bool UpdateAndRestetIfHasElapsed()
		{
			UpdateTime();
			bool hasElapsed = base.HasElapsed;
			if (hasElapsed)
			{
				Reset();
			}
			return hasElapsed;
		}
	}

	public class Ticker : ClasstypeTimePeices
	{
		public int Ticks { get; protected set; }

		public bool WasFirstTick
		{
			get
			{
				return Ticks == 1;
			}
		}

		public bool WasntFirstTick
		{
			get
			{
				return !WasFirstTick;
			}
		}

		public bool JustTicked { get; private set; }

		protected Ticker()
		{
		}

		public Ticker(float length)
		{
			InitializeTicker(length);
		}

		public Ticker(float minLength, float maxLength)
		{
			InitializeTicker(minLength, maxLength);
		}

		protected void InitializeTicker(float length)
		{
			InitializeTimePeice(length);
			Ticks = 0;
		}

		protected void InitializeTicker(float minLength, float maxLength)
		{
			InitializeRandomTimePeice(minLength, maxLength);
			Ticks = 0;
		}

		public void ResetTicks()
		{
			Ticks = 0;
		}

		public void ResetAll()
		{
			Reset();
			ResetTicks();
		}

		public bool Tick()
		{
			UpdateTime();
			if (JustTicked)
			{
				JustTicked = false;
			}
			bool hasElapsed = base.HasElapsed;
			if (hasElapsed)
			{
				JustTicked = true;
				Reset();
				Ticks++;
			}
			return hasElapsed;
		}
	}

	public abstract class ClasstypeAutoTickers
	{
		protected bool isValueCurrent;

		private bool firstRead;

		private Ticker tickTimer;

		protected int ticks
		{
			get
			{
				return tickTimer.Ticks;
			}
		}

		protected void InitializeAutoTicker(float tickTime)
		{
			tickTimer = new Ticker(tickTime);
			isValueCurrent = false;
			firstRead = true;
		}

		public bool UpdateAndCheckIfChanged()
		{
			bool flag = tickTimer.Tick();
			if (firstRead)
			{
				firstRead = false;
				flag = true;
			}
			if (flag)
			{
				isValueCurrent = false;
			}
			return flag;
		}

		private void Reset()
		{
			tickTimer.ResetAll();
			isValueCurrent = false;
			firstRead = true;
		}
	}

	public class Stepper : ClasstypeAutoTickers
	{
		private const float defaultSteps = 5f;

		private float stepHeight;

		private float valueStored;

		private MathUtils.Range valueRange;

		public bool HasElapsed { get; private set; }

		public Stepper(float totalTime)
		{
			Initialize(totalTime, 0.2f, 1f, 0f);
		}

		public Stepper(float totalTime, int steps)
		{
			Initialize(totalTime, 1f / (float)steps, 1f, 0f);
		}

		public Stepper(float totalTime, int steps, float finalValue)
		{
			Initialize(totalTime, 1f / (float)steps, finalValue, 0f);
		}

		public Stepper(float totalTime, int steps, float finalValue, float startingValue)
		{
			Initialize(totalTime, 1f / (float)steps, finalValue, startingValue);
		}

		public Stepper(float totalTime, float stepHeight)
		{
			Initialize(totalTime, stepHeight, 1f, 0f);
		}

		public Stepper(float totalTime, float stepHeight, float finalValue)
		{
			Initialize(totalTime, stepHeight, finalValue, 0f);
		}

		public Stepper(float totalTime, float stepHeight, float finalValue, float startingValue)
		{
			Initialize(totalTime, stepHeight, finalValue, startingValue);
		}

		private void Initialize(float totalTime, float stepHeight, float finalValue, float startingValue)
		{
			valueRange = new MathUtils.Range(startingValue, finalValue);
			if (MathUtils.IsNotBetween01(stepHeight))
			{
				stepHeight = 0.2f;
			}
			this.stepHeight = stepHeight;
			InitializeAutoTicker(totalTime * stepHeight);
		}

		public float GetValue()
		{
			if (HasElapsed)
			{
				return valueRange.Max;
			}
			if (!isValueCurrent)
			{
				valueStored = valueRange.FromPercent((float)base.ticks * stepHeight);
				if (valueStored >= valueRange.Max)
				{
					valueStored = valueRange.Max;
					HasElapsed = true;
				}
				isValueCurrent = true;
			}
			return valueStored;
		}
	}
}
