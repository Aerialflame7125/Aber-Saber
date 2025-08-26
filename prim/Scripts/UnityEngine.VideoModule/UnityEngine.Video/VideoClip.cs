using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Video;

public sealed class VideoClip : Object
{
	public extern string originalPath
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern ulong frameCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern double frameRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern double length
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern uint width
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern uint height
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern uint pixelAspectRatioNumerator
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern uint pixelAspectRatioDenominator
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

	private VideoClip()
	{
	}

	public ushort GetAudioChannelCount(ushort audioTrackIdx)
	{
		return INTERNAL_CALL_GetAudioChannelCount(this, audioTrackIdx);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern ushort INTERNAL_CALL_GetAudioChannelCount(VideoClip self, ushort audioTrackIdx);

	public uint GetAudioSampleRate(ushort audioTrackIdx)
	{
		return INTERNAL_CALL_GetAudioSampleRate(this, audioTrackIdx);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern uint INTERNAL_CALL_GetAudioSampleRate(VideoClip self, ushort audioTrackIdx);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern string GetAudioLanguage(ushort audioTrackIdx);
}
