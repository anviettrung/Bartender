﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
	[System.Serializable]
	public class View
	{
		public string viewName;
		public Vector3 position;
		public Vector3 rotation;
		public Vector3 scale;
	}

	public List<View> camViews = new List<View>();

	public void FocusView(string name, float transitionTime)
	{
		View focusV = GetView(name);
		if (focusV != null) {

			View startV = GetCurrentViewSetting();

			StartCoroutine(CoroutineUtils.LinearAction(transitionTime, (weight) => {
				gameObject.transform.position = Vector3.Lerp(startV.position, focusV.position, weight);
				gameObject.transform.eulerAngles = AngleLerp(startV.rotation, focusV.rotation, weight);
				gameObject.transform.localScale = Vector3.Lerp(startV.scale, focusV.scale, weight);
			}));

		}
	}

	public View GetView(string name)
	{
		for (int i = 0; i < camViews.Count; i++)
			if (camViews[i].viewName == name)
				return camViews[i];

		return null;
	}

	public View GetCurrentViewSetting()
	{
		View newView = new View();
		newView.position = transform.position;
		newView.rotation = transform.eulerAngles;
		newView.scale = transform.localScale;

		return newView;
	}

	public void SaveCurrentView()
	{
		camViews.Add(GetCurrentViewSetting());
	}

	Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
	{
		float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
		float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
		float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
		Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
		return Lerped;
	}
}