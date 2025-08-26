namespace System.Web.UI;

/// <summary>Defines the metadata attribute that ASP.NET server controls use to specify whether they participate in loading view-state information by <see cref="P:System.Web.UI.Control.ID" />. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ViewStateModeByIdAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateModeByIdAttribute" /> class.</summary>
	public ViewStateModeByIdAttribute()
	{
	}
}
