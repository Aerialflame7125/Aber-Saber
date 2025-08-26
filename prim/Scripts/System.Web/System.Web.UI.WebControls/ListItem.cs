using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a data item in a data-bound list control. This class cannot be inherited.</summary>
[ControlBuilder(typeof(ListItemControlBuilder))]
[TypeConverter(typeof(ExpandableObjectConverter))]
[ParseChildren(true, "Text")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ListItem : IAttributeAccessor, IParserAccessor, IStateManager
{
	private string text;

	private string value;

	private bool selected;

	private bool dirty;

	private bool enabled = true;

	private bool tracking;

	private StateBag sb;

	private AttributeCollection attrs;

	/// <summary>Gets a collection of attribute name and value pairs for the <see cref="T:System.Web.UI.WebControls.ListItem" /> that are not directly supported by the class.</summary>
	/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> that contains a collection of name and value pairs.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AttributeCollection Attributes
	{
		get
		{
			if (attrs != null)
			{
				return attrs;
			}
			if (sb == null)
			{
				sb = new StateBag(ignoreCase: true);
				if (tracking)
				{
					sb.TrackViewState();
				}
			}
			return attrs = new AttributeCollection(sb);
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>
	///     <see langword="true" /> if view state is being tracked; otherwise <see langword="false" />.  The default is <see langword="true" />.</returns>
	bool IStateManager.IsTrackingViewState => tracking;

	/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the item is selected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[TypeConverter("System.Web.UI.MinimizableAttributeTypeConverter")]
	[DefaultValue(false)]
	public bool Selected
	{
		get
		{
			return selected;
		}
		set
		{
			selected = value;
			if (tracking)
			{
				SetDirty();
			}
		}
	}

	/// <summary>Gets or sets the text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
	/// <returns>The text displayed in a list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
	public string Text
	{
		get
		{
			string empty = text;
			if (empty == null)
			{
				empty = value;
			}
			if (empty == null)
			{
				empty = string.Empty;
			}
			return empty;
		}
		set
		{
			text = value;
			if (tracking)
			{
				SetDirty();
			}
		}
	}

	/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</summary>
	/// <returns>The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. The default is <see cref="F:System.String.Empty" />.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string Value
	{
		get
		{
			string empty = value;
			if (empty == null)
			{
				empty = text;
			}
			if (empty == null)
			{
				empty = string.Empty;
			}
			return empty;
		}
		set
		{
			this.value = value;
			if (tracking)
			{
				SetDirty();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the list item is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the list item is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			enabled = value;
			if (tracking)
			{
				SetDirty();
			}
		}
	}

	internal bool HasAttributes
	{
		get
		{
			if (attrs != null)
			{
				return attrs.Count > 0;
			}
			return false;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text, value, and enabled data.</summary>
	/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
	/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />.</param>
	/// <param name="enabled">Indicates whether the <see cref="T:System.Web.UI.WebControls.ListItem" /> is enabled.</param>
	public ListItem(string text, string value, bool enabled)
		: this(text, value)
	{
		this.enabled = enabled;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text and value data.</summary>
	/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	/// <param name="value">The value associated with the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	public ListItem(string text, string value)
	{
		this.text = text;
		this.value = value;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class with the specified text data.</summary>
	/// <param name="text">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	public ListItem(string text)
		: this(text, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItem" /> class.</summary>
	public ListItem()
		: this(null, null)
	{
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.ListItem" /> from the specified text.</summary>
	/// <param name="s">The text to display in the list control for the item represented by the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that represents the text specified by the <paramref name="s" /> parameter.</returns>
	public static ListItem FromString(string s)
	{
		return new ListItem(s);
	}

	/// <summary>Determines whether the specified object has the same value and text as the current list item.</summary>
	/// <param name="o">The object to compare with the current list item.</param>
	/// <returns>
	///     <see langword="true" /> if the specified object is equivalent to the current list item; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object o)
	{
		if (!(o is ListItem listItem))
		{
			return false;
		}
		if (listItem.Text == Text)
		{
			return listItem.Value == Value;
		}
		return false;
	}

	/// <summary>Serves as a hash function for a particular type, and is suitable for use in hashing algorithms and data structures like a hash table.</summary>
	public override int GetHashCode()
	{
		return Text.GetHashCode() ^ Value.GetHashCode();
	}

	/// <summary>Returns the attribute value of the list item control having the specified attribute name.</summary>
	/// <param name="name">The name component of an attribute's name/value pair. </param>
	/// <returns>The value of the specified attribute.</returns>
	string IAttributeAccessor.GetAttribute(string key)
	{
		if (attrs == null)
		{
			return null;
		}
		return Attributes[key];
	}

	/// <summary>Sets an attribute of the list item control with the specified name and value.</summary>
	/// <param name="name">The name component of the attribute's name/value pair. </param>
	/// <param name="value">The value component of the attribute's name/value pair. </param>
	void IAttributeAccessor.SetAttribute(string key, string value)
	{
		Attributes[key] = value;
	}

	/// <summary>Allows the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property to be persisted as inner content.</summary>
	/// <param name="obj">The specified object that is parsed. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="obj" /> is a <see cref="T:System.Web.UI.DataBoundLiteralControl" />.- or -
	///         <paramref name="obj" /> is not a <see cref="T:System.Web.UI.LiteralControl" />. </exception>
	void IParserAccessor.AddParsedSubObject(object obj)
	{
		if (!(obj is LiteralControl literalControl))
		{
			throw new HttpException("'ListItem' cannot have children of type " + obj.GetType());
		}
		Text = literalControl.Text;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
	void IStateManager.LoadViewState(object state)
	{
		LoadViewState(state);
	}

	internal void LoadViewState(object state)
	{
		if (state != null)
		{
			object[] array = (object[])state;
			if (array[0] != null)
			{
				sb = new StateBag(ignoreCase: true);
				sb.LoadViewState(array[0]);
				sb.SetDirty(dirty: true);
			}
			if (array[1] != null)
			{
				text = (string)array[1];
			}
			if (array[2] != null)
			{
				value = (string)array[2];
			}
			if (array[3] != null)
			{
				selected = (bool)array[3];
			}
			if (array[4] != null)
			{
				enabled = (bool)array[4];
			}
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	internal object SaveViewState()
	{
		if (!dirty)
		{
			return null;
		}
		return new object[5]
		{
			(sb != null) ? sb.SaveViewState() : null,
			text,
			value,
			selected,
			enabled
		};
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	internal void TrackViewState()
	{
		tracking = true;
		if (sb != null)
		{
			sb.TrackViewState();
			sb.SetDirty(dirty: true);
		}
	}

	/// <summary>Returns a string that represents the current object.</summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString()
	{
		return Text;
	}

	internal void SetDirty()
	{
		dirty = true;
	}
}
