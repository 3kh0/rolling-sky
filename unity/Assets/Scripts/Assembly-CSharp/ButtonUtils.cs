using UnityEngine;

public class ButtonUtils : MonoBehaviour
{
	public enum Name
	{
		Null = 0,
		Back = 1,
		BackAlt = 2,
		Rate = 3,
		Play = 4,
		Resume = 5,
		Pause = 6,
		GetLives = 7,
		Next = 8,
		Video = 9,
		VideoAlt = 10,
		Shop = 11,
		Settings = 12,
		Leaderboard = 13,
		Music = 14,
		Sound = 15,
		Record = 16,
		Alerts = 17,
		Restore = 18,
		Language = 19,
		Language0 = 20,
		Language1 = 21,
		Language2 = 22,
		Language3 = 23,
		Language4 = 24,
		Language5 = 25,
		Language6 = 26,
		Language7 = 27,
		Language8 = 28,
		Language9 = 29,
		Language10 = 30,
		Language11 = 31,
		Left = 32,
		Right = 33,
		OK = 34,
		Achievements = 35,
		GoogleController = 36,
		GoogleControllerActive = 37,
		Debug = 99
	}

	public class Button
	{
		private enum Coord
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public readonly Name Name;

		public readonly Transform Transform;

		public readonly bool IsNull;

		public readonly bool HasText;

		public readonly bool HasRenderer;

		private FrameAnim.SequenceAuto animator;

		private FrameAnim.Frame group;

		private MathUtils.Range offscreenRange;

		private Coord offscreenCoord;

		private bool offscreenRangeSet;

		private TextMesh[] textMeshs;

		private Renderer[] renderers;

		public bool IsNotNull
		{
			get
			{
				return !IsNull;
			}
		}

		public bool HasNotText
		{
			get
			{
				return !HasText;
			}
		}

		public bool HasNotRenderer
		{
			get
			{
				return !HasRenderer;
			}
		}

		public bool IsPressed
		{
			get
			{
				return animator.isLast;
			}
		}

		public bool IsNotPressed
		{
			get
			{
				return !IsPressed;
			}
		}

		public bool IsShown
		{
			get
			{
				if (IsNull)
				{
					return false;
				}
				return group.IsShown;
			}
		}

		public bool IsHidden
		{
			get
			{
				if (IsNull)
				{
					return false;
				}
				return group.IsHidden;
			}
		}

		public bool IsShownPressed
		{
			get
			{
				return IsShown && IsPressed;
			}
		}

		public bool IsShownNotPressed
		{
			get
			{
				return IsShown && IsNotPressed;
			}
		}

		public Button(Name name, Transform transform)
		{
			Name = name;
			Transform = transform;
			IsNull = Transform == null;
			if (!IsNull)
			{
				group = new FrameAnim.Frame(Transform);
				animator = new FrameAnim.SequenceAuto(Transform);
				textMeshs = Transform.GetComponentsInChildren<TextMesh>();
				renderers = Transform.GetComponentsInChildren<Renderer>();
				HasText = textMeshs != null && textMeshs.Length > 0;
				HasRenderer = renderers != null && renderers.Length > 0;
			}
			Reset();
		}

		public Button(string nullButton)
		{
			Name = Name.Null;
			IsNull = true;
			Reset();
		}

		public void Reset()
		{
			offscreenRangeSet = false;
		}

		public void Press()
		{
			if (IsNotNull)
			{
				animator.ShowLast();
			}
		}

		public void Depress()
		{
			if (IsNotNull)
			{
				animator.ShowFirst();
			}
		}

		public void SetPressedTo(bool pressed)
		{
			if (IsNotNull)
			{
				if (pressed)
				{
					Press();
				}
				else
				{
					Depress();
				}
			}
		}

		public void Hide()
		{
			if (IsNotNull)
			{
				group.Hide();
			}
		}

		public void Show()
		{
			if (IsNotNull)
			{
				group.Show();
			}
		}

		public void ShowDepressed()
		{
			Show();
			Depress();
		}

		public void ShowPressed()
		{
			Show();
			Press();
		}

		public void SetX(float newX)
		{
			if (IsNotNull)
			{
				TransformUtils.SetX(Transform, newX);
			}
		}

		public void SetY(float newY)
		{
			if (IsNotNull)
			{
				TransformUtils.SetY(Transform, newY);
			}
		}

		public void SetZ(float newZ)
		{
			if (IsNotNull)
			{
				TransformUtils.SetZ(Transform, newZ);
			}
		}

		public void SetRelativeToCorner(float defaultAspectRatio)
		{
			if (IsNotNull)
			{
				TransformUtils.SetRelativeToCorner(Transform, defaultAspectRatio);
			}
		}

