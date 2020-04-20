using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Recipe : ScriptableObject
{
	public enum DrinkType
	{
		None,
		Tea,
		Cocktail,
		Milkshake,
		Custom
	}
	public abstract DrinkType GetDrinkType();

	[System.Serializable]
	public class FillRequirement
	{
		[System.Serializable]
		public struct FillComponent
		{
			public SOLiquid soLiquid;
			public int amountRequire;
		}

		public List<FillComponent> components;

		public int GetTotalAmount()
		{
			int res = 0;
			for (int i = 0; i < components.Count; i++)
				res += components[i].amountRequire;

			return res;
		}

		public float GetComponentPercentage(int x)
		{
			return (float)components[x].amountRequire / GetTotalAmount();
		}

	}
	[Header("Main drink")]
	public Recipe.FillRequirement fillRequirement;

}

