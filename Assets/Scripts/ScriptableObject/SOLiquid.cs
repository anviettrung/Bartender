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
}
