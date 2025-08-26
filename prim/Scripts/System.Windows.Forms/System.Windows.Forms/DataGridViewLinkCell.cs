using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms;

/// <summary>Represents a cell that contains a link. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewLinkCell : DataGridViewCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> control to accessibility client applications.</summary>
	protected class DataGridViewLinkCellAccessibleObject : DataGridViewCellAccessibleObject
	{
		/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
		/// <returns>The string "Click".</returns>
		public override string DefaultAction => "Click";

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</param>
		public DataGridViewLinkCellAccessibleObject(DataGridViewCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The cell returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null and a <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property value of -1, indicating that the cell is in a shared row.</exception>
		[System.MonoTODO("Stub, does nothing")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public override void DoDefaultAction()
		{
		}

		/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
		/// <returns>The value –1.</returns>
		public override int GetChildCount()
		{
			return -1;
		}
	}

	private Color activeLinkColor;

	private LinkBehavior linkBehavior;

	private Color linkColor;

	private bool linkVisited;

	private Cursor parent_cursor;

	private bool trackVisitedState;

	private bool useColumnTextForLinkValue;

	private Color visited_link_color;

	private LinkState linkState;

	/// <summary>Gets or sets the color used to display an active link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color ActiveLinkColor
	{
		get
		{
			return activeLinkColor;
		}
		set
		{
			activeLinkColor = value;
		}
	}

	/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.LinkBehavior" /> value.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(LinkBehavior.SystemDefault)]
	public LinkBehavior LinkBehavior
	{
		get
		{
			return linkBehavior;
		}
		set
		{
			linkBehavior = value;
		}
	}

	/// <summary>Gets or sets the color used to display an inactive and unvisited link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color LinkColor
	{
		get
		{
			return linkColor;
		}
		set
		{
			linkColor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the link was visited.</summary>
	/// <returns>true if the link has been visited; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool LinkVisited
	{
		get
		{
			return linkVisited;
		}
		set
		{
			linkVisited = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the link changes color when it is visited.</summary>
	/// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool TrackVisitedState
	{
		get
		{
			return trackVisitedState;
		}
		set
		{
			trackVisitedState = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
	/// <returns>true if the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; false if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseColumnTextForLinkValue
	{
		get
		{
			return useColumnTextForLinkValue;
		}
		set
		{
			useColumnTextForLinkValue = value;
		}
	}

	/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color VisitedLinkColor
	{
		get
		{
			return visited_link_color;
		}
		set
		{
			visited_link_color = value;
		}
	}

	/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType => ((object)base.ValueType != null) ? base.ValueType : typeof(object);

	/// <summary>Gets the type of the cell's hosted editing control.</summary>
	/// <returns>Always null. </returns>
	/// <filterpriority>1</filterpriority>
	public override Type EditType => null;

	/// <summary>Gets the display <see cref="T:System.Type" /> of the cell value.</summary>
	/// <returns>Always <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType => typeof(string);

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> class.</summary>
	public DataGridViewLinkCell()
	{
		activeLinkColor = Color.Red;
		linkColor = Color.FromArgb(0, 0, 255);
		trackVisitedState = true;
		visited_link_color = Color.FromArgb(128, 0, 128);
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewLinkCell dataGridViewLinkCell = (DataGridViewLinkCell)base.Clone();
		dataGridViewLinkCell.activeLinkColor = activeLinkColor;
		dataGridViewLinkCell.linkColor = linkColor;
		dataGridViewLinkCell.linkVisited = linkVisited;
		dataGridViewLinkCell.linkBehavior = linkBehavior;
		dataGridViewLinkCell.visited_link_color = visited_link_color;
		dataGridViewLinkCell.trackVisitedState = trackVisitedState;
		return dataGridViewLinkCell;
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewLinkCell {{ ColumnIndex={base.ColumnIndex}, RowIndex={base.RowIndex} }}";
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewLinkCellAccessibleObject(this);
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
		object formattedValue = base.FormattedValue;
		Size empty = Size.Empty;
		if (formattedValue != null)
		{
			empty = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			empty.Height += 3;
			return new Rectangle(1, (base.OwningRow.Height - empty.Height) / 2 - 1, empty.Width, empty.Height);
		}
		return new Rectangle(1, 10, 0, 0);
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

	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		object formattedValue = base.FormattedValue;
		if (formattedValue != null)
		{
			Size result = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			result.Height = Math.Max(result.Height, 20);
			result.Width += 4;
			return result;
		}
		return new Size(21, 20);
	}

	/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override object GetValue(int rowIndex)
	{
		if (useColumnTextForLinkValue)
		{
			return (base.OwningColumn as DataGridViewLinkColumn).Text;
		}
		return base.GetValue(rowIndex);
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when a key is released and the cell has focus.</summary>
	/// <returns>true if the SPACE key was released, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.TrackVisitedState" /> property is true, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.LinkVisited" /> property is false, and the CTRL, ALT, and SHIFT keys are not pressed; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press.</param>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		if (e.KeyCode != Keys.Space && trackVisitedState && !linkVisited && !e.Shift && !e.Control && !e.Alt)
		{
			return true;
		}
		return false;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
	/// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
	protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return true;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
	/// <returns>true if the link displayed by the cell is not in the normal state; otherwise, false.</returns>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override bool MouseLeaveUnsharesRow(int rowIndex)
	{
		return linkState != LinkState.Normal;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
	/// <returns>true if the mouse pointer is over the link and the link is has not yet changed color to reflect the hover state; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
	protected override bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		if (linkState == LinkState.Hover)
		{
			return true;
		}
		return false;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell. </summary>
	/// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
	protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return linkState == LinkState.Hover;
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
	{
		if ((e.KeyData & Keys.Space) == Keys.Space)
		{
			linkState = LinkState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseDown(e);
		linkState = LinkState.Active;
		base.DataGridView.InvalidateCell(this);
	}

	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override void OnMouseLeave(int rowIndex)
	{
		base.OnMouseLeave(rowIndex);
		linkState = LinkState.Normal;
		base.DataGridView.InvalidateCell(this);
		base.DataGridView.Cursor = parent_cursor;
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (linkState != LinkState.Hover)
		{
			linkState = LinkState.Hover;
			base.DataGridView.InvalidateCell(this);
			parent_cursor = base.DataGridView.Cursor;
			base.DataGridView.Cursor = Cursors.Hand;
		}
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseUp(e);
		linkState = LinkState.Hover;
		LinkVisited = true;
		base.DataGridView.InvalidateCell(this);
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		Font font = cellStyle.Font;
		switch (LinkBehavior)
		{
		case LinkBehavior.SystemDefault:
		case LinkBehavior.AlwaysUnderline:
			font = new Font(font, FontStyle.Underline);
			break;
		case LinkBehavior.HoverUnderline:
			if (linkState == LinkState.Hover)
			{
				font = new Font(font, FontStyle.Underline);
			}
			break;
		}
		Color foreColor = ((linkState == LinkState.Active) ? ActiveLinkColor : ((!linkVisited) ? LinkColor : VisitedLinkColor));
		TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
		cellBounds.Height -= 2;
		cellBounds.Width -= 2;
		if (formattedValue != null)
		{
			TextRenderer.DrawText(graphics, formattedValue.ToString(), font, cellBounds, foreColor, flags);
		}
	}
}
