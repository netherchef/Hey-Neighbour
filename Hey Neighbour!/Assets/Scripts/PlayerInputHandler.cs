using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum InputType { NULL, MouseClick }

public class PlayerInputHandler : MonoBehaviour
{
	[Header ("Components:")]

	public Camera cam;

	public Vector3 ClickPosition ()
	{
		if (Input.touchCount > 0) return cam.ScreenToWorldPoint (Input.GetTouch (0).position);
		return cam.ScreenToWorldPoint (Input.mousePosition);
	}

	public bool Click ()
	{
		if (Input.touchCount > 0) return Input.GetTouch (0).phase == TouchPhase.Began;
		return Input.GetMouseButtonDown (0);
	}

	public bool Drag ()
	{
		if (Input.touchCount <= 0) return false;
		return Input.GetTouch (0).phase == TouchPhase.Moved;
	}
}