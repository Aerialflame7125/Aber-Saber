using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[Obsolete("This component is part of the legacy UI system and will be removed in a future release.")]
public sealed class GUITexture : GUIElement
{
	public Color color
	{
		get
		{
			INTERNAL_get_color(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_color(ref value);
		}
	}

	public extern Texture texture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public Rect pixelInset
	{
		get
		{
			INTERNAL_get_pixelInset(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_pixelInset(ref value);
		}
	}

	public extern RectOffset border
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
	private extern void INTERNAL_get_color(out Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_color(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_pixelInset(out Rect value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_pixelInset(ref Rect value);
}
