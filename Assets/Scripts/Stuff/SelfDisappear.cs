using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisappear : MonoBehaviour
{
	public Collider2D destroyBox;
	public MasterController controller;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == destroyBox) {
			gameObject.SetActive(false);
			controller.Fill();
		}
	}
}
