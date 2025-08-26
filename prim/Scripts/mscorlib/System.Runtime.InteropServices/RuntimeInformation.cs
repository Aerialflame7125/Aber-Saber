using System.IO;
using Mono;

namespace System.Runtime.InteropServices;

/// <summary>Provides information about the .NET runtime installation.</summary>
public static class RuntimeInformation
{
	/// <summary>Returns a string that indicates the name of the .NET installation on which an app is running.</summary>
	/// <returns>The name of the .NET installation on which the app is running.</returns>
	public static string FrameworkDescription => "Mono " + Mono.Runtime.GetDisplayName();

	/// <summary>Gets a string that describes the operating system on which the app is running.</summary>
	/// <returns>The description of the operating system on which the app is running.</returns>
	public static string OSDescription => Environment.OSVersion.VersionString;

	/// <summary>Gets the platform architecture on which the current app is running.</summary>
	/// <returns>The platform architecture on which the current app is running.</returns>
	public static Architecture OSArchitecture
	{
		get
		{
			if (!Environment.Is64BitOperatingSystem)
			{
				return Architecture.X86;
			}
			return Architecture.X64;
		}
	}

	/// <summary>Gets the process architecture of the currently running app.</summary>
	/// <returns>The process architecture of the currently running app.</returns>
	public static Architecture ProcessArchitecture
	{
		get
		{
			if (!Environment.Is64BitProcess)
			{
				return Architecture.X86;
			}
			return Architecture.X64;
		}
	}

	/// <summary>Indicates whether the current application is running on the specified platform.</summary>
	/// <param name="osPlatform">A platform.</param>
	/// <returns>
	///   <see langword="true" /> if the current app is running on the specified platform; otherwise, <see langword="false" />.</returns>
	public static bool IsOSPlatform(OSPlatform osPlatform)
	{
		switch (Environment.OSVersion.Platform)
		{
		case PlatformID.Win32NT:
			return osPlatform == OSPlatform.Windows;
		case PlatformID.Unix:
			if (File.Exists("/usr/lib/libc.dylib"))
			{
				return osPlatform == OSPlatform.OSX;
			}
			return osPlatform == OSPlatform.Linux;
		default:
			return false;
		}
	}
}
