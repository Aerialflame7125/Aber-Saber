using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms;

/// <summary>Represents a group of items displayed within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[Serializable]
[DefaultProperty("Header")]
[DesignTimeVisible(false)]
[ToolboxItem(false)]
[TypeConverter(typeof(ListViewGroupConverter))]
public sealed class ListViewGroup : ISerializable
{
	internal string header = string.Empty;

	private string name;

	private HorizontalAlignment header_alignment;

	private ListView list_view_owner;

	private ListView.ListViewItemCollection items;

	private object tag;

	private Rectangle header_bounds = Rectangle.Empty;

	internal int starting_row;

	internal int starting_item;

	internal int rows;

	internal int current_item;

	internal Point items_area_location;

	private bool is_default_group;

	private int item_count;

	/// <summary>Gets or sets the header text for the group.</summary>
	/// <returns>The text to display for the group header. The default is "ListViewGroup".</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public string Header
	{
		get
		{
			return header;
		}
		set
		{
			if (!header.Equals(value))
			{
				header = value;
				if (list_view_owner != null)
				{
					list_view_owner.Redraw(recalculate: true);
				}
			}
		}
	}

	/// <summary>Gets or sets the alignment of the group header text.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.HorizontalAlignment" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment HeaderAlignment
	{
		get
		{
			return header_alignment;
		}
		set
		{
			if (!header_alignment.Equals(value))
			{
				if (value != 0 && value != HorizontalAlignment.Right && value != HorizontalAlignment.Center)
				{
					throw new InvalidEnumArgumentException("HeaderAlignment", (int)value, typeof(HorizontalAlignment));
				}
				header_alignment = value;
				if (list_view_owner != null)
				{
					list_view_owner.Redraw(recalculate: true);
				}
			}
		}
	}

	/// <summary>Gets a collection containing all items associated with this group.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the group. If there are no items in the group, an empty <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> object is returned.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ListView.ListViewItemCollection Items => items;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains this group. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListView" /> control that contains this group.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ListView ListView => list_view_owner;

	internal ListView ListViewOwner
	{
		get
		{
			return list_view_owner;
		}
		set
		{
			list_view_owner = value;
			if (!is_default_group)
			{
				items.Owner = value;
			}
		}
	}

	internal Rectangle HeaderBounds
	{
		get
		{
			Rectangle result = header_bounds;
			result.X -= list_view_owner.h_marker;
			result.Y -= list_view_owner.v_marker;
			return result;
		}
		set
		{
			if (list_view_owner != null)
			{
				list_view_owner.item_control.Invalidate(HeaderBounds);
			}
			header_bounds = value;
			if (list_view_owner != null)
			{
				list_view_owner.item_control.Invalidate(HeaderBounds);
			}
		}
	}

	internal bool IsDefault
	{
		get
		{
			return is_default_group;
		}
		set
		{
			is_default_group = value;
		}
	}

	internal int ItemCount
	{
		get
		{
			return (!is_default_group) ? items.Count : item_count;
		}
		set
		{
			if (!is_default_group)
			{
				throw new InvalidOperationException("ItemCount cannot be set for non-default groups.");
			}
			item_count = value;
		}
	}

	/// <summary>Gets or sets the name of the group.</summary>
	/// <returns>The name of the group.</returns>
	[DefaultValue("")]
	[Browsable(true)]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets or sets the object that contains data about the group.</summary>
	/// <returns>An <see cref="T:System.Object" /> for storing the additional data. </returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	[Localizable(false)]
	[Bindable(true)]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the default header text of "ListViewGroup" and the default left header alignment.</summary>
	public ListViewGroup()
		: this("ListViewGroup", HorizontalAlignment.Left)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property and using the default left header alignment.</summary>
	/// <param name="header">The text to display for the group header. </param>
	public ListViewGroup(string header)
		: this(header, HorizontalAlignment.Left)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties. </summary>
	/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property.</param>
	/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property.</param>
	public ListViewGroup(string key, string headerText)
		: this(headerText, HorizontalAlignment.Left)
	{
		name = key;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified header text and the specified header alignment.</summary>
	/// <param name="header">The text to display for the group header. </param>
	/// <param name="headerAlignment">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. </param>
	public ListViewGroup(string header, HorizontalAlignment headerAlignment)
	{
		this.header = header;
		header_alignment = headerAlignment;
		items = new ListView.ListViewItemCollection(list_view_owner, this);
	}

	private ListViewGroup(SerializationInfo info, StreamingContext context)
	{
		header = info.GetString("Header");
		name = info.GetString("Name");
		header_alignment = (HorizontalAlignment)info.GetInt32("HeaderAlignment");
		tag = info.GetValue("Tag", typeof(object));
		int @int = info.GetInt32("ListViewItemCount");
		if (@int > 0)
		{
			if (items == null)
			{
				items = new ListView.ListViewItemCollection(list_view_owner);
			}
			for (int i = 0; i < @int; i++)
			{
				items.Add((ListViewItem)info.GetValue($"ListViewItem_{i}", typeof(ListViewItem)));
			}
		}
	}

	/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
	/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Header", header);
		info.AddValue("Name", name);
		info.AddValue("HeaderAlignment", header_alignment);
		info.AddValue("Tag", tag);
		info.AddValue("ListViewItemCount", items.Count);
		int num = 0;
		foreach (ListViewItem item in items)
		{
			info.AddValue($"ListViewItem_{num}", item);
			num++;
		}
	}

	internal int GetActualItemCount()
	{
		if (is_default_group)
		{
			return item_count;
		}
		int num = 0;
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].ListView != null)
			{
				num++;
			}
		}
		return num;
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return header;
	}
}
