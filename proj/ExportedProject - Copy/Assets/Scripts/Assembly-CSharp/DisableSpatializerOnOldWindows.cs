using System;
using UnityEngine;

public class DisableSpatializerOnOldWindows : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	private void Awake()
	{
		if (SystemInfo.operatingSystem.IndexOf("Windows 10", StringComparison.OrdinalIgnoreCase) < 0)
		{
			_audioSource.spatialize = false;
		}
	}
}
