using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.XR;

[RequiredByNativeCode]
public static class InputTracking
{
	private enum TrackingStateEventType
	{
		NodeAdded,
		NodeRemoved,
		TrackingAcquired,
		TrackingLost
	}

	public static extern bool disablePositionalTracking
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static event Action<XRNodeState> trackingAcquired;

	public static event Action<XRNodeState> trackingLost;

	public static event Action<XRNodeState> nodeAdded;

	public static event Action<XRNodeState> nodeRemoved;

	public static Vector3 GetLocalPosition(XRNode node)
	{
		INTERNAL_CALL_GetLocalPosition(node, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetLocalPosition(XRNode node, out Vector3 value);

	public static Quaternion GetLocalRotation(XRNode node)
	{
		INTERNAL_CALL_GetLocalRotation(node, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetLocalRotation(XRNode node, out Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void Recenter();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern string GetNodeName(ulong uniqueID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void GetNodeStatesInternal(object nodeStates);

	public static void GetNodeStates(List<XRNodeState> nodeStates)
	{
		if (nodeStates == null)
		{
			throw new ArgumentNullException("nodeStates");
		}
		nodeStates.Clear();
		GetNodeStatesInternal(nodeStates);
	}

	[RequiredByNativeCode]
	private static void InvokeTrackingEvent(TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
	{
		Action<XRNodeState> action = null;
		XRNodeState obj = default(XRNodeState);
		obj.uniqueID = (ulong)uniqueID;
		obj.nodeType = nodeType;
		obj.tracked = tracked;
		((Action<XRNodeState>)(eventType switch
		{
			TrackingStateEventType.TrackingAcquired => InputTracking.trackingAcquired, 
			TrackingStateEventType.TrackingLost => InputTracking.trackingLost, 
			TrackingStateEventType.NodeAdded => InputTracking.nodeAdded, 
			TrackingStateEventType.NodeRemoved => InputTracking.nodeRemoved, 
			_ => throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType), 
		}))?.Invoke(obj);
	}

	static InputTracking()
	{
		InputTracking.trackingAcquired = null;
		InputTracking.trackingLost = null;
		InputTracking.nodeAdded = null;
		InputTracking.nodeRemoved = null;
	}
}
