using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGLiquid : MonoBehaviour
{
	public Color MainColor {
		get {
			return rend.material.GetColor("_Tint");
		}
		set {
			rend.material.SetColor("_Tint", value);
		}
	}
	public Color TopColor {
		get {
			return rend.material.GetColor("_TopColor");
		}
		set {
			rend.material.SetColor("_TopColor", value);
		}
	}
	public Color RimColor {
		get {
			return rend.material.GetColor("_RimColor");
		}
		set {
			rend.material.SetColor("_RimColor", value);
		}
	}
	public float FillAmountFloat {
		get {
			return rend.material.GetFloat("_FillAmount");
		}
		set {
			rend.material.SetFloat("_FillAmount", value);
		}
	}
	public int FillAmountInt;
	//public Texture2D mainTexture;
	public Renderer rend;


	public void Init(SOLiquid data)
	{
		MainColor = data.mainColor;
		TopColor = data.topColor;
		RimColor = data.rimColor;
	}

	#region Realtime update
#if UNITY_EDITOR
	[Header("Realtime test")]
	public bool realtimeUpdate;
	public Color mainColor;
	public Color topColor;
	public Color rimColor;
	public float fillAmount;

	protected void Update()
	{
		if (realtimeUpdate) {
			MainColor = mainColor;
			TopColor = topColor;
			RimColor = rimColor;
			FillAmountFloat = fillAmount;
		} else {
			mainColor = MainColor;
			topColor = TopColor;
			rimColor = RimColor;
			fillAmount = FillAmountFloat;
		}
	}
#endif
	#endregion
}
