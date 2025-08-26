using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
[NativeHeader("Runtime/Camera/Camera.h")]
[UsedByNativeCode]
[NativeHeader("Runtime/Graphics/RenderTexture.h")]
public class RenderTexture : Texture
{
	public extern int depth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public RenderBuffer colorBuffer
	{
		get
		{
			GetColorBuffer(out var res);
			return res;
		}
	}

	public RenderBuffer depthBuffer
	{
		get
		{
			GetDepthBuffer(out var res);
			return res;
		}
	}

	public static extern RenderTexture active
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("RenderTexture.enabled is always now, no need to use it")]
	public static extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public override extern int width
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public override extern int height
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public override extern TextureDimension dimension
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("MipMap")]
	public extern bool useMipMap
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("SRGBReadWrite")]
	public extern bool sRGB
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("ColorFormat")]
	public extern RenderTextureFormat format
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("VRUsage")]
	public extern VRTextureUsage vrUsage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("Memoryless")]
	public extern RenderTextureMemoryless memorylessMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool autoGenerateMips
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int volumeDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int antiAliasing
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool bindTextureMS
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enableRandomWrite
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useDynamicScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public bool isPowerOfTwo
	{
		get
		{
			return GetIsPowerOfTwo();
		}
		set
		{
		}
	}

	[Obsolete("Use RenderTexture.dimension instead.", false)]
	public bool isCubemap
	{
		get
		{
			return dimension == TextureDimension.Cube;
		}
		set
		{
			dimension = ((!value) ? TextureDimension.Tex2D : TextureDimension.Cube);
		}
	}

	[Obsolete("Use RenderTexture.dimension instead.", false)]
	public bool isVolume
	{
		get
		{
			return dimension == TextureDimension.Tex3D;
		}
		set
		{
			dimension = ((!value) ? TextureDimension.Tex2D : TextureDimension.Tex3D);
		}
	}

	public RenderTextureDescriptor descriptor
	{
		get
		{
			return GetDescriptor();
		}
		set
		{
			ValidateRenderTextureDesc(value);
			SetRenderTextureDescriptor(value);
		}
	}

	protected internal RenderTexture()
	{
	}

	public RenderTexture(RenderTextureDescriptor desc)
	{
		ValidateRenderTextureDesc(desc);
		Internal_Create(this);
		SetRenderTextureDescriptor(desc);
	}

	public RenderTexture(RenderTexture textureToCopy)
	{
		if (textureToCopy == null)
		{
			throw new ArgumentNullException("textureToCopy");
		}
		ValidateRenderTextureDesc(textureToCopy.descriptor);
		Internal_Create(this);
		SetRenderTextureDescriptor(textureToCopy.descriptor);
	}

	public RenderTexture(int width, int height, int depth, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
	{
		Internal_Create(this);
		this.width = width;
		this.height = height;
		this.depth = depth;
		this.format = format;
		bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
		SetSRGBReadWrite((readWrite != 0) ? (readWrite == RenderTextureReadWrite.sRGB) : flag);
	}

	public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
		: this(width, height, depth, format, RenderTextureReadWrite.Default)
	{
	}

	public RenderTexture(int width, int height, int depth)
		: this(width, height, depth, RenderTextureFormat.Default, RenderTextureReadWrite.Default)
	{
	}

	private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
	{
		INTERNAL_CALL_SetRenderTextureDescriptor(this, ref desc);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetRenderTextureDescriptor(RenderTexture self, ref RenderTextureDescriptor desc);

	private RenderTextureDescriptor GetDescriptor()
	{
		INTERNAL_CALL_GetDescriptor(this, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetDescriptor(RenderTexture self, out RenderTextureDescriptor value);

	private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
	{
		return INTERNAL_CALL_GetTemporary_Internal(ref desc);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern RenderTexture INTERNAL_CALL_GetTemporary_Internal(ref RenderTextureDescriptor desc);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void ReleaseTemporary(RenderTexture temp);

	public void ResolveAntiAliasedSurface()
	{
		Internal_ResolveAntiAliasedSurface(null);
	}

	public void ResolveAntiAliasedSurface(RenderTexture target)
	{
		Internal_ResolveAntiAliasedSurface(target);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void Internal_ResolveAntiAliasedSurface(RenderTexture target);

	public void DiscardContents()
	{
		INTERNAL_CALL_DiscardContents(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_DiscardContents(RenderTexture self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void DiscardContents(bool discardColor, bool discardDepth);

	public void MarkRestoreExpected()
	{
		INTERNAL_CALL_MarkRestoreExpected(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_MarkRestoreExpected(RenderTexture self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void GetColorBuffer(out RenderBuffer res);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void GetDepthBuffer(out RenderBuffer res);

	public IntPtr GetNativeDepthBufferPtr()
	{
		INTERNAL_CALL_GetNativeDepthBufferPtr(this, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetNativeDepthBufferPtr(RenderTexture self, out IntPtr value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetGlobalShaderProperty(string propertyName);

	[Obsolete("GetTexelOffset always returns zero now, no point in using it.")]
	public Vector2 GetTexelOffset()
	{
		return Vector2.zero;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool SupportsStencil(RenderTexture rt);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern VRTextureUsage GetActiveVRUsage();

	[Obsolete("SetBorderColor is no longer supported.", true)]
	public void SetBorderColor(Color color)
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool GetIsPowerOfTwo();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool Create();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Release();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsCreated();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void GenerateMips();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void SetSRGBReadWrite(bool srgb);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("RenderTextureScripting::Create")]
	private static extern void Internal_Create([Writable] RenderTexture rt);

	private static void ValidateRenderTextureDesc(RenderTextureDescriptor desc)
	{
		if (desc.width <= 0)
		{
			throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
		}
		if (desc.height <= 0)
		{
			throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
		}
		if (desc.volumeDepth <= 0)
		{
			throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
		}
		if (desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8)
		{
			throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
		}
		if (desc.depthBufferBits != 0 && desc.depthBufferBits != 16 && desc.depthBufferBits != 24)
		{
			throw new ArgumentException("RenderTextureDesc depthBufferBits must be 0, 16, or 24.", "desc.depthBufferBits");
		}
	}

	public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
	{
		ValidateRenderTextureDesc(desc);
		desc.createdFromScript = true;
		return GetTemporary_Internal(desc);
	}

	private static RenderTexture GetTemporaryImpl(int width, int height, int depthBuffer = 0, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false)
	{
		RenderTextureDescriptor desc = new RenderTextureDescriptor(width, height, format, depthBuffer);
		desc.sRGB = readWrite != RenderTextureReadWrite.Linear;
		desc.msaaSamples = antiAliasing;
		desc.memoryless = memorylessMode;
		desc.vrUsage = vrUsage;
		desc.useDynamicScale = useDynamicScale;
		return GetTemporary(desc);
	}

	public static RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, vrUsage);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format, readWrite, antiAliasing);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format, readWrite);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
	{
		return GetTemporaryImpl(width, height, depthBuffer, format);
	}

	public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
	{
		return GetTemporaryImpl(width, height, depthBuffer);
	}

	public static RenderTexture GetTemporary(int width, int height)
	{
		return GetTemporaryImpl(width, height);
	}
}
