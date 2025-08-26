using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering;

[UsedByNativeCode]
public struct VisibleLight
{
	public LightType lightType;

	public Color finalColor;

	public Rect screenRect;

	public Matrix4x4 localToWorld;

	public float range;

	public float spotAngle;

	private int instanceId;

	public VisibleLightFlags flags;

	public Light light => GetLightObject(instanceId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern Light GetLightObject(int instanceId);
}
