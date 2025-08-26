using System;

namespace UnityEngine.Experimental.Rendering;

[Flags]
public enum RendererConfiguration
{
	None = 0,
	PerObjectLightProbe = 1,
	PerObjectReflectionProbes = 2,
	PerObjectLightProbeProxyVolume = 4,
	PerObjectLightmaps = 8,
	ProvideLightIndices = 0x10,
	PerObjectMotionVectors = 0x20,
	PerObjectLightIndices8 = 0x40,
	ProvideReflectionProbeIndices = 0x80,
	PerObjectOcclusionProbe = 0x100,
	PerObjectOcclusionProbeProxyVolume = 0x200,
	PerObjectShadowMask = 0x400
}
