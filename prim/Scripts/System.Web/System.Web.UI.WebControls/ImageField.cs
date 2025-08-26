using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a field that is displayed as an image in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ImageField : DataControlField
{
	/// <summary>Represents the "this" expression.</summary>
	public static readonly string ThisExpression = "!";

	private PropertyDescriptor imageProperty;

	private PropertyDescriptor textProperty;

	/// <summary>Gets or sets the alternate text displayed for an image in the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
	/// <returns>The alternate text for an image displayed in the <see cref="T:System.Web.UI.WebControls.ImageField" /> object. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string AlternateText
	{
		get
		{
			object obj = base.ViewState["AlternateText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["AlternateText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether empty string ("") values are converted to <see langword="null" /> when the field values are returned from the data source.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="F:System.String.Empty" /> values should be converted to <see langword="null" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ConvertEmptyStringToNull
	{
		get
		{
			object obj = base.ViewState["ConvertEmptyStringToNull"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			base.ViewState["ConvertEmptyStringToNull"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the field from the data source that contains the values to bind to the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
	/// <returns>The name of the field to bind the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</returns>
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataAlternateTextField
	{
		get
		{
			object obj = base.ViewState["DataAlternateTextField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataAlternateTextField"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the string that specifies the format in which the alternate text for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered.</summary>
	/// <returns>A string that specifies the format in which the alternate text for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered. The default is an empty string (""), which indicates that now special formatting is applied to the alternate text.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataAlternateTextFormatString
	{
		get
		{
			object obj = base.ViewState["DataAlternateTextFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataAlternateTextFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the field from the data source that contains the values to bind to the <see cref="P:System.Web.UI.MobileControls.Image.ImageUrl" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.MobileControls.Image.ImageUrl" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</returns>
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataImageUrlField
	{
		get
		{
			object obj = base.ViewState["DataImageUrlField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataImageUrlField"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the string that specifies the format in which the URL for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered.</summary>
	/// <returns>A string that specifies the format in which the URL for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered. The default is the empty string ("") , which indicates that no special formatting is applied to the URLs.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataImageUrlFormatString
	{
		get
		{
			object obj = base.ViewState["DataImageUrlFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataImageUrlFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the text to display in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object when the value of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property is <see langword="null" />.</summary>
	/// <returns>The text to display when the value of a field is <see langword="null" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string NullDisplayText
	{
		get
		{
			object obj = base.ViewState["NullDisplayText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			base.ViewState["NullDisplayText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an alternate image displayed in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object when the value of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property is <see langword="null" />.</summary>
	/// <returns>The URL to an alternate image displayed when the value of a field is <see langword="null" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string NullImageUrl
	{
		get
		{
			object obj = base.ViewState["NullImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["NullImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether the values of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property can be modified in edit mode.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the field values cannot be modified in edit mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ReadOnly
	{
		get
		{
			object obj = base.ViewState["ReadOnly"];
			if (obj == null)
			{
				return false;
			}
			return (bool)obj;
		}
		set
		{
			base.ViewState["ReadOnly"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
	/// <param name="enableSorting">
	///       <see langword="true" /> if sorting is supported; otherwise, <see langword="false" />. </param>
	/// <param name="control">The data control that contains the <see cref="T:System.Web.UI.WebControls.ImageField" />. </param>
	/// <returns>Always returns <see langword="true" />.</returns>
	public override bool Initialize(bool enableSorting, Control control)
	{
		return base.Initialize(enableSorting, control);
	}

	/// <summary>Fills the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object.</summary>
	/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the values of the specified cell.</param>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the values to retrieve.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="includeReadOnly">
	///       <see langword="true" /> to include the values of read-only fields; otherwise, <see langword="false" />.</param>
	public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
	{
		if ((ReadOnly && !includeReadOnly) || cell.Controls.Count == 0)
		{
			return;
		}
		bool flag = (rowState & (DataControlRowState.Edit | DataControlRowState.Insert)) != 0;
		if (includeReadOnly || flag)
		{
			Control control = cell.Controls[0];
			if (control is Image)
			{
				dictionary[DataImageUrlField] = ((Image)control).ImageUrl;
			}
			else if (control is TextBox)
			{
				dictionary[DataImageUrlField] = ((TextBox)control).Text;
			}
		}
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object with the specified cell type, row state, and row index.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize. </param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values. </param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values. </param>
	/// <param name="rowIndex">The zero-based index of the row. </param>
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

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object with the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize. </param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values. </param>
	protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
	{
		if ((rowState & (DataControlRowState.Edit | DataControlRowState.Insert)) != 0 && !ReadOnly)
		{
			TextBox child = new TextBox();
			cell.Controls.Add(child);
		}
		else if (DataImageUrlField.Length > 0)
		{
			Image image = new Image();
			image.ControlStyle.CopyFrom(base.ControlStyle);
			cell.Controls.Add(image);
		}
	}

	/// <summary>Applies the format specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlFormatString" /> property to a field value.</summary>
	/// <param name="dataValue">The value to transform.</param>
	/// <returns>The transformed value.</returns>
	protected virtual string FormatImageUrlValue(object dataValue)
	{
		if (dataValue == null)
		{
			return null;
		}
		if (DataImageUrlFormatString.Length > 0)
		{
			return string.Format(DataImageUrlFormatString, dataValue);
		}
		return dataValue.ToString();
	}

	/// <summary>Applies the format specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataAlternateTextFormatString" /> property to the alternate text value contained in the specified <see cref="T:System.Web.UI.Control" /> object.</summary>
	/// <param name="controlContainer">The <see cref="T:System.Web.UI.Control" /> that contains the alternate text value to transform.</param>
	/// <returns>The transformed value.</returns>
	protected virtual string GetFormattedAlternateText(Control controlContainer)
	{
		if (DataAlternateTextField.Length > 0)
		{
			if (textProperty == null)
			{
				textProperty = GetProperty(controlContainer, DataAlternateTextField);
			}
			object value = GetValue(controlContainer, DataAlternateTextField, ref textProperty);
			if (value == null || (value.ToString().Length == 0 && ConvertEmptyStringToNull))
			{
				return NullDisplayText;
			}
			if (DataAlternateTextFormatString.Length > 0)
			{
				return string.Format(DataAlternateTextFormatString, value);
			}
			return value.ToString();
		}
		return AlternateText;
	}

	/// <summary>Retrieves the value of the specified field from the specified control.</summary>
	/// <param name="controlContainer">The <see cref="T:System.Web.UI.Control" /> that contains the field value.</param>
	/// <param name="fieldName">The name of the field for which to retrieve the value.</param>
	/// <param name="cachedDescriptor">A <see cref="T:System.ComponentModel.PropertyDescriptor" />, passed by reference, that represents the properties of the field.</param>
	/// <returns>The value of the specified field.</returns>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="controlContainer" /> parameter is <see langword="null" />.- or -The data item associated with the container control is <see langword="null" />.- or -The field specified by the <paramref name="fieldName" /> parameter could not be found.</exception>
	protected virtual object GetValue(Control controlContainer, string fieldName, ref PropertyDescriptor cachedDescriptor)
	{
		if (base.DesignMode)
		{
			return GetDesignTimeValue();
		}
		object dataItem = DataBinder.GetDataItem(controlContainer);
		if (dataItem == null)
		{
			throw new HttpException("A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.");
		}
		if (fieldName == ThisExpression)
		{
			return dataItem;
		}
		if (cachedDescriptor != null)
		{
			return cachedDescriptor.GetValue(dataItem);
		}
		return GetProperty(controlContainer, fieldName).GetValue(dataItem);
	}

	private PropertyDescriptor GetProperty(Control controlContainer, string fieldName)
	{
		if (fieldName == ThisExpression)
		{
			return null;
		}
		IDataItemContainer dataItemContainer = (IDataItemContainer)controlContainer;
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItemContainer.DataItem)?[fieldName];
		if (propertyDescriptor == null)
		{
			throw new InvalidOperationException("Property '" + fieldName + "' not found in object of type " + dataItemContainer.DataItem.GetType());
		}
		return propertyDescriptor;
	}

	/// <summary>Retrieves the value used for a field's value when rendering the <see cref="T:System.Web.UI.WebControls.ImageField" /> object in a designer.</summary>
	/// <returns>The value to display in the designer as the field's value.</returns>
	protected virtual string GetDesignTimeValue()
	{
		return "Databound";
	}

	/// <summary>Binds the value of a field to the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.ImageField" /> object contains a control that is not a <see cref="T:System.Web.UI.WebControls.TextBox" /> control in edit mode and is not an <see cref="T:System.Web.UI.WebControls.Image" /> control with a <see cref="T:System.Web.UI.WebControls.Label" /> control in read-only mode.</exception>
	protected virtual void OnDataBindField(object sender, EventArgs e)
	{
		Control control = (Control)sender;
		ControlCollection controlCollection = control?.Controls;
		Control namingContainer = control.NamingContainer;
		Control control2;
		if (sender is DataControlFieldCell)
		{
			if (controlCollection.Count == 0)
			{
				return;
			}
			control2 = controlCollection[0];
		}
		else
		{
			if (!(sender is Image) && !(sender is TextBox))
			{
				return;
			}
			control2 = control;
		}
		if (imageProperty == null)
		{
			imageProperty = GetProperty(namingContainer, DataImageUrlField);
		}
		if (control2 is TextBox)
		{
			object value = GetValue(namingContainer, DataImageUrlField, ref imageProperty);
			((TextBox)control2).Text = ((value != null) ? value.ToString() : string.Empty);
		}
		else
		{
			if (!(control2 is Image))
			{
				return;
			}
			Image obj = (Image)control2;
			string text = FormatImageUrlValue(GetValue(namingContainer, DataImageUrlField, ref imageProperty));
			if (text == null || (ConvertEmptyStringToNull && text.Length == 0))
			{
				if (NullImageUrl == null || NullImageUrl.Length == 0)
				{
					control2.Visible = false;
					controlCollection.Add(new Label
					{
						Text = NullDisplayText
					});
				}
				else
				{
					text = NullImageUrl;
				}
			}
			obj.ImageUrl = text;
			obj.AlternateText = GetFormattedAlternateText(namingContainer);
		}
	}

	/// <summary>Determines whether the controls contained in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object support callbacks.</summary>
	public override void ValidateSupportsCallback()
	{
	}

	/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.ImageField" /> class.</summary>
	/// <returns>A new instance of the <see cref="T:System.Web.UI.WebControls.ImageField" /> class.</returns>
	protected override DataControlField CreateField()
	{
		return new ImageField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.ImageField" /> object to the specified object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object that receives the copy.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		ImageField obj = (ImageField)newField;
		obj.AlternateText = AlternateText;
		obj.ConvertEmptyStringToNull = ConvertEmptyStringToNull;
		obj.DataAlternateTextField = DataAlternateTextField;
		obj.DataAlternateTextFormatString = DataAlternateTextFormatString;
		obj.DataImageUrlField = DataImageUrlField;
		obj.DataImageUrlFormatString = DataImageUrlFormatString;
		obj.NullDisplayText = NullDisplayText;
		obj.NullImageUrl = NullImageUrl;
		obj.ReadOnly = ReadOnly;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageField" /> class.</summary>
	public ImageField()
	{
	}
}
