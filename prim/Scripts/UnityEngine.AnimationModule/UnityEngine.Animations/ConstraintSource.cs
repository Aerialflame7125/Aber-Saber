using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations;

[Serializable]
[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Runtime/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
[UsedByNativeCode]
[NativeHeader("Runtime/Animation/Constraints/Constraint.bindings.h")]
public struct ConstraintSource
{
	[NativeName("sourceTransform")]
	private Transform m_SourceTransform;

	[NativeName("weight")]
	private float m_Weight;

	public Transform sourceTransform
	{
		get
		{
			return m_SourceTransform;
		}
		set
		{
			m_SourceTransform = value;
		}
	}

	public float weight
	{
		get
		{
			return m_Weight;
		}
		set
		{
			m_Weight = value;
		}
	}
}
