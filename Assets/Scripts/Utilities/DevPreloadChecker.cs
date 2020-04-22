using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreloadChecker : MonoBehaviour
{
	public int sceneToPlayIndex = 1; // auto play second scene

#if UNITY_EDITOR
	private void Awake()
	{
		DevPreload devPreloadObject = Object.FindObjectOfType<DevPreload>();

		if (devPreloadObject != null) {
			sceneToPlayIndex = devPreloadObject.currentSceneIndex;
			Destroy(devPreloadObject.gameObject);
		}
	}
#endif

	private void Start()
	{
		SceneManager.LoadScene(sceneToPlayIndex);
	}

}
