using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
public sealed class Texture2DArray : Texture
{
	public extern int depth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTextureLayerCount")]
		get;
	}

	public extern TextureFormat format
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTextureFormat")]
		get;
	}

	public Texture2DArray(int width, int height, int depth, TextureFormat format, bool mipmap, [DefaultValue("false")] bool linear)
	{
		Internal_Create(this, width, height, depth, format, mipmap, linear);
	}

	public Texture2DArray(int width, int height, int depth, TextureFormat format, bool mipmap)
		: this(width, height, depth, format, mipmap, linear: false)
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels(Color[] colors, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels(Color[] colors, int arrayElement)
	{
		int miplevel = 0;
		SetPixels(colors, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels32(Color32[] colors, int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels32(Color32[] colors, int arrayElement)
	{
		int miplevel = 0;
		SetPixels32(colors, arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color[] GetPixels(int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color[] GetPixels(int arrayElement)
	{
		int miplevel = 0;
		return GetPixels(arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color32[] GetPixels32(int arrayElement, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color32[] GetPixels32(int arrayElement)
	{
		int miplevel = 0;
		return GetPixels32(arrayElement, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetIsReadable")]
	private extern bool IsReadable();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("Texture2DArrayScripting::Create")]
	private static extern bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, TextureFormat format, bool mipmap, bool linear);

	private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, TextureFormat format, bool mipmap, bool linear)
	{
		if (!Internal_CreateImpl(mono, w, h, d, format, mipmap, linear))
		{
			throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "Texture2DArrayScripting::Apply", HasExplicitThis = true)]
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
