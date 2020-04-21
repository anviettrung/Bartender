using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CGLiquid))]
public class CGLiquidEditor : Editor
{
	CGLiquid mTarget;

	private void Awake()
	{
		mTarget = (CGLiquid)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Load Data")) {
			mTarget.Init();
		}

		if (GUILayout.Button("Save data")) {
			mTarget.data.mainColor = mTarget.rend.sharedMaterial.GetColor("_Tint");
			mTarget.data.topColor = mTarget.rend.sharedMaterial.GetColor("_TopColor");
			mTarget.data.rimColor = mTarget.rend.sharedMaterial.GetColor("_RimColor");
		}
	}
}
