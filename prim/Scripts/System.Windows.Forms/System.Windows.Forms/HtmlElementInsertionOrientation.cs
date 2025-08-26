namespace System.Windows.Forms;

/// <summary>Defines values that describe where to insert a new element when using <see cref="M:System.Windows.Forms.HtmlElement.InsertAdjacentElement(System.Windows.Forms.HtmlElementInsertionOrientation,System.Windows.Forms.HtmlElement)" />.</summary>
/// <filterpriority>2</filterpriority>
public enum HtmlElementInsertionOrientation
{
	/// <summary>Insert the element before the current element.</summary>
	BeforeBegin,
	/// <summary>Insert the element after the current element, but before all other content in the current element.</summary>
	AfterBegin,
	/// <summary>Insert the element after the current element.</summary>
	BeforeEnd,
	/// <summary>Insert the element after the current element, but after all other content in the current element.</summary>
	AfterEnd
}
