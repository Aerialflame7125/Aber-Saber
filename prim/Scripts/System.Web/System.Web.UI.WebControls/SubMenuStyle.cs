using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style of a submenu in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
public class SubMenuStyle : Style, ICustomTypeDescriptor
{
	private const string HORZ_PADD = "HorizontalPadding";

	private const string VERT_PADD = "VerticalPadding";

	/// <summary>Gets or sets the amount of space to the left and right of a submenu.</summary>
	/// <returns>The amount of space to the left and right of the text of a submenu. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of <see cref="P:System.Web.UI.WebControls.Unit.Type" /><see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	public Unit HorizontalPadding
	{
		get
		{
			if (IsSet("HorizontalPadding"))
			{
				return (Unit)base.ViewState["HorizontalPadding"];
			}
			return Unit.Empty;
		}
		set
		{
			base.ViewState["HorizontalPadding"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space above and below a submenu.</summary>
	/// <returns>The amount of space above and below a submenu. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of <see cref="P:System.Web.UI.WebControls.Unit.Type" /><see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	public Unit VerticalPadding
	{
		get
		{
			if (IsSet("VerticalPadding"))
			{
				return (Unit)base.ViewState["VerticalPadding"];
			}
			return Unit.Empty;
		}
		set
		{
			base.ViewState["VerticalPadding"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class. </summary>
	public SubMenuStyle()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class with the specified view-state information. </summary>
	/// <param name="bag">The view-state information of the current request.</param>
	public SubMenuStyle(StateBag bag)
		: base(bag)
	{
	}

	private bool IsSet(string v)
	{
		return base.ViewState[v] != null;
	}

	/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> object to copy.</param>
	public override void CopyFrom(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.CopyFrom(s);
		if (s is SubMenuStyle subMenuStyle)
		{
			if (subMenuStyle.IsSet("HorizontalPadding"))
			{
				HorizontalPadding = subMenuStyle.HorizontalPadding;
			}
			if (subMenuStyle.IsSet("VerticalPadding"))
			{
				VerticalPadding = subMenuStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with those of the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> object to combine settings with.</param>
	public override void MergeWith(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		if (IsEmpty)
		{
			CopyFrom(s);
			return;
		}
		base.MergeWith(s);
		if (s is SubMenuStyle subMenuStyle)
		{
			if (subMenuStyle.IsSet("HorizontalPadding") && !IsSet("HorizontalPadding"))
			{
				HorizontalPadding = subMenuStyle.HorizontalPadding;
			}
			if (subMenuStyle.IsSet("VerticalPadding") && !IsSet("VerticalPadding"))
			{
				VerticalPadding = subMenuStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Returns the current instance of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> class to its original state.</summary>
	public override void Reset()
	{
		if (IsSet("HorizontalPadding"))
		{
			base.ViewState.Remove("HorizontalPadding");
		}
		if (IsSet("VerticalPadding"))
		{
			base.ViewState.Remove("VerticalPadding");
		}
		base.Reset();
	}

	/// <summary>Adds the style properties of the <see cref="T:System.Web.UI.WebControls.SubMenuStyle" /> object to the specified <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
	/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> object to which to add the style properties.</param>
	/// <param name="urlResolver">The <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL).</param>
	protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
	{
		base.FillStyleAttributes(attributes, urlResolver);
		if (IsSet("HorizontalPadding"))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingLeft, HorizontalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingRight, HorizontalPadding.ToString());
		}
		if (IsSet("VerticalPadding"))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingTop, VerticalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingBottom, VerticalPadding.ToString());
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetAttributes" />.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
	[MonoTODO("Not implemented")]
	System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetClassName" />.</summary>
	/// <returns>The class name of the object, or <see langword="null" /> if the class does not have a name.</returns>
	[MonoTODO("Not implemented")]
	string ICustomTypeDescriptor.GetClassName()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetComponentName" />.</summary>
	/// <returns>The name of the object, or <see langword="null" /> if the object does not have a name.</returns>
	[MonoTODO("Not implemented")]
	string ICustomTypeDescriptor.GetComponentName()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetConverter" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or <see langword="null" /> if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
	[MonoTODO("Not implemented")]
	TypeConverter ICustomTypeDescriptor.GetConverter()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent" />.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> object that represents the default event for the object, or <see langword="null" /> if the object has no events.</returns>
	[MonoTODO("Not implemented")]
	EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object that represents the default property for this object, or <see langword="null" /> if the object does not have properties.</returns>
	[MonoTODO("Not implemented")]
	PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEditor(System.Type)" />.</summary>
	/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
	/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or <see langword="null" /> if the editor cannot be found.</returns>
	[MonoTODO("Not implemented")]
	object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents" />.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that contains the events for this instance.</returns>
	[MonoTODO("Not implemented")]
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents(System.Attribute[])" />.</summary>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that contains the filtered events for this instance.</returns>
	[MonoTODO("Not implemented")]
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] arr)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties for this instance.</returns>
	[MonoTODO("Not implemented")]
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[])" />.</summary>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object that contains the filtered properties for this instance.</returns>
	[MonoTODO("Not implemented")]
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] arr)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor)" />.</summary>
	/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
	[MonoTODO("Not implemented")]
	object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
	{
		throw new NotImplementedException();
	}
}
