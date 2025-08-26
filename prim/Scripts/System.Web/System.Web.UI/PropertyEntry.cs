using System.Reflection;

namespace System.Web.UI;

/// <summary>Acts as the base class for all property entry classes.</summary>
public abstract class PropertyEntry
{
	private Type type;

	private string name;

	private string filter;

	private PropertyInfo pinfo;

	/// <summary>Gets the type of the class that declares this member.</summary>
	/// <returns>The <see cref="T:System.Type" /> that declares this member.</returns>
	public Type DeclaringType => pinfo.DeclaringType;

	/// <summary>Gets or sets the value pertaining to the filter portion of an expression.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the value of the filter on an expression.</returns>
	public string Filter
	{
		get
		{
			return filter;
		}
		set
		{
			filter = value;
		}
	}

	/// <summary>Gets or sets the property name that the expression applies to.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the property name.</returns>
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets or sets an object containing attributes of the property the expression applies to.</summary>
	/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> containing the attributes of the property.</returns>
	public PropertyInfo PropertyInfo
	{
		get
		{
			return pinfo;
		}
		set
		{
			pinfo = value;
		}
	}

	/// <summary>Gets or sets the type of the entry.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the entry.</returns>
	public Type Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}

	internal PropertyEntry()
	{
	}
}
