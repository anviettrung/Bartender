using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraTransition))]
public class CameraTransitionEditor : Editor
{
	CameraTransition mTarget;

	private void Awake()
	{
		mTarget = (CameraTransition)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Save current view")) {
			mTarget.SaveCurrentView();
		}
	}
}
