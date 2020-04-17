using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		} else if (Input.GetMouseButton(0) && readyWater && !IsPointerOverUIObject()) {
			readyWater = false;
			waterFallMachineModel.SpawnDrop();
			Invoke("Release", cooldownTime);
		}
	}

	void Release()
	{
		readyWater = true;
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
