using System;
using UnityEngine;
using UnityEngine.XR;

public class VRPlatformHelper : PersistentSingleton<VRPlatformHelper>
{
	public enum VRPlatformSDK
	{
		OpenVR = 0,
		Oculus = 1,
		Unknown = 2
	}

	public struct VRNodeTransform
	{
		public Vector3 pos;

		public Quaternion rot;

		public bool valid;
	}

	public enum InputControllerDevice
	{
		Unknown = 0,
		OculusTouch = 1,
		Other = 2
	}

	private OpenVRHelper _openVRHeper;

	private OculusVRHelper _oculusVRHelper;

	private static InputControllerDevice _inputControllerDevice;

	public VRPlatformSDK vrPlatformSDK { get; private set; }

	public event Action inputFocusWasCapturedEvent;

	public event Action inputFocusWasReleasedEvent;

	public event Action dashboardWasActivatedEvent;

	public event Action dashboardWasDectivatedEvent;

	private void Awake()
	{
		vrPlatformSDK = VRPlatformSDK.Unknown;
		if (XRSettings.loadedDeviceName.IndexOf("oculus", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			vrPlatformSDK = VRPlatformSDK.Oculus;
			_oculusVRHelper = new OculusVRHelper();
			_oculusVRHelper.inputFocusWasCapturedEvent += HandleInputFocusWasCaptured;
			_oculusVRHelper.inputFocusWasReleasedEvent += HandleInputFocusWasReleased;
		}
		else if (XRSettings.loadedDeviceName.IndexOf("openvr", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			vrPlatformSDK = VRPlatformSDK.OpenVR;
			_openVRHeper = new OpenVRHelper();
			_openVRHeper.inputFocusWasCapturedEvent += HandleInputFocusWasCaptured;
			_openVRHeper.inputFocusWasReleasedEvent += HandleInputFocusWasReleased;
			_openVRHeper.dashboardWasActivatedEvent += HandleInputFocusWasCaptured;
			_openVRHeper.dashboardWasDectivatedEvent += HandleInputFocusWasReleased;
		}
	}

	private void Update()
	{
		if (_openVRHeper != null)
		{
			_openVRHeper.Update();
		}
		else if (_oculusVRHelper != null)
		{
			_oculusVRHelper.Update();
		}
	}

	private void FixedUpdate()
	{
		if (_oculusVRHelper != null)
		{
			_oculusVRHelper.FixedUpdate();
		}
	}

	private void LateUpdate()
	{
		if (_oculusVRHelper != null)
		{
			_oculusVRHelper.LateUpdate();
		}
	}

	private void HandleInputFocusWasCaptured()
	{
		if (this.inputFocusWasCapturedEvent != null)
		{
			this.inputFocusWasCapturedEvent();
		}
	}

	private void HandleInputFocusWasReleased()
	{
		if (this.inputFocusWasReleasedEvent != null)
		{
			this.inputFocusWasReleasedEvent();
		}
	}

	private void HandleDashboardWasActivated()
	{
		if (this.dashboardWasActivatedEvent != null)
		{
			this.dashboardWasActivatedEvent();
		}
	}

	private void HandleDashboardWasDectivated()
	{
		if (this.dashboardWasDectivatedEvent != null)
		{
			this.dashboardWasDectivatedEvent();
		}
	}

	public void TriggerHapticPulse(XRNode node, float strength = 1f)
	{
		if (node == XRNode.LeftHand || node == XRNode.RightHand)
		{
			if (_openVRHeper != null)
			{
				_openVRHeper.TriggerHapticPulse(node, strength);
			}
			else if (_oculusVRHelper != null)
			{
				_oculusVRHelper.TriggerHapticPulse(node, strength);
			}
		}
	}

	public VRNodeTransform GetNodePos(int nodeIndex)
	{
		if (_openVRHeper != null)
		{
			return _openVRHeper.GetNodePos(nodeIndex);
		}
		return default(VRNodeTransform);
	}

	public void AdjustPlatformSpecificControllerTransform(Transform transform)
	{
		if (vrPlatformSDK == VRPlatformSDK.Oculus)
		{
			transform.Rotate(-40f, 0f, 0f);
			transform.Translate(0f, 0f, 0.055f);
		}
	}

	public static bool UsingOculusTouch()
	{
		if (_inputControllerDevice == InputControllerDevice.Unknown)
		{
			if (XRDevice.model.IndexOf("oculus", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				_inputControllerDevice = InputControllerDevice.OculusTouch;
			}
			else
			{
				_inputControllerDevice = InputControllerDevice.Other;
			}
		}
		return _inputControllerDevice == InputControllerDevice.OculusTouch;
	}
}
