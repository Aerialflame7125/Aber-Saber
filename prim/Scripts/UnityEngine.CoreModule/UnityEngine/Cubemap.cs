using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[ExcludeFromPreset]
[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
public sealed class Cubemap : Texture
{
	public extern int mipmapCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("CountDataMipmaps")]
		get;
	}

	public extern TextureFormat format
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTextureFormat")]
		get;
	}

	internal Cubemap(int ext, TextureFormat format, bool mipmap, IntPtr nativeTex)
	{
		Internal_Create(this, ext, format, mipmap, nativeTex);
	}

	public Cubemap(int ext, TextureFormat format, bool mipmap)
		: this(ext, format, mipmap, IntPtr.Zero)
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color[] GetPixels(CubemapFace face, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color[] GetPixels(CubemapFace face)
	{
		int miplevel = 0;
		return GetPixels(face, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels(Color[] colors, CubemapFace face, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels(Color[] colors, CubemapFace face)
	{
		int miplevel = 0;
		SetPixels(colors, face, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CubemapScripting::Create")]
	private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, TextureFormat format, bool mipmap, IntPtr nativeTex);

	private static void Internal_Create([Writable] Cubemap mono, int ext, TextureFormat format, bool mipmap, IntPtr nativeTex)
	{
		if (!Internal_CreateImpl(mono, ext, format, mipmap, nativeTex))
		{
			throw new UnityException("Failed to create texture because of invalid parameters.");
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
	private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetIsReadable")]
	private extern bool IsReadable();

	[NativeName("SetPixel")]
	private void SetPixelImpl(int image, int x, int y, Color color)
	{
		SetPixelImpl_Injected(image, x, y, ref color);
	}

	[NativeName("GetPixel")]
	private Color GetPixelImpl(int image, int x, int y)
	{
		GetPixelImpl_Injected(image, x, y, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("FixupEdges")]
	public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

	public void SmoothEdges()
	{
		SmoothEdges(1);
	}

	public static Cubemap CreateExternalTexture(int ext, TextureFormat format, bool mipmap, IntPtr nativeTex)
	{
		if (nativeTex == IntPtr.Zero)
		{
			throw new ArgumentException("nativeTex can not be null");
		}
		return new Cubemap(ext, format, mipmap, nativeTex);
	}

	public void SetPixel(CubemapFace face, int x, int y, Color color)
	{
		if (!IsReadable())
		{
			throw CreateNonReadableException(this);
		}
		SetPixelImpl((int)face, x, y, color);
	}

	public Color GetPixel(CubemapFace face, int x, int y)
	{
		if (!IsReadable())
		{
			throw CreateNonReadableException(this);
		}
		return GetPixelImpl((int)face, x, y);
	}

	public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
	{
		if (!IsReadable())
		{
			throw CreateNonReadableException(this);
		}
		ApplyImpl(updateMipmaps, makeNoLongerReadable);
	}

	public void Apply(bool updateMipmaps)
	{
		Apply(updateMipmaps, makeNoLongerReadable: false);
	}

	public void Apply()
	{
		Apply(updateMipmaps: true, makeNoLongerReadable: false);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);
}
