using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public sealed class Input
{
	private static Gyroscope m_MainGyro = null;

	private static LocationService locationServiceInstance;

	private static Compass compassInstance;

	public static extern bool compensateSensors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
	public static extern bool isGyroAvailable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static Gyroscope gyro
	{
		get
		{
			if (m_MainGyro == null)
			{
				m_MainGyro = new Gyroscope(mainGyroIndex_Internal());
			}
			return m_MainGyro;
		}
	}

	public static Vector3 mousePosition
	{
		get
		{
			INTERNAL_get_mousePosition(out var value);
			return value;
		}
	}

	public static Vector2 mouseScrollDelta
	{
		get
		{
			INTERNAL_get_mouseScrollDelta(out var value);
			return value;
		}
	}

	public static extern bool mousePresent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool simulateMouseWithTouches
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool anyKey
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool anyKeyDown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern string inputString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static Vector3 acceleration
	{
		get
		{
			INTERNAL_get_acceleration(out var value);
			return value;
		}
	}

	public static AccelerationEvent[] accelerationEvents
	{
		get
		{
			int num = accelerationEventCount;
			AccelerationEvent[] array = new AccelerationEvent[num];
			for (int i = 0; i < num; i++)
			{
				ref AccelerationEvent reference = ref array[i];
				reference = GetAccelerationEvent(i);
			}
			return array;
		}
	}

	public static extern int accelerationEventCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static Touch[] touches
	{
		get
		{
			int num = touchCount;
			Touch[] array = new Touch[num];
			for (int i = 0; i < num; i++)
			{
				ref Touch reference = ref array[i];
				reference = GetTouch(i);
			}
			return array;
		}
	}

	public static extern int touchCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
	public static extern bool eatKeyPressOnTextFieldFocus
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool touchPressureSupported
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool stylusTouchSupported
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool touchSupported
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool multiTouchEnabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static LocationService location
	{
		get
		{
			if (locationServiceInstance == null)
			{
				locationServiceInstance = new LocationService();
			}
			return locationServiceInstance;
		}
	}

	public static Compass compass
	{
		get
		{
			if (compassInstance == null)
			{
				compassInstance = new Compass();
			}
			return compassInstance;
		}
	}

	public static extern DeviceOrientation deviceOrientation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern IMECompositionMode imeCompositionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern string compositionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool imeIsSelected
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static Vector2 compositionCursorPos
	{
		get
		{
			INTERNAL_get_compositionCursorPos(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_compositionCursorPos(ref value);
		}
	}

	public static extern bool backButtonLeavesApp
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int mainGyroIndex_Internal();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyInt(int key);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyString(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyUpInt(int key);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyUpString(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyDownInt(int key);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool GetKeyDownString(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern float GetAxis(string axisName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern float GetAxisRaw(string axisName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetButton(string buttonName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetButtonDown(string buttonName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetButtonUp(string buttonName);

	public static bool GetKey(string name)
	{
		return GetKeyString(name);
	}

	public static bool GetKey(KeyCode key)
	{
		return GetKeyInt((int)key);
	}

	public static bool GetKeyDown(string name)
	{
		return GetKeyDownString(name);
	}

	public static bool GetKeyDown(KeyCode key)
	{
		return GetKeyDownInt((int)key);
	}

	public static bool GetKeyUp(string name)
	{
		return GetKeyUpString(name);
	}

	public static bool GetKeyUp(KeyCode key)
	{
		return GetKeyUpInt((int)key);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern string[] GetJoystickNames();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetMouseButton(int button);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetMouseButtonDown(int button);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool GetMouseButtonUp(int button);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void ResetInputAxes();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_get_mousePosition(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_get_mouseScrollDelta(out Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_get_acceleration(out Vector3 value);

	public static AccelerationEvent GetAccelerationEvent(int index)
	{
		INTERNAL_CALL_GetAccelerationEvent(index, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetAccelerationEvent(int index, out AccelerationEvent value);

	public static Touch GetTouch(int index)
	{
		INTERNAL_CALL_GetTouch(index, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetTouch(int index, out Touch value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_get_compositionCursorPos(out Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_set_compositionCursorPos(ref Vector2 value);
}
