using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Provides the base class for types that represent a panel item on a smart tag panel.</summary>
public abstract class DesignerActionItem
{
	private bool allow_associate;

	private string category;

	private string description;

	private string display_name;

	private IDictionary properties;

	/// <summary>Gets or sets a value indicating whether to allow this item to be placed into a group of items that have the same <see cref="P:System.ComponentModel.Design.DesignerActionItem.Category" /> property value.</summary>
	/// <returns>
	///   <see langword="true" /> if the item can be grouped; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool AllowAssociate
	{
		get
		{
			return allow_associate;
		}
		set
		{
			allow_associate = value;
		}
	}

	/// <summary>Gets the group name for an item.</summary>
	/// <returns>A string that represents the group that the item is a member of.</returns>
	public virtual string Category => category;

	/// <summary>Gets the supplemental text for the item.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the descriptive text for the item.</returns>
	public virtual string Description => description;

	/// <summary>Gets the text for this item.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the display text for the item.</returns>
	public virtual string DisplayName => display_name;

	/// <summary>Gets a reference to a collection that can be used to store programmer-defined key/value pairs.</summary>
	/// <returns>A collection that implements <see cref="T:System.Collections.IDictionary" />.</returns>
	public IDictionary Properties
	{
		get
		{
			if (properties == null)
			{
				properties = new Hashtable();
			}
			return properties;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> class.</summary>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> that defines the groupings of panel entries.</param>
	/// <param name="description">Supplemental text for this item, potentially used in ToolTips or the status bar.</param>
	public DesignerActionItem(string displayName, string category, string description)
	{
		display_name = displayName;
		this.description = description;
		this.category = category;
	}
}
