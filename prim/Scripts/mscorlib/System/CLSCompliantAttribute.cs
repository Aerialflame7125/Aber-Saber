using System.Runtime.InteropServices;

namespace System;

/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
[Serializable]
[ComVisible(true)]
[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public sealed class CLSCompliantAttribute : Attribute
{
	private bool m_compliant;

	/// <summary>Gets the Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
	/// <returns>
	///   <see langword="true" /> if the program element is CLS-compliant; otherwise, <see langword="false" />.</returns>
	public bool IsCompliant => m_compliant;

	/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
	/// <param name="isCompliant">
	///   <see langword="true" /> if CLS-compliant; otherwise, <see langword="false" />.</param>
	public CLSCompliantAttribute(bool isCompliant)
	{
		m_compliant = isCompliant;
	}
}
