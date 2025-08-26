using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticFeedbackController : ScriptableObject
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private const float kContinuesRumbleImpulseStrength = 0.3f;

	private const float kContinuesRumbleIntervalTime = 0.01f;

	private Dictionary<XRNode, bool> _continuousRumble = new Dictionary<XRNode, bool>();

	private bool GetContinousRumbleForNode(XRNode node)
	{
		bool value = false;
		if (_continuousRumble.TryGetValue(node, out value))
		{
			return value;
		}
		return false;
	}

	public void ContinuousRumble(XRNode node)
	{
		if (_mainSettingsModel.controllersRumbleEnabled && !GetContinousRumbleForNode(node))
		{
			_continuousRumble[node] = true;
			SharedCoroutineStarter.instance.StartCoroutine(ContinuousRumbleCoroutine(node));
		}
	}

	public void Rumble(XRNode node, float duration, float impulseLength, float intervalTime)
	{
		if (_mainSettingsModel.controllersRumbleEnabled)
		{
			SharedCoroutineStarter.instance.StartCoroutine(OneShotRumbleCoroutine(node, duration, impulseLength, intervalTime));
		}
	}

	private IEnumerator ContinuousRumbleCoroutine(XRNode node)
	{
		YieldInstruction waitForIntervalTime = new WaitForSeconds(0.01f);
		VRPlatformHelper vrPlatformHelper = PersistentSingleton<VRPlatformHelper>.instance;
		while (GetContinousRumbleForNode(node))
		{
			vrPlatformHelper.TriggerHapticPulse(node, 0.3f);
			_continuousRumble[node] = false;
			yield return waitForIntervalTime;
		}
	}

	private IEnumerator OneShotRumbleCoroutine(XRNode node, float duration, float impulseStrength, float intervalTime = 0f)
	{
		float startTime = Time.timeSinceLevelLoad;
		YieldInstruction waitForIntervalTime = new WaitForSeconds(intervalTime);
		VRPlatformHelper vrPlatformHelper = PersistentSingleton<VRPlatformHelper>.instance;
		while (Time.timeSinceLevelLoad - startTime < duration)
		{
			vrPlatformHelper.TriggerHapticPulse(node, impulseStrength);
			if (intervalTime == 0f)
			{
				yield return null;
			}
			else
			{
				yield return waitForIntervalTime;
			}
		}
	}
}
