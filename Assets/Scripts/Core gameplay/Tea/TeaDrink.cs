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
			return liquidFragment[liquidFragment.Count-1];
		}
	}

	[Header("Events")]
	public BM.EventDrink onFillChange = new BM.EventDrink();

	#endregion

	#region Unity Callback
	public void Start()
	{
		dropReceiverBox.onDropEnter.AddListener(OnDropEnterLiquid);
		//onFillChange.AddListener();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.G)) {
			CreateLiquidFragment(soLiquidToSpawn);
		} else if (Input.GetKeyDown(KeyCode.H)) {
			IncreaseLiquidAmount(incAmountPerDrop);
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

	#endregion

	#region Liquid Handler
	public void CreateLiquidFragment(SOLiquid dataModel)
	{
		CGLiquid clone = Instantiate(liquidModel).GetComponent<CGLiquid>();

		clone.Init(dataModel);
		clone.SetDrink(this);
		clone.FillAmountFloat = AmountI2F(currentAmount);

		RenderQueueSetter.Set(clone.gameObject, renderQueueMax - liquidFragment.Count);
		liquidFragment.Add(clone);

		clone.transform.SetParent(this.transform);
		clone.gameObject.SetActive(true);
	}

	public void OnDropEnterLiquid()
	{
		IncreaseLiquidAmount(incAmountPerDrop);
	}

	public void IncreaseLiquidAmount(int amount)
	{
		currentAmount += amount;
		lastLiquidFragment.FillAmountFloat = AmountI2F(currentAmount);

		if (amount > 0) {
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
