using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Liquid Fill Data", menuName = "Base Cup Data/Liquid Fill", order = 0)]
public class SOLiquidFill : ScriptableObject
{
	[System.Serializable]
	public struct LiquidFillFragment
	{
		public SOLiquid soLiquid;
		public int fillAmount;
	}

	public int fragmentCap; // Max number of different liquid color
	public List<LiquidFillFragment> fragments;
	public int maxFillAmount; // fill amount to reach 100%
	public int fillAmountCap; // limit fill amount. Liquids can't go over this limit (always smaller than maxFillAmount)
}
