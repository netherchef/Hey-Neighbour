using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
	[Header ("Components:")]

	public Transform camTransform;

	public void Drag_Cam (Vector3 deltaPos)
	{
		camTransform.position += deltaPos * Time.deltaTime;
	}
}