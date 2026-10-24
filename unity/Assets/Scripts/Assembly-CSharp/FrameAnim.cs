using System.Collections.Generic;
using UnityEngine;

public class FrameAnim : MonoBehaviour
{
	public struct Frame
	{
		private const bool debugMode = false;

		private Transform transform;

		private Transform[] transforms;

		private Renderer renderer;

		private Renderer[] renderers;

		private bool renderable;

		private bool multiple;

		public bool IsHidden { get; private set; }

		public bool IsShown
		{
			get
			{
				return !IsHidden;
			}
		}

		public Frame(Transform transform)
		{
			this = default(Frame);
			InitializeHidable(transform, null, false, false);
		}

		public Frame(bool startHidden, Transform transform)
		{
			this = default(Frame);
			InitializeHidable(transform, null, startHidden, false);
		}

		public Frame(params Transform[] transforms)
		{
			this = default(Frame);
			InitializeHidable(null, transforms, false, true);
		}

		public Frame(bool startHidden, params Transform[] transforms)
		{
			this = default(Frame);
			InitializeHidable(null, transforms, startHidden, true);
		}

		public Frame(GameObject gameObject)
		{
			this = default(Frame);
			InitializeHidable(ToTransform(gameObject), null, false, false);
		}

		public Frame(bool startHidden, GameObject gameObject)
		{
			this = default(Frame);
			InitializeHidable(ToTransform(gameObject), null, startHidden, false);
		}

		public Frame(params GameObject[] gameObjects)
		{
			this = default(Frame);
			InitializeHidable(null, ToTransforms(gameObjects), false, true);
		}

		public Frame(bool startHidden, params GameObject[] gameObjects)
		{
			this = default(Frame);
			InitializeHidable(null, ToTransforms(gameObjects), startHidden, true);
		}

		public Frame(Component component)
		{
			this = default(Frame);
			InitializeHidable(ToTransform(component), null, false, false);
		}

		public Frame(bool startHidden, Component component)
		{
			this = default(Frame);
			InitializeHidable(ToTransform(component), null, startHidden, false);
		}

		public Frame(params Component[] components)
		{
			this = default(Frame);
			InitializeHidable(null, ToTransforms(components), false, true);
		}

		public Frame(bool startHidden, params Component[] components)
		{
			this = default(Frame);
			InitializeHidable(null, ToTransforms(components), startHidden, true);
		}

		private void InitializeHidable(Transform transform, Transform[] transforms, bool startHidden, bool multiple)
		{
			if (multiple)
			{
				this.transforms = new Transform[transforms.Length];
				this.transforms = transforms;
				bool flag = false;
				List<Renderer> list = new List<Renderer>();
				for (int i = 0; i < transforms.Length; i++)
				{
					Renderer renderer = ((!(transforms[i] != null)) ? null : transforms[i].GetComponent<Renderer>());
					if (renderer != null)
					{
						list.Add(renderer);
						flag = true;
					}
				}
				if (flag)
				{
					renderers = list.ToArray();
					renderable = true;
				}
				else
				{
					renderable = false;
				}
				list.Clear();
				list = null;
			}
			else
			{
				this.transform = transform;
				this.renderer = ((!(transform != null)) ? null : transform.GetComponent<Renderer>());
				renderable = this.renderer != null;
			}
			this.multiple = multiple;
			if (startHidden)
			{
				Hide();
			}
			else
			{
				IsHidden = false;
			}
			this.multiple = multiple;
		}

		public void Hide()
		{
			if (IsHidden)
			{
				return;
			}
			IsHidden = true;
			if (multiple)
			{
				for (int i = 0; i < transforms.Length; i++)
				{
					FrameAnim.Hide(transforms[i], true);
				}
			}
			else
			{
				FrameAnim.Hide(transform, true);
			}
		}

		public void Show()
		{
			if (IsShown)
			{
				return;
			}
			IsHidden = false;
			if (multiple)
			{
				for (int i = 0; i < transforms.Length; i++)
				{
					FrameAnim.Show(transforms[i], true);
				}
			}
			else
			{
				FrameAnim.Show(transform, true);
			}
		}

		public void SetTo(bool show)
		{
			if (show)
			{
				Show();
			}
			else
			{
				Hide();
			}
		}

		public void Toggle()
		{
			if (IsShown)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}

