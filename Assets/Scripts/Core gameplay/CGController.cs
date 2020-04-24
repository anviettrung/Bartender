using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CGController : MonoBehaviour
{
	[Header("Model")]
	public WaterFallMachine waterFallMachineModel;

	[Header("Setting")]
	public bool play;
	public float cooldownTime;
	public bool readyWater = true;
	public bool pressingPourButton = false;


	private void Update()
	{
		//if (Input.GetMouseButton(0) && !IsPointerOverUIObject()) {
		//	Pour();
		//}
		if (pressingPourButton)
			Pour();
	}

	public void PressPourButton (bool s)
	{
		pressingPourButton = s;
	}

	public void Pour()
	{
		if (readyWater) {
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
