using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[StructLayout(LayoutKind.Sequential)]
[RequiredByNativeCode]
public class AsyncOperation : YieldInstruction
{
	internal IntPtr m_Ptr;

	private Action<AsyncOperation> m_completeCallback;

	public extern bool isDone
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float progress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern int priority
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool allowSceneActivation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public event Action<AsyncOperation> completed
	{
		add
		{
			if (isDone)
			{
				value(this);
			}
			else
			{
				m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(m_completeCallback, value);
			}
		}
		remove
		{
			m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(m_completeCallback, value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadAndSerializationSafe]
	[GeneratedByOldBindingsGenerator]
	private extern void InternalDestroy();

	~AsyncOperation()
	{
		InternalDestroy();
	}

	[RequiredByNativeCode]
	internal void InvokeCompletionEvent()
	{
		if (m_completeCallback != null)
		{
			m_completeCallback(this);
			m_completeCallback = null;
		}
	}
}
