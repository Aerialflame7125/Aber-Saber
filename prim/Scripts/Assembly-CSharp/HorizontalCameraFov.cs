using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HorizontalCameraFov : MonoBehaviour
{
	public float _horizontalFOV;

	private void Awake()
	{
		GetComponent<Camera>().fieldOfView = 57.29578f * (2f * Mathf.Atan(Mathf.Tan(_horizontalFOV * ((float)Math.PI / 180f) * 0.5f) / GetComponent<Camera>().aspect));
	}
}
