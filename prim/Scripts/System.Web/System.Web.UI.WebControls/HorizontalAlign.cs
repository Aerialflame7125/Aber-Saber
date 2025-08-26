using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Specifies the horizontal alignment of items within a container.</summary>
[TypeConverter(typeof(HorizontalAlignConverter))]
public enum HorizontalAlign
{
	/// <summary>The horizontal alignment is not set.</summary>
	NotSet,
	/// <summary>The contents of a container are left justified.</summary>
	Left,
	/// <summary>The contents of a container are centered.</summary>
	Center,
	/// <summary>The contents of a container are right justified.</summary>
	Right,
	/// <summary>The contents of a container are uniformly spread out and aligned with both the left and right margins.</summary>
	Justify
}
