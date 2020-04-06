using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaleBaseOnCameraSize : MonoBehaviour
{
	public Camera observeCamera;
	public float scaleMulti = 2;
	public bool updateRealtime = false;

	private void OnEnable()
	{
		transform.localScale = new Vector3(1, 1, 1) * scaleMulti * observeCamera.orthographicSize;
	}

	private void Update()
	{
		if (updateRealtime)
			transform.localScale = new Vector3(1, 1, 1) * scaleMulti * observeCamera.orthographicSize;
	}
}
