using UnityEngine;

public class Elastic
{
	public class Single
	{
		protected class Tension
		{
			private const float timeMultiplier = 10f;

			private readonly float friction;

			private readonly bool frictionEnabled;

			private float value;

			private bool hasFriction;

			private float frictionPercent;

			public Tension(float value, float friction = 0f)
			{
				this.value = value;
				this.friction = friction;
				if (friction > 0f)
				{
					frictionEnabled = true;
					Reset();
				}
			}

			public void Set(float newValue)
			{
				value = newValue;
			}

			public float Get()
			{
				float num = value;
				if (frictionEnabled && hasFriction)
				{
					num *= 1f - frictionPercent;
					frictionPercent -= 10f * Time.smoothDeltaTime / friction;
					if (frictionPercent <= 0f)
					{
						hasFriction = false;
					}
				}
				return num;
			}

			public void Reset()
			{
				if (frictionEnabled)
				{
					frictionPercent = 1f;
					hasFriction = true;
				}
			}

			public override string ToString()
			{
				string text = "Tension: " + value.ToString("0.00");
				if (friction > 0f)
				{
					text = text + " (friction: " + friction.ToString("0.00");
					if (hasFriction)
					{
						text = string.Format(text, ", friction now: {0}%", (frictionPercent * 100f).ToString("0"));
					}
					text += ")";
				}
				return text;
			}
		}

		protected Tension tension;

		protected float margin;

		private float startingValue;

		public float TargetValue { get; protected set; }

		public float SmoothValue { get; protected set; }

		public bool HasStopped { get; protected set; }

		public bool IsMoving
		{
			get
			{
				return !HasStopped;
			}
		}

		public bool IsCentered { get; private set; }

		public bool IsNotCentered
		{
			get
			{
				return !IsCentered;
			}
		}

		public Single()
		{
			InitializeElastic(1f, 0f, 0f, 0.001f);
		}

		public Single(float tension)
		{
			InitializeElastic(tension, 0f, 0f, 0.001f);
		}

		public Single(float tension, float startingValue)
		{
			InitializeElastic(tension, startingValue, 0f, 0.001f);
		}

		public Single(float tension, float startingValue, float friction)
		{
			InitializeElastic(tension, startingValue, friction, 0.001f);
		}

		public Single(float tension, float startingValue, float friction, float margin)
		{
			InitializeElastic(tension, startingValue, friction, margin);
		}

		protected void InitializeElastic(float tension, float startingValue, float friction, float margin)
		{
			this.tension = new Tension(tension, friction);
			this.margin = margin;
			ForceSetTarget(startingValue);
			this.startingValue = startingValue;
			IsCentered = true;
		}

		public void Update()
		{
			if (IsMoving)
			{
				if (MathUtils.Distance(TargetValue, SmoothValue) <= margin)
				{
					Reset();
				}
				else
				{
					SmoothValue = Lerp(SmoothValue, TargetValue, tension.Get() * Time.smoothDeltaTime);
				}
			}
		}

		protected float Lerp(float first, float second, float weight)
		{
			weight = MathUtils.Clamp01(weight);
			return first * (1f - weight) + second * weight;
		}

		protected void Reset()
		{
			SmoothValue = TargetValue;
			HasStopped = true;
			if (SmoothValue == startingValue)
			{
				IsCentered = true;
			}
			OnReset();
			tension.Reset();
		}

		protected virtual void OnReset()
		{
		}

		public void SetTarget(float newTargetValue)
		{
			if (TargetValue != newTargetValue)
			{
				TargetValue = newTargetValue;
				HasStopped = false;
				IsCentered = false;
				tension.Reset();
			}
		}

		public void SetTargetAndUpdate(float newTargetValue)
		{
			SetTarget(newTargetValue);
			Update();
		}

		public void ForceSetTarget(float newTargetValue)
		{
			TargetValue = newTargetValue;
			Reset();
		}

		public virtual void SetTension(float newTension)
		{
			tension.Set(newTension);
		}

		public void SetMargin(float newMargin)
		{
			margin = newMargin;
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public virtual string ToString(bool showAttributes)
		{
			string text = "Elastic - TargetValue " + TargetValue.ToString("0.00");
			if (IsMoving)
			{
				text = text + ", SmoothValue " + SmoothValue.ToString("0.00");
			}
			if (showAttributes)
			{
				text = text + ", " + GetAttributesString();
			}
			return text;
		}

		public string GetAttributesString()
		{
			if (margin == 0.001f)
			{
				return tension.ToString();
			}
			return string.Format("{0}, Margin {1}", tension.ToString(), margin.ToString("0.0000"));
		}
	}

	public class Advanced : Single
	{
		protected Tension storedTension;

		protected bool usingTemporaryTension;

		public Advanced()
		{
			InitializeElastic(1f, 0f, 0f, 0.001f);
		}

		public Advanced(float tension)
		{
			InitializeElastic(tension, 0f, 0f, 0.001f);
		}

		public Advanced(float tension, float startingValue)
		{
			InitializeElastic(tension, startingValue, 0f, 0.001f);
		}

		public Advanced(float tension, float startingValue, float friction)
		{
			InitializeElastic(tension, startingValue, friction, 0.001f);
		}

		public Advanced(float tension, float startingValue, float friction, float margin)
		{
			InitializeElastic(tension, startingValue, friction, margin);
		}

		public void SetTemporaryTension(float temporaryTension)
		{
			if (!usingTemporaryTension)
			{
				storedTension = tension;
				usingTemporaryTension = true;
			}
			tension = new Tension(temporaryTension);
		}

		protected virtual void OnSetTemporaryTension(float temporaryTension)
		{
		}

		public void SetTemporaryTensionAndTarget(float newTargetValue, float temporaryTension)
		{
			SetTarget(newTargetValue);
			SetTemporaryTension(temporaryTension);
		}

		public override void SetTension(float newTension)
		{
			if (usingTemporaryTension)
			{
				storedTension = new Tension(newTension);
			}
			else
			{
				tension = new Tension(newTension);
			}
		}

		protected override void OnReset()
		{
			if (usingTemporaryTension)
			{
				tension = storedTension;
				usingTemporaryTension = false;
			}
		}

		public override string ToString()
		{
			return ToString(true);
		}

		public override string ToString(bool showAttributes)
		{
			string text = "Elastic Advanced - TargetValue " + base.TargetValue.ToString("0.00");
			if (base.IsMoving)
			{
				text = text + ", SmoothValue " + base.SmoothValue.ToString("0.00");
				if (usingTemporaryTension)
				{
					text = text + ", Temporary Tension " + tension.Get() * 100f / storedTension.Get() + "%";
				}
			}
			if (showAttributes)
			{
				text = text + ", " + GetAttributesString();
			}
			return text;
		}
	}

	public class AdvancedPredefined : Advanced
	{
		private float temporaryTensionAmmount;

		public AdvancedPredefined()
		{
			InitializeElastic(1f, 0f, 0f, 0.001f);
			temporaryTensionAmmount = 1f;
		}

		public AdvancedPredefined(float tension, float temporaryTension)
		{
			InitializeElastic(tension, 0f, 0f, 0.001f);
			temporaryTensionAmmount = temporaryTension;
		}

		public AdvancedPredefined(float tension, float temporaryTension, float startingValue)
		{
			InitializeElastic(tension, startingValue, 0f, 0.001f);
			temporaryTensionAmmount = temporaryTension;
		}

		public AdvancedPredefined(float tension, float temporaryTension, float startingValue, float friction)
		{
			InitializeElastic(tension, startingValue, friction, 0.001f);
			temporaryTensionAmmount = temporaryTension;
		}

