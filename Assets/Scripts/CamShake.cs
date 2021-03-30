using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamShake : MonoBehaviour
{
	public Transform camTransform;

	[SerializeField]
	float shakeDuration, shakeAmount, decreaseFactor;


	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
			camTransform = GetComponent(typeof(Transform)) as Transform;		
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else {
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
	public void Shake()
    {
		Debug.Log("Shake");
		shakeDuration = .25f;
    }
}

