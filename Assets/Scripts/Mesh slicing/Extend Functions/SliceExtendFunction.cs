using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceExtendFunction : MonoBehaviour
{
	public string slicedFragmentTag = "Untagged";

	public void RemoveSliceableTag(GameObject obj)
	{
		obj.tag = slicedFragmentTag; 
	}

	public void AddRigidbody(GameObject obj)
	{
		if (obj.GetComponent<Rigidbody>() == null)
			obj.AddComponent<Rigidbody>();
	}

	public void AddMeshCollider(GameObject obj)
	{
		Collider c = obj.GetComponent<Collider>();
		if (c != null) {
			Destroy(c);
			obj.AddComponent<MeshCollider>().convex = true;

		}
	}

	public void DisableTriggerCollider(GameObject obj)
	{
		Collider c = obj.GetComponent<Collider>();
		if (c != null) {
			c.isTrigger = false;
		}
	}

	public void TurnOnGravity(GameObject obj)
	{
		Rigidbody rb = obj.GetComponent<Rigidbody>();
		if (rb != null) {
			rb.useGravity = true;
		}
	}

}
