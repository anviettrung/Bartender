using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallMachine : MonoBehaviour
{
	public GameObject dropModel;
	public GameObject foodModel;

	public float dropSpawnDelta = 0.1f;
	public Transform dropHolder;

	public void Spawn(string modelTypeName)
	{
		switch (modelTypeName) {
			case "Drop":
				GameObject clone1 = Instantiate(dropModel);
				clone1.transform.position = dropHolder.transform.position + Vector3.right * Random.Range(-dropSpawnDelta, dropSpawnDelta);
				clone1.transform.SetParent(dropHolder);
				clone1.SetActive(true);
				break;
			case "Food":
				GameObject clone2 = Instantiate(foodModel);
				clone2.SetActive(true);
				break;
		}
	}
}
