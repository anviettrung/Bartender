using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
	[Header("Main settings")]
	public float capFill;
	public float currentFill;
	public float progress;

	[Header("Liquid")]
	public float liquidDropFill;
	public GameObject liquidPrefab;
	public Transform liquidSpawner;
	public float timePerSpawn;

	public float cooldownTime;
	public bool canDrop = true;

	[Header("Filler")]
	public Transform inviBoxTransform;
	public float inviBoxLowest;
	public float inviBoxHighest;

	public MeshRenderer meshRenderer;
	public Material filler;
	public float fillerLowest;
	public float fillerHighest;
	public float fil;

	public void Start()
	{
		filler = meshRenderer.material;
		Fill();
	}

	public void CreateDrop()
	{
		// Create
		SelfDisappear clone = Instantiate(liquidPrefab).GetComponent<SelfDisappear>();
		clone.gameObject.SetActive(true);
		clone.transform.SetParent(liquidPrefab.transform.parent);
		// Cooldown
		canDrop = false;
		Invoke("Release", cooldownTime);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && canDrop)
			CreateDrop();
	}

	public void Release()
	{
		canDrop = true;
	}

	public void Fill()
	{
		currentFill += liquidDropFill;
		progress = currentFill / capFill;

		inviBoxTransform.position = new Vector3(
			inviBoxTransform.position.x,
			Mathf.Lerp(inviBoxLowest, inviBoxHighest, progress),
			inviBoxTransform.position.z);

		filler.SetFloat("_FillAmount", Mathf.Lerp(fillerLowest, fillerHighest, progress));
	}
}
