using System.Collections;
using System.Collections.Generic;
using BM;
using UnityEngine;
using UnityEngine.Events;

public class DropReceiver : MonoBehaviour
{
	public EventDrop onDropEnter = new EventDrop();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		LiquidDrop drop = collision.GetComponent<LiquidDrop>();
		if (collision.tag == "Drop" && drop != null)
			onDropEnter.Invoke(drop);
	}
}
