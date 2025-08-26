using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Audio;

public class AudioMixerSnapshot : Object, ISubAssetNotDuplicatable
{
	public extern AudioMixer audioMixer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	internal AudioMixerSnapshot()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void TransitionTo(float timeToReach);
}
