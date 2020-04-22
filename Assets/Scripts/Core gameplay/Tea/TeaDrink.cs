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
	public TeaRecipe recipe;

	[Header("Test")]
	public SOLiquid soLiquidToSpawn;
	public int incAmountPerDrop;
	public bool isShaking = false;
	public float cameraXangle;
	public float transitionTime;

	[Header("Working variable")]
	public int currentAmount = 0;
	public int currentJellyUnit = 0;

	[Header("Cup setting")]
	public CGLiquid liquidModel;
	public float liquidLowestFA;
	public float liquidHighestFA;

	public DropReceiver dropReceiverBox;
	public float dropReceiverBoxLowest;
	public float dropReceiverBoxHighest;

	public GameObject cupLid;

	[Header("Liquid")]
	public int maxAmount;
	public List<CGLiquid> liquidFragment;
	public CGLiquid lastLiquidFragment {
		get {
			return (liquidFragment.Count > 0) ? liquidFragment[liquidFragment.Count-1] : null;
		}
	}

	[Header("Stage")]
	public Stage curStage = Stage.Initialization;
	public enum Stage
	{
		Initialization,
		CuttingJelly,
		FillUp,
		Shake,
		Complete
	}

	[Header("Events")]
	public BM.EventDrink onFillChange = new BM.EventDrink();

	#endregion

	#region Unity Callback
	public void Start()
	{
		dropReceiverBox.onDropEnter.AddListener(OnDropEnterLiquid);
		onFillChange.AddListener((TeaDrink drink) => {
			if (IsFullLiquid()) {
				GetMatchPercentageOfRequirement();
			}
		});
	}

	public void Update()
	{
		switch (curStage) {
			case Stage.Initialization:
				// add condition later;
				curStage = Stage.CuttingJelly;
				break;
			case Stage.CuttingJelly:
				if (IsFullJelly()) {
					// disable stage
					GlobalAccess.Instance.cuttingJellyMachine.gameObject.SetActive(false);
					GlobalAccess.Instance.bigJelly.SetActive(false);

					// enable next stage
					GlobalAccess.Instance.waterFallMachine.gameObject.SetActive(true);
					GlobalAccess.Instance.waterFallMachineController.enabled = true;
					GlobalAccess.Instance.nextJuiceButton.SetActive(true);

					float startAngle = GlobalAccess.Instance.cameraGroup.transform.eulerAngles.x;
					StartCoroutine(CoroutineUtils.LinearAction(transitionTime, (weight) => {
						GlobalAccess.Instance.cameraGroup.transform.eulerAngles = new Vector3(
							Mathf.Lerp(startAngle, cameraXangle, weight),
							GlobalAccess.Instance.cameraGroup.transform.eulerAngles.y,
							GlobalAccess.Instance.cameraGroup.transform.eulerAngles.z
						);
					}));

					curStage = Stage.FillUp;
				}
				break;
			case Stage.FillUp:
				if (IsFullLiquid()) {
					GlobalAccess.Instance.waterFallMachine.gameObject.SetActive(false);
					GlobalAccess.Instance.waterFallMachineController.enabled = false;
					GlobalAccess.Instance.nextJuiceButton.SetActive(false);
					GlobalAccess.Instance.bangChuyen.SetActive(false);

					curStage = Stage.Shake;
				}
				break;
			case Stage.Shake:
				if (!isShaking) {
					StartCoroutine(CoroutineUtils.Chain(
						CoroutineUtils.Do(() => {
							cupLid.gameObject.SetActive(true);
							isShaking = true;
						}),
						CoroutineUtils.WaitForSeconds(1),
						// Shake
						Shake(10, 10, 2),
						CoroutineUtils.Do(() => {
							gameObject.transform.eulerAngles = Vector3.zero;

							curStage = Stage.Complete;
						})
					));
				}
					
				break;
			case Stage.Complete:
				GlobalAccess.Instance.completeUIText.SetActive(true);
				break;
		}

	}
	#endregion

	#region Initialization

	#endregion

	#region Change Stage condition
	public bool IsFullJelly()
	{
		return currentJellyUnit >= recipe.jellyUnitRequirement;
	}

	public bool IsFullLiquid()
	{
		return currentAmount >= maxAmount;
	}

	#endregion

	#region Check Requirements

	public float GetMatchPercentageOfRequirement()
	{
		float matchPercentage = 1;
		for (int i = 0; i < recipe.fillRequirement.components.Count; i++) {
			float fillPercent = 0;

			for (int j = 0; j < liquidFragment.Count; j++) {
				if (BM.Utility.CompareColor(liquidFragment[j].MainColor, recipe.fillRequirement.components[i].soLiquid.mainColor)) {
					fillPercent = (float)liquidFragment[j].FillAmountInt / maxAmount;
					break;
				}
			}

			if (fillPercent < recipe.fillRequirement.GetComponentPercentage(i))
				matchPercentage *= fillPercent / recipe.fillRequirement.GetComponentPercentage(i);
			else
				matchPercentage *= recipe.fillRequirement.GetComponentPercentage(i) / fillPercent;
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
		if (liquidFragment.Count > 0) // ignore first fragment
			clone.FillAmountUnderFloat = AmountI2F(currentAmount);
		clone.FillAmountInt = 0;

		RenderQueueSetter.Set(clone.gameObject, 2000 + liquidFragment.Count);
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

		//if (delta > 0) {
		//	Color lastLiquidTopColor = lastLiquidFragment.TopColor;
		//	for (int i = 0; i < liquidFragment.Count - 1; i++)
		//		liquidFragment[i].TopColor = lastLiquidTopColor;
		//}

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

	public void LerpAllLiquid()
	{
		SOLiquid final = new SOLiquid();

	}

	#endregion

	#region Food Decoration Handler
	public void IncreaseJellyItem(GameObject obj)
	{
		if (obj.tag == "Jelly") {
			currentJellyUnit++;
		}
	}

	#endregion

	#region Shake Handler
	IEnumerator Shake(float amp, float speed, float time)
	{
		float elapsed = 0;
		float startTime = Time.time;
		do {
			gameObject.transform.eulerAngles = new Vector3(
				gameObject.transform.eulerAngles.x,
				gameObject.transform.eulerAngles.y,
				Mathf.Sin(speed * elapsed) * amp
			);

			yield return new WaitForEndOfFrame();
			elapsed += Time.deltaTime;
		} while (elapsed < time);
	}

	#endregion
}
