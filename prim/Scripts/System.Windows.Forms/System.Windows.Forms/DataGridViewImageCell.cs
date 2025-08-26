using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Displays a graphic in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewImageCell : DataGridViewCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> to accessibility client applications.</summary>
	protected class DataGridViewImageCellAccessibleObject : DataGridViewCellAccessibleObject
	{
		/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</summary>
		/// <returns>An empty string ("").</returns>
		public override string DefaultAction => string.Empty;

		/// <summary>Gets the text associated with the image in the image cell.</summary>
		/// <returns>The text associated with the image in the image cell.</returns>
		public override string Description => (base.Owner as DataGridViewImageCell).Description;

		/// <summary>Gets a string representing the formatted value of the owning cell. </summary>
		/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</param>
		public DataGridViewImageCellAccessibleObject(DataGridViewCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
		public override void DoDefaultAction()
		{
		}

		/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
		/// <returns>The value –1.</returns>
		public override int GetChildCount()
		{
			return -1;
		}
	}

	private object defaultNewRowValue;

	private string description;

	private DataGridViewImageCellLayout imageLayout;

	private bool valueIsIcon;

	private static Image missing_image;

	/// <summary>Gets the default value that is used when creating a new row.</summary>
	/// <returns>An object containing a default image placeholder, or null to display an empty cell.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object DefaultNewRowValue => missing_image;

	/// <summary>Gets or sets the text associated with the image.</summary>
	/// <returns>The text associated with the image displayed in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}

	/// <summary>Gets the type of the cell's hosted editing control. </summary>
	/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. As implemented in this class, this property is always null.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type EditType => null;

	/// <summary>Gets the type of the formatted value associated with the cell.</summary>
	/// <returns>A <see cref="T:System.Type" /> object representing display value type of the cell, which is the <see cref="T:System.Drawing.Image" /> type if the <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to false or the <see cref="T:System.Drawing.Icon" /> type otherwise.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType => (!valueIsIcon) ? typeof(Image) : typeof(Icon);

	/// <summary>Gets or sets the graphics layout for the cell. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> for this cell. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.NotSet" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The supplied <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> value is invalid. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DataGridViewImageCellLayout.NotSet)]
	public DataGridViewImageCellLayout ImageLayout
	{
		get
		{
			return imageLayout;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewImageCellLayout), value))
			{
				throw new InvalidEnumArgumentException("Value is invalid image cell layout.");
			}
			imageLayout = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether this cell displays an <see cref="T:System.Drawing.Icon" /> value.</summary>
	/// <returns>true if this cell displays an <see cref="T:System.Drawing.Icon" /> value; otherwise, false.</returns>
	[DefaultValue(false)]
	public bool ValueIsIcon
	{
		get
		{
			return valueIsIcon;
		}
		set
		{
			valueIsIcon = value;
		}
	}

	/// <summary>Gets or sets the data type of the values in the cell. </summary>
	/// <returns>The <see cref="T:System.Type" /> of the cell's value.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType
	{
		get
		{
			if ((object)base.ValueType != null)
			{
				return base.ValueType;
			}
			if (base.OwningColumn != null && (object)base.OwningColumn.ValueType != null)
			{
				return base.OwningColumn.ValueType;
			}
			if (valueIsIcon)
			{
				return typeof(Icon);
			}
			return typeof(Image);
		}
		set
		{
			base.ValueType = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
	/// <param name="valueIsIcon">The cell will display an <see cref="T:System.Drawing.Icon" /> value.</param>
	public DataGridViewImageCell(bool valueIsIcon)
	{
		this.valueIsIcon = valueIsIcon;
		imageLayout = DataGridViewImageCellLayout.NotSet;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, configuring it for use with cell values other than <see cref="T:System.Drawing.Icon" /> objects.</summary>
	public DataGridViewImageCell()
		: this(valueIsIcon: false)
	{
	}

	static DataGridViewImageCell()
	{
		missing_image = ResourceImageLoader.Get("image-missing.png");
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell)base.Clone();
		dataGridViewImageCell.defaultNewRowValue = defaultNewRowValue;
		dataGridViewImageCell.description = description;
		dataGridViewImageCell.valueIsIcon = valueIsIcon;
		return dataGridViewImageCell;
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewImageCellAccessibleObject(this);
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		Rectangle result = Rectangle.Empty;
		Image image = (Image)GetFormattedValue(base.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.PreferredSize);
		if (image == null)
		{
			image = missing_image;
		}
		switch (imageLayout)
		{
		case DataGridViewImageCellLayout.NotSet:
		case DataGridViewImageCellLayout.Normal:
			result = new Rectangle((base.Size.Width - image.Width) / 2, (base.Size.Height - image.Height) / 2, image.Width, image.Height);
			break;
		case DataGridViewImageCellLayout.Stretch:
			result = new Rectangle(Point.Empty, base.Size);
			break;
		case DataGridViewImageCellLayout.Zoom:
		{
			Size size = ((!((float)image.Width / (float)image.Height >= (float)base.Size.Width / (float)base.Size.Height)) ? new Size(image.Width * base.Size.Height / image.Height, base.Size.Height) : new Size(base.Size.Width, image.Height * base.Size.Width / image.Width));
			result = new Rectangle((base.Size.Width - size.Width) / 2, (base.Size.Height - size.Height) / 2, size.Width, size.Height);
			break;
		}
		}
		return result;
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
		{
			return Rectangle.Empty;
		}
		Size size = new Size(12, 11);
		return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
	}

	/// <summary>Returns a graphic as it would be displayed in the cell.</summary>
	/// <returns>An object that represents the formatted image.</returns>
	/// <param name="value">The value to be formatted. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell. </param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed. </param>
	protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
	{
		return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
	}

	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		Image image = (Image)base.FormattedValue;
		if (image == null)
		{
			return new Size(21, 20);
		}
		if (image != null)
		{
			return new Size(image.Width + 1, image.Height + 1);
		}
		return new Size(21, 20);
	}

	/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override object GetValue(int rowIndex)
	{
		return base.GetValue(rowIndex);
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="elementState"></param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		Image image = ((formattedValue != null) ? ((Image)formattedValue) : missing_image);
		Rectangle rect = Rectangle.Empty;
		switch (imageLayout)
		{
		case DataGridViewImageCellLayout.NotSet:
		case DataGridViewImageCellLayout.Normal:
			rect = AlignInRectangle(new Rectangle(2, 2, cellBounds.Width - 4, cellBounds.Height - 4), image.Size, cellStyle.Alignment);
			break;
		case DataGridViewImageCellLayout.Stretch:
			rect = new Rectangle(Point.Empty, cellBounds.Size);
			break;
		case DataGridViewImageCellLayout.Zoom:
		{
			Size size = ((!((float)image.Width / (float)image.Height >= (float)base.Size.Width / (float)base.Size.Height)) ? new Size(image.Width * base.Size.Height / image.Height, base.Size.Height) : new Size(base.Size.Width, image.Height * base.Size.Width / image.Width));
			rect = new Rectangle((base.Size.Width - size.Width) / 2, (base.Size.Height - size.Height) / 2, size.Width, size.Height);
			break;
		}
		}
		rect.X += cellBounds.Left;
		rect.Y += cellBounds.Top;
		graphics.DrawImage(image, rect);
	}
}