		public AdvancedPredefined(float tension, float temporaryTension, float startingValue, float friction, float margin)
		{
			InitializeElastic(tension, startingValue, friction, margin);
			temporaryTensionAmmount = temporaryTension;
		}

		protected override void OnSetTemporaryTension(float newTemporaryTension)
		{
			temporaryTensionAmmount = newTemporaryTension;
		}

		public void SetTemporaryTensionTarget(float newTargetValue)
		{
			SetTarget(newTargetValue);
			SetTemporaryTension(temporaryTensionAmmount);
		}
	}

	public abstract class ClassTypeSet
	{
		private Single[] elastics;

		private Advanced[] elasticsAdvanced;

		private int setSize;

		private string setSizeName;

		private bool usingAdvancedElastics;

		public bool IsMoving
		{
			get
			{
				for (int i = 0; i < setSize; i++)
				{
					if (GetIsMovingFor(i))
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool IsCentered
		{
			get
			{
				for (int i = 0; i < setSize; i++)
				{
					if (GetIsNotCenteredFor(i))
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool HasStopped
		{
			get
			{
				return !IsMoving;
			}
		}

		public bool IsNotCentered
		{
			get
			{
				return !IsCentered;
			}
		}

		protected void InitializeSet(int setSize, string setSizeName, bool useAdvancedElastics, params float[] inputValues)
		{
			this.setSize = setSize;
			this.setSizeName = setSizeName;
			usingAdvancedElastics = useAdvancedElastics;
			if (useAdvancedElastics)
			{
				elasticsAdvanced = new Advanced[setSize];
			}
			else
			{
				elastics = new Single[setSize];
			}
			int[,] array = MathUtils.ToParamsIndices(inputValues.Length, setSize, 4);
			for (int i = 0; i < setSize; i++)
			{
				if (useAdvancedElastics)
				{
					elasticsAdvanced[i] = new Advanced(inputValues[array[i, 0]], inputValues[array[i, 1]], inputValues[array[i, 2]], inputValues[array[i, 3]]);
				}
				else
				{
					elastics[i] = new Single(inputValues[array[i, 0]], inputValues[array[i, 1]], inputValues[array[i, 2]], inputValues[array[i, 3]]);
				}
			}
		}

		public void Update()
		{
			if (usingAdvancedElastics)
			{
				for (int i = 0; i < setSize; i++)
				{
					elasticsAdvanced[i].Update();
				}
			}
			else
			{
				for (int j = 0; j < setSize; j++)
				{
					elastics[j].Update();
				}
			}
		}

		public void SetTarget(float newSharedTargetValue)
		{
			for (int i = 0; i < setSize; i++)
			{
				SetTargetFor(i, newSharedTargetValue);
			}
		}

		protected void SetTargetFor(int elasticIndex, float newTargetValue)
		{
			if (usingAdvancedElastics)
			{
				elasticsAdvanced[elasticIndex].SetTarget(newTargetValue);
			}
			else
			{
				elastics[elasticIndex].SetTarget(newTargetValue);
			}
		}

		public void SetTargetAndUpdate(float newSharedTargetValue)
		{
			for (int i = 0; i < setSize; i++)
			{
				SetTargetAndUpdateFor(i, newSharedTargetValue);
			}
		}

		protected void SetTargetAndUpdateFor(int elasticIndex, float newTargetValue)
		{
			if (usingAdvancedElastics)
			{
				elasticsAdvanced[elasticIndex].SetTargetAndUpdate(newTargetValue);
			}
			else
			{
				elastics[elasticIndex].SetTargetAndUpdate(newTargetValue);
			}
		}

		public void ForceSetTarget(float newSharedTargetValue)
		{
			for (int i = 0; i < setSize; i++)
			{
				ForceSetTargetFor(i, newSharedTargetValue);
			}
		}

		protected void ForceSetTargetFor(int elasticIndex, float newTargetValue)
		{
			if (usingAdvancedElastics)
			{
				elasticsAdvanced[elasticIndex].ForceSetTarget(newTargetValue);
			}
			else
			{
				elastics[elasticIndex].ForceSetTarget(newTargetValue);
			}
		}

		public virtual void SetTension(float newSharedTension)
		{
			for (int i = 0; i < setSize; i++)
			{
				SetTensionFor(i, newSharedTension);
			}
		}

		protected virtual void SetTensionFor(int elasticIndex, float newTension)
		{
			if (usingAdvancedElastics)
			{
				elasticsAdvanced[elasticIndex].SetTension(newTension);
			}
			else
			{
				elastics[elasticIndex].SetTension(newTension);
			}
		}

		public void SetMargin(float newSharedMargin)
		{
			for (int i = 0; i < setSize; i++)
			{
				SetMarginFor(i, newSharedMargin);
			}
		}

		protected void SetMarginFor(int elasticIndex, float newMargin)
		{
			if (usingAdvancedElastics)
			{
				elasticsAdvanced[elasticIndex].SetMargin(newMargin);
			}
			else
			{
				elastics[elasticIndex].SetMargin(newMargin);
			}
		}

		protected bool GetIsMovingFor(int elasticIndex)
		{
			if (usingAdvancedElastics)
			{
				return elasticsAdvanced[elasticIndex].IsMoving;
			}
			return elastics[elasticIndex].IsMoving;
		}

		protected bool GetIsCenteredFor(int elasticIndex)
		{
			if (usingAdvancedElastics)
			{
				return elasticsAdvanced[elasticIndex].IsCentered;
			}
			return elastics[elasticIndex].IsCentered;
		}

		protected bool GetHasStoppedFor(int elasticIndex)
		{
			return !GetIsMovingFor(elasticIndex);
		}

		protected bool GetIsNotCenteredFor(int elasticIndex)
		{
			return !GetIsCenteredFor(elasticIndex);
		}

		protected float GetTargetValueFor(int elasticIndex)
		{
			if (usingAdvancedElastics)
			{
				return elasticsAdvanced[elasticIndex].TargetValue;
			}
			return elastics[elasticIndex].TargetValue;
		}

		protected float GetSmoothValueFor(int elasticIndex)
		{
			if (usingAdvancedElastics)
			{
				return elasticsAdvanced[elasticIndex].SmoothValue;
			}
			return elastics[elasticIndex].SmoothValue;
		}

		protected string GetAttributesStringFor(int elasticIndex)
		{
			if (usingAdvancedElastics)
			{
				return elasticsAdvanced[elasticIndex].GetAttributesString();
			}
			return elastics[elasticIndex].GetAttributesString();
		}

		private string GetTargetValueAsStringFor(int elasticIndex)
		{
			return GetTargetValueFor(elasticIndex).ToString("0.00");
		}

		private string GetSmoothValueAsStringFor(int elasticIndex)
		{
			return GetSmoothValueFor(elasticIndex).ToString("0.00");
		}

		private string GetTensionStringFor(int elasticIndex)
		{
			return GetTensionStringFor(elasticIndex);
		}

		public override string ToString()
		{
			return ToString(true);
		}

		public virtual string ToString(bool showAttributes)
		{
			string text = GetTargetValueAsStringFor(0);
			for (int i = 1; i < setSize; i++)
			{
				text = text + ", " + GetTargetValueAsStringFor(i);
			}
			string text2 = string.Format("Elastic {0} - TargetValues ({1})", setSizeName, text);
			if (IsMoving)
			{
				text = GetSmoothValueAsStringFor(0);
				for (int j = 1; j < setSize; j++)
				{
					text = text + ", " + GetSmoothValueAsStringFor(j);
				}
				text2 = string.Format("{0}, SmooothValues ({1})", text2, text);
			}
			if (showAttributes)
			{
				text = GetAttributesStringFor(0);
				for (int k = 1; k < setSize; k++)
				{
					text = text + ", " + GetAttributesStringFor(k);
				}
				text2 = string.Format("{0}, Attributes ({1})", text2, text);
			}
			return text2;
		}
	}

	public class Pair : ClassTypeSet
	{
		private const int sizeOfSet = 2;

		private const string nameOfSet = "Pair";

		public Vector2 SmoothValues
		{
			get
			{
				return new Vector2(GetSmoothValueFor(0), GetSmoothValueFor(1));
			}
		}

		public Vector2 TargetValues
		{
			get
			{
				return new Vector2(GetTargetValueFor(0), GetTargetValueFor(1));
			}
		}

		public float SmoothValueX
		{
			get
			{
				return GetSmoothValueFor(0);
			}
		}

		public float SmoothValueY
		{
			get
			{
				return GetSmoothValueFor(1);
			}
		}

		public float TargetValueX
		{
			get
			{
				return GetTargetValueFor(0);
			}
		}

		public float TargetValueY
		{
			get
			{
				return GetTargetValueFor(1);
			}
		}

		public float SmoothResultValue
		{
			get
			{
				return GetSmoothValueFor(0) - GetSmoothValueFor(1);
			}
		}

		public bool IsMovingX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool IsMovingY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool HasStoppedX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool HasStoppedY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool IsCenteredX
		{
			get
			{
				return GetIsCenteredFor(0);
			}
		}

		public bool IsCenteredY
		{
			get
			{
				return GetIsCenteredFor(1);
			}
		}

		public bool IsNotCenteredX
		{
			get
			{
				return GetIsNotCenteredFor(0);
			}
		}

		public bool IsNotCenteredY
		{
			get
			{
				return GetIsNotCenteredFor(1);
			}
		}

		public Pair(float tension = 1f, float sharedStartingValue = 0f, float sharedFriction = 0f, float sharedMargin = 0.001f)
		{
			InitializeSet(2, "Pair", false, tension, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Pair(float tensionX, float tensionY, float sharedStartingValue, float sharedFriction, float sharedMargin)
		{
			InitializeSet(2, "Pair", false, tensionX, tensionY, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Pair(float tensionX, float tensionY, float startingValueX, float startingValueY, float sharedFriction, float sharedMargin)
		{
			InitializeSet(2, "Pair", false, tensionX, tensionY, startingValueX, startingValueY, sharedFriction, sharedMargin);
		}

		public Pair(float tensionX, float tensionY, float startingValueX, float startingValueY, float frictionX, float frictionY, float sharedMargin)
		{
			InitializeSet(2, "Pair", false, tensionX, tensionY, startingValueX, startingValueY, frictionX, frictionY, sharedMargin);
		}

		public Pair(float tensionX, float tensionY, float startingValueX, float startingValueY, float frictionX, float frictionY, float marginX, float marginY)
		{
			InitializeSet(2, "Pair", false, tensionX, tensionY, startingValueX, startingValueY, frictionX, frictionY, marginX, marginY);
		}

		public void SetTargets(float newTargetValueX, float newTargetValueY)
		{
			SetTargetX(newTargetValueX);
			SetTargetY(newTargetValueY);
		}

		public void SetTargets(Vector2 newTargetValues)
		{
			SetTargetX(newTargetValues.x);
			SetTargetY(newTargetValues.y);
		}

		public void SetTargetX(float newTargetValue)
		{
			SetTargetFor(0, newTargetValue);
		}

		public void SetTargetY(float newTargetValue)
		{
			SetTargetFor(1, newTargetValue);
		}

		public void SetTargetsAndUpdate(float newTargetValueX, float newTargetValueY)
		{
			SetTargetAndUpdateX(newTargetValueX);
			SetTargetAndUpdateY(newTargetValueY);
		}

		public void SetTargetsAndUpdate(Vector2 newTargetValues)
		{
			SetTargetAndUpdateX(newTargetValues.x);
			SetTargetAndUpdateY(newTargetValues.y);
		}

		public void SetTargetAndUpdateX(float newTargetValue)
		{
			SetTargetAndUpdateFor(0, newTargetValue);
		}

		public void SetTargetAndUpdateY(float newTargetValue)
		{
			SetTargetAndUpdateFor(1, newTargetValue);
		}

		public void ForceSetTargets(float newTargetValueX, float newTargetValueY)
		{
			ForceSetTargetX(newTargetValueX);
			ForceSetTargetY(newTargetValueY);
		}

		public void ForceSetTargets(Vector2 newTargetValues)
		{
			ForceSetTargetX(newTargetValues.x);
			ForceSetTargetY(newTargetValues.y);
		}

		public void ForceSetTargetX(float newTargetValue)
		{
			ForceSetTargetFor(0, newTargetValue);
		}

		public void ForceSetTargetY(float newTargetValue)
		{
			ForceSetTargetFor(1, newTargetValue);
		}

		public void SetTensions(float newTensionX, float newTensionY)
		{
			SetTensionX(newTensionX);
			SetTensionY(newTensionY);
		}

		public void SetTensions(Vector2 newTensions)
		{
			SetTensionX(newTensions.x);
			SetTensionY(newTensions.y);
		}

		public void SetTensionX(float newTension)
		{
			SetTensionFor(0, newTension);
		}

		public void SetTensionY(float newTension)
		{
			SetTensionFor(1, newTension);
		}

		public void SetMargins(float newMarginX, float newMarginY)
		{
			SetMarginX(newMarginX);
			SetMarginY(newMarginY);
		}

		public void SetMargins(Vector2 newMargins)
		{
			SetMarginX(newMargins.x);
			SetMarginY(newMargins.y);
		}

		public void SetMarginX(float newMargin)
		{
			SetMarginFor(0, newMargin);
		}

		public void SetMarginY(float newMargin)
		{
			SetMarginFor(1, newMargin);
		}
	}

	public class Trio : ClassTypeSet
	{
		private const int sizeOfSet = 3;

		private const string nameOfSet = "Trio";

		public Vector3 SmoothValues
		{
			get
			{
				return new Vector3(GetSmoothValueFor(0), GetSmoothValueFor(1), GetSmoothValueFor(2));
			}
		}

		public Vector3 TargetValues
		{
			get
			{
				return new Vector3(GetTargetValueFor(0), GetTargetValueFor(1), GetTargetValueFor(2));
			}
		}

		public float SmoothValueX
		{
			get
			{
				return GetSmoothValueFor(0);
			}
		}

		public float SmoothValueY
		{
			get
			{
				return GetSmoothValueFor(1);
			}
		}

		public float SmoothValueZ
		{
			get
			{
				return GetSmoothValueFor(2);
			}
		}

		public float TargetValueX
		{
			get
			{
				return GetTargetValueFor(0);
			}
		}

		public float TargetValueY
		{
			get
			{
				return GetTargetValueFor(1);
			}
		}

		public float TargetValueZ
		{
			get
			{
				return GetTargetValueFor(2);
			}
		}

		public bool IsMovingX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool IsMovingY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool IsMovingZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool HasStoppedX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool HasStoppedY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool HasStoppedZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool IsCenteredX
		{
			get
			{
				return GetIsCenteredFor(0);
			}
		}

		public bool IsCenteredY
		{
			get
			{
				return GetIsCenteredFor(1);
			}
		}

		public bool IsCenteredZ
		{
			get
			{
				return GetIsCenteredFor(2);
			}
		}

		public bool IsNotCenteredX
		{
			get
			{
				return GetIsNotCenteredFor(0);
			}
		}

		public bool IsNotCenteredY
		{
			get
			{
				return GetIsNotCenteredFor(1);
			}
		}

		public bool IsNotCenteredZ
		{
			get
			{
				return GetIsNotCenteredFor(2);
			}
		}

		public Trio(float tension = 1f, float sharedStartingValue = 0f, float sharedFriction = 0f, float sharedMargin = 0.001f)
		{
			InitializeSet(3, "Trio", false, tension, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Trio(float tensionX, float tensionY, float tensionZ, float sharedStartingValue, float sharedFriction, float sharedMargin)
		{
			InitializeSet(3, "Trio", false, tensionX, tensionY, tensionZ, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Trio(float tensionX, float tensionY, float tensionZ, float startingValueX, float startingValueY, float startingValueZ, float sharedFriction, float sharedMargin)
		{
			InitializeSet(3, "Trio", false, tensionX, tensionY, tensionZ, startingValueX, startingValueY, startingValueZ, sharedFriction, sharedMargin);
		}

		public Trio(float tensionX, float tensionY, float tensionZ, float startingValueX, float startingValueY, float startingValueZ, float frictionX, float frictionY, float frictionZ, float sharedMargin)
		{
			InitializeSet(3, "Trio", false, tensionX, tensionY, tensionZ, startingValueX, startingValueY, startingValueZ, frictionX, frictionY, frictionZ, sharedMargin);
		}

		public Trio(float tensionX, float tensionY, float tensionZ, float startingValueX, float startingValueY, float startingValueZ, float frictionX, float frictionY, float frictionZ, float marginX, float marginY, float marginZ)
		{
			InitializeSet(3, "Trio", false, tensionX, tensionY, tensionZ, startingValueX, startingValueY, startingValueZ, frictionX, frictionY, frictionZ, marginX, marginY, marginZ);
		}

		public void SetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ)
		{
			SetTargetX(newTargetValueX);
			SetTargetY(newTargetValueY);
			SetTargetZ(newTargetValueZ);
		}

		public void SetTargets(Vector3 newTargetValues)
		{
			SetTargetX(newTargetValues.x);
			SetTargetY(newTargetValues.y);
			SetTargetZ(newTargetValues.z);
		}

		public void SetTargetX(float newTargetValue)
		{
			SetTargetFor(0, newTargetValue);
		}

		public void SetTargetY(float newTargetValue)
		{
			SetTargetFor(1, newTargetValue);
		}

		public void SetTargetZ(float newTargetValue)
		{
			SetTargetFor(2, newTargetValue);
		}

		public void SetTargetsAndUpdate(float newTargetValueX, float newTargetValueY, float newTargetValueZ)
		{
			SetTargetAndUpdateX(newTargetValueX);
			SetTargetAndUpdateY(newTargetValueY);
			SetTargetAndUpdateZ(newTargetValueZ);
		}

		public void SetTargetsAndUpdate(Vector3 newTargetValues)
		{
			SetTargetAndUpdateX(newTargetValues.x);
			SetTargetAndUpdateY(newTargetValues.y);
			SetTargetAndUpdateZ(newTargetValues.z);
		}

		public void SetTargetAndUpdateX(float newTargetValue)
		{
			SetTargetAndUpdateFor(0, newTargetValue);
		}

		public void SetTargetAndUpdateY(float newTargetValue)
		{
			SetTargetAndUpdateFor(1, newTargetValue);
		}

		public void SetTargetAndUpdateZ(float newTargetValue)
		{
			SetTargetAndUpdateFor(2, newTargetValue);
		}

		public void ForceSetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ)
		{
			ForceSetTargetX(newTargetValueX);
			ForceSetTargetY(newTargetValueY);
			ForceSetTargetZ(newTargetValueZ);
		}

		public void ForceSetTargets(Vector3 newTargetValues)
		{
			ForceSetTargetX(newTargetValues.x);
			ForceSetTargetY(newTargetValues.y);
			ForceSetTargetZ(newTargetValues.z);
		}

		public void ForceSetTargetX(float newTargetValue)
		{
			ForceSetTargetFor(0, newTargetValue);
		}

		public void ForceSetTargetY(float newTargetValue)
		{
			ForceSetTargetFor(1, newTargetValue);
		}

		public void ForceSetTargetZ(float newTargetValue)
		{
			ForceSetTargetFor(2, newTargetValue);
		}

		public void SetTensions(float newTensionX, float newTensionY, float newTensionZ)
		{
			SetTensionX(newTensionX);
			SetTensionY(newTensionY);
			SetTensionZ(newTensionZ);
		}

		public void SetTensions(Vector3 newTensions)
		{
			SetTensionX(newTensions.x);
			SetTensionY(newTensions.y);
			SetTensionZ(newTensions.z);
		}

		public void SetTensionX(float newTension)
		{
			SetTensionFor(0, newTension);
		}

		public void SetTensionY(float newTension)
		{
			SetTensionFor(1, newTension);
		}

		public void SetTensionZ(float newTension)
		{
			SetTensionFor(2, newTension);
		}

		public void SetMargins(float newMarginX, float newMarginY, float newMarginZ)
		{
			SetMarginX(newMarginX);
			SetMarginY(newMarginY);
			SetMarginZ(newMarginZ);
		}

		public void SetMargins(Vector3 newMargins)
		{
			SetMarginX(newMargins.x);
			SetMarginY(newMargins.y);
			SetMarginZ(newMargins.z);
		}

		public void SetMarginX(float newMargin)
		{
			SetMarginFor(0, newMargin);
		}

		public void SetMarginY(float newMargin)
		{
			SetMarginFor(1, newMargin);
		}

		public void SetMarginZ(float newMargin)
		{
			SetMarginFor(2, newMargin);
		}
	}

	public class Quad : ClassTypeSet
	{
		private const int sizeOfSet = 4;

		private const string nameOfSet = "Quad";

		public Vector4 SmoothValues
		{
			get
			{
				return new Vector4(GetSmoothValueFor(0), GetSmoothValueFor(1), GetSmoothValueFor(2), GetSmoothValueFor(3));
			}
		}

		public Vector4 TargetValues
		{
			get
			{
				return new Vector4(GetTargetValueFor(0), GetTargetValueFor(1), GetTargetValueFor(2), GetTargetValueFor(3));
			}
		}

		public float SmoothValueX
		{
			get
			{
				return GetSmoothValueFor(0);
			}
		}

		public float SmoothValueY
		{
			get
			{
				return GetSmoothValueFor(1);
			}
		}

		public float SmoothValueZ
		{
			get
			{
				return GetSmoothValueFor(2);
			}
		}

		public float SmoothValueW
		{
			get
			{
				return GetSmoothValueFor(3);
			}
		}

		public float TargetValueX
		{
			get
			{
				return GetTargetValueFor(0);
			}
		}

		public float TargetValueY
		{
			get
			{
				return GetTargetValueFor(1);
			}
		}

		public float TargetValueZ
		{
			get
			{
				return GetTargetValueFor(2);
			}
		}

		public float TargetValueW
		{
			get
			{
				return GetTargetValueFor(3);
			}
		}

		public bool IsMovingX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool IsMovingY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool IsMovingZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool IsMovingW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool HasStoppedX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool HasStoppedY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool HasStoppedZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool HasStoppedW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool IsCenteredX
		{
			get
			{
				return GetIsCenteredFor(0);
			}
		}

		public bool IsCenteredY
		{
			get
			{
				return GetIsCenteredFor(1);
			}
		}

		public bool IsCenteredZ
		{
			get
			{
				return GetIsCenteredFor(2);
			}
		}

		public bool IsCenteredW
		{
			get
			{
				return GetIsCenteredFor(3);
			}
		}

		public bool IsNotCenteredX
		{
			get
			{
				return GetIsNotCenteredFor(0);
			}
		}

		public bool IsNotCenteredY
		{
			get
			{
				return GetIsNotCenteredFor(1);
			}
		}

		public bool IsNotCenteredZ
		{
			get
			{
				return GetIsNotCenteredFor(2);
			}
		}

		public bool IsNotCenteredW
		{
			get
			{
				return GetIsNotCenteredFor(3);
			}
		}

		public Quad(float tension = 1f, float sharedStartingValue = 0f, float sharedFriction = 0f, float sharedMargin = 0.001f)
		{
			InitializeSet(4, "Quad", false, tension, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Quad(float tensionX, float tensionY, float tensionZ, float tensionW, float sharedStartingValue, float sharedFriction, float sharedMargin)
		{
			InitializeSet(4, "Quad", false, tensionX, tensionY, tensionZ, tensionW, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Quad(float tensionX, float tensionY, float tensionZ, float tensionW, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float sharedFriction, float sharedMargin)
		{
			InitializeSet(4, "Quad", false, tensionX, tensionY, tensionZ, tensionW, startingValueX, startingValueY, startingValueZ, startingValueW, sharedFriction, sharedMargin);
		}

		public Quad(float tensionX, float tensionY, float tensionZ, float tensionW, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float frictionX, float frictionY, float frictionZ, float frictionW, float sharedMargin)
		{
			InitializeSet(4, "Quad", false, tensionX, tensionY, tensionZ, tensionW, startingValueX, startingValueY, startingValueZ, startingValueW, frictionX, frictionY, frictionZ, frictionW, sharedMargin);
		}

		public Quad(float tensionX, float tensionY, float tensionZ, float tensionW, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float frictionX, float frictionY, float frictionZ, float frictionW, float marginX, float marginY, float marginZ, float marginW)
		{
			InitializeSet(4, "Quad", false, tensionX, tensionY, tensionZ, tensionW, startingValueX, startingValueY, startingValueZ, startingValueW, frictionX, frictionY, frictionZ, frictionW, marginX, marginY, marginZ, marginW);
		}

		public void SetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW)
		{
			SetTargetX(newTargetValueX);
			SetTargetY(newTargetValueY);
			SetTargetZ(newTargetValueZ);
			SetTargetW(newTargetValueW);
		}

		public void SetTargets(Vector4 newTargetValues)
		{
			SetTargetX(newTargetValues.x);
			SetTargetY(newTargetValues.y);
			SetTargetZ(newTargetValues.z);
			SetTargetW(newTargetValues.w);
		}

		public void SetTargetX(float newTargetValue)
		{
			SetTargetFor(0, newTargetValue);
		}

		public void SetTargetY(float newTargetValue)
		{
			SetTargetFor(1, newTargetValue);
		}

		public void SetTargetZ(float newTargetValue)
		{
			SetTargetFor(2, newTargetValue);
		}

		public void SetTargetW(float newTargetValue)
		{
			SetTargetFor(3, newTargetValue);
		}

		public void SetTargetsAndUpdate(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW)
		{
			SetTargetAndUpdateX(newTargetValueX);
			SetTargetAndUpdateY(newTargetValueY);
			SetTargetAndUpdateZ(newTargetValueZ);
			SetTargetAndUpdateW(newTargetValueW);
		}

		public void SetTargetsAndUpdate(Vector4 newTargetValues)
		{
			SetTargetAndUpdateX(newTargetValues.x);
			SetTargetAndUpdateY(newTargetValues.y);
			SetTargetAndUpdateZ(newTargetValues.z);
			SetTargetAndUpdateW(newTargetValues.w);
		}

		public void SetTargetAndUpdateX(float newTargetValue)
		{
			SetTargetAndUpdateFor(0, newTargetValue);
		}

		public void SetTargetAndUpdateY(float newTargetValue)
		{
			SetTargetAndUpdateFor(1, newTargetValue);
		}

		public void SetTargetAndUpdateZ(float newTargetValue)
		{
			SetTargetAndUpdateFor(2, newTargetValue);
		}

		public void SetTargetAndUpdateW(float newTargetValue)
		{
			SetTargetAndUpdateFor(3, newTargetValue);
		}

		public void ForceSetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW)
		{
			ForceSetTargetX(newTargetValueX);
			ForceSetTargetY(newTargetValueY);
			ForceSetTargetZ(newTargetValueZ);
			ForceSetTargetW(newTargetValueW);
		}

		public void ForceSetTargets(Vector4 newTargetValues)
		{
			ForceSetTargetX(newTargetValues.x);
			ForceSetTargetY(newTargetValues.y);
			ForceSetTargetZ(newTargetValues.z);
			ForceSetTargetW(newTargetValues.w);
		}

		public void ForceSetTargetX(float newTargetValue)
		{
			ForceSetTargetFor(0, newTargetValue);
		}

		public void ForceSetTargetY(float newTargetValue)
		{
			ForceSetTargetFor(1, newTargetValue);
		}

		public void ForceSetTargetZ(float newTargetValue)
		{
			ForceSetTargetFor(2, newTargetValue);
		}

		public void ForceSetTargetW(float newTargetValue)
		{
			ForceSetTargetFor(3, newTargetValue);
		}

		public void SetTensions(float newTensionX, float newTensionY, float newTensionZ, float newTensionW)
		{
			SetTensionX(newTensionX);
			SetTensionY(newTensionY);
			SetTensionZ(newTensionZ);
			SetTensionW(newTensionW);
		}

		public void SetTensions(Vector4 newTensions)
		{
			SetTensionX(newTensions.x);
			SetTensionY(newTensions.y);
			SetTensionZ(newTensions.z);
			SetTensionW(newTensions.w);
		}

		public void SetTensionX(float newTension)
		{
			SetTensionFor(0, newTension);
		}

		public void SetTensionY(float newTension)
		{
			SetTensionFor(1, newTension);
		}

		public void SetTensionZ(float newTension)
		{
			SetTensionFor(2, newTension);
		}

		public void SetTensionW(float newTension)
		{
			SetTensionFor(3, newTension);
		}

		public void SetMargins(float newMarginX, float newMarginY, float newMarginZ, float newMarginW)
		{
			SetMarginX(newMarginX);
			SetMarginY(newMarginY);
			SetMarginZ(newMarginZ);
			SetMarginW(newMarginW);
		}

		public void SetMargins(Vector4 newMargins)
		{
			SetMarginX(newMargins.x);
			SetMarginY(newMargins.y);
			SetMarginZ(newMargins.z);
			SetMarginW(newMargins.w);
		}

		public void SetMarginX(float newMargin)
		{
			SetMarginFor(0, newMargin);
		}

		public void SetMarginY(float newMargin)
		{
			SetMarginFor(1, newMargin);
		}

		public void SetMarginZ(float newMargin)
		{
			SetMarginFor(2, newMargin);
		}

		public void SetMarginW(float newMargin)
		{
			SetMarginFor(3, newMargin);
		}
	}

	public class Quint : ClassTypeSet
	{
		private const int sizeOfSet = 5;

		private const string nameOfSet = "Quint";

		public Vector5 SmoothValues
		{
			get
			{
				return new Vector5(GetSmoothValueFor(0), GetSmoothValueFor(1), GetSmoothValueFor(2), GetSmoothValueFor(3), GetSmoothValueFor(4));
			}
		}

		public Vector5 TargetValues
		{
			get
			{
				return new Vector5(GetTargetValueFor(0), GetTargetValueFor(1), GetTargetValueFor(2), GetTargetValueFor(3), GetTargetValueFor(4));
			}
		}

		public float SmoothValueX
		{
			get
			{
				return GetSmoothValueFor(0);
			}
		}

		public float SmoothValueY
		{
			get
			{
				return GetSmoothValueFor(1);
			}
		}

		public float SmoothValueZ
		{
			get
			{
				return GetSmoothValueFor(2);
			}
		}

		public float SmoothValueW
		{
			get
			{
				return GetSmoothValueFor(3);
			}
		}

		public float SmoothValueV
		{
			get
			{
				return GetSmoothValueFor(4);
			}
		}

		public float TargetValueX
		{
			get
			{
				return GetTargetValueFor(0);
			}
		}

		public float TargetValueY
		{
			get
			{
				return GetTargetValueFor(1);
			}
		}

		public float TargetValueZ
		{
			get
			{
				return GetTargetValueFor(2);
			}
		}

		public float TargetValueW
		{
			get
			{
				return GetTargetValueFor(3);
			}
		}

		public float TargetValueV
		{
			get
			{
				return GetTargetValueFor(4);
			}
		}

		public bool IsMovingX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool IsMovingY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool IsMovingZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool IsMovingW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool IsMovingV
		{
			get
			{
				return GetIsMovingFor(4);
			}
		}

		public bool HasStoppedX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool HasStoppedY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool HasStoppedZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool HasStoppedW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool HasStoppedV
		{
			get
			{
				return GetIsMovingFor(4);
			}
		}

		public bool IsCenteredX
		{
			get
			{
				return GetIsCenteredFor(0);
			}
		}

		public bool IsCenteredY
		{
			get
			{
				return GetIsCenteredFor(1);
			}
		}

		public bool IsCenteredZ
		{
			get
			{
				return GetIsCenteredFor(2);
			}
		}

		public bool IsCenteredW
		{
			get
			{
				return GetIsCenteredFor(3);
			}
		}

		public bool IsCenteredV
		{
			get
			{
				return GetIsCenteredFor(4);
			}
		}

		public bool IsNotCenteredX
		{
			get
			{
				return GetIsNotCenteredFor(0);
			}
		}

		public bool IsNotCenteredY
		{
			get
			{
				return GetIsNotCenteredFor(1);
			}
		}

		public bool IsNotCenteredZ
		{
			get
			{
				return GetIsNotCenteredFor(2);
			}
		}

		public bool IsNotCenteredW
		{
			get
			{
				return GetIsNotCenteredFor(3);
			}
		}

		public bool IsNotCenteredV
		{
			get
			{
				return GetIsNotCenteredFor(4);
			}
		}

		public Quint(float tension = 1f, float sharedStartingValue = 0f, float sharedFriction = 0f, float sharedMargin = 0.001f)
		{
			InitializeSet(5, "Quint", false, tension, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Quint(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float sharedStartingValue, float sharedFriction, float sharedMargin)
		{
			InitializeSet(5, "Quint", false, tensionX, tensionY, tensionZ, tensionW, tensionV, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Quint(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float sharedFriction, float sharedMargin)
		{
			InitializeSet(5, "Quint", false, tensionX, tensionY, tensionZ, tensionW, tensionV, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, sharedFriction, sharedMargin);
		}

		public Quint(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float frictionX, float frictionY, float frictionZ, float frictionW, float frictionV, float sharedMargin)
		{
			InitializeSet(5, "Quint", false, tensionX, tensionY, tensionZ, tensionW, tensionV, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, frictionX, frictionY, frictionZ, frictionW, frictionV, sharedMargin);
		}

		public Quint(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float frictionX, float frictionY, float frictionZ, float frictionW, float frictionV, float marginX, float marginY, float marginZ, float marginW, float marginV)
		{
			InitializeSet(5, "Quint", false, tensionX, tensionY, tensionZ, tensionW, tensionV, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, frictionX, frictionY, frictionZ, frictionW, frictionV, marginX, marginY, marginZ, marginW, marginV);
		}

		public void SetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV)
		{
			SetTargetX(newTargetValueX);
			SetTargetY(newTargetValueY);
			SetTargetZ(newTargetValueZ);
			SetTargetW(newTargetValueW);
			SetTargetV(newTargetValueV);
		}

		public void SetTargets(Vector5 newTargetValues)
		{
			SetTargetX(newTargetValues.x);
			SetTargetY(newTargetValues.y);
			SetTargetZ(newTargetValues.z);
			SetTargetW(newTargetValues.w);
			SetTargetV(newTargetValues.v);
		}

		public void SetTargetX(float newTargetValue)
		{
			SetTargetFor(0, newTargetValue);
		}

		public void SetTargetY(float newTargetValue)
		{
			SetTargetFor(1, newTargetValue);
		}

		public void SetTargetZ(float newTargetValue)
		{
			SetTargetFor(2, newTargetValue);
		}

		public void SetTargetW(float newTargetValue)
		{
			SetTargetFor(3, newTargetValue);
		}

		public void SetTargetV(float newTargetValue)
		{
			SetTargetFor(4, newTargetValue);
		}

		public void SetTargetsAndUpdate(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV)
		{
			SetTargetAndUpdateX(newTargetValueX);
			SetTargetAndUpdateY(newTargetValueY);
			SetTargetAndUpdateZ(newTargetValueZ);
			SetTargetAndUpdateW(newTargetValueW);
			SetTargetAndUpdateV(newTargetValueV);
		}

		public void SetTargetsAndUpdate(Vector5 newTargetValues)
		{
			SetTargetAndUpdateX(newTargetValues.x);
			SetTargetAndUpdateY(newTargetValues.y);
			SetTargetAndUpdateZ(newTargetValues.z);
			SetTargetAndUpdateW(newTargetValues.w);
			SetTargetAndUpdateV(newTargetValues.v);
		}

		public void SetTargetAndUpdateX(float newTargetValue)
		{
			SetTargetAndUpdateFor(0, newTargetValue);
		}

		public void SetTargetAndUpdateY(float newTargetValue)
		{
			SetTargetAndUpdateFor(1, newTargetValue);
		}

		public void SetTargetAndUpdateZ(float newTargetValue)
		{
			SetTargetAndUpdateFor(2, newTargetValue);
		}

		public void SetTargetAndUpdateW(float newTargetValue)
		{
			SetTargetAndUpdateFor(3, newTargetValue);
		}

		public void SetTargetAndUpdateV(float newTargetValue)
		{
			SetTargetAndUpdateFor(4, newTargetValue);
		}

		public void ForceSetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV)
		{
			ForceSetTargetX(newTargetValueX);
			ForceSetTargetY(newTargetValueY);
			ForceSetTargetZ(newTargetValueZ);
			ForceSetTargetW(newTargetValueW);
			ForceSetTargetV(newTargetValueV);
		}

		public void ForceSetTargets(Vector5 newTargetValues)
		{
			ForceSetTargetX(newTargetValues.x);
			ForceSetTargetY(newTargetValues.y);
			ForceSetTargetZ(newTargetValues.z);
			ForceSetTargetW(newTargetValues.w);
			ForceSetTargetV(newTargetValues.v);
		}

		public void ForceSetTargetX(float newTargetValue)
		{
			ForceSetTargetFor(0, newTargetValue);
		}

		public void ForceSetTargetY(float newTargetValue)
		{
			ForceSetTargetFor(1, newTargetValue);
		}

		public void ForceSetTargetZ(float newTargetValue)
		{
			ForceSetTargetFor(2, newTargetValue);
		}

		public void ForceSetTargetW(float newTargetValue)
		{
			ForceSetTargetFor(3, newTargetValue);
		}

		public void ForceSetTargetV(float newTargetValue)
		{
			ForceSetTargetFor(4, newTargetValue);
		}

		public void SetTensions(float newTensionX, float newTensionY, float newTensionZ, float newTensionW, float newTensionV)
		{
			SetTensionX(newTensionX);
			SetTensionY(newTensionY);
			SetTensionZ(newTensionZ);
			SetTensionW(newTensionW);
			SetTensionV(newTensionV);
		}

		public void SetTensions(Vector5 newTensions)
		{
			SetTensionX(newTensions.x);
			SetTensionY(newTensions.y);
			SetTensionZ(newTensions.z);
			SetTensionW(newTensions.w);
			SetTensionV(newTensions.v);
		}

		public void SetTensionX(float newTension)
		{
			SetTensionFor(0, newTension);
		}

		public void SetTensionY(float newTension)
		{
			SetTensionFor(1, newTension);
		}

		public void SetTensionZ(float newTension)
		{
			SetTensionFor(2, newTension);
		}

		public void SetTensionW(float newTension)
		{
			SetTensionFor(3, newTension);
		}

		public void SetTensionV(float newTension)
		{
			SetTensionFor(4, newTension);
		}

		public void SetMargins(float newMarginX, float newMarginY, float newMarginZ, float newMarginW, float newMarginV)
		{
			SetMarginX(newMarginX);
			SetMarginY(newMarginY);
			SetMarginZ(newMarginZ);
			SetMarginW(newMarginW);
			SetMarginV(newMarginV);
		}

		public void SetMargins(Vector5 newMargins)
		{
			SetMarginX(newMargins.x);
			SetMarginY(newMargins.y);
			SetMarginZ(newMargins.z);
			SetMarginW(newMargins.w);
			SetMarginV(newMargins.v);
		}

		public void SetMarginX(float newMargin)
		{
			SetMarginFor(0, newMargin);
		}

		public void SetMarginY(float newMargin)
		{
			SetMarginFor(1, newMargin);
		}

		public void SetMarginZ(float newMargin)
		{
			SetMarginFor(2, newMargin);
		}

		public void SetMarginW(float newMargin)
		{
			SetMarginFor(3, newMargin);
		}

		public void SetMarginV(float newMargin)
		{
			SetMarginFor(4, newMargin);
		}
	}

	public class Hex : ClassTypeSet
	{
		private const int sizeOfSet = 6;

		private const string nameOfSet = "Hex";

		public Vector6 SmoothValues
		{
			get
			{
				return new Vector6(GetSmoothValueFor(0), GetSmoothValueFor(1), GetSmoothValueFor(2), GetSmoothValueFor(3), GetSmoothValueFor(4), GetSmoothValueFor(5));
			}
		}

		public Vector6 TargetValues
		{
			get
			{
				return new Vector6(GetTargetValueFor(0), GetTargetValueFor(1), GetTargetValueFor(2), GetTargetValueFor(3), GetTargetValueFor(4), GetTargetValueFor(5));
			}
		}

		public float SmoothValueX
		{
			get
			{
				return GetSmoothValueFor(0);
			}
		}

		public float SmoothValueY
		{
			get
			{
				return GetSmoothValueFor(1);
			}
		}

		public float SmoothValueZ
		{
			get
			{
				return GetSmoothValueFor(2);
			}
		}

		public float SmoothValueW
		{
			get
			{
				return GetSmoothValueFor(3);
			}
		}

		public float SmoothValueV
		{
			get
			{
				return GetSmoothValueFor(4);
			}
		}

		public float SmoothValueU
		{
			get
			{
				return GetSmoothValueFor(5);
			}
		}

		public float TargetValueX
		{
			get
			{
				return GetTargetValueFor(0);
			}
		}

		public float TargetValueY
		{
			get
			{
				return GetTargetValueFor(1);
			}
		}

		public float TargetValueZ
		{
			get
			{
				return GetTargetValueFor(2);
			}
		}

		public float TargetValueW
		{
			get
			{
				return GetTargetValueFor(3);
			}
		}

		public float TargetValueV
		{
			get
			{
				return GetTargetValueFor(4);
			}
		}

		public float TargetValueU
		{
			get
			{
				return GetTargetValueFor(5);
			}
		}

		public bool IsMovingX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool IsMovingY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool IsMovingZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool IsMovingW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool IsMovingV
		{
			get
			{
				return GetIsMovingFor(4);
			}
		}

		public bool IsMovingU
		{
			get
			{
				return GetIsMovingFor(5);
			}
		}

		public bool HasStoppedX
		{
			get
			{
				return GetIsMovingFor(0);
			}
		}

		public bool HasStoppedY
		{
			get
			{
				return GetIsMovingFor(1);
			}
		}

		public bool HasStoppedZ
		{
			get
			{
				return GetIsMovingFor(2);
			}
		}

		public bool HasStoppedW
		{
			get
			{
				return GetIsMovingFor(3);
			}
		}

		public bool HasStoppedV
		{
			get
			{
				return GetIsMovingFor(4);
			}
		}

		public bool HasStoppedU
		{
			get
			{
				return GetIsMovingFor(5);
			}
		}

		public bool IsCenteredX
		{
			get
			{
				return GetIsCenteredFor(0);
			}
		}

		public bool IsCenteredY
		{
			get
			{
				return GetIsCenteredFor(1);
			}
		}

		public bool IsCenteredZ
		{
			get
			{
				return GetIsCenteredFor(2);
			}
		}

		public bool IsCenteredW
		{
			get
			{
				return GetIsCenteredFor(3);
			}
		}

		public bool IsCenteredV
		{
			get
			{
				return GetIsCenteredFor(4);
			}
		}

		public bool IsCenteredU
		{
			get
			{
				return GetIsCenteredFor(5);
			}
		}

		public bool IsNotCenteredX
		{
			get
			{
				return GetIsNotCenteredFor(0);
			}
		}

		public bool IsNotCenteredY
		{
			get
			{
				return GetIsNotCenteredFor(1);
			}
		}

		public bool IsNotCenteredZ
		{
			get
			{
				return GetIsNotCenteredFor(2);
			}
		}

		public bool IsNotCenteredW
		{
			get
			{
				return GetIsNotCenteredFor(3);
			}
		}

		public bool IsNotCenteredV
		{
			get
			{
				return GetIsNotCenteredFor(4);
			}
		}

		public bool IsNotCenteredU
		{
			get
			{
				return GetIsNotCenteredFor(5);
			}
		}

		public Hex(float tension = 1f, float sharedStartingValue = 0f, float sharedFriction = 0f, float sharedMargin = 0.001f)
		{
			InitializeSet(6, "Hex", false, tension, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Hex(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float tensionU, float sharedStartingValue, float sharedFriction, float sharedMargin)
		{
			InitializeSet(6, "Hex", false, tensionX, tensionY, tensionZ, tensionW, tensionV, tensionU, sharedStartingValue, sharedFriction, sharedMargin);
		}

		public Hex(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float tensionU, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float startingValueU, float sharedFriction, float sharedMargin)
		{
			InitializeSet(6, "Hex", false, tensionX, tensionY, tensionZ, tensionW, tensionV, tensionU, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, startingValueU, sharedFriction, sharedMargin);
		}

		public Hex(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float tensionU, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float startingValueU, float frictionX, float frictionY, float frictionZ, float frictionW, float frictionV, float frictionU, float sharedMargin)
		{
			InitializeSet(6, "Hex", false, tensionX, tensionY, tensionZ, tensionW, tensionV, tensionU, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, startingValueU, frictionX, frictionY, frictionZ, frictionW, frictionV, frictionU, sharedMargin);
		}

		public Hex(float tensionX, float tensionY, float tensionZ, float tensionW, float tensionV, float tensionU, float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float startingValueU, float frictionX, float frictionY, float frictionZ, float frictionW, float frictionV, float frictionU, float marginX, float marginY, float marginZ, float marginW, float marginV, float marginU)
		{
			InitializeSet(6, "Hex", false, tensionX, tensionY, tensionZ, tensionW, tensionV, tensionU, startingValueX, startingValueY, startingValueZ, startingValueW, startingValueV, startingValueU, frictionX, frictionY, frictionZ, frictionW, frictionV, frictionU, marginX, marginY, marginZ, marginW, marginV, marginU);
		}

		public void SetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV, float newTargetValueU)
		{
			SetTargetX(newTargetValueX);
			SetTargetY(newTargetValueY);
			SetTargetZ(newTargetValueZ);
			SetTargetW(newTargetValueW);
			SetTargetV(newTargetValueV);
			SetTargetU(newTargetValueU);
		}

		public void SetTargets(Vector6 newTargetValues)
		{
			SetTargetX(newTargetValues.x);
			SetTargetY(newTargetValues.y);
			SetTargetZ(newTargetValues.z);
			SetTargetW(newTargetValues.w);
			SetTargetV(newTargetValues.v);
			SetTargetU(newTargetValues.u);
		}

		public void SetTargetX(float newTargetValue)
		{
			SetTargetFor(0, newTargetValue);
		}

		public void SetTargetY(float newTargetValue)
		{
			SetTargetFor(1, newTargetValue);
		}

		public void SetTargetZ(float newTargetValue)
		{
			SetTargetFor(2, newTargetValue);
		}

		public void SetTargetW(float newTargetValue)
		{
			SetTargetFor(3, newTargetValue);
		}

		public void SetTargetV(float newTargetValue)
		{
			SetTargetFor(4, newTargetValue);
		}

		public void SetTargetU(float newTargetValue)
		{
			SetTargetFor(5, newTargetValue);
		}

		public void SetTargetsAndUpdate(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV, float newTargetValueU)
		{
			SetTargetAndUpdateX(newTargetValueX);
			SetTargetAndUpdateY(newTargetValueY);
			SetTargetAndUpdateZ(newTargetValueZ);
			SetTargetAndUpdateW(newTargetValueW);
			SetTargetAndUpdateV(newTargetValueV);
			SetTargetAndUpdateU(newTargetValueU);
		}

		public void SetTargetsAndUpdate(Vector6 newTargetValues)
		{
			SetTargetAndUpdateX(newTargetValues.x);
			SetTargetAndUpdateY(newTargetValues.y);
			SetTargetAndUpdateZ(newTargetValues.z);
			SetTargetAndUpdateW(newTargetValues.w);
			SetTargetAndUpdateV(newTargetValues.v);
			SetTargetAndUpdateU(newTargetValues.u);
		}

		public void SetTargetAndUpdateX(float newTargetValue)
		{
			SetTargetAndUpdateFor(0, newTargetValue);
		}

		public void SetTargetAndUpdateY(float newTargetValue)
		{
			SetTargetAndUpdateFor(1, newTargetValue);
		}

		public void SetTargetAndUpdateZ(float newTargetValue)
		{
			SetTargetAndUpdateFor(2, newTargetValue);
		}

		public void SetTargetAndUpdateW(float newTargetValue)
		{
			SetTargetAndUpdateFor(3, newTargetValue);
		}

		public void SetTargetAndUpdateV(float newTargetValue)
		{
			SetTargetAndUpdateFor(4, newTargetValue);
		}

		public void SetTargetAndUpdateU(float newTargetValue)
		{
			SetTargetAndUpdateFor(5, newTargetValue);
		}

		public void ForceSetTargets(float newTargetValueX, float newTargetValueY, float newTargetValueZ, float newTargetValueW, float newTargetValueV, float newTargetValueU)
		{
			ForceSetTargetX(newTargetValueX);
			ForceSetTargetY(newTargetValueY);
			ForceSetTargetZ(newTargetValueZ);
			ForceSetTargetW(newTargetValueW);
			ForceSetTargetV(newTargetValueV);
			ForceSetTargetU(newTargetValueU);
		}

		public void ForceSetTargets(Vector6 newTargetValues)
		{
			ForceSetTargetX(newTargetValues.x);
			ForceSetTargetY(newTargetValues.y);
			ForceSetTargetZ(newTargetValues.z);
			ForceSetTargetW(newTargetValues.w);
			ForceSetTargetV(newTargetValues.v);
			ForceSetTargetU(newTargetValues.u);
		}

		public void ForceSetTargetX(float newTargetValue)
		{
			ForceSetTargetFor(0, newTargetValue);
		}

		public void ForceSetTargetY(float newTargetValue)
		{
			ForceSetTargetFor(1, newTargetValue);
		}

		public void ForceSetTargetZ(float newTargetValue)
		{
			ForceSetTargetFor(2, newTargetValue);
		}

		public void ForceSetTargetW(float newTargetValue)
		{
			ForceSetTargetFor(3, newTargetValue);
		}

		public void ForceSetTargetV(float newTargetValue)
		{
			ForceSetTargetFor(4, newTargetValue);
		}

		public void ForceSetTargetU(float newTargetValue)
		{
			ForceSetTargetFor(5, newTargetValue);
		}

		public void SetTensions(float newTensionX, float newTensionY, float newTensionZ, float newTensionW, float newTensionV, float newTensionU)
		{
			SetTensionX(newTensionX);
			SetTensionY(newTensionY);
			SetTensionZ(newTensionZ);
			SetTensionW(newTensionW);
			SetTensionV(newTensionV);
			SetTensionU(newTensionU);
		}

		public void SetTensions(Vector6 newTensions)
		{
			SetTensionX(newTensions.x);
			SetTensionY(newTensions.y);
			SetTensionZ(newTensions.z);
			SetTensionW(newTensions.w);
			SetTensionV(newTensions.v);
			SetTensionU(newTensions.u);
		}

		public void SetTensionX(float newTension)
		{
			SetTensionFor(0, newTension);
		}

		public void SetTensionY(float newTension)
		{
			SetTensionFor(1, newTension);
		}

		public void SetTensionZ(float newTension)
		{
			SetTensionFor(2, newTension);
		}

		public void SetTensionW(float newTension)
		{
			SetTensionFor(3, newTension);
		}

		public void SetTensionV(float newTension)
		{
			SetTensionFor(4, newTension);
		}

		public void SetTensionU(float newTension)
		{
			SetTensionFor(5, newTension);
		}

		public void SetMargins(float newMarginX, float newMarginY, float newMarginZ, float newMarginW, float newMarginV, float newMarginU)
		{
			SetMarginX(newMarginX);
			SetMarginY(newMarginY);
			SetMarginZ(newMarginZ);
			SetMarginW(newMarginW);
			SetMarginV(newMarginV);
			SetMarginU(newMarginU);
		}

		public void SetMargins(Vector6 newMargins)
		{
			SetMarginX(newMargins.x);
			SetMarginY(newMargins.y);
			SetMarginZ(newMargins.z);
			SetMarginW(newMargins.w);
			SetMarginV(newMargins.v);
			SetMarginU(newMargins.u);
		}

		public void SetMarginX(float newMargin)
		{
			SetMarginFor(0, newMargin);
		}

		public void SetMarginY(float newMargin)
		{
			SetMarginFor(1, newMargin);
		}

		public void SetMarginZ(float newMargin)
		{
			SetMarginFor(2, newMargin);
		}

		public void SetMarginW(float newMargin)
		{
			SetMarginFor(3, newMargin);
		}

		public void SetMarginV(float newMargin)
		{
			SetMarginFor(4, newMargin);
		}

		public void SetMarginU(float newMargin)
		{
			SetMarginFor(5, newMargin);
		}
	}

	private const string defaultFloatToStringFormat = "0.00";

	private const float defaultMargin = 0.001f;

	private const float defaultTension = 1f;

	private const float defaultTemporaryTension = 1f;
}
