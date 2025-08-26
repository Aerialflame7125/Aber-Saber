using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering;

public struct ShaderPassName
{
	private int m_NameIndex;

	internal int nameIndex => m_NameIndex;

	public ShaderPassName(string name)
	{
		m_NameIndex = Init(name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int Init(string name);
}
