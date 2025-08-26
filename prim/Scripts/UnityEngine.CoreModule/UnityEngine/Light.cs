using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Transform))]
[NativeHeader("Runtime/Camera/Light.h")]
public sealed class Light : Behaviour
{
	private int m_BakedIndex;

	public extern LightShadows shadows
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float shadowStrength
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern LightShadowResolution shadowResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Shadow softness is removed in Unity 5.0+")]
	public extern float shadowSoftness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Shadow softness is removed in Unity 5.0+")]
	public extern float shadowSoftnessFade
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float cookieSize
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Texture cookie
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern LightRenderMode renderMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("bakedIndex has been removed please use bakingOutput.isBaked instead.")]
	public int bakedIndex
	{
		get
		{
			return m_BakedIndex;
		}
		set
		{
			m_BakedIndex = value;
		}
	}

	public extern int commandBufferCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern int pixelLightCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
	public float shadowConstantBias
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
	public float shadowObjectSizeBias
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
	public bool attenuate
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	[NativeProperty("LightType")]
	public extern LightType type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float spotAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Color color
	{
		get
		{
			get_color_Injected(out var ret);
			return ret;
		}
		set
		{
			set_color_Injected(ref value);
		}
	}

	public extern float colorTemperature
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float intensity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float bounceIntensity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int shadowCustomResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float shadowBias
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float shadowNormalBias
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float shadowNearPlane
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float range
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Flare flare
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public LightBakingOutput bakingOutput
	{
		get
		{
			get_bakingOutput_Injected(out var ret);
			return ret;
		}
		set
		{
			set_bakingOutput_Injected(ref value);
		}
	}

	public extern int cullingMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void SetFalloffTable(float[] input);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void SetAllLightsFalloffToInverseSquared();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void SetAllLightsFalloffToUnityLegacy();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetLightDirty();

	public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
	{
		AddCommandBuffer(evt, buffer, ShadowMapPass.All);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask);

	public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
	{
		AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void RemoveCommandBuffers(LightEvent evt);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void RemoveAllCommandBuffers();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern Light[] GetLights(LightType type, int layer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_color_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_color_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_bakingOutput_Injected(out LightBakingOutput ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_bakingOutput_Injected(ref LightBakingOutput value);
}
