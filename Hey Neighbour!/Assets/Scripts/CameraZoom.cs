using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoomMode { NULL, House, Island, World }

public class CameraZoom : MonoBehaviour
{
	[Header ("Components:")]

	public Camera cam;

	[Header ("Variables:")]

	public ZoomMode zoomMode;

	[Header ("Zoom Sizes:")]

	public float house = 4f;
	public float island = 8f;
	public float world = 16f;

	private void OnEnable ()
	{
		ZoomTo_World ();
	}

	public void ZoomTo_House ()
	{
		zoomMode = ZoomMode.House;
		Set_OrthoSize_By_ZoomMode ();
	}

	public void ZoomTo_Island ()
	{
		zoomMode = ZoomMode.Island;
		Set_OrthoSize_By_ZoomMode ();
	}

	public void ZoomTo_World ()
	{
		zoomMode = ZoomMode.World;
		Set_OrthoSize_By_ZoomMode ();
	}

	private void Set_OrthoSize_By_ZoomMode ()
	{
		switch (zoomMode)
		{
			case ZoomMode.Island:
				cam.orthographicSize = island;
				break;
			case ZoomMode.House:
				cam.orthographicSize = house;
				break;
			case ZoomMode.World:
				cam.orthographicSize = world;
				break;
		}
	}

	public void Zoom_Away ()
	{
		cam.orthographicSize += 10f * Time.deltaTime;
	}
}
