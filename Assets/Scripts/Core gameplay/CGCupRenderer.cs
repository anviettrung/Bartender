using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGCupRenderer : MonoBehaviour
{
	public SOLiquidFill soLiquidFill;
	public bool realtimeUpdate;

	[Header("Temp")]
	public DropReceiver dropReceiverBox;
	public int incAmountPerDrop;

	public GameObject liquidModel;
	public float liquidLowestFA;
	public float liquidHighestFA;

	public float dropReceiverBoxLowest;
	public float dropReceiverBoxHighest;

	protected List<CGLiquid> liquidFragments;

	protected int curFillAmount = 0;

	public void Start()
	{
		dropReceiverBox.onDropEnter.AddListener((drop) => {
			CGLiquid lastLiquid = liquidFragments[liquidFragments.Count - 1];
			lastLiquid.FillAmountInt += incAmountPerDrop;

			curFillAmount += incAmountPerDrop;
			float fillPercentage = (float)curFillAmount / (float)soLiquidFill.maxFillAmount;
			float fillHigh = Mathf.Lerp(liquidLowestFA, liquidHighestFA, fillPercentage);
			lastLiquid.FillAmountFloat = fillHigh;
		});
		InitRenderLiquidFill(soLiquidFill);
	}

	private void Update()
	{

	}

	protected void ClearFragments()
	{
		if (liquidFragments != null)
			for (int i = 0; i < liquidFragments.Count; i++)
				Destroy(liquidFragments[i].gameObject);
	}

	public void InitRenderLiquidFill(SOLiquidFill data)
	{
		ClearFragments();
		liquidFragments = new List<CGLiquid>();

		int renderQueueMax = 2000 + data.fragmentCap;
		curFillAmount = 0;
		Color lastFragmentTopColor = data.fragments[data.fragments.Count-1].soLiquid.topColor;
		for (int i = 0; i < data.fragments.Count; i++) {

			CGLiquid lqClone = Instantiate(liquidModel).GetComponent<CGLiquid>();
			lqClone.transform.SetParent(transform);

			lqClone.Init(data.fragments[i].soLiquid);
			lqClone.TopColor = lastFragmentTopColor;
			lqClone.FillAmountInt = data.fragments[i].fillAmount;

			curFillAmount += data.fragments[i].fillAmount;
			float fillPercentage = (float)curFillAmount / (float)data.maxFillAmount;
			float fillHigh = Mathf.Lerp(liquidLowestFA, liquidHighestFA, fillPercentage);
			lqClone.FillAmountFloat = fillHigh;

			dropReceiverBox.transform.position = new Vector3(
				dropReceiverBox.transform.position.x,
				Mathf.Lerp(dropReceiverBoxLowest, dropReceiverBoxHighest, fillPercentage),
				dropReceiverBox.transform.position.z
			);

			RenderQueueSetter.Set(lqClone.gameObject, renderQueueMax - i);

			liquidFragments.Add(lqClone);

			lqClone.gameObject.SetActive(true);

		}
	}
}
