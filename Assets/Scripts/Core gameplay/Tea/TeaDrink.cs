using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeaDrink : MonoBehaviour
{
	#region Properties
	[Header("General setting")]
	public string drinkName;
	public int renderQueueMax = 2020;

	public int maxAmount;

	[Header("Test")]
	public SOLiquid soLiquidToSpawn;
	public int incAmountPerDrop;
	public SOFillRequirement requirement;

	[Header("Working variable")]
	public int currentAmount = 0;


	[Header("Cup setting")]
	public CGLiquid liquidModel;
	public float liquidLowestFA;
	public float liquidHighestFA;

	public DropReceiver dropReceiverBox;
	public float dropReceiverBoxLowest;
	public float dropReceiverBoxHighest;

	[Header("Liquid")]
	public List<CGLiquid> liquidFragment;
	public CGLiquid lastLiquidFragment {
		get {
			return (liquidFragment.Count > 0) ? liquidFragment[liquidFragment.Count-1] : null;
		}
	}

	[Header("Events")]
	public BM.EventDrink onFillChange = new BM.EventDrink();

	#endregion

	#region Unity Callback
	public void Start()
	{
		dropReceiverBox.onDropEnter.AddListener(OnDropEnterLiquid);
		onFillChange.AddListener((TeaDrink drink) => {
			if (IsFillFull()) {
				GetMatchPercentageOfRequirement();
			}
		});
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.G)) {
			CreateLiquidFragment(soLiquidToSpawn);
		} else if (Input.GetKeyDown(KeyCode.H)) {
			IncreaseLiquidAmount(incAmountPerDrop);
		} else if (Input.GetKeyDown(KeyCode.J)) {
			for (int i = 0; i < liquidFragment.Count-1; i++)
				liquidFragment[i].LerpToLiquid(lastLiquidFragment, 1);
		}

	}
	#endregion

	#region Initialization

	#endregion

	#region Check Requirements
	public bool IsFillFull()
	{
		return currentAmount >= maxAmount;
	}

	public float GetMatchPercentageOfRequirement()
	{
		float matchPercentage = 1;
		for (int i = 0; i < requirement.components.Count; i++) {
			float fillPercent = 0;

			for (int j = 0; j < liquidFragment.Count; j++) {
				if (BM.Utility.CompareColor(liquidFragment[j].MainColor, requirement.components[i].soLiquid.mainColor)) {
					fillPercent = (float)liquidFragment[j].FillAmountInt / maxAmount;
					break;
				}
			}

			if (fillPercent < requirement.GetComponentPercentage(i))
				matchPercentage *= fillPercent / requirement.GetComponentPercentage(i);
			else
				matchPercentage *= requirement.GetComponentPercentage(i) / fillPercent;
		}

		Debug.Log(matchPercentage);
		return matchPercentage;
	}

	#endregion

	#region Liquid Handler
	public void CreateLiquidFragment(SOLiquid dataModel)
	{
		CGLiquid clone = Instantiate(liquidModel).GetComponent<CGLiquid>();

		clone.Init(dataModel);
		clone.SetDrink(this);
		clone.FillAmountFloat = AmountI2F(currentAmount);
		clone.FillAmountInt = 0;

		RenderQueueSetter.Set(clone.gameObject, renderQueueMax - liquidFragment.Count);
		liquidFragment.Add(clone);

		clone.transform.SetParent(this.transform);
		clone.gameObject.SetActive(true);
	}

	public void OnDropEnterLiquid(LiquidDrop drop)
	{
		if (lastLiquidFragment == null || drop.data != lastLiquidFragment.data) {
			CreateLiquidFragment(drop.data);
			IncreaseLiquidAmount(incAmountPerDrop);
		} else {
			IncreaseLiquidAmount(incAmountPerDrop);
		}

		ObjectPool.Instance.PushBackToPool(drop.gameObject);
	}

	public void IncreaseLiquidAmount(int amount)
	{
		int delta = Mathf.Clamp(currentAmount + amount, 0, maxAmount) - currentAmount;
		lastLiquidFragment.FillAmountInt += delta;
		currentAmount = delta + currentAmount;
		lastLiquidFragment.FillAmountFloat = AmountI2F(currentAmount);

		if (delta > 0) {
			Color lastLiquidTopColor = lastLiquidFragment.TopColor;
			for (int i = 0; i < liquidFragment.Count - 1; i++)
				liquidFragment[i].TopColor = lastLiquidTopColor;
		}

		onFillChange.Invoke(this);
		UpdateDropReceiver();
	}

	public void UpdateDropReceiver()
	{
		dropReceiverBox.transform.position = new Vector3(
			dropReceiverBox.transform.position.x,
			Mathf.Lerp(dropReceiverBoxLowest, dropReceiverBoxHighest, (float)currentAmount / maxAmount),
			dropReceiverBox.transform.position.z);
	}

	public float AmountI2F(int amount) // convert Amount Int to Float
	{
		return Mathf.Lerp(liquidLowestFA, liquidHighestFA, (float)amount / maxAmount);
	}

	#endregion

	#region Food Decoration Handler
	#endregion

	#region Shake Handler
	#endregion
}
