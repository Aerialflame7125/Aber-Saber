using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling;

[UsedByNativeCode]
[MovedFrom("UnityEngine")]
public sealed class Profiler
{
	public static extern bool supported
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern string logFile
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool enableBinaryLog
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("maxNumberOfSamplesPerFrame is no longer needed, as the profiler buffers auto expand as needed")]
	public static extern int maxNumberOfSamplesPerFrame
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
	public static extern uint usedHeapSize
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern long usedHeapSizeLong
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	private Profiler()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Conditional("UNITY_EDITOR")]
	[GeneratedByOldBindingsGenerator]
	public static extern void AddFramesFromFile(string file);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[Conditional("ENABLE_PROFILER")]
	[ThreadAndSerializationSafe]
	public static extern void BeginThreadProfiling(string threadGroupName, string threadName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Conditional("ENABLE_PROFILER")]
	[ThreadAndSerializationSafe]
	[GeneratedByOldBindingsGenerator]
	public static extern void EndThreadProfiling();

	[Conditional("ENABLE_PROFILER")]
	public static void BeginSample(string name)
	{
		BeginSampleOnly(name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Conditional("ENABLE_PROFILER")]
	[GeneratedByOldBindingsGenerator]
	public static extern void BeginSample(string name, Object targetObject);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void BeginSampleOnly(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Conditional("ENABLE_PROFILER")]
	[GeneratedByOldBindingsGenerator]
	public static extern void EndSample();

	[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
	public static int GetRuntimeMemorySize(Object o)
	{
		return (int)GetRuntimeMemorySizeLong(o);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetRuntimeMemorySizeLong(Object o);

	[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
	public static uint GetMonoHeapSize()
	{
		return (uint)GetMonoHeapSizeLong();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetMonoHeapSizeLong();

	[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
	public static uint GetMonoUsedSize()
	{
		return (uint)GetMonoUsedSizeLong();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetMonoUsedSizeLong();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool SetTempAllocatorRequestedSize(uint size);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern uint GetTempAllocatorSize();

	[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
	public static uint GetTotalAllocatedMemory()
	{
		return (uint)GetTotalAllocatedMemoryLong();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetTotalAllocatedMemoryLong();

	[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
	public static uint GetTotalUnusedReservedMemory()
	{
		return (uint)GetTotalUnusedReservedMemoryLong();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetTotalUnusedReservedMemoryLong();

	[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
	public static uint GetTotalReservedMemory()
	{
		return (uint)GetTotalReservedMemoryLong();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetTotalReservedMemoryLong();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern long GetAllocatedMemoryForGraphicsDriver();
}
