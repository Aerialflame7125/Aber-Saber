using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[ExcludeFromPreset]
[ExcludeFromObjectFactory]
public sealed class MovieTexture : Texture
{
	public extern AudioClip audioClip
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

	public extern bool isPlaying
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isReadyToPlay
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float duration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	private MovieTexture()
	{
	}

	public void Play()
	{
		INTERNAL_CALL_Play(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Play(MovieTexture self);

	public void Stop()
	{
		INTERNAL_CALL_Stop(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Stop(MovieTexture self);

	public void Pause()
	{
		INTERNAL_CALL_Pause(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Pause(MovieTexture self);
}
