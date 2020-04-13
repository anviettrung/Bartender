using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Requirement", menuName = "Liquid Fill Requirement", order = 1)]
public class SOFillRequirement : ScriptableObject
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
