using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Windows;

public static class CrashReporting
{
	[ThreadAndSerializationSafe]
	public static extern string crashReportFolder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}
}
