using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
	public Vector3 rotateSpeed;
	public bool rotating = true;

    // Update is called once per frame
    void Update()
    {
		if (rotating)
			transform.Rotate(rotateSpeed * Time.deltaTime);
    }
}
