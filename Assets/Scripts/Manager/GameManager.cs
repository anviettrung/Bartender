using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

	#region Scene Manager
	// Scene Manager
	public enum SceneID {
		TeaDrink
	}

	public void ResetLevel()
	{
		SceneManager.LoadScene((int)SceneID.TeaDrink);
	}

	#endregion
}
