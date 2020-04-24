using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Liquid Data", menuName = "Base Cup Data/Liquid", order = 1)]
public class SOLiquid : ScriptableObject
{
	public Color mainColor;
	public Color topColor;
	public Color rimColor;
	public Texture2D mainTexture;

	public static SOLiquid Combine(List<CGLiquid> liquids, int total)
	{
		SOLiquid result = CreateInstance<SOLiquid>();

		for (int i = 0; i < liquids.Count; i++) {

			result.mainColor += liquids[i].data.mainColor * liquids[i].FillAmountInt;
			result.topColor += liquids[i].data.topColor * liquids[i].FillAmountInt;
			result.rimColor += liquids[i].data.rimColor * liquids[i].FillAmountInt;

		}

		result.mainColor /= total;
		result.topColor /= total;
		result.rimColor /= total;

		return result;
	}
}
