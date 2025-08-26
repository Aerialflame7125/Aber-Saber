using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEditor.Experimental;

public sealed class RenderSettings
{
	public static extern bool useRadianceAmbientProbe
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}
}
