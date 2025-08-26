namespace System.Web.UI;

/// <summary>Adds Cascading Style Sheet (CSS) editing capabilities to a property at design time.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class CssClassPropertyAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CssClassPropertyAttribute" /> class.</summary>
	public CssClassPropertyAttribute()
	{
	}
}
