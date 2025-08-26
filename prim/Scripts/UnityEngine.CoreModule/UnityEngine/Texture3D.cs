using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[ExcludeFromPreset]
[NativeHeader("Runtime/Graphics/Texture3D.h")]
public sealed class Texture3D : Texture
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

	public Texture3D(int width, int height, int depth, TextureFormat format, bool mipmap)
	{
		Internal_Create(this, width, height, depth, format, mipmap);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color[] GetPixels([DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color[] GetPixels()
	{
		int miplevel = 0;
		return GetPixels(miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public Color32[] GetPixels32()
	{
		int miplevel = 0;
		return GetPixels32(miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels(Color[] colors)
	{
		int miplevel = 0;
		SetPixels(colors, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel);

	[ExcludeFromDocs]
	public void SetPixels32(Color32[] colors)
	{
		int miplevel = 0;
		SetPixels32(colors, miplevel);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetIsReadable")]
	private extern bool IsReadable();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("Texture3DScripting::Create")]
	private static extern bool Internal_CreateImpl([Writable] Texture3D mono, int w, int h, int d, TextureFormat format, bool mipmap);

	private static void Internal_Create([Writable] Texture3D mono, int w, int h, int d, TextureFormat format, bool mipmap)
	{
		if (!Internal_CreateImpl(mono, w, h, d, format, mipmap))
		{
			throw new UnityException("Failed to create texture because of invalid parameters.");
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "Texture3DScripting::Apply", HasExplicitThis = true)]
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
