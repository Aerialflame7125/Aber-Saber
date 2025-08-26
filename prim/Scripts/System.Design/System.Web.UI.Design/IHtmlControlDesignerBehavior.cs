namespace System.Web.UI.Design;

/// <summary>Defines an interface that enables the extension of specific behaviors of an HTML control designer.</summary>
[Obsolete("Use IControlDesignerTag and IControlDesignerView instead")]
public interface IHtmlControlDesignerBehavior
{
	/// <summary>Gets or sets the designer that the behavior is associated with.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.HtmlControlDesigner" /> that the behavior is associated with.  
	///
	///  The <see cref="T:System.Web.UI.Design.IHtmlControlDesignerBehavior" /> interface is obsolete. Use the <see cref="T:System.Web.UI.Design.IControlDesignerTag" /> and <see cref="T:System.Web.UI.Design.IControlDesignerView" /> interfaces for equivalent control designer functionality.</returns>
	HtmlControlDesigner Designer { get; set; }

	/// <summary>Gets the element that the designer is associated with.</summary>
	/// <returns>The object that the designer is associated with.  
	///
	///  The <see cref="T:System.Web.UI.Design.IHtmlControlDesignerBehavior" /> interface is obsolete. Use the <see cref="T:System.Web.UI.Design.IControlDesignerTag" /> and <see cref="T:System.Web.UI.Design.IControlDesignerView" /> interfaces for equivalent control designer functionality.</returns>
	object DesignTimeElement { get; }

	/// <summary>Gets the specified attribute.</summary>
	/// <param name="attribute">The attribute to retrieve.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	/// <returns>The attribute that was retrieved.</returns>
	object GetAttribute(string attribute, bool ignoreCase);

	/// <summary>Gets the specified style attribute.</summary>
	/// <param name="attribute">The style attribute to retrieve.</param>
	/// <param name="designTimeOnly">
	///   <see langword="true" /> if the attribute is only active at design time; otherwise, <see langword="false" />.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	/// <returns>The style attribute that was retrieved.</returns>
	object GetStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

	/// <summary>Removes the specified attribute.</summary>
	/// <param name="attribute">The attribute to remove.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	void RemoveAttribute(string attribute, bool ignoreCase);

	/// <summary>Removes the specified style attribute.</summary>
	/// <param name="attribute">The style attribute to remove.</param>
	/// <param name="designTimeOnly">
	///   <see langword="true" /> if the attribute is only active at design time; otherwise, <see langword="false" />.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	void RemoveStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

	/// <summary>Sets the specified attribute to the specified object.</summary>
	/// <param name="attribute">The attribute to set.</param>
	/// <param name="value">The object on which to set the attribute.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	void SetAttribute(string attribute, object value, bool ignoreCase);

	/// <summary>Sets the specified style attribute to the specified object.</summary>
	/// <param name="attribute">The attribute to set.</param>
	/// <param name="designTimeOnly">
	///   <see langword="true" /> if the attribute is only active at design-time; otherwise, <see langword="false" />.</param>
	/// <param name="value">The object to set the attribute on.</param>
	/// <param name="ignoreCase">
	///   <see langword="true" /> if the attribute syntax is case-insensitive; otherwise, <see langword="false" />.</param>
	void SetStyleAttribute(string attribute, bool designTimeOnly, object value, bool ignoreCase);
}