		public void SetOffscreenX(float offscreenX, bool moveOffscreen = true)
		{
			if (IsNotNull)
			{
				offscreenRange = new MathUtils.Range(Transform.localPosition.x, Transform.localPosition.x + offscreenX);
				offscreenCoord = Coord.X;
				offscreenRangeSet = true;
				if (moveOffscreen)
				{
					TransformUtils.SetX(Transform, offscreenRange.Max, true);
				}
			}
		}

		public void SetOffscreenY(float offscreenY, bool moveOffscreen = true)
		{
			if (IsNotNull)
			{
				offscreenRange = new MathUtils.Range(Transform.localPosition.y, Transform.localPosition.y + offscreenY);
				offscreenCoord = Coord.Y;
				offscreenRangeSet = true;
				if (moveOffscreen)
				{
					TransformUtils.SetY(Transform, offscreenRange.Max, true);
				}
			}
		}

		public void SetOffscreenZ(float offscreenZ, bool moveOffscreen = true)
		{
			if (IsNotNull)
			{
				offscreenRange = new MathUtils.Range(Transform.localPosition.z, Transform.localPosition.z + offscreenZ);
				offscreenCoord = Coord.Z;
				offscreenRangeSet = true;
				if (moveOffscreen)
				{
					TransformUtils.SetZ(Transform, offscreenRange.Max, true);
				}
			}
		}

		public void SlideOnscreen(float onscreenPercent)
		{
			SetPosition(1f - onscreenPercent);
		}

		public void SlideOffscreen(float offscreenPercent)
		{
			SetPosition(offscreenPercent);
		}

		public void Onscreen()
		{
			SetPosition(0f);
		}

		public void Offscreen()
		{
			SetPosition(1f);
		}

		public void SetText(string textToDisplay)
		{
			if (HasText)
			{
				TextMesh[] array = textMeshs;
				foreach (TextMesh textMesh in array)
				{
					textMesh.text = textToDisplay;
				}
			}
		}

		public void SetText(string onTextToDisplay, string offTextToDisplay)
		{
			if (HasText)
			{
				textMeshs[0].text = onTextToDisplay;
				textMeshs[1].text = offTextToDisplay;
			}
		}

		public void SetTextColor(Color textColor)
		{
			if (HasText)
			{
				TextMesh[] array = textMeshs;
				foreach (TextMesh textMesh in array)
				{
					textMesh.color = textColor;
				}
			}
		}

		private void SetPosition(float offscreenPercent)
		{
			if (offscreenRangeSet)
			{
				float num = offscreenRange.FromPercent01(offscreenPercent);
				switch (offscreenCoord)
				{
				case Coord.X:
					TransformUtils.SetX(Transform, num, true);
					break;
				case Coord.Y:
					TransformUtils.SetY(Transform, num, true);
					break;
				case Coord.Z:
					TransformUtils.SetZ(Transform, num, true);
					break;
				default:
					Debug.LogError(string.Format("BTUT: ERROR: Unhandled offscreenCoord case of {0} in SetOnOffscreen - check logic or extend case statement", offscreenCoord));
					break;
				}
			}
			else
			{
				Debug.LogError(string.Format("BTUT: ERROR: Attempt to move button {0} on/off screen before any offscreenRange was set.  Please first call SetOffscreenX, SetOffscreenY or SetOffscreenZ to set an offscreenRange", Name));
			}
		}
	}

