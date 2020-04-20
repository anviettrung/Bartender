using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject cameraGroup;

	private void Start()
	{
		LevelManager.Instance.OpenLevel(0);
	}
}
