using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public struct DrivenRectTransformTracker
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern bool CanRecordModifications();

	public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
	{
	}

	[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
	public void Clear(bool revertValues)
	{
		Clear();
	}

	public void Clear()
	{
	}
}
