using System.ComponentModel;

namespace System.Windows.Forms.PropertyGridInternal;

/// <summary>Defines methods and a property that allow filtering on specific attributes.</summary>
public interface IRootGridEntry
{
	/// <summary>Gets or sets the attributes on which the property browser filters.</summary>
	/// <returns>The attributes on which the property browser filters.</returns>
	AttributeCollection BrowsableAttributes { get; set; }

	/// <summary>Sorts the properties in the property browser.</summary>
	/// <param name="showCategories">true to group the properties by category; otherwise, false.</param>
	void ShowCategories(bool showCategories);

	/// <summary>Resets the <see cref="P:System.Windows.Forms.PropertyGridInternal.IRootGridEntry.BrowsableAttributes" /> property to the default value.</summary>
	void ResetBrowsableAttributes();
}
