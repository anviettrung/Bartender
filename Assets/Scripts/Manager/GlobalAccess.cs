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

	[Header("UI")]
	public GameObject nextJuiceButton;
	public GameObject waterFallUIButtonGroup;
	public GameObject completeUIText;
	public GameObject shakeGuide;
	public GameObject shakeTriggerButton;

	[Header("Other")]
	public ShakeDetection shakeDetection;
	public Animator textEffectAnimator;

	#endregion

	private void Start()
	{
		switch (SceneMaster.Instance.GetCurrentScene()) {
			case SceneMaster.Scene.TeaDrink:
				LevelManager.Instance.OpenLevel(0);
				break;
			case SceneMaster.Scene.Cocktail:
				LevelManager.Instance.OpenLevel(1);
				break;
		}
	}

	public void StartGame()
	{
		SceneMaster.Instance.LoadNextScene();
	}

	#region Drink model
	[Header("Drink model")]
	public TeaDrink teaDrinkModel;
	public Drink cocktailDrinkModel;

	public Drink GetModel(Recipe.DrinkType type)
	{
		switch (type) {
			case Recipe.DrinkType.Tea:
				return teaDrinkModel;
			case Recipe.DrinkType.Cocktail:
				return cocktailDrinkModel;
			default:
				return null;
		}
	}

	#endregion
}
