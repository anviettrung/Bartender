using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
	[System.Serializable]
	public class Item // ObjectPool.Item
	{
		public ObjectPoolTag model; // object to pool
		public string tagID;
		public int amount;       // pool amount
		public bool hideInHierachy;
		public bool shoulExpand;
		public List<ObjectPoolTag> pooledObjects;

		public void InitPool()
		{
			pooledObjects = new List<ObjectPoolTag>(amount);
			for (int i = 0; i < amount; i++) {
				ObjectPoolTag obj = Instantiate(model.gameObject).GetComponent<ObjectPoolTag>();
				obj.pool = this;
				if (hideInHierachy) obj.gameObject.hideFlags = HideFlags.HideInHierarchy;
				obj.transform.SetParent(ObjectPool.Instance.transform);
				obj.gameObject.SetActive(false);
				pooledObjects.Add(obj);
			}
		}

		public GameObject GetObject()
		{
			for (int i = 0; i < pooledObjects.Count; i++) {
				if (!pooledObjects[i].gameObject.activeInHierarchy) {
					//if (hideInHierachy)
						//pooledObjects[i].hideFlags = HideFlags.None;
					return pooledObjects[i].gameObject;
				}
			}
			return null;
		}
	}

	public List<ObjectPool.Item> items;

	protected void Start()
	{
		for (int i = 0; i < items.Count; i++) {
			items[i].InitPool();
		}
	}

	public GameObject GetObject(string tagID)
	{
		for (int i = 0; i < items.Count; i++) {
			if (items[i].tagID == tagID)
				return items[i].GetObject();
		}

		return null;
	}

	public void PushBackToPool(GameObject obj)
	{
		ObjectPoolTag objTag = obj.GetComponent<ObjectPoolTag>();
		if (objTag != null) {
			obj.gameObject.SetActive(false);
			//if (objTag.pool.hideInHierachy)
				//obj.hideFlags = HideFlags.HideInHierarchy;
		} else {
			Debug.Log("No Pool found");
		}
	}
}
