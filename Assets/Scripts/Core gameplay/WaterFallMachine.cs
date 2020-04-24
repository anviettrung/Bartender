using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallMachine : MonoBehaviour
{
	public LiquidDrop dropModel;

	public GameObject machine;

	public Renderer dropTextureRend;
	public Transform dropHolder;

	public float dropSpawnDelta = 0.1f;

	public SOLiquid curLiquidData {
		get {
			return allLiquidTypes[curLiquidTypeID];
		}
	}

	public List<SOLiquid> allLiquidTypes = new List<SOLiquid>();
	public int curLiquidTypeID = 0;

	private bool isRotating = false;
	private readonly float EPSILON;

	public void SpawnDrop(SOLiquid lqData)
	{
		LiquidDrop clone = ObjectPool.Instance.GetObject("drop").GetComponent<LiquidDrop>();
		//LiquidDrop clone = Instantiate(dropModel.gameObject).GetComponent<LiquidDrop>();
		clone.data = lqData;
		dropTextureRend.material.SetColor("_Color", lqData.topColor);
		clone.transform.position = dropHolder.transform.position + Vector3.right * Random.Range(-dropSpawnDelta, dropSpawnDelta);
		clone.transform.SetParent(dropHolder);
		clone.gameObject.SetActive(true);
	}

	public void RotateMachine(float degree)
	{
		isRotating = true;
		Vector3 start = machine.transform.eulerAngles;
		Vector3 end = start + Vector3.forward * degree;
		StartCoroutine(CoroutineUtils.Chain(
			CoroutineUtils.LinearAction(0.5f, (weight) => {
				machine.transform.eulerAngles = Vector3.Lerp(start, end, weight);
			}),
			CoroutineUtils.Do(() => isRotating = false)
		));

	}

	public void SpawnDrop()
	{
		SpawnDrop(curLiquidData);
	}

	public void NextLiquidType()
	{
		if (!isRotating) {
			curLiquidTypeID = (curLiquidTypeID + 1) % allLiquidTypes.Count;
			RotateMachine(90);
		}
	}

	public void PreviousLiquidType()
	{
		if (!isRotating) {
			if (curLiquidTypeID > 0)
				curLiquidTypeID = (curLiquidTypeID - 1) % allLiquidTypes.Count;
			else
				curLiquidTypeID = allLiquidTypes.Count - 1;
			RotateMachine(-90);
		}
	}

	//public void Spawn(string modelTypeName)
	//{
	//	switch (modelTypeName) {
	//		case "Drop":
	//			//SpawnDrop();
	//			break;
	//		case "Food":
	//			GameObject clone2 = Instantiate(foodModel);
	//			clone2.SetActive(true);
	//			break;
	//	}
	//}
}
