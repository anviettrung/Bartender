using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingJellyMachine : MonoBehaviour
{
	#region Properties
	public GameObject jellyUnitModel;
	public float jellyUnitVolume;
	public float volumeScale;
	public float randomZrange;
	#endregion

	#region Event Function
	public void DestroyJellyFragment(GameObject other)
	{
		 if (other.tag == "JellyFragment") {
			MeshFilter meshFilter = other.GetComponent<MeshFilter>();
			Vector3 meshBox = meshFilter.sharedMesh.bounds.size;
			float volume = meshBox.x * meshBox.y * meshBox.z * volumeScale;

			GenerateJellyUnit((int)(volume / jellyUnitVolume));

			Destroy(other.gameObject);
		}
	}

	public void GenerateJellyUnit(int n)
	{
		for (int i = 0; i < n; i++) {
			GameObject cloneUnit = Instantiate(jellyUnitModel, jellyUnitModel.transform.parent);
			cloneUnit.transform.Translate(0, 0, Random.Range(-randomZrange, randomZrange));
			cloneUnit.transform.rotation = Random.rotation;
			cloneUnit.SetActive(true);
		}
	}

	#endregion
}
