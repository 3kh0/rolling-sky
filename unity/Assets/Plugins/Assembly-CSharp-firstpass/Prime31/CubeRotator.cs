using UnityEngine;

namespace Prime31
{
	public class CubeRotator : MonoBehaviour
	{
		public float speed = 15f;

		private Transform cube;

		private bool shouldRotate = true;

		private void Start()
		{
			cube = GetComponent<Transform>();
		}

		private void Update()
		{
			if (shouldRotate)
			{
				cube.Rotate(Vector3.forward, Time.deltaTime * speed);
			}
		}

		public void togglePauseRotation()
		{
			Debug.Log("toggle pause");
			shouldRotate = !shouldRotate;
		}
	}
}
