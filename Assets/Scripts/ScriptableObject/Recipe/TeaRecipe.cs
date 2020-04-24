using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tea Recipe", menuName = "Recipe/Tea Recipe")]
public class TeaRecipe : Recipe
{
	#region Properties
	[Header("Jelly")]
	public int jellyUnitRequirement;
	#endregion

	#region Functions
	public override DrinkType GetDrinkType()
	{
		return DrinkType.Tea;
	}

	#endregion
}
