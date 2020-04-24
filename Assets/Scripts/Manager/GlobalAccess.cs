using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAccess : Singleton<GlobalAccess>
{
	#region All Access References
	[Header("Camera")]
	public GameObject cameraGroup;
	public CameraTransition cameraTransition;

	[Header("old Level Manager access")]
	public TeaDrink drinkModel;
	public CuttingJellyMachine cuttingJellyMachine;
	public GameObject bigJelly;
	public MouseSlice sliceManager;
	public WaterFallMachine waterFallMachine;
	public CGController waterFallMachineController;
	public GameObject bangChuyen;
	public GameObject nextJuiceButton;
	public GameObject completeUIText;

	#endregion

	private void Start()
	{
		switch (SceneMaster.Instance.GetCurrentScene()) {
			case SceneMaster.Scene.TeaDrink:
				LevelManager.Instance.OpenLevel(0);
				break;
		}
	}

	public void StartGame()
	{
		SceneMaster.Instance.LoadNextScene();
	}
}
