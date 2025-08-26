using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using Valve.VR;

public class OpenVRHelper
{
	private EventSystem _disabledEventSystem;

	private TrackedDevicePose_t[] _poses = new TrackedDevicePose_t[64];

	private TrackedDevicePose_t[] _gamePoses = new TrackedDevicePose_t[0];

	public event Action inputFocusWasCapturedEvent;

	public event Action inputFocusWasReleasedEvent;

	public event Action dashboardWasActivatedEvent;

	public event Action dashboardWasDectivatedEvent;

	public OpenVRHelper()
	{
		Application.onBeforeRender += OnBeforeRender;
	}

	private void DisableEventSystem()
	{
		if (!(_disabledEventSystem == null))
		{
			return;
		}
		_disabledEventSystem = EventSystem.current;
		if (_disabledEventSystem != null)
		{
			if (!_disabledEventSystem.enabled)
			{
				_disabledEventSystem = null;
			}
			else
			{
				_disabledEventSystem.enabled = false;
			}
		}
	}

	private void EnableEventSystem()
	{
		if (_disabledEventSystem != null)
		{
			_disabledEventSystem.enabled = true;
			_disabledEventSystem = null;
		}
	}

	private void OnBeforeRender()
	{
		OpenVR.Compositor?.GetLastPoses(_poses, _gamePoses);
	}

	public void Update()
	{
		CVRSystem system = OpenVR.System;
		if (system == null)
		{
			return;
		}
		VREvent_t pEvent = default(VREvent_t);
		uint uncbVREvent = (uint)Marshal.SizeOf(typeof(VREvent_t));
		for (int i = 0; i < 64; i++)
		{
			if (!system.PollNextEvent(ref pEvent, uncbVREvent))
			{
				break;
			}
			switch ((EVREventType)pEvent.eventType)
			{
			case EVREventType.VREvent_Quit:
				Application.Quit();
				break;
			case EVREventType.VREvent_DashboardActivated:
				if (this.dashboardWasActivatedEvent != null)
				{
					this.dashboardWasActivatedEvent();
				}
				DisableEventSystem();
				break;
			case EVREventType.VREvent_DashboardDeactivated:
				if (this.dashboardWasDectivatedEvent != null)
				{
					this.dashboardWasDectivatedEvent();
				}
				EnableEventSystem();
				break;
			case EVREventType.VREvent_InputFocusCaptured:
				if (pEvent.data.process.oldPid == 0)
				{
					if (this.inputFocusWasCapturedEvent != null)
					{
						this.inputFocusWasCapturedEvent();
					}
					DisableEventSystem();
				}
				break;
			case EVREventType.VREvent_InputFocusReleased:
				if (pEvent.data.process.pid == 0)
				{
					if (this.inputFocusWasReleasedEvent != null)
					{
						this.inputFocusWasReleasedEvent();
					}
					EnableEventSystem();
				}
				break;
			default:
				SteamVR_Events.System((EVREventType)pEvent.eventType).Send(pEvent);
				break;
			}
		}
	}

	public void TriggerHapticPulse(XRNode node, float strength = 1f)
	{
		CVRSystem system = OpenVR.System;
		if (system != null)
		{
			ETrackedControllerRole unDeviceType = ((node == XRNode.LeftHand) ? ETrackedControllerRole.LeftHand : ETrackedControllerRole.RightHand);
			uint trackedDeviceIndexForControllerRole = system.GetTrackedDeviceIndexForControllerRole(unDeviceType);
			uint unAxisId = 0u;
			system.TriggerHapticPulse(trackedDeviceIndexForControllerRole, unAxisId, (char)(strength * 3999f));
		}
	}

	public VRPlatformHelper.VRNodeTransform GetNodePos(int nodeIndex)
	{
		VRPlatformHelper.VRNodeTransform result = default(VRPlatformHelper.VRNodeTransform);
		if (nodeIndex < _poses.Length)
		{
			TrackedDevicePose_t trackedDevicePose_t = _poses[nodeIndex];
			result.valid = trackedDevicePose_t.bPoseIsValid && trackedDevicePose_t.bDeviceIsConnected;
			if (result.valid)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(trackedDevicePose_t.mDeviceToAbsoluteTracking);
				result.pos = rigidTransform.pos;
				result.rot = rigidTransform.rot;
			}
		}
		return result;
	}
}
