using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGLiquid : MonoBehaviour
{
	public SOLiquid data;

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

	public TeaDrink drink;

	public Renderer rend;

	public void Init(SOLiquid _data)
	{
		MainColor = _data.mainColor;
		TopColor = _data.topColor;
		RimColor = _data.rimColor;

		data = _data;
	}

	public void SetDrink(TeaDrink d)
	{
		drink = d;
	}

	/// <summary>
	/// Compares liquid and liquid
	/// </summary>
	public static bool CompareL2L(CGLiquid a, CGLiquid b)
	{
		return BM.Utility.CompareColor(a.MainColor, b.MainColor);
	}

	public void LerpToLiquid(CGLiquid target, float t)
	{
		StartCoroutine(BM.Utility.LerpColorOverTime(MainColor, target.MainColor, t, (res) => {
			MainColor = res;
		}));
		StartCoroutine(BM.Utility.LerpColorOverTime(TopColor, target.TopColor, t, (res) => {
			TopColor = res;
		}));
		StartCoroutine(BM.Utility.LerpColorOverTime(RimColor, target.RimColor, t, (res) => {
			RimColor = res;
		}));
	}

	//	#region Realtime update
	//#if UNITY_EDITOR
	//	[Header("Realtime test")]
	//	public bool realtimeUpdate;
	//	public Color mainColor;
	//	public Color topColor;
	//	public Color rimColor;
	//	public float fillAmount;

	//	protected void Update()
	//	{
	//		if (realtimeUpdate) {
	//			MainColor = mainColor;
	//			TopColor = topColor;
	//			RimColor = rimColor;
	//			FillAmountFloat = fillAmount;
	//		} else {
	//			mainColor = MainColor;
	//			topColor = TopColor;
	//			rimColor = RimColor;
	//			fillAmount = FillAmountFloat;
	//		}
	//	}
	//#endif
	//#endregion
}
