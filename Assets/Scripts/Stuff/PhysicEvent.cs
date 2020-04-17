using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicEvent : MonoBehaviour
{
	public BM.EventGameObject onTriggerEnter = new BM.EventGameObject();
	public BM.EventGameObject onTriggerExit = new BM.EventGameObject();

	public BM.EventGameObject onCollisionEnter = new BM.EventGameObject();
	public BM.EventGameObject onCollisionExit = new BM.EventGameObject();


	private void OnTriggerEnter(Collider other)
	{
		onTriggerEnter.Invoke(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		onTriggerExit.Invoke(other.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		onCollisionEnter.Invoke(collision.gameObject);
	}

	private void OnCollisionExit(Collision collision)
	{
		onCollisionExit.Invoke(collision.gameObject);
	}
}
