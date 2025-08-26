using System;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequireComponent(typeof(Transform))]
public sealed class AudioSource : AudioBehaviour
{
	internal AudioSourceExtension spatializerExtension = null;

	internal AudioSourceExtension ambisonicExtension = null;

	public extern float volume
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float pitch
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float time
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[ThreadAndSerializationSafe]
	public extern int timeSamples
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[ThreadAndSerializationSafe]
	public extern AudioClip clip
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern AudioMixerGroup outputAudioMixerGroup
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool isPlaying
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isVirtual
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool loop
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool ignoreListenerVolume
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool playOnAwake
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool ignoreListenerPause
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern AudioVelocityUpdateMode velocityUpdateMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float panStereo
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float spatialBlend
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	internal extern bool spatializeInternal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public bool spatialize
	{
		get
		{
			return spatializeInternal;
		}
		set
		{
			if (spatializeInternal == value)
			{
				return;
			}
			spatializeInternal = value;
			if (value)
			{
				AudioSourceExtension audioSourceExtension = AudioExtensionManager.AddSpatializerExtension(this);
				if (audioSourceExtension != null && isPlaying)
				{
					AudioExtensionManager.GetReadyToPlay(audioSourceExtension);
				}
			}
		}
	}

	public extern bool spatializePostEffects
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float reverbZoneMix
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool bypassEffects
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool bypassListenerEffects
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool bypassReverbZones
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float dopplerLevel
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float spread
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
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

	public extern bool mute
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float minDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float maxDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern AudioRolloffMode rolloffMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
	public extern float minVolume
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
	public extern float maxVolume
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
	public extern float rolloffFactor
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
	public extern void Play([DefaultValue("0")] ulong delay);

	[ExcludeFromDocs]
	public void Play()
	{
		ulong delay = 0uL;
		Play(delay);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void PlayDelayed(float delay);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void PlayScheduled(double time);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetScheduledStartTime(double time);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetScheduledEndTime(double time);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void Stop();

	public void Pause()
	{
		INTERNAL_CALL_Pause(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Pause(AudioSource self);

	public void UnPause()
	{
		INTERNAL_CALL_UnPause(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_UnPause(AudioSource self);

	[ExcludeFromDocs]
	public void PlayOneShot(AudioClip clip)
	{
		float volumeScale = 1f;
		PlayOneShot(clip, volumeScale);
	}

	public void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale)
	{
		if (clip != null && clip.ambisonic)
		{
			AudioSourceExtension audioSourceExtension = AudioExtensionManager.AddAmbisonicDecoderExtension(this);
			if (audioSourceExtension != null)
			{
				AudioExtensionManager.GetReadyToPlay(audioSourceExtension);
			}
		}
		PlayOneShotHelper(clip, volumeScale);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void PlayOneShotHelper(AudioClip clip, [DefaultValue("1.0F")] float volumeScale);

	[ExcludeFromDocs]
	private void PlayOneShotHelper(AudioClip clip)
	{
		float volumeScale = 1f;
		PlayOneShotHelper(clip, volumeScale);
	}

	[ExcludeFromDocs]
	public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
	{
		float num = 1f;
		PlayClipAtPoint(clip, position, num);
	}

	public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
	{
		GameObject gameObject = new GameObject("One shot audio");
		gameObject.transform.position = position;
		AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
		audioSource.clip = clip;
		audioSource.spatialBlend = 1f;
		audioSource.volume = volume;
		audioSource.Play();
		Object.Destroy(gameObject, clip.length * ((!(Time.timeScale < 0.01f)) ? Time.timeScale : 0.01f));
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern AnimationCurve GetCustomCurve(AudioSourceCurveType type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void GetOutputDataHelper(float[] samples, int channel);

	[Obsolete("GetOutputData return a float[] is deprecated, use GetOutputData passing a pre allocated array instead.")]
	public float[] GetOutputData(int numSamples, int channel)
	{
		float[] array = new float[numSamples];
		GetOutputDataHelper(array, channel);
		return array;
	}

	public void GetOutputData(float[] samples, int channel)
	{
		GetOutputDataHelper(samples, channel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void GetSpectrumDataHelper(float[] samples, int channel, FFTWindow window);

	[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData passing a pre allocated array instead.")]
	public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
	{
		float[] array = new float[numSamples];
		GetSpectrumDataHelper(array, channel, window);
		return array;
	}

	public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
	{
		GetSpectrumDataHelper(samples, channel, window);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern int GetNumExtensionProperties();

	internal int GetNumExtensionPropertiesForThisExtension(PropertyName extensionName)
	{
		return INTERNAL_CALL_GetNumExtensionPropertiesForThisExtension(this, ref extensionName);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int INTERNAL_CALL_GetNumExtensionPropertiesForThisExtension(AudioSource self, ref PropertyName extensionName);

	internal PropertyName ReadExtensionName(int sourceIndex)
	{
		INTERNAL_CALL_ReadExtensionName(this, sourceIndex, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ReadExtensionName(AudioSource self, int sourceIndex, out PropertyName value);

	internal PropertyName ReadExtensionPropertyName(int sourceIndex)
	{
		INTERNAL_CALL_ReadExtensionPropertyName(this, sourceIndex, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ReadExtensionPropertyName(AudioSource self, int sourceIndex, out PropertyName value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern float ReadExtensionPropertyValue(int sourceIndex);

	internal bool ReadExtensionProperty(PropertyName extensionName, PropertyName propertyName, ref float propertyValue)
	{
		return INTERNAL_CALL_ReadExtensionProperty(this, ref extensionName, ref propertyName, ref propertyValue);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_ReadExtensionProperty(AudioSource self, ref PropertyName extensionName, ref PropertyName propertyName, ref float propertyValue);

	internal void ClearExtensionProperties(PropertyName extensionName)
	{
		INTERNAL_CALL_ClearExtensionProperties(this, ref extensionName);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ClearExtensionProperties(AudioSource self, ref PropertyName extensionName);

	internal AudioSourceExtension AddSpatializerExtension(Type extensionType)
	{
		if (spatializerExtension == null)
		{
			spatializerExtension = ScriptableObject.CreateInstance(extensionType) as AudioSourceExtension;
		}
		return spatializerExtension;
	}

	internal AudioSourceExtension AddAmbisonicExtension(Type extensionType)
	{
		if (ambisonicExtension == null)
		{
			ambisonicExtension = ScriptableObject.CreateInstance(extensionType) as AudioSourceExtension;
		}
		return ambisonicExtension;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool SetSpatializerFloat(int index, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool GetSpatializerFloat(int index, out float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool SetAmbisonicDecoderFloat(int index, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool GetAmbisonicDecoderFloat(int index, out float value);
}
