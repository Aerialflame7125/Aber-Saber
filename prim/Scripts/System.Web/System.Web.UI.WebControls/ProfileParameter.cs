using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of an ASP.NET Profile property to a parameter object. </summary>
[DefaultProperty("PropertyName")]
public class ProfileParameter : Parameter
{
	/// <summary>Gets or sets the name of the ASP.NET Profile property that the parameter binds to.</summary>
	/// <returns>A string that identifies the ASP.NET Profile property that the parameter binds to.</returns>
	[DefaultValue("")]
	public string PropertyName
	{
		get
		{
			object obj = base.ViewState["PropertyName"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			base.ViewState["PropertyName"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class.</summary>
	public ProfileParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance from which the current instance is initialized.</param>
	protected ProfileParameter(ProfileParameter original)
		: base(original)
	{
		PropertyName = original.PropertyName;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
	public ProfileParameter(string name, string propertyName)
		: base(name)
	{
		PropertyName = propertyName;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />.</param>
	/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
	public ProfileParameter(string name, TypeCode type, string propertyName)
		: base(name, type)
	{
		PropertyName = propertyName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to. </summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="dbType">The database type that the parameter represents. </param>
	/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to.</param>
	public ProfileParameter(string name, DbType dbType, string propertyName)
		: base(name, dbType)
	{
		PropertyName = propertyName;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object that is an exact duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new ProfileParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Form where the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> is used.</param>
	/// <returns>An object that represents the updated and current value of the parameter. If the context or the ASP.NET Profile is null (<see langword="Nothing" /> in Visual Basic), the <see cref="M:System.Web.UI.WebControls.ProfileParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (context == null || context.Profile == null)
		{
			return null;
		}
		if (string.IsNullOrEmpty(PropertyName))
		{
			return null;
		}
		return context.Profile[PropertyName];
	}
}
