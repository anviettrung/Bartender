using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Set", menuName = "Recipe/Set", order = 0)]
public class RecipeSet : ScriptableObject
{
	public List<Recipe> recipes;
}
