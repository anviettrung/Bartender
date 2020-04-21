using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload : MonoBehaviour
{
#if UNITY_EDITOR
	public int currentSceneIndex;

	void Awake()
	{
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		GameObject check = GameObject.Find("__app");
		if (check == null) {

			// __app will call this scene after preload && destroy this DevPreload object
			DontDestroyOnLoad(gameObject);

			SceneManager.LoadScene("_preload");
		}
	}
#endif
}
