using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Contains information about a single data-binding expression in an ASP.NET server control, which allows rapid-application development (RAD) designers, such as Microsoft Visual Studio, to create data-binding expressions at design time. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataBinding
{
	private string propertyName;

	private Type propertyType;

	private string expression;

	/// <summary>Gets or sets the data-binding expression to be evaluated.</summary>
	/// <returns>The data-binding expression to be evaluated.</returns>
	public string Expression
	{
		get
		{
			return expression;
		}
		set
		{
			expression = value;
		}
	}

	/// <summary>Gets the name of the ASP.NET server control property to bind data to.</summary>
	/// <returns>The property to bind data to.</returns>
	public string PropertyName => propertyName;

	/// <summary>Gets the .NET Framework type of the data-bound ASP.NET server control property.</summary>
	/// <returns>The .NET Framework type of the data-bound property.</returns>
	public Type PropertyType => propertyType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBinding" /> class.</summary>
	/// <param name="propertyName">The property to bind data to. </param>
	/// <param name="propertyType">The .NET Framework type of the property to bind data to. </param>
	/// <param name="expression">The data-binding expression to be evaluated. </param>
	public DataBinding(string propertyName, Type propertyType, string expression)
	{
		this.propertyName = propertyName;
		this.propertyType = propertyType;
		this.expression = expression;
	}

	/// <summary>Determines whether the specified object is the same instance of the <see cref="T:System.Web.UI.DataBinding" /> class as the current object.</summary>
	/// <param name="obj">The object to compare against the current <see cref="T:System.Web.UI.DataBinding" />. </param>
	/// <returns>
	///     <see langword="true" /> if the data-binding property names match; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is DataBinding dataBinding))
		{
			return false;
		}
		if (dataBinding.Expression == expression && dataBinding.PropertyName == propertyName)
		{
			return dataBinding.PropertyType == propertyType;
		}
		return false;
	}

	/// <summary>Retrieves the hash code for an instance of the <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return propertyName.GetHashCode() + (propertyType.GetHashCode() << 1) + (expression.GetHashCode() << 2);
	}
}
