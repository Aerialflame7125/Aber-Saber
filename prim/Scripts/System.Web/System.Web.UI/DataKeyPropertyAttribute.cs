namespace System.Web.UI;

/// <summary>Specifies the default property of a control that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> property binds to at run time.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DataKeyPropertyAttribute : Attribute
{
	private readonly string _name;

	/// <summary>Gets the name of the data-key property attribute.</summary>
	/// <returns>The name of the data-key property attribute.</returns>
	public string Name => _name;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class by using the name of the data-key property attribute.</summary>
	/// <param name="name">The name of the data-key property attribute.</param>
	public DataKeyPropertyAttribute(string name)
	{
		_name = name;
	}

	/// <summary>Compares the name of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> object to a specified object.</summary>
	/// <param name="obj">The object to compare.</param>
	/// <returns>
	///     <see langword="true" /> if the name is the same as the object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is DataKeyPropertyAttribute dataKeyPropertyAttribute)
		{
			return string.Equals(_name, dataKeyPropertyAttribute.Name, StringComparison.Ordinal);
		}
		return false;
	}

	/// <summary>Gets the hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</summary>
	/// <returns>The hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</returns>
	public override int GetHashCode()
	{
		if (Name == null)
		{
			return 0;
		}
		return Name.GetHashCode();
	}
}
