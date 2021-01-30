using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDecider : MonoBehaviour
{
	[Header ("Components:")]

	public Transform camTransform;

	[Header ("Menu:")]

	public GameObject menu;
	public GameObject background;
	public Image sceneHider;

	[Header ("Scripts:")]

	public PlayerInputHandler playerInputHandler;

	public CameraZoom cameraZoom;
	public CameraPositioner cameraPositioner;

	[Header ("Debug:")]

	public bool debug;

	private void Update ()
	{
		if (menu.activeSelf) return;

		if (playerInputHandler.Drag ())
		{
			cameraPositioner.Drag_Cam (-Input.GetTouch (0).deltaPosition);

			return;
		}

		if (playerInputHandler.Click ())
		{
			GameObject clickedObject = Clicked_Object ();

			Update_View (clickedObject);
		}
	}

	public void Toggle_Menu ()
	{
		if (!menu.activeSelf)
		{
			menu.SetActive (true);
			background.SetActive (true);
		}
		else
		{
			menu.SetActive (false);
			background.SetActive (false);
		}
	}

	public void Quit ()
	{
		StartCoroutine ("Quit_Sequence");
	}

	private IEnumerator Quit_Sequence ()
	{
		sceneHider.gameObject.SetActive (true);

		while (sceneHider.color.a < 1f)
		{
			cameraZoom.Zoom_Away ();

			Color hiderCol = sceneHider.color;
			hiderCol.a += Time.deltaTime;
			sceneHider.color = hiderCol;

			yield return null;
		}

		Application.Quit ();
	}

	private GameObject Clicked_Object ()
	{
		Vector3 mousePos = playerInputHandler.ClickPosition ();
		mousePos.z = -5f;

		RaycastHit2D hit = Physics2D.Raycast (mousePos, new Vector3 (0, 0, 1), 10f, LayerMask.GetMask ("Selectable"));

		if (debug) Debug.DrawRay (mousePos, new Vector3 (0, 0, 10), Color.red);

		if (hit)
		{
			if (debug) print (hit.transform.name);

			return hit.transform.gameObject;
		}

		return null;
	}

	private void Update_View (GameObject clickObj)
	{
		switch (cameraZoom.zoomMode)
		{
			case ZoomMode.World:

				if (clickObj == null) return;

				if (clickObj.CompareTag ("Island") || clickObj.CompareTag ("House"))
				{
					camTransform.position = clickObj.transform.position + new Vector3 (0, 0, camTransform.position.z);
					cameraZoom.ZoomTo_Island ();
				}

				break;

			case ZoomMode.Island:

				if (clickObj == null)
				{
					camTransform.position = new Vector3 (0, 0, camTransform.position.z);
					cameraZoom.ZoomTo_World ();
				}
				else if (clickObj.CompareTag ("House"))
				{
					camTransform.position = clickObj.transform.position + new Vector3 (0, 0, camTransform.position.z);
					cameraZoom.ZoomTo_House ();
				}

				break;

			case ZoomMode.House:

				if (clickObj == null)
				{
					camTransform.position = new Vector3 (0, 0, camTransform.position.z);
					cameraZoom.ZoomTo_Island ();
				}
				else if (clickObj.CompareTag ("House"))
				{
					camTransform.position = clickObj.transform.position + new Vector3 (0, 0, camTransform.position.z);
				}
				else if (clickObj.CompareTag ("Island"))
				{
					camTransform.position = clickObj.transform.position + new Vector3 (0, 0, camTransform.position.z);
					cameraZoom.ZoomTo_Island ();
				}

				break;
		}
	}
}
