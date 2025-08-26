using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Specifies the vertical alignment of an object or text in a control.</summary>
[TypeConverter(typeof(VerticalAlignConverter))]
public enum VerticalAlign
{
	/// <summary>Vertical alignment is not set.</summary>
	NotSet,
	/// <summary>Text or object is aligned with the top of the enclosing control.</summary>
	Top,
	/// <summary>Text or object is aligned with the center of the enclosing control.</summary>
	Middle,
	/// <summary>Text or object is aligned with the bottom of the enclosing control.</summary>
	Bottom
}
