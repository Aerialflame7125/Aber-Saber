using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
public sealed class CubemapArray : Texture
{
	public extern int cubemapCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern TextureFormat format
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTextureFormat")]
		get;
	}

	public CubemapArray(int faceSize, int cubemapCount, TextureFormat format, bool mipmap, [DefaultValue("false")] bool linear)
	{
		Internal_Create(this, faceSize, cubemapCount, format, mipmap, linear);
	}

	public CubemapArray(int faceSize, int cubemapCount, TextureFormat format, bool mipmap)
		: this(faceSize, cubemapCount, format, mipmap, linear: false)
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels(Color[] colors, CubemapFace face, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
	{
		int miplevel = 0;
		SetPixels(colors, face, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
	{
		int miplevel = 0;
		SetPixels32(colors, face, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color[] GetPixels(CubemapFace face, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color[] GetPixels(CubemapFace face, int arrayElement)
	{
		int miplevel = 0;
		return GetPixels(face, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color32[] GetPixels32(CubemapFace face, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color32[] GetPixels32(CubemapFace face, int arrayElement)
	{
		int miplevel = 0;
		return GetPixels32(face, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetIsReadable")]
	private extern bool IsReadable();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CubemapArrayScripting::Create")]
	private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, TextureFormat format, bool mipmap, bool linear);

	private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, TextureFormat format, bool mipmap, bool linear)
	{
		if (!Internal_CreateImpl(mono, ext, count, format, mipmap, linear))
		{
			throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
	private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

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
}
