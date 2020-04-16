using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallMachine : MonoBehaviour
{
	public LiquidDrop dropModel;

	public Renderer dropTextureRend;
	public Transform dropHolder;

	public float dropSpawnDelta = 0.1f;

	public SOLiquid curLiquidData;

	public void SpawnDrop(SOLiquid lqData)
	{
		LiquidDrop clone1 = Instantiate(dropModel.gameObject).GetComponent<LiquidDrop>();
		clone1.data = lqData;
		dropTextureRend.material.SetColor("_Color", lqData.topColor);
		clone1.transform.position = dropHolder.transform.position + Vector3.right * Random.Range(-dropSpawnDelta, dropSpawnDelta);
		clone1.transform.SetParent(dropHolder);
		clone1.gameObject.SetActive(true);
	}

	public void SpawnDrop()
	{
		SpawnDrop(curLiquidData);
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
