using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreloadChecker : MonoBehaviour
{
#if UNITY_EDITOR
	public int sceneToPlayIndex = 1; // auto play second scene

	private void Awake()
	{
		DevPreload devPreloadObject = Object.FindObjectOfType<DevPreload>();

		if (devPreloadObject != null) {
			sceneToPlayIndex = devPreloadObject.currentSceneIndex;
			Destroy(devPreloadObject.gameObject);
		}
	}

	private void Start()
	{
		SceneManager.LoadScene(sceneToPlayIndex);
	}

#endif
}
