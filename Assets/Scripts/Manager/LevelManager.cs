using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	#region Properties
	[Header("General")]
	public RecipeSet recipeBasicSet;
	[SerializeField] private List<bool> isCompleteLevel;

	[Header("Opening-level infomation")]
	private Drink curDrink;
	public Drink CurDrink {
		get {
			return curDrink;
		}
		set {
			if (curDrink != null)
				Destroy(curDrink.gameObject);
			curDrink = value;
		}
	}

	#endregion

	#region Open Level
	public bool OpenLevel(int x)
	{
		// Generate empty drink
		Drink model = GlobalAccess.Instance.GetModel(recipeBasicSet.recipes[x].GetDrinkType());
		if (model == null) {
			Debug.LogWarning("Can't find drink model\nInstantiate failed");
			return false;
		}
		CurDrink = Instantiate(model);

		// Setting for new drink instance
		CurDrink.base_recipe = recipeBasicSet.recipes[x];

		// Enable / trigger in
		CurDrink.gameObject.SetActive(true);

		return true; // Open successfully
	}
	#endregion
}
