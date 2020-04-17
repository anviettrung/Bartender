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
		LiquidDrop clone = ObjectPool.Instance.GetObject("drop").GetComponent<LiquidDrop>();
		//LiquidDrop clone = Instantiate(dropModel.gameObject).GetComponent<LiquidDrop>();
		clone.data = lqData;
		dropTextureRend.material.SetColor("_Color", lqData.topColor);
		clone.transform.position = dropHolder.transform.position + Vector3.right * Random.Range(-dropSpawnDelta, dropSpawnDelta);
		clone.transform.SetParent(dropHolder);
		clone.gameObject.SetActive(true);
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
