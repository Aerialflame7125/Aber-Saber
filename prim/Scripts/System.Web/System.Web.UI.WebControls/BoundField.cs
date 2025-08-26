using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a field that is displayed as text in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class BoundField : DataControlField
{
	/// <summary>Represents the "this" expression.</summary>
	public static readonly string ThisExpression = "!";

	/// <summary>Gets or sets a value indicating whether the formatting string specified by the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property is applied to field values when the data-bound control that contains the <see cref="T:System.Web.UI.WebControls.BoundField" /> object is in edit mode.</summary>
	/// <returns>
	///     <see langword="true" /> to apply the formatting string to field values in edit mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ApplyFormatInEditMode
	{
		get
		{
			return base.ViewState.GetBool("ApplyFormatInEditMode", def: false);
		}
		set
		{
			base.ViewState["ApplyFormatInEditMode"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether empty string values ("") are automatically converted to null values when the data field is updated in the data source.</summary>
	/// <returns>
	///     <see langword="true" /> to automatically convert empty string values to null values; otherwise, the <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ConvertEmptyStringToNull
	{
		get
		{
			return base.ViewState.GetBool("ConvertEmptyStringToNull", def: true);
		}
		set
		{
			base.ViewState["ConvertEmptyStringToNull"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	[DefaultValue("")]
	public virtual string DataField
	{
		get
		{
			return base.ViewState.GetString("DataField", string.Empty);
		}
		set
		{
			base.ViewState["DataField"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
	/// <returns>A formatting string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataFormatString
	{
		get
		{
			return base.ViewState.GetString("DataFormatString", string.Empty);
		}
		set
		{
			base.ViewState["DataFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the text that is displayed in the header of a data control.</summary>
	/// <returns>The text displayed in the header of a data control. The default value is an empty string ("").</returns>
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public override string HeaderText
	{
		get
		{
			return base.ViewState.GetString("HeaderText", string.Empty);
		}
		set
		{
			base.ViewState["HeaderText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption displayed for a field when the field's value is null.</summary>
	/// <returns>The caption displayed for a field when the field's value is null. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[WebCategory("Behavior")]
	public virtual string NullDisplayText
	{
		get
		{
			return base.ViewState.GetString("NullDisplayText", string.Empty);
		}
		set
		{
			base.ViewState["NullDisplayText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether the value of the field can be modified in edit mode.</summary>
	/// <returns>
	///     <see langword="true" /> to prevent the value of the field from being modified in edit mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ReadOnly
	{
		get
		{
			return base.ViewState.GetBool("ReadOnly", def: false);
		}
		set
		{
			base.ViewState["ReadOnly"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> if field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("HtmlEncode")]
	public virtual bool HtmlEncode
	{
		get
		{
			return base.ViewState.GetBool("HtmlEncode", def: true);
		}
		set
		{
			base.ViewState["HtmlEncode"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value that indicates whether text that is created by applying the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property to the <see cref="T:System.Web.UI.WebControls.BoundField" /> value should be HTML encoded when it is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the text should be HTML-encoded; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool HtmlEncodeFormatString
	{
		get
		{
			return base.ViewState.GetBool("HtmlEncodeFormatString", def: true);
		}
		set
		{
			base.ViewState["HtmlEncodeFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets a value indicating whether HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <returns>Always returns <see langword="true" /> to indicate that HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
	protected virtual bool SupportsHtmlEncode => true;

	/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object.</summary>
	/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains the values to retrieve.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="includeReadOnly">
	///       <see langword="true" /> to include the values of read-only fields; otherwise, <see langword="false" />.</param>
	public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
	{
		if (IsEditable(rowState))
		{
			if (cell.Controls.Count > 0)
			{
				TextBox textBox = (TextBox)cell.Controls[0];
				dictionary[DataField] = textBox.Text;
			}
		}
		else if (includeReadOnly)
		{
			dictionary[DataField] = cell.Text;
		}
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <param name="enableSorting">
	///       <see langword="true" /> if sorting is supported; otherwise, <see langword="false" />.</param>
	/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.BoundField" />.</param>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool Initialize(bool enableSorting, Control control)
	{
		return base.Initialize(enableSorting, control);
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="rowIndex">The zero-based index of the row.</param>
	public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		base.InitializeCell(cell, cellType, rowState, rowIndex);
		if (cellType == DataControlCellType.DataCell)
		{
			InitializeDataCell(cell, rowState);
			if ((rowState & DataControlRowState.Insert) == 0)
			{
				cell.DataBinding += OnDataBindField;
			}
		}
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
	{
		if (IsEditable(rowState))
		{
			TextBox textBox = new TextBox();
			cell.Controls.Add(textBox);
			textBox.ToolTip = HeaderText;
		}
	}

	internal bool IsEditable(DataControlRowState rowState)
	{
		if ((rowState & DataControlRowState.Edit) == 0 || ReadOnly)
		{
			if ((rowState & DataControlRowState.Insert) != 0)
			{
				return InsertVisible;
			}
			return false;
		}
		return true;
	}

	/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <param name="dataValue">The field value to format.</param>
	/// <param name="encode">
	///       <see langword="true" /> to encode the value; otherwise, <see langword="false" />.</param>
	/// <returns>The field value converted to the format specified by <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" />.</returns>
	protected virtual string FormatDataValue(object dataValue, bool encode)
	{
		bool htmlEncodeFormatString = HtmlEncodeFormatString;
		string text = ((dataValue != null) ? dataValue.ToString() : string.Empty);
		string text2;
		if (dataValue == null || (text.Length == 0 && ConvertEmptyStringToNull))
		{
			if (NullDisplayText.Length == 0)
			{
				encode = false;
				text2 = "&nbsp;";
			}
			else
			{
				text2 = NullDisplayText;
			}
		}
		else
		{
			string dataFormatString = DataFormatString;
			text2 = (string.IsNullOrEmpty(dataFormatString) ? text : ((!(!encode || htmlEncodeFormatString)) ? string.Format(dataFormatString, encode ? HttpUtility.HtmlEncode(text) : text) : string.Format(dataFormatString, dataValue)));
		}
		if (encode && htmlEncodeFormatString)
		{
			return HttpUtility.HtmlEncode(text2);
		}
		return text2;
	}

	/// <summary>Retrieves the value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <param name="controlContainer">The container for the field value.</param>
	/// <returns>The value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The container specified by the <paramref name="controlContainer" /> parameter is <see langword="null" />.- or - The container specified by the <paramref name="controlContainer" /> parameter does not have a data item.- or - The data field was not found. </exception>
	protected virtual object GetValue(Control controlContainer)
	{
		if (base.DesignMode)
		{
			return GetDesignTimeValue();
		}
		return GetBoundValue(controlContainer);
	}

	/// <summary>Retrieves the value used for a field's value when rendering the <see cref="T:System.Web.UI.WebControls.BoundField" /> object in a designer.</summary>
	/// <returns>The value to display in the designer as the field's value.</returns>
	protected virtual object GetDesignTimeValue()
	{
		return "Databound";
	}

	private object GetBoundValue(Control controlContainer)
	{
		object dataItem = DataBinder.GetDataItem(controlContainer);
		if (dataItem == null)
		{
			throw new HttpException("A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.");
		}
		if (DataField == ThisExpression)
		{
			return dataItem;
		}
		if (DataField == string.Empty)
		{
			return null;
		}
		return DataBinder.GetPropertyValue(dataItem, DataField);
	}

	/// <summary>Restores the previously stored view-state information for this field.</summary>
	/// <param name="state">Represents the control state to be restored.</param>
	protected override void LoadViewState(object state)
	{
		base.LoadViewState(state);
	}

	/// <summary>Binds the value of a field to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.TextBox" /> or a <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
	protected virtual void OnDataBindField(object sender, EventArgs e)
	{
		Control bindingContainer = ((Control)sender).BindingContainer;
		if (!(bindingContainer is INamingContainer))
		{
			throw new HttpException("A DataControlField must be within an INamingContainer.");
		}
		object value = GetValue(bindingContainer);
		TextBox textBox = sender as TextBox;
		if (textBox == null && sender is DataControlFieldCell { Controls: var controls } dataControlFieldCell)
		{
			if ((controls?.Count ?? 0) == 1)
			{
				textBox = controls[0] as TextBox;
			}
			if (textBox == null)
			{
				dataControlFieldCell.Text = FormatDataValue(value, SupportsHtmlEncode && HtmlEncode);
				return;
			}
		}
		if (textBox == null)
		{
			throw new HttpException("Bound field " + DataField + " contains a control that isn't a TextBox.  Override OnDataBindField to inherit from BoundField and add different controls.");
		}
		if (ApplyFormatInEditMode)
		{
			textBox.Text = FormatDataValue(value, SupportsHtmlEncode && HtmlEncode);
		}
		else
		{
			textBox.Text = ((value != null) ? value.ToString() : NullDisplayText);
		}
	}

	/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
	/// <returns>An empty <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
	protected override DataControlField CreateField()
	{
		return new BoundField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> to.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		BoundField obj = (BoundField)newField;
		obj.ConvertEmptyStringToNull = ConvertEmptyStringToNull;
		obj.DataField = DataField;
		obj.DataFormatString = DataFormatString;
		obj.NullDisplayText = NullDisplayText;
		obj.ReadOnly = ReadOnly;
		obj.HtmlEncode = HtmlEncode;
	}

	/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object support callbacks.</summary>
	public override void ValidateSupportsCallback()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BoundField" /> class.</summary>
	public BoundField()
	{
	}
}
