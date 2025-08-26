using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering;

public struct DrawRendererSettings
{
	[StructLayout(LayoutKind.Sequential, Size = 64)]
	[UnsafeValueType]
	[CompilerGenerated]
	public struct _003CshaderPassNames_003E__FixedBuffer0
	{
		public int FixedElementField;
	}

	private const int kMaxShaderPasses = 16;

	public static readonly int maxShaderPasses = 16;

	public DrawRendererSortSettings sorting;

	private _003CshaderPassNames_003E__FixedBuffer0 shaderPassNames;

	public RendererConfiguration rendererConfiguration;

	public DrawRendererFlags flags;

	private int m_OverrideMaterialInstanceId;

	private int m_OverrideMaterialPassIdx;

	public unsafe DrawRendererSettings(Camera camera, ShaderPassName shaderPassName)
	{
		rendererConfiguration = RendererConfiguration.None;
		flags = DrawRendererFlags.EnableInstancing;
		m_OverrideMaterialInstanceId = 0;
		m_OverrideMaterialPassIdx = 0;
		fixed (int* ptr = &shaderPassNames.FixedElementField)
		{
			for (int i = 0; i < maxShaderPasses; i++)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, i) = -1;
			}
		}
		fixed (int* ptr2 = &shaderPassNames.FixedElementField)
		{
			*ptr2 = shaderPassName.nameIndex;
		}
		rendererConfiguration = RendererConfiguration.None;
		flags = DrawRendererFlags.EnableInstancing;
		InitializeSortSettings(camera, out sorting);
	}

	public void SetOverrideMaterial(Material mat, int passIndex)
	{
		if (mat == null)
		{
			m_OverrideMaterialInstanceId = 0;
		}
		else
		{
			m_OverrideMaterialInstanceId = mat.GetInstanceID();
		}
		m_OverrideMaterialPassIdx = passIndex;
	}

	public unsafe void SetShaderPassName(int index, ShaderPassName shaderPassName)
	{
		if (index >= maxShaderPasses || index < 0)
		{
			throw new ArgumentOutOfRangeException("index", $"Index should range from 0 - DrawRendererSettings.maxShaderPasses ({maxShaderPasses}), was {index}");
		}
		fixed (int* ptr = &shaderPassNames.FixedElementField)
		{
			System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index) = shaderPassName.nameIndex;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void InitializeSortSettings(Camera camera, out DrawRendererSortSettings sortSettings);
}
