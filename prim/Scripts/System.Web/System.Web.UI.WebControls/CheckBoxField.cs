using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a Boolean field that is displayed as a check box in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CheckBoxField : BoundField
{
	/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ApplyFormatInEditMode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool ApplyFormatInEditMode
	{
		get
		{
			throw GetNotSupportedPropException("ApplyFormatInEditMode");
		}
		set
		{
			throw GetNotSupportedPropException("ApplyFormatInEditMode");
		}
	}

	/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ConvertEmptyStringToNull" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool ConvertEmptyStringToNull
	{
		get
		{
			throw GetNotSupportedPropException("ConvertEmptyStringToNull");
		}
		set
		{
			throw GetNotSupportedPropException("ConvertEmptyStringToNull");
		}
	}

	/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
	/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[TypeConverter("System.Web.UI.Design.DataSourceBooleanViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public override string DataField
	{
		get
		{
			return base.DataField;
		}
		set
		{
			base.DataField = value;
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the value of the field. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	/// <returns>A formatting string that specifies the display format for the value of the field. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string DataFormatString
	{
		get
		{
			throw GetNotSupportedPropException("DataFormatString");
		}
		set
		{
			throw GetNotSupportedPropException("DataFormatString");
		}
	}

	/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.HtmlEncode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool HtmlEncode
	{
		get
		{
			throw GetNotSupportedPropException("HtmlEncode");
		}
		set
		{
			throw GetNotSupportedPropException("HtmlEncode");
		}
	}

	/// <summary>Gets or sets a value that indicates whether the formatted text should be HTML encoded before it is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the text should be HTML encoded; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool HtmlEncodeFormatString
	{
		get
		{
			return base.HtmlEncodeFormatString;
		}
		set
		{
			base.HtmlEncodeFormatString = value;
		}
	}

	/// <summary>Gets or sets the text displayed for a field when the field's value is <see langword="null" />. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	/// <returns>The text displayed for a field with a value of <see langword="null" />. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string NullDisplayText
	{
		get
		{
			throw GetNotSupportedPropException("NullDisplayText");
		}
		set
		{
			throw GetNotSupportedPropException("NullDisplayText");
		}
	}

	/// <summary>Gets a Boolean value indicating whether the control supports HTML encoding.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	protected override bool SupportsHtmlEncode => false;

	/// <summary>Gets or sets the caption to display next to each check box in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
	/// <returns>The caption displayed next to each check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return base.ViewState.GetString("Text", string.Empty);
		}
		set
		{
			base.ViewState["Text"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
	{
		bool flag = IsEditable(rowState);
		CheckBox checkBox = new CheckBox();
		checkBox.Enabled = flag;
		if (flag)
		{
			checkBox.ToolTip = HeaderText;
		}
		checkBox.Text = Text;
		cell.Controls.Add(checkBox);
	}

	/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object.</summary>
	/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the values to retrieve.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="includeReadOnly">
	///       <see langword="true" /> to include the values of read-only fields; otherwise, <see langword="false" />.</param>
	public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
	{
		if (IsEditable(rowState) || includeReadOnly)
		{
			CheckBox checkBox = (CheckBox)cell.Controls[0];
			dictionary[DataField] = checkBox.Checked;
		}
	}

	/// <summary>Binds the value of a field to a check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.- or -The field value cannot be converted to a Boolean value. </exception>
	protected override void OnDataBindField(object sender, EventArgs e)
	{
		try
		{
			Control control = (Control)sender;
			object value = GetValue(control.NamingContainer);
			CheckBox checkBox = sender as CheckBox;
			if (checkBox == null && sender is DataControlFieldCell { Controls: var controls })
			{
				if ((controls?.Count ?? 0) == 1)
				{
					checkBox = controls[0] as CheckBox;
				}
				if (checkBox == null)
				{
					return;
				}
			}
			if (checkBox == null)
			{
				throw new HttpException("CheckBox field '" + DataField + "' contains a control that isn't a CheckBox.  Override OnDataBindField to inherit from CheckBoxField and add different controls.");
			}
			if (value != null && value != DBNull.Value)
			{
				checkBox.Checked = (bool)value;
			}
			else if (string.IsNullOrEmpty(DataField))
			{
				checkBox.Visible = false;
				return;
			}
			if (!checkBox.Visible)
			{
				checkBox.Visible = true;
			}
		}
		catch (HttpException)
		{
			throw;
		}
		catch (Exception ex2)
		{
			throw new HttpException(ex2.Message, ex2);
		}
	}

	/// <summary>Retrieves the value used for the field's value when rendering the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object in a designer.</summary>
	/// <returns>Always returns <see langword="true" />.</returns>
	protected override object GetDesignTimeValue()
	{
		return true;
	}

	/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
	/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" />.</returns>
	protected override DataControlField CreateField()
	{
		return new CheckBoxField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> to.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		CheckBoxField obj = (CheckBoxField)newField;
		obj.DataField = DataField;
		obj.ReadOnly = ReadOnly;
		obj.Text = Text;
	}

	/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object support callbacks.</summary>
	public override void ValidateSupportsCallback()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
	public CheckBoxField()
	{
	}
}
