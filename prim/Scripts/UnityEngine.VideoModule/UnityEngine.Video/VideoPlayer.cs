using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Video;

[RequireComponent(typeof(Transform))]
[UsedByNativeCode]
public sealed class VideoPlayer : Behaviour
{
	public delegate void EventHandler(VideoPlayer source);

	public delegate void ErrorEventHandler(VideoPlayer source, string message);

	public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

	public delegate void TimeEventHandler(VideoPlayer source, double seconds);

	public extern VideoSource source
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern string url
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern VideoClip clip
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern VideoRenderMode renderMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Camera targetCamera
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern RenderTexture targetTexture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Renderer targetMaterialRenderer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern string targetMaterialProperty
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern VideoAspectRatio aspectRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float targetCameraAlpha
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Video3DLayout targetCamera3DLayout
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Texture texture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isPrepared
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool waitForFirstFrame
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

	public extern bool isPlaying
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool canSetTime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern double time
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern long frame
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool canStep
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool canSetPlaybackSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float playbackSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool isLooping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool canSetTimeSource
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern VideoTimeSource timeSource
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern VideoTimeReference timeReference
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern double externalReferenceTime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool canSetSkipOnDrop
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool skipOnDrop
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern ulong frameCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float frameRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern ushort audioTrackCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern ushort controlledAudioTrackMaxCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern ushort controlledAudioTrackCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern VideoAudioOutputMode audioOutputMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool canSetDirectAudioVolume
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool sendFrameReadyEvents
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public event EventHandler prepareCompleted;

	public event EventHandler loopPointReached;

	public event EventHandler started;

	public event EventHandler frameDropped;

	public event ErrorEventHandler errorReceived;

	public event EventHandler seekCompleted;

	public event TimeEventHandler clockResyncOccurred;

	public event FrameReadyEventHandler frameReady;

	public void Prepare()
	{
		INTERNAL_CALL_Prepare(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Prepare(VideoPlayer self);

	public void Play()
	{
		INTERNAL_CALL_Play(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Play(VideoPlayer self);

	public void Pause()
	{
		INTERNAL_CALL_Pause(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Pause(VideoPlayer self);

	public void Stop()
	{
		INTERNAL_CALL_Stop(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Stop(VideoPlayer self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void StepForward();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern string GetAudioLanguageCode(ushort trackIndex);

	public ushort GetAudioChannelCount(ushort trackIndex)
	{
		return INTERNAL_CALL_GetAudioChannelCount(this, trackIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern ushort INTERNAL_CALL_GetAudioChannelCount(VideoPlayer self, ushort trackIndex);

	public void EnableAudioTrack(ushort trackIndex, bool enabled)
	{
		INTERNAL_CALL_EnableAudioTrack(this, trackIndex, enabled);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_EnableAudioTrack(VideoPlayer self, ushort trackIndex, bool enabled);

	public bool IsAudioTrackEnabled(ushort trackIndex)
	{
		return INTERNAL_CALL_IsAudioTrackEnabled(this, trackIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_IsAudioTrackEnabled(VideoPlayer self, ushort trackIndex);

	public float GetDirectAudioVolume(ushort trackIndex)
	{
		return INTERNAL_CALL_GetDirectAudioVolume(this, trackIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern float INTERNAL_CALL_GetDirectAudioVolume(VideoPlayer self, ushort trackIndex);

	public void SetDirectAudioVolume(ushort trackIndex, float volume)
	{
		INTERNAL_CALL_SetDirectAudioVolume(this, trackIndex, volume);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetDirectAudioVolume(VideoPlayer self, ushort trackIndex, float volume);

	public bool GetDirectAudioMute(ushort trackIndex)
	{
		return INTERNAL_CALL_GetDirectAudioMute(this, trackIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_GetDirectAudioMute(VideoPlayer self, ushort trackIndex);

	public void SetDirectAudioMute(ushort trackIndex, bool mute)
	{
		INTERNAL_CALL_SetDirectAudioMute(this, trackIndex, mute);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetDirectAudioMute(VideoPlayer self, ushort trackIndex, bool mute);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern AudioSource GetTargetAudioSource(ushort trackIndex);

	public void SetTargetAudioSource(ushort trackIndex, AudioSource source)
	{
		INTERNAL_CALL_SetTargetAudioSource(this, trackIndex, source);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetTargetAudioSource(VideoPlayer self, ushort trackIndex, AudioSource source);

	[RequiredByNativeCode]
	private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
	{
		if (source.prepareCompleted != null)
		{
			source.prepareCompleted(source);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
	{
		if (source.frameReady != null)
		{
			source.frameReady(source, frameIdx);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
	{
		if (source.loopPointReached != null)
		{
			source.loopPointReached(source);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeStartedCallback_Internal(VideoPlayer source)
	{
		if (source.started != null)
		{
			source.started(source);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
	{
		if (source.frameDropped != null)
		{
			source.frameDropped(source);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
	{
		if (source.errorReceived != null)
		{
			source.errorReceived(source, errorStr);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
	{
		if (source.seekCompleted != null)
		{
			source.seekCompleted(source);
		}
	}

	[RequiredByNativeCode]
	private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
	{
		if (source.clockResyncOccurred != null)
		{
			source.clockResyncOccurred(source, seconds);
		}
	}
}
