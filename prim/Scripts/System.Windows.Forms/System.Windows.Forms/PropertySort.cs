using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies how properties are sorted in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public enum PropertySort
{
	/// <summary>Properties are displayed in the order in which they are retrieved from the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	NoSort,
	/// <summary>Properties are sorted in an alphabetical list.</summary>
	Alphabetical,
	/// <summary>Properties are displayed according to their category in a group. The categories are defined by the properties themselves.</summary>
	Categorized,
	/// <summary>Properties are displayed according to their category in a group. The properties are further sorted alphabetically within the group. The categories are defined by the properties themselves.</summary>
	CategorizedAlphabetical
}
