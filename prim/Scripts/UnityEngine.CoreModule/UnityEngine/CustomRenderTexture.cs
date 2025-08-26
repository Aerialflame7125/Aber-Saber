using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
public sealed class CustomRenderTexture : RenderTexture
{
	public extern Material material
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Material initializationMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Texture initializationTexture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern CustomRenderTextureInitializationSource initializationSource
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public Color initializationColor
	{
		get
		{
			INTERNAL_get_initializationColor(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_initializationColor(ref value);
		}
	}

	public extern CustomRenderTextureUpdateMode updateMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern CustomRenderTextureUpdateMode initializationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern CustomRenderTextureUpdateZoneSpace updateZoneSpace
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int shaderPass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern uint cubemapFaceMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool doubleBuffered
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool wrapUpdateZones
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public CustomRenderTexture(int width, int height, RenderTextureFormat format, RenderTextureReadWrite readWrite)
	{
		Internal_CreateCustomRenderTexture(this, readWrite);
		this.width = width;
		this.height = height;
		base.format = format;
	}

	public CustomRenderTexture(int width, int height, RenderTextureFormat format)
	{
		Internal_CreateCustomRenderTexture(this, RenderTextureReadWrite.Default);
		this.width = width;
		this.height = height;
		base.format = format;
	}

	public CustomRenderTexture(int width, int height)
	{
		Internal_CreateCustomRenderTexture(this, RenderTextureReadWrite.Default);
		this.width = width;
		this.height = height;
		base.format = RenderTextureFormat.Default;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_CreateCustomRenderTexture([Writable] CustomRenderTexture rt, RenderTextureReadWrite readWrite);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void Update([DefaultValue("1")] int count);

	[ExcludeFromDocs]
	public void Update()
	{
		int count = 1;
		Update(count);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void Initialize();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void ClearUpdateZones();

	private bool IsCubemapFaceEnabled(CubemapFace face)
	{
		return (cubemapFaceMask & (1 << (int)face)) != 0;
	}

	private void EnableCubemapFace(CubemapFace face, bool value)
	{
		uint num = cubemapFaceMask;
		uint num2 = (uint)(1 << (int)face);
		num = ((!value) ? (num & ~num2) : (num | num2));
		cubemapFaceMask = num;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void GetUpdateZonesInternal(object updateZones);

	public void GetUpdateZones(List<CustomRenderTextureUpdateZone> updateZones)
	{
		GetUpdateZonesInternal(updateZones);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void SetUpdateZonesInternal(CustomRenderTextureUpdateZone[] updateZones);

	public void SetUpdateZones(CustomRenderTextureUpdateZone[] updateZones)
	{
		if (updateZones == null)
		{
			throw new ArgumentNullException("updateZones");
		}
		SetUpdateZonesInternal(updateZones);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_initializationColor(out Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_initializationColor(ref Color value);
}
