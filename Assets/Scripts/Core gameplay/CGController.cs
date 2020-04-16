using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGController : MonoBehaviour
{
	[Header("Model")]
	public CGCupRenderer drinkModel;
	public WaterFallMachine waterFallMachineModel;

	[Header("Tracking")]
	public CGCupRenderer currentDrink;

	[Header("Setting")]
	public bool play;
	public float cooldownTime;
	public bool readyWater = true;


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O)) {
//			waterFallMachineModel.Spawn("Food");
		} else if (Input.GetKey(KeyCode.P) && readyWater) {
			readyWater = false;
			waterFallMachineModel.SpawnDrop();
			Invoke("Release", cooldownTime);
		}
	}

	void Release()
	{
		readyWater = true;
	}
}
