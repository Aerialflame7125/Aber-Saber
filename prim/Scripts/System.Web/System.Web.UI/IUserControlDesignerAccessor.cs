namespace System.Web.UI;

/// <summary>Defines the properties that allow the designer to access information about a user control at design time.</summary>
public interface IUserControlDesignerAccessor
{
	/// <summary>When implemented, gets or sets text between the opening and closing tags of a user control.</summary>
	/// <returns>The text placed between the opening and closing tags of a user control.</returns>
	string InnerText { get; set; }

	/// <summary>When implemented, gets or sets the full tag name of the user control.</summary>
	/// <returns>The full tag name of the user control.</returns>
	string TagName { get; set; }
}
