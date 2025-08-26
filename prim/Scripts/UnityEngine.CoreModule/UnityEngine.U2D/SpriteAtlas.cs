using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.U2D;

public sealed class SpriteAtlas : Object
{
	public extern bool isVariant
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern string tag
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern int spriteCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern int GetSprites(Sprite[] sprites);

	public int GetSprites(Sprite[] sprites, string name)
	{
		return GetSpritesByName(sprites, name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern int GetSpritesByName(Sprite[] sprites, string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Sprite GetSprite(string name);
}
