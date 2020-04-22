using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : Singleton<SceneMaster>
{
	public Animator transitor;
	public float transitionTime;

	public enum Scene
	{
		Preload,
		Menu,
		TeaDrink
	}

	public void LoadNextScene()
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadScene(Scene sceneID)
	{
		LoadScene((int)sceneID);
	}

	public void LoadScene(int x)
	{
		// Play transition
		transitor.SetTrigger("Start");

		StartCoroutine(CoroutineUtils.Chain(
			CoroutineUtils.WaitForSecondsRealtime(transitionTime),
			CoroutineUtils.Do(() => SceneManager.LoadScene(x))
		));
	}
}
