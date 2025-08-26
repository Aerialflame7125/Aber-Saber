using System.Runtime.CompilerServices;

namespace System;

internal static class LocalAppContextSwitches
{
	private static int _zipFileUseBackslash;

	internal const string ZipFileUseBackslashName = "Switch.System.IO.Compression.ZipFile.UseBackslash";

	public static bool ZipFileUseBackslash
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return LocalAppContext.GetCachedSwitchValue("Switch.System.IO.Compression.ZipFile.UseBackslash", ref _zipFileUseBackslash);
		}
	}
}
