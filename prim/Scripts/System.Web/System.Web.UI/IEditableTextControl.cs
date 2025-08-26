namespace System.Web.UI;

/// <summary>Represents a control that renders text that can be changed by the user.</summary>
public interface IEditableTextControl : ITextControl
{
	/// <summary>Occurs when the content of the text changes between posts to the server. </summary>
	event EventHandler TextChanged;
}
