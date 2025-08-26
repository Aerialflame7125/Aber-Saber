using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA;

[NativeHeader("Runtime/VR/HoloLens/HolographicSettings.h")]
[StaticAccessor("HolographicSettings::GetInstance()", StaticAccessorType.Dot)]
public class HolographicSettings
{
	public enum HolographicReprojectionMode
	{
		PositionAndOrientation,
		OrientationOnly,
		Disabled
	}

	[NativeConditional("ENABLE_HOLOLENS_MODULE")]
	public static extern bool IsContentProtectionEnabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static HolographicReprojectionMode ReprojectionMode
	{
		get
		{
			return HolographicReprojectionMode.Disabled;
		}
		set
		{
		}
	}

	[Obsolete("Support for toggling latent frame presentation has been removed, and IsLatentFramePresentation will always return true", false)]
	public static bool IsLatentFramePresentation => true;

	[Obsolete("Support for toggling latent frame presentation has been removed", true)]
	public static void ActivateLatentFramePresentation(bool activated)
	{
	}
}
