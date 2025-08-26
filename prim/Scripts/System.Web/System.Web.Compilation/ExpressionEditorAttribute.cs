namespace System.Web.Compilation;

/// <summary>Specifies the design-time editor of the expression builder. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ExpressionEditorAttribute : Attribute
{
	private string _editorTypeName;

	/// <summary>Used by an expression editor to retrieve the editor type name.</summary>
	/// <returns>The name of the editor type.</returns>
	public string EditorTypeName => _editorTypeName;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type object.</summary>
	/// <param name="type">The type reference to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
	public ExpressionEditorAttribute(Type type)
		: this((type != null) ? type.AssemblyQualifiedName : null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type name.</summary>
	/// <param name="typeName">The name of the type to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="typeName" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).</exception>
	public ExpressionEditorAttribute(string typeName)
	{
		if (string.IsNullOrEmpty(typeName))
		{
			throw new ArgumentNullException("typeName");
		}
		_editorTypeName = typeName;
	}

	/// <summary>Indicates whether this instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class and a specified object are equal.</summary>
	/// <param name="obj">An instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class or a class that derives from it.</param>
	/// <returns>
	///     <see langword="true" /> if value is not <see langword="null" /> and <see cref="P:System.Web.Compilation.ExpressionEditorAttribute.EditorTypeName" /> is equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is ExpressionEditorAttribute expressionEditorAttribute)
		{
			return expressionEditorAttribute.EditorTypeName == EditorTypeName;
		}
		return false;
	}

	/// <summary>Retrieves the hash code for the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> attribute.</summary>
	/// <returns>The hash code of the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" />.</returns>
	public override int GetHashCode()
	{
		return EditorTypeName.GetHashCode();
	}
}
