using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling;

[UsedByNativeCode]
public sealed class Recorder
{
	internal IntPtr m_Ptr;

	internal static Recorder s_InvalidRecorder = new Recorder();

	public bool isValid => m_Ptr != IntPtr.Zero;

	[ThreadAndSerializationSafe]
	public extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[ThreadAndSerializationSafe]
	public extern long elapsedNanoseconds
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[ThreadAndSerializationSafe]
	public extern int sampleBlockCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	internal Recorder()
	{
	}

	~Recorder()
	{
		if (m_Ptr != IntPtr.Zero)
		{
			DisposeNative();
		}
	}

	public static Recorder Get(string samplerName)
	{
		Recorder @internal = GetInternal(samplerName);
		if (@internal == null)
		{
			return s_InvalidRecorder;
		}
		return @internal;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern Recorder GetInternal(string samplerName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadAndSerializationSafe]
	[GeneratedByOldBindingsGenerator]
	private extern void DisposeNative();
}
