using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public sealed class AudioSettings
{
	public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);

	public static extern AudioSpeakerMode driverCapabilities
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern AudioSpeakerMode speakerMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	internal static extern int profilerCaptureFlags
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[ThreadAndSerializationSafe]
	public static extern double dspTime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern int outputSampleRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	internal static extern bool unityAudioDisabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static event AudioConfigurationChangeHandler OnAudioConfigurationChanged;

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
	public static extern void SetDSPBufferSize(int bufferLength, int numBuffers);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern string GetSpatializerPluginName();

	public static AudioConfiguration GetConfiguration()
	{
		INTERNAL_CALL_GetConfiguration(out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetConfiguration(out AudioConfiguration value);

	public static bool Reset(AudioConfiguration config)
	{
		return INTERNAL_CALL_Reset(ref config);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_Reset(ref AudioConfiguration config);

	[RequiredByNativeCode]
	internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
	{
		if (AudioSettings.OnAudioConfigurationChanged != null)
		{
			AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
		}
	}

	[RequiredByNativeCode]
	internal static void InvokeOnAudioManagerUpdate()
	{
		AudioExtensionManager.Update();
	}

	[RequiredByNativeCode]
	internal static void InvokeOnAudioSourcePlay(AudioSource source)
	{
		AudioSourceExtension audioSourceExtension = AudioExtensionManager.AddSpatializerExtension(source);
		if (audioSourceExtension != null)
		{
			AudioExtensionManager.GetReadyToPlay(audioSourceExtension);
		}
		if (source.clip != null && source.clip.ambisonic)
		{
			AudioSourceExtension audioSourceExtension2 = AudioExtensionManager.AddAmbisonicDecoderExtension(source);
			if (audioSourceExtension2 != null)
			{
				AudioExtensionManager.GetReadyToPlay(audioSourceExtension2);
			}
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern string GetAmbisonicDecoderPluginName();
}
