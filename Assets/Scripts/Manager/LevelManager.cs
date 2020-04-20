using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	#region Properties
	[Header("General")]
	public List<Recipe> levelRecipe;
	[SerializeField] private List<bool> isCompleteLevel;

	[Header("Opening-level infomation")]
	private TeaDrink curDrink;
	public TeaDrink CurDrink {
		get {
			return curDrink;
		}
		set {
			if (curDrink != null)
				Destroy(curDrink.gameObject);
			curDrink = value;
		}
	}

	[Header("Temp resources")]
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

	#region Open Level
	public bool OpenLevel(int x)
	{
		// Generate empty drink
		CurDrink = Instantiate(drinkModel);

		// Read level Setting
		CurDrink.recipe = (TeaRecipe)levelRecipe[x];

		// Enable / trigger in
		CurDrink.gameObject.SetActive(true);

		return true; // Open successfully
	}
	#endregion
}
