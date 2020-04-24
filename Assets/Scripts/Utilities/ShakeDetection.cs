using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeDetection : MonoBehaviour
{
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 2.0f;

	float lowPassFilterFactor;
	Vector3 lowPassValue;

	Vector3 deltaAcceleration = Vector3.zero;

	void Start()
	{
	    lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
	    shakeDetectionThreshold *= shakeDetectionThreshold;
	    lowPassValue = Input.acceleration;
	}

	void Update()
	{
	    Vector3 acceleration = Input.acceleration;
	    lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
	    deltaAcceleration = acceleration - lowPassValue;
	}

	public bool IsShaking()
	{
#if UNITY_EDITOR
		return isShaking;
#endif
		return deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold;
	}

#if UNITY_EDITOR
	bool isShaking = false;

	public void SetShakingStatus(bool s)
	{
		isShaking = s;
	}
#endif
}
