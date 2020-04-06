using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage<T1, T2>: MonoBehaviour where T1: StageData where T2: StageData
{
	[Header("Base streaming Data")]
	public T1 inputData;
	public T2 outputData;

	public void Start()
	{
		outputData.Copy(inputData);
	}
}