		public bool CheckIfVisible()
		{
			if (!renderable || IsHidden)
			{
				return false;
			}
			if (multiple)
			{
				for (int i = 0; i < renderers.Length; i++)
				{
					if (renderers[i].isVisible)
					{
						return true;
					}
				}
				return false;
			}
			return renderer.isVisible;
		}

		public Material Retexture(Texture newTexture)
		{
			return Retexture(newTexture, false, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterial)
		{
			return Retexture(newTexture, useSharedMaterial, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterial, Material lastMaterialRetextured)
		{
			if (multiple)
			{
				Material material = null;
				for (int i = 0; i < renderers.Length; i++)
				{
					Material material2 = ((!useSharedMaterial) ? renderers[i].material : renderers[i].sharedMaterial);
					if (material != null && material != material2 && lastMaterialRetextured != null && lastMaterialRetextured != material2)
					{
						material2.mainTexture = newTexture;
					}
					material = material2;
				}
				return material;
			}
			Material material3 = ((!useSharedMaterial) ? renderer.material : renderer.sharedMaterial);
			if (lastMaterialRetextured != null && lastMaterialRetextured == material3)
			{
				material3.mainTexture = newTexture;
			}
			return material3;
		}

		public void RotateTo(Quaternion newLocalRotation)
		{
			if (multiple)
			{
				for (int i = 0; i < transforms.Length; i++)
				{
					transforms[i].localRotation = newLocalRotation;
				}
			}
			else
			{
				transform.localRotation = newLocalRotation;
			}
		}

		public Quaternion GetRotation()
		{
			return GetRoot().localRotation;
		}

		public Transform GetRoot()
		{
			if (multiple)
			{
				return transforms[0];
			}
			return transform;
		}

		public void SetX(float newX)
		{
			SetX(newX, false);
		}

		public void SetX(float newX, bool localCoordinates)
		{
			TransformUtils.SetX(GetRoot(), newX, localCoordinates);
		}

		public void SetY(float newY)
		{
			SetY(newY, false);
		}

		public void SetY(float newY, bool localCoordinates)
		{
			TransformUtils.SetY(GetRoot(), newY, localCoordinates);
		}

		public void SetZ(float newZ)
		{
			SetZ(newZ, false);
		}

		public void SetZ(float newZ, bool localCoordinates)
		{
			TransformUtils.SetZ(GetRoot(), newZ, localCoordinates);
		}

		public float GetX()
		{
			return GetX(false);
		}

		public float GetX(bool localCoordinates)
		{
			if (localCoordinates)
			{
				return GetRoot().transform.localPosition.x;
			}
			return GetRoot().transform.position.x;
		}

		public float GetY()
		{
			return GetY(false);
		}

		public float GetY(bool localCoordinates)
		{
			if (localCoordinates)
			{
				return GetRoot().transform.localPosition.y;
			}
			return GetRoot().transform.position.y;
		}

		public float GetZ()
		{
			return GetZ(false);
		}

		public float GetZ(bool localCoordinates)
		{
			if (localCoordinates)
			{
				return GetRoot().transform.localPosition.z;
			}
			return GetRoot().transform.position.z;
		}

		public void Clear()
		{
			if (IsHidden)
			{
				Show();
			}
		}

		public string DebugString(bool hiding)
		{
			return string.Format("{0} hidable {1} {2}", (!hiding) ? "Showing" : "Hiding", (!multiple) ? "singular" : "multiple from", (!multiple) ? transform.name : transforms[0].name);
		}
	}

	public abstract class ClasstypeFrames
	{
		protected bool isHiddenSaved;

		protected bool isEmpty;

		protected bool isInitialized;

		protected bool hasLooped;

		protected bool hasBlank;

		protected int currentIndex;

		protected bool isSaved;

		protected bool lastFrameHidden;

		public bool IsHidden { get; protected set; }

		public bool IsShown
		{
			get
			{
				return !IsHidden;
			}
		}

		public int Length { get; protected set; }

		public int LengthSansBlank { get; protected set; }

		protected bool isNotEmpty
		{
			get
			{
				return !isEmpty;
			}
		}

		protected bool isNotInitialized
		{
			get
			{
				return !isInitialized;
			}
		}

		public bool isLast
		{
			get
			{
				return Query(true);
			}
		}

		public bool isFirst
		{
			get
			{
				return Query(false);
			}
		}

		public void Hide()
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("Hide", "Frame(Sequence/SequenceSet ParentClass)"));
			}
			else if (!isEmpty && !IsHidden)
			{
				for (int i = 0; i < Length; i++)
				{
					HideEntry(i);
				}
				IsHidden = true;
				if (isHiddenSaved)
				{
					isSaved = true;
				}
				else
				{
					lastFrameHidden = true;
				}
			}
		}

