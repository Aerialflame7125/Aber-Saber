using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
[NativeHeader("Runtime/Graphics/Texture.h")]
public class Texture : Object
{
	public extern uint updateCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern int masterTextureLimit
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("AnisoLimit")]
	public static extern AnisotropicFiltering anisotropicFiltering
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public virtual int width
	{
		get
		{
			return GetDataWidth();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public virtual int height
	{
		get
		{
			return GetDataHeight();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public virtual TextureDimension dimension
	{
		get
		{
			return GetDimension();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public extern TextureWrapMode wrapMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetWrapModeU")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern TextureWrapMode wrapModeU
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern TextureWrapMode wrapModeV
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern TextureWrapMode wrapModeW
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern FilterMode filterMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int anisoLevel
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float mipMapBias
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 texelSize
	{
		[NativeName("GetNpotTexelSize")]
		get
		{
			get_texelSize_Injected(out var ret);
			return ret;
		}
	}

	protected Texture()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void IncrementUpdateCount();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("SetGlobalAnisoLimits")]
	public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int GetDataWidth();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int GetDataHeight();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern TextureDimension GetDimension();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern IntPtr GetNativeTexturePtr();

	[Obsolete("Use GetNativeTexturePtr instead.", false)]
	public int GetNativeTextureID()
	{
		return (int)GetNativeTexturePtr();
	}

	internal UnityException CreateNonReadableException(Texture t)
	{
		return new UnityException($"Texture '{t.name}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.");
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_texelSize_Injected(out Vector2 ret);
}
