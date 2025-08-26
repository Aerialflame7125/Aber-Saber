using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Audio;

public class AudioMixerGroup : Object, ISubAssetNotDuplicatable
{
	public extern AudioMixer audioMixer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	internal AudioMixerGroup()
	{
	}
}