		public void Show()
		{
			EnactShow(isHiddenSaved ? currentIndex : 0);
		}

		public void ShowNormal()
		{
			EnactShow(0);
		}

		public void ShowPressed()
		{
			EnactShow(1);
		}

		public void ShowFirst()
		{
			EnactShow(0);
		}

		public void ShowLast()
		{
			EnactShow(Length - 1);
		}

		public void Show(int number)
		{
			EnactShow(number);
		}

		public void ShowIfHas(int number)
		{
			if (isInitialized && isNotEmpty && MathUtils.IndexIsWithin(number, Length))
			{
				EnactShow(number, true);
			}
		}

		public void ShowIfHas(int number, int backupNumber)
		{
			if (isInitialized && isNotEmpty)
			{
				if (MathUtils.IndexIsWithin(number, Length))
				{
					EnactShow(number, true);
				}
				else
				{
					EnactShow(backupNumber, true);
				}
			}
		}

		public void Next()
		{
			Shift(1);
		}

		public void Next(int skipping)
		{
			Shift(1 + skipping);
		}

		public void Previous()
		{
			Shift(-1);
		}

		public void Previous(int skipping)
		{
			Shift(-1 - skipping);
		}

		public void Shift(int by)
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("Next", "Frame(Sequence/SequenceSet ParentClass)"));
			}
			else if (!isEmpty)
			{
				if (!lastFrameHidden)
				{
					HideEntry(currentIndex);
				}
				currentIndex += by;
				if ((currentIndex >= Length) | (currentIndex < 0))
				{
					hasLooped = true;
				}
				if (hasLooped)
				{
					currentIndex = MathUtils.Indexed(currentIndex, Length);
				}
				ShowEntry(currentIndex);
				lastFrameHidden = false;
				IsHidden = false;
			}
		}

		public bool CheckIfVisible()
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("isVisible", "Frame(Sequence/SequenceSet ParentClass)"));
				return false;
			}
			if (isEmpty || IsHidden)
			{
				return false;
			}
			return IsVisibleEntry(currentIndex);
		}

		private bool Query(bool isLast)
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("isLast", "Frame(Sequence/SequenceSet ParentClass)"));
				return false;
			}
			if (isEmpty || IsHidden)
			{
				return false;
			}
			if (isLast)
			{
				return currentIndex == Length - 1;
			}
			return currentIndex == 0;
		}

		public void SetSaveHidden(bool on = true)
		{
			isHiddenSaved = on;
		}

		private void EnactShow(int number, bool preverified = false)
		{
			if (!preverified && (!isInitialized || !isNotEmpty))
			{
				return;
			}
			if (MathUtils.IndexIsNotWithin(number, Length))
			{
				Debug.LogError(ErrorStrings.ValueOutOfRange(number, "Show(number)", Length - 1));
				return;
			}
			if (isSaved)
			{
				isSaved = false;
				ShowEntry(currentIndex);
				IsHidden = false;
				return;
			}
			if (IsHidden)
			{
				IsHidden = false;
			}
			else if (number != currentIndex)
			{
				HideEntry(currentIndex);
			}
			ShowEntry(number);
			lastFrameHidden = false;
			currentIndex = number;
		}

		protected virtual void ShowEntry(int number)
		{
			Debug.LogError(ErrorStrings.FunctionNotOverriden("ShowEntry", "Frames Parent Class"));
		}

		protected virtual void HideEntry(int number)
		{
			Debug.LogError(ErrorStrings.FunctionNotOverriden("HideEntry", "Frames Parent Class"));
		}

		protected virtual bool IsVisibleEntry(int number)
		{
			Debug.LogError(ErrorStrings.FunctionNotOverriden("IsVisibleEntry", "Frames Parent Class"));
			return false;
		}
	}

	public class Sequence : ClasstypeFrames
	{
		private Frame[] entries;

		public Sequence(params Transform[] transforms)
		{
			InitializeSequence(transforms, false, 0, 0);
		}

		public Sequence(bool startHidden, params Transform[] transforms)
		{
			InitializeSequence(transforms, startHidden, 0, 0);
		}

		public Sequence(int startAt, params Transform[] transforms)
		{
			InitializeSequence(transforms, false, startAt, 0);
		}

		public Sequence(params Component[] components)
		{
			InitializeSequence(ToTransforms(components), false, 0, 0);
		}

		public Sequence(bool startHidden, params Component[] components)
		{
			InitializeSequence(ToTransforms(components), startHidden, 0, 0);
		}

		public Sequence(int startAt, params Component[] components)
		{
			InitializeSequence(ToTransforms(components), false, startAt, 0);
		}

		public Sequence(bool startHidden, int blankFrames, params Transform[] transforms)
		{
			InitializeSequence(transforms, startHidden, 0, blankFrames);
		}

		public Sequence(int startAt, int blankFrames, params Transform[] transforms)
		{
			InitializeSequence(transforms, false, startAt, blankFrames);
		}

		public Sequence(bool startHidden, int blankFrames, params Component[] components)
		{
			InitializeSequence(ToTransforms(components), startHidden, 0, blankFrames);
		}

		public Sequence(int startAt, int blankFrames, params Component[] components)
		{
			InitializeSequence(ToTransforms(components), false, startAt, blankFrames);
		}

		protected Sequence()
		{
		}

		protected void InitializeSequence(Transform[] transforms, bool startHidden, int startAt, int blankFrames)
		{
			if (transforms == null || transforms.Length == 0)
			{
				isEmpty = true;
			}
			else
			{
				int num = 0;
				while (true)
				{
					if (num < transforms.Length)
					{
						if (transforms[num] != null)
						{
							isEmpty = false;
							break;
						}
						num++;
						continue;
					}
					isEmpty = true;
					break;
				}
			}
			if (isEmpty)
			{
				Debug.LogWarning("Error - no valid frames passed to sequence");
				entries = null;
				base.IsHidden = false;
				currentIndex = -1;
				isInitialized = true;
				return;
			}
			base.Length = transforms.Length;
			if (startHidden)
			{
				base.IsHidden = true;
				lastFrameHidden = true;
			}
			else
			{
				currentIndex = startAt;
			}
			entries = new Frame[base.Length];
			for (int i = 0; i < base.Length; i++)
			{
				bool startHidden2 = startHidden || i != currentIndex;
				entries[i] = new Frame(startHidden2, transforms[i]);
			}
			if (blankFrames > 0)
			{
				hasBlank = true;
				base.LengthSansBlank = base.Length;
				base.Length += blankFrames;
			}
			isInitialized = true;
		}

		protected bool IsntBlank(int number)
		{
			return !hasBlank || number < base.LengthSansBlank;
		}

		public Material Retexture(Texture newTexture)
		{
			return Retexture(newTexture, false, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterials)
		{
			return Retexture(newTexture, useSharedMaterials, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterials, Material lastMaterialRetextured)
		{
			Frame[] array = entries;
			foreach (Frame frame in array)
			{
				lastMaterialRetextured = frame.Retexture(newTexture, useSharedMaterials, lastMaterialRetextured);
			}
			return lastMaterialRetextured;
		}

		public int GetCurrentIndex()
		{
			return currentIndex;
		}

		public void UpdateAlternate()
		{
			Quaternion rotation = entries[currentIndex].GetRotation();
			for (int i = 0; i < entries.Length; i++)
			{
				if (i != currentIndex)
				{
					entries[i].RotateTo(rotation);
				}
			}
		}

		protected override void ShowEntry(int number)
		{
			if (IsntBlank(number))
			{
				entries[number].Show();
			}
		}

		protected override void HideEntry(int number)
		{
			if (IsntBlank(number))
			{
				entries[number].Hide();
			}
		}

		protected override bool IsVisibleEntry(int number)
		{
			return IsntBlank(number) && entries[number].CheckIfVisible();
		}

		public Transform GetCurrentRoot()
		{
			return entries[currentIndex].GetRoot();
		}

		public void Clear()
		{
			if (entries == null)
			{
				Debug.LogWarning("FMAN: ERROR: Attempt to clear FrameAnimator before 'entries' was ever defined?!  Check logic");
			}
			else
			{
				for (int i = 0; i < entries.Length; i++)
				{
					ShowEntry(i);
				}
			}
			entries = null;
			base.IsHidden = false;
			currentIndex = -1;
			isInitialized = false;
		}
	}

	public class SequenceSet : ClasstypeFrames
	{
		private Sequence[] entries;

		public SequenceSet(params Sequence[] sequences)
		{
			InitializeSequenceSet(sequences, false, 0, 0);
		}

		public SequenceSet(bool startHidden, params Sequence[] sequences)
		{
			InitializeSequenceSet(sequences, startHidden, 0, 0);
		}

		public SequenceSet(int startAtSequence, params Sequence[] sequences)
		{
			InitializeSequenceSet(sequences, false, startAtSequence, 0);
		}

		public SequenceSet(int startAtSequence, int startAtFrame, params Sequence[] sequences)
		{
			InitializeSequenceSet(sequences, false, startAtSequence, startAtFrame);
		}

		public SequenceSet(params Component[][] sequenceFrames)
		{
			InitializeSequenceSet(ToSequenceArray(sequenceFrames), false, 0, 0);
		}

		public SequenceSet(bool startHidden, params Component[][] sequenceFrames)
		{
			InitializeSequenceSet(ToSequenceArray(sequenceFrames), startHidden, 0, 0);
		}

		public SequenceSet(int startAtSequence, params Component[][] sequenceFrames)
		{
			InitializeSequenceSet(ToSequenceArray(sequenceFrames), false, startAtSequence, 0);
		}

		public SequenceSet(int startAtSequence, int startAtFrame, params Component[][] sequenceFrames)
		{
			InitializeSequenceSet(ToSequenceArray(sequenceFrames), false, startAtSequence, startAtFrame);
		}

		protected static Sequence[] ToSequenceArray(Component[][] sequenceFrames)
		{
			if (sequenceFrames == null)
			{
				return null;
			}
			int length = sequenceFrames.GetLength(0);
			if (length == 0)
			{
				return null;
			}
			Sequence[] array = new Sequence[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new Sequence(sequenceFrames[i]);
			}
			return array;
		}

		protected void InitializeSequenceSet(Sequence[] sequences, bool startHidden, int startAtSequence, int startAtFrame)
		{
			if (sequences.Length == 0)
			{
				isEmpty = true;
			}
			if (isEmpty)
			{
				Debug.LogError("Error - no valid frames passed to sequence");
				entries = null;
				base.IsHidden = false;
				lastFrameHidden = true;
				isInitialized = true;
				return;
			}
			base.Length = sequences.Length;
			if (startHidden)
			{
				base.IsHidden = true;
				lastFrameHidden = true;
			}
			else
			{
				currentIndex = startAtSequence;
			}
			entries = new Sequence[base.Length];
			for (int i = 0; i < base.Length; i++)
			{
				entries[i] = sequences[i];
				if (!startHidden && i == currentIndex)
				{
					entries[i].Show(startAtFrame);
				}
				else
				{
					entries[i].Hide();
				}
			}
			isInitialized = true;
		}

		public void NextFrame()
		{
			ShiftFrame(1);
		}

		public void NextFrame(int skipping)
		{
			ShiftFrame(1 + skipping);
		}

		public void PreviousFrame()
		{
			ShiftFrame(-1);
		}

		public void PreviousFrame(int skipping)
		{
			ShiftFrame(-1 - skipping);
		}

		public void ShiftFrame(int by)
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("Shiftrame", "SequenceSet"));
			}
			else if (!isEmpty)
			{
				entries[currentIndex].Shift(by);
			}
		}

		public void ShowFrame(int number)
		{
			AccessFrame(number);
		}

		public void ShowFirstFrame()
		{
			AccessFrame(0);
		}

		public void ShowLastFrame()
		{
			AccessFrame(-1);
		}

		private void AccessFrame(int number)
		{
			if (!isEmpty)
			{
				if (number == -1)
				{
					entries[currentIndex].ShowLast();
				}
				else
				{
					entries[currentIndex].Show(number);
				}
			}
		}

		public bool CheckIfLastFrame()
		{
			return QueryFrame(true);
		}

		public bool CheckIfFirstFrame()
		{
			return QueryFrame(false);
		}

		private bool QueryFrame(bool isLast)
		{
			if (!isInitialized)
			{
				Debug.LogError(ErrorStrings.FunctionNotInitialized("isLast", "SequenceSet"));
				return false;
			}
			if (isEmpty || base.IsHidden)
			{
				return false;
			}
			if (isLast)
			{
				return entries[currentIndex].isLast;
			}
			return entries[currentIndex].isFirst;
		}

		public Material Retexture(Texture newTexture)
		{
			return Retexture(newTexture, false, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterials)
		{
			return Retexture(newTexture, useSharedMaterials, null);
		}

		public Material Retexture(Texture newTexture, bool useSharedMaterials, Material lastMaterialRetextured)
		{
			Sequence[] array = entries;
			foreach (Sequence sequence in array)
			{
				lastMaterialRetextured = sequence.Retexture(newTexture, useSharedMaterials, lastMaterialRetextured);
			}
			return lastMaterialRetextured;
		}

		public int GetCurrentFrameIndex()
		{
			return (!isEmpty) ? entries[currentIndex].GetCurrentIndex() : (-1);
		}

		public int GetCurrentSequenceIndex()
		{
			return currentIndex;
		}

		protected override void ShowEntry(int number)
		{
			entries[number].Show();
		}

		protected override void HideEntry(int number)
		{
			entries[number].Hide();
		}

		protected override bool IsVisibleEntry(int number)
		{
			return entries[number].CheckIfVisible();
		}
	}

	public class SequenceAuto : Sequence
	{
		private const string defaultSwitchNameStart = "state";

		public SequenceAuto(Transform parentTransform, string switchNameStart = "state")
		{
			int length = switchNameStart.Length;
			Transform[] componentsInChildren = parentTransform.GetComponentsInChildren<Transform>();
			NumberedTransform[] array = new NumberedTransform[componentsInChildren.Length];
			int num = 0;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (!componentsInChildren[i].Equals(parentTransform) && componentsInChildren[i].name.Length >= length)
				{
					string text = componentsInChildren[i].name.Substring(0, length);
					if (text.ToLowerInvariant().Equals(switchNameStart.ToLowerInvariant()))
					{
						array[num] = new NumberedTransform(componentsInChildren[i], length, i);
						num++;
					}
				}
			}
			for (int j = 0; j < num - 1; j++)
			{
				for (int k = j; k < num - 1; k++)
				{
					if (array[k].Number > array[k + 1].Number)
					{
						NumberedTransform numberedTransform = array[k];
						array[k] = array[k + 1];
						array[k + 1] = numberedTransform;
					}
				}
			}
			Transform[] array2 = new Transform[num];
			for (int l = 0; l < num; l++)
			{
				array2[l] = array[l].Transform;
				string text2 = ((!(array2[l] == null)) ? array2[l].name : "NULL");
			}
			InitializeSequence(array2, false, 0, 0);
		}
	}

	private struct NumberedTransform
	{
		private const int ammountToAddToUnnumbered = 10000;

		public readonly int Number;

		public readonly Transform Transform;

		public readonly bool IsNegative;

		public NumberedTransform(Transform transform, int lengthOfNameToRemove, int foundOrder)
		{
			Transform = transform;
			string s = transform.name.Substring(lengthOfNameToRemove, transform.name.Length - lengthOfNameToRemove);
			int result;
			if (int.TryParse(s, out result))
			{
				IsNegative = result < 0;
				if (IsNegative)
				{
					result *= -1;
				}
			}
			else
			{
				result = foundOrder + 10000;
				IsNegative = false;
			}
			Number = result;
		}
	}

	private const float hideDistance = 100000f;

	private const float hideCheckDistance = -50000f;

	private static Transform ToTransform(GameObject gameObject)
	{
		Transform result = null;
		if (gameObject != null)
		{
			result = gameObject.transform;
		}
		return result;
	}

	private static Transform ToTransform(Component component)
	{
		Transform result = null;
		if (component != null)
		{
			result = component.transform;
		}
		return result;
	}

	private static Transform[] ToTransforms(GameObject[] gameObjects)
	{
		Transform[] array = null;
		if (gameObjects != null)
		{
			array = new Transform[gameObjects.Length];
			for (int i = 0; i < gameObjects.Length; i++)
			{
				array[i] = ToTransform(gameObjects[i]);
			}
		}
		return array;
	}

	private static Transform[] ToTransforms(Component[] components)
	{
		Transform[] array = null;
		if (components != null)
		{
			array = new Transform[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				array[i] = ToTransform(components[i]);
			}
		}
		return array;
	}

	private static void Hide(Transform item, bool hide = true)
	{
		MoveHide(item, hide);
	}

	private static void Show(Transform item, bool show = true)
	{
		MoveHide(item, !show);
	}

	private static void MoveHide(Transform item, bool hide)
	{
		if (item == null)
		{
			return;
		}
		if (hide)
		{
			if (item.localPosition.x > -50000f)
			{
				item.localPosition += Vector3.left * 100000f;
			}
		}
		else if (item.localPosition.x < -50000f)
		{
			item.localPosition += Vector3.right * 100000f;
		}
	}
}
