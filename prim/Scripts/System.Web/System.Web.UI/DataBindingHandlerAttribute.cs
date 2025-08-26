namespace System.Web.UI;

/// <summary>Specifies a design-time class that performs data binding of controls within a designer. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DataBindingHandlerAttribute : Attribute
{
	private string _typeName;

	/// <summary>Defines the default attribute for the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class.</summary>
	public static readonly DataBindingHandlerAttribute Default = new DataBindingHandlerAttribute();

	/// <summary>Gets the type name of the data-binding handler. </summary>
	/// <returns>The type name of the handler. If the type name is <see langword="null" />, this property returns an empty string ("").</returns>
	public string HandlerTypeName
	{
		get
		{
			if (_typeName == null)
			{
				return string.Empty;
			}
			return _typeName;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class using no parameters. This is the default constructor.</summary>
	public DataBindingHandlerAttribute()
	{
		_typeName = string.Empty;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class of the specified <see cref="T:System.Type" />.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> for the data-binding handler. </param>
	public DataBindingHandlerAttribute(Type type)
	{
		_typeName = type.AssemblyQualifiedName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class with the specified type name.</summary>
	/// <param name="typeName">The fully qualified name of the data-binding handler <see cref="T:System.Type" />. </param>
	public DataBindingHandlerAttribute(string typeName)
	{
		_typeName = typeName;
	}

	/// <summary>Determines whether two object instances are equal.</summary>
	/// <param name="obj">The object to compare to the current <see cref="T:System.Web.UI.DataBindingHandlerAttribute" />.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="obj" /> parameter equals the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is DataBindingHandlerAttribute dataBindingHandlerAttribute)
		{
			return string.Compare(HandlerTypeName, dataBindingHandlerAttribute.HandlerTypeName, StringComparison.Ordinal) == 0;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return HandlerTypeName.GetHashCode();
	}
}
