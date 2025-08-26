using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
[NativeHeader("Runtime/Animation/AnimatorInfo.h")]
[NativeHeader("Runtime/Animation/ScriptBindings/Animation.bindings.h")]
public struct AnimatorClipInfo
{
	private int m_ClipInstanceID;

	private float m_Weight;

	public AnimationClip clip => (m_ClipInstanceID == 0) ? null : InstanceIDToAnimationClipPPtr(m_ClipInstanceID);

	public float weight => m_Weight;

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("AnimationBindings::InstanceIDToAnimationClipPPtr")]
	private static extern AnimationClip InstanceIDToAnimationClipPPtr(int instanceID);
}
