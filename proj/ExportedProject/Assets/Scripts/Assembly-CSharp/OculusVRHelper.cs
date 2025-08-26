using System;
using UnityEngine;
using UnityEngine.XR;

public class OculusVRHelper
{
	private OVRHapticsClip _hapticsClip;

	private bool _userPresent;

	private bool _hasInputFocus;

	private bool _hasVrFocus;

	public event Action inputFocusWasCapturedEvent;

	public event Action inputFocusWasReleasedEvent;

	public OculusVRHelper()
	{
		_hapticsClip = new OVRHapticsClip(6);
		_hapticsClip.WriteSample(0);
		_hapticsClip.WriteSample(0);
		_hapticsClip.WriteSample(0);
		_hapticsClip.WriteSample(0);
		_hapticsClip.WriteSample(0);
		_hapticsClip.WriteSample(byte.MaxValue);
	}

	public void Update()
	{
		if (OVRPlugin.shouldQuit)
		{
			Application.Quit();
		}
		if (OVRPlugin.shouldRecenter)
		{
			OVRPlugin.RecenterTrackingOrigin(OVRPlugin.RecenterFlags.Default);
		}
		bool userPresent = OVRPlugin.userPresent;
		if ((_userPresent && !userPresent) || _userPresent || userPresent)
		{
		}
		_userPresent = userPresent;
		bool hasInputFocus = OVRPlugin.hasInputFocus;
		if ((_hasInputFocus && !hasInputFocus) || _hasInputFocus || hasInputFocus)
		{
		}
		_hasInputFocus = hasInputFocus;
		bool hasVrFocus = OVRPlugin.hasVrFocus;
		if (_hasVrFocus && !hasVrFocus)
		{
			if (this.inputFocusWasCapturedEvent != null)
			{
				this.inputFocusWasCapturedEvent();
			}
		}
		else if (!_hasVrFocus && hasVrFocus && this.inputFocusWasReleasedEvent != null)
		{
			this.inputFocusWasReleasedEvent();
		}
		_hasVrFocus = hasVrFocus;
		OVRInput.Update();
	}

	public void FixedUpdate()
	{
		OVRInput.FixedUpdate();
	}

	public void LateUpdate()
	{
		OVRHaptics.Process();
	}

	public void TriggerHapticPulse(XRNode node, float strength = 1f)
	{
		OVRHaptics.OVRHapticsChannel oVRHapticsChannel = ((node != XRNode.LeftHand) ? OVRHaptics.RightChannel : OVRHaptics.LeftChannel);
		oVRHapticsChannel.Mix(_hapticsClip);
	}
}
