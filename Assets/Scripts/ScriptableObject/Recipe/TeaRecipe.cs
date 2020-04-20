using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tea Recipe", menuName = "Recipe/Tea Recipe", order = 0)]
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
