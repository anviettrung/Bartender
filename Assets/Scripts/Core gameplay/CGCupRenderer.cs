using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGCupRenderer : MonoBehaviour
{
	public SOLiquidFill soLiquidFill;

	[Header("Temp")]
	public GameObject liquidModel;
	public float liquidLowestFA;
	public float liquidHighestFA;

	public void Start()
	{
		RenderLiquidFill();
	}

	public void RenderLiquidFill()
	{
		int renderQueueMax = 2000 + soLiquidFill.fragmentCap;

		int curFillAmount = 0;
		Color lastFragmentTopColor = soLiquidFill.fragments[soLiquidFill.fragments.Count-1].soLiquid.topColor;
		for (int i = 0; i < soLiquidFill.fragments.Count; i++) {

			GameObject lqClone = Instantiate(liquidModel);
			lqClone.transform.SetParent(transform);

			Material lqMat = lqClone.GetComponent<Renderer>().material;
			SOLiquid lqData = soLiquidFill.fragments[i].soLiquid;

			lqMat.SetColor("_Tint", lqData.mainColor);
			lqMat.SetColor("_TopColor", lastFragmentTopColor);
			lqMat.SetColor("_RimColor", lqData.rimColor);

			curFillAmount += soLiquidFill.fragments[i].fillAmount;
			float fillHigh = (float)curFillAmount / (float)soLiquidFill.maxFillAmount;
			fillHigh = Mathf.Lerp(liquidLowestFA, liquidHighestFA, fillHigh);
			lqMat.SetFloat("_FillAmount", fillHigh);

			RenderQueueSetter.Set(lqClone, renderQueueMax - i);

			lqClone.SetActive(true);

		}
	}
}
