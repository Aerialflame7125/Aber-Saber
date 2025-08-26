namespace System.Web.UI;

/// <summary>Provides an interface that classes implement to provide navigation user interface data and values to navigation controls. </summary>
public interface INavigateUIData
{
	/// <summary>Gets text that represents the description of a navigation node of a navigation control.</summary>
	/// <returns>Text that is the description of a node of a navigation control; otherwise, <see langword="null" />.</returns>
	string Description { get; }

	/// <summary>Gets the text that represents the name of a navigation node of a navigation control.</summary>
	/// <returns>Text that represents the name of a node of a navigation control; otherwise, <see langword="null" />.</returns>
	string Name { get; }

	/// <summary>Gets the URL to navigate to when the navigation node is clicked.</summary>
	/// <returns>The URL to navigate to when the node is clicked; otherwise, <see langword="null" />.</returns>
	string NavigateUrl { get; }

	/// <summary>Gets a non-displayed value that is used to store any additional data about the navigation node.</summary>
	/// <returns>A value that is not displayed and is used to store additional data about the navigation node; otherwise, <see langword="null" />.</returns>
	string Value { get; }
}
