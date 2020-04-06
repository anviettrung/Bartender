using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderQueueSetter : MonoBehaviour
{
	public int expectedRenderQueue = 2002;

	// Start is called before the first frame update
	void Start()
	{
		// get all renderers in this object and its children:
		Renderer[] renders = GetComponentsInChildren<Renderer>();
		foreach (Renderer rendr in renders) {
			rendr.material.renderQueue = expectedRenderQueue; // set their renderQueue
		}
	}

  	public static void Set(GameObject o, int rendQueue)
	{
		// get all renderers in this object and its children:
		Renderer[] renders = o.GetComponentsInChildren<Renderer>();
		foreach (Renderer rendr in renders) {
			rendr.material.renderQueue = rendQueue; // set their renderQueue
		}
	}
}
