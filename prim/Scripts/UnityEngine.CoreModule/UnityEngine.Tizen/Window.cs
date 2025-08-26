using System;
using UnityEngine.Bindings;

namespace UnityEngine.Tizen;

[NativeHeader("PlatformDependent/TizenPlayer/TizenBindings.h")]
[NativeConditional("UNITY_TIZEN_API")]
public sealed class Window
{
	public unsafe static IntPtr windowHandle => (IntPtr)(void*)null;

	public static IntPtr evasGL => IntPtr.Zero;
}