	public static bool CheckButtonHit(Transform buttonToCheck)
	{
		bool result = false;
		Vector2 vector = Input.mousePosition;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), out hitInfo) && hitInfo.transform.Equals(buttonToCheck))
		{
			result = true;
		}
		return result;
	}

	public static bool CheckButtonNotHit(Transform buttonToCheck)
	{
		return !ButtonWasHit(buttonToCheck);
	}

	public static bool ButtonWasHit(Transform buttonToCheck)
	{
		bool result = false;
		Vector2 vector = Input.mousePosition;
		RaycastHit[] array = Physics.RaycastAll(Camera.main.ScreenPointToRay(vector));
		if (array != null && array.Length > 0)
		{
			RaycastHit[] array2 = array;
			foreach (RaycastHit raycastHit in array2)
			{
				if (raycastHit.transform.Equals(buttonToCheck))
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public static bool ButtonWasNotHit(Transform buttonToCheck)
	{
		return !ButtonWasHit(buttonToCheck);
	}

	public static bool ButtonWasHit(Transform[] buttonsToCheck)
	{
		int indexOfHitButton;
		return ButtonWasHit(buttonsToCheck, out indexOfHitButton);
	}

	public static bool ButtonWasHit(Transform[] buttonsToCheck, out int indexOfHitButton)
	{
		bool flag = false;
		indexOfHitButton = -1;
		if (buttonsToCheck != null && buttonsToCheck.Length > 0)
		{
			Vector2 vector = Input.mousePosition;
			RaycastHit[] array = Physics.RaycastAll(Camera.main.ScreenPointToRay(vector));
			if (array != null && array.Length == 0)
			{
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit = array2[i];
					for (int j = 0; j < buttonsToCheck.Length; j++)
					{
						if (raycastHit.transform.Equals(buttonsToCheck[j]))
						{
							indexOfHitButton = j;
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
		}
		return flag;
	}

	public static bool ButtonWasHit(Transform[] buttonsToCheck, out Transform buttonThatWasHit)
	{
		int indexOfHitButton;
		bool flag = ButtonWasHit(buttonsToCheck, out indexOfHitButton);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck[indexOfHitButton];
		}
		else
		{
			buttonThatWasHit = null;
		}
		return flag;
	}

	public static bool ButtonWasHit(ArrayUtils.List<Transform> buttonsToCheck)
	{
		int buttonThatWasHit;
		return ButtonWasHit(buttonsToCheck, out buttonThatWasHit);
	}

	public static bool ButtonWasHit(ArrayUtils.List<Transform> buttonsToCheck, out int buttonThatWasHit)
	{
		bool flag = false;
		buttonThatWasHit = -1;
		if (buttonsToCheck != null && buttonsToCheck.Length > 0)
		{
			Vector2 vector = Input.mousePosition;
			RaycastHit[] array = Physics.RaycastAll(Camera.main.ScreenPointToRay(vector));
			if (array != null && array.Length > 0)
			{
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit = array2[i];
					for (int j = 0; j < buttonsToCheck.Length; j++)
					{
						if (raycastHit.transform.Equals(buttonsToCheck[j]))
						{
							buttonThatWasHit = j;
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
		}
		return flag;
	}

	public static bool ButtonWasHit(ArrayUtils.List<Transform> buttonsToCheck, out Transform buttonThatWasHit)
	{
		int buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck[buttonThatWasHit2];
		}
		else
		{
			buttonThatWasHit = null;
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Transform> buttonsToCheck)
	{
		KeyType buttonThatWasHit;
		return ButtonWasHit(buttonsToCheck, out buttonThatWasHit);
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Button> buttonsToCheck)
	{
		KeyType buttonThatWasHit;
		return ButtonWasHit(buttonsToCheck, out buttonThatWasHit);
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Transform> buttonsToCheck, out KeyType buttonThatWasHit)
	{
		bool flag = false;
		buttonThatWasHit = default(KeyType);
		if (buttonsToCheck != null && buttonsToCheck.Length > 0)
		{
			Vector2 vector = Input.mousePosition;
			RaycastHit[] array = Physics.RaycastAll(Camera.main.ScreenPointToRay(vector));
			if (array != null && array.Length > 0)
			{
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit = array2[i];
					for (int j = 0; j < buttonsToCheck.Length; j++)
					{
						if (raycastHit.transform.Equals(buttonsToCheck.Values[j]))
						{
							buttonThatWasHit = buttonsToCheck.Keys[j];
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Button> buttonsToCheck, out KeyType buttonThatWasHit)
	{
		bool flag = false;
		buttonThatWasHit = default(KeyType);
		if (buttonsToCheck != null && buttonsToCheck.Length > 0)
		{
			Vector2 vector = Input.mousePosition;
			RaycastHit[] array = Physics.RaycastAll(Camera.main.ScreenPointToRay(vector));
			if (array != null && array.Length > 0)
			{
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit = array2[i];
					for (int j = 0; j < buttonsToCheck.Length; j++)
					{
						if (raycastHit.transform.Equals(buttonsToCheck.Values[j].Transform))
						{
							buttonThatWasHit = buttonsToCheck.Keys[j];
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Transform> buttonsToCheck, out Transform buttonThatWasHit)
	{
		KeyType buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck[buttonThatWasHit2];
		}
		else
		{
			buttonThatWasHit = null;
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Button> buttonsToCheck, out Transform buttonThatWasHit)
	{
		KeyType buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck[buttonThatWasHit2].Transform;
		}
		else
		{
			buttonThatWasHit = null;
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Transform> buttonsToCheck, out int buttonThatWasHit)
	{
		KeyType buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck.IndexOfKey(buttonThatWasHit2);
		}
		else
		{
			buttonThatWasHit = -1;
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Button> buttonsToCheck, out int buttonThatWasHit)
	{
		KeyType buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck.IndexOfKey(buttonThatWasHit2);
		}
		else
		{
			buttonThatWasHit = -1;
		}
		return flag;
	}

	public static bool ButtonWasHit<KeyType>(ArrayUtils.SmartDict<KeyType, Button> buttonsToCheck, out Button buttonThatWasHit)
	{
		KeyType buttonThatWasHit2;
		bool flag = ButtonWasHit(buttonsToCheck, out buttonThatWasHit2);
		if (flag)
		{
			buttonThatWasHit = buttonsToCheck[buttonThatWasHit2];
		}
		else
		{
			buttonThatWasHit = new Button(null);
		}
		return flag;
	}
}
