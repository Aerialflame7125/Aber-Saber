namespace System.Web.UI.Design;

/// <summary>Provides an interface for design-time access to the HTML markup for a control that is associated with a control designer.</summary>
public interface IControlDesignerTag
{
	/// <summary>Gets a value indicating whether or not an attribute or the content of a tag has changed.</summary>
	/// <returns>
	///   <see langword="true" /> if the tag has changed; otherwise, <see langword="false" />.</returns>
	bool IsDirty { get; }

	/// <summary>Retrieves the value of the identified attribute on the tag.</summary>
	/// <param name="name">The name of the attribute.</param>
	/// <returns>A string containing the value of the attribute.</returns>
	string GetAttribute(string name);

	/// <summary>Retrieves the HTML markup for the content of the tag.</summary>
	/// <returns>The HTML markup for the content of the tag.</returns>
	string GetContent();

	/// <summary>Retrieves the complete HTML markup for the control, including the outer tags.</summary>
	/// <returns>The outer HTML markup for the control.</returns>
	string GetOuterContent();

	/// <summary>Deletes the specified attribute from the tag.</summary>
	/// <param name="name">The name of the attribute.</param>
	void RemoveAttribute(string name);

	/// <summary>Sets the value of the specified attribute and creates the attribute, if necessary.</summary>
	/// <param name="name">The attribute name.</param>
	/// <param name="value">The attribute value.</param>
	void SetAttribute(string name, string value);

	/// <summary>Sets the HTML markup for the content of the tag.</summary>
	/// <param name="content">The HTML markup for the content of the tag.</param>
	void SetContent(string content);

	/// <summary>Sets the <see cref="P:System.Web.UI.Design.IControlDesignerTag.IsDirty" /> property of the tag.</summary>
	/// <param name="dirty">The value for the <see cref="P:System.Web.UI.Design.IControlDesignerTag.IsDirty" /> property.</param>
	void SetDirty(bool dirty);
}
