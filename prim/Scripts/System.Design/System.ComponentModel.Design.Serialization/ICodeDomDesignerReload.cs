using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides an interface that can be used to optimize the reloading of a designer.</summary>
public interface ICodeDomDesignerReload
{
	/// <summary>Indicates whether the designer should reload in order to import the specified compile unit correctly.</summary>
	/// <param name="newTree">A <see cref="T:System.CodeDom.CodeCompileUnit" /> containing the designer document code.</param>
	/// <returns>
	///   <see langword="true" /> if the designer should reload; otherwise, <see langword="false" />.</returns>
	bool ShouldReloadDesigner(CodeCompileUnit newTree);
}
