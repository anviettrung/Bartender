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
	public Transform forcePoint;
	public float forceAmp;

	public List<Vector3> spawnPosition;
	public float timePerGenUnit;

	private int genStack = 0;
	private bool generating = false;
	#endregion

	#region Event Function
	public void Update()
	{
		if (genStack > 0 && !generating) {
			StartCoroutine(GenerateJellyUnit(genStack, timePerGenUnit));
			genStack = 0;
		}
	}


	public void DestroyJellyFragment(GameObject other)
	{
		 if (other.tag == "JellyFragment") {
			MeshFilter meshFilter = other.GetComponent<MeshFilter>();
			Vector3 meshBox = meshFilter.sharedMesh.bounds.size;
			float volume = meshBox.x * meshBox.y * meshBox.z * volumeScale;

			//GenerateJellyUnit((int)(volume / jellyUnitVolume));

			genStack += (int)(volume / jellyUnitVolume);

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

	public IEnumerator GenerateJellyUnit(int n, float timePerUnit)
	{
		int count = 0;
		while (count < n) {
			GameObject cloneUnit = Instantiate(jellyUnitModel, jellyUnitModel.transform.parent);
			cloneUnit.transform.Translate(spawnPosition[count % spawnPosition.Count]);
			cloneUnit.transform.rotation = Random.rotation;
			cloneUnit.SetActive(true);

			yield return new WaitForSeconds(timePerUnit);
			count++;
		}
	}

	public void ForceAway(GameObject obj)
	{
		Vector3 direct = (obj.transform.position - forcePoint.position + Vector3.up).normalized;
		//obj.GetComponent<Rigidbody>().AddExplosionForce(forceAmp, forcePoint.position, 3);
		obj.GetComponent<Rigidbody>().AddForceAtPosition(direct * forceAmp, forcePoint.position);
	}

	#endregion
}
