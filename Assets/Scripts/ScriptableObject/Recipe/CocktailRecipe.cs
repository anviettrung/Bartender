using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cocktail Recipe", menuName = "Recipe/Cocktail Recipe")]
public class CocktailRecipe : Recipe
{
	#region Properties
	[Header("Fruit")]
	public int fruit;

	[Header("Shake")]
	public int shakePoint;

	#endregion

	#region Functions
	public override DrinkType GetDrinkType()
	{
		return DrinkType.Cocktail;
	}
	#endregion
}
