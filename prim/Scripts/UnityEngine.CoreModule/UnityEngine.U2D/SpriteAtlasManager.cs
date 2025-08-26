using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.U2D;

public sealed class SpriteAtlasManager
{
	public delegate void RequestAtlasCallback(string tag, Action<SpriteAtlas> action);

	public static event RequestAtlasCallback atlasRequested;

	[RequiredByNativeCode]
	private static bool RequestAtlas(string tag)
	{
		if (SpriteAtlasManager.atlasRequested != null)
		{
			SpriteAtlasManager.atlasRequested(tag, Register);
			return true;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern void Register(SpriteAtlas spriteAtlas);

	static SpriteAtlasManager()
	{
		SpriteAtlasManager.atlasRequested = null;
	}
}
