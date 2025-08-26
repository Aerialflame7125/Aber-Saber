using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a column of cells that contain links in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
/// <filterpriority>2</filterpriority>
[ToolboxBitmap("")]
public class DataGridViewLinkColumn : DataGridViewColumn
{
	private string text = string.Empty;

	/// <summary>Gets or sets the color used to display an active link within cells in the column. </summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color ActiveLinkColor
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.ActiveLinkColor;
		}
		set
		{
			if (ActiveLinkColor == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.ActiveLinkColor = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.ActiveLinkColor = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Gets or sets the template used to create new cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> instance.</returns>
	/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override DataGridViewCell CellTemplate
	{
		get
		{
			return base.CellTemplate;
		}
		set
		{
			base.CellTemplate = value as DataGridViewLinkCell;
		}
	}

	/// <summary>Gets or sets a value that represents the behavior of links within cells in the column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LinkBehavior" /> value indicating the link behavior. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(LinkBehavior.SystemDefault)]
	public LinkBehavior LinkBehavior
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.LinkBehavior;
		}
		set
		{
			if (LinkBehavior == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.LinkBehavior = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.LinkBehavior = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Gets or sets the color used to display an unselected link within cells in the column.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color. </returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color LinkColor
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.LinkColor;
		}
		set
		{
			if (LinkColor == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.LinkColor = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.LinkColor = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Gets or sets the link text displayed in a column's cells if <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.UseColumnTextForLinkValue" /> is true.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the link text.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("")]
	[DefaultValue(null)]
	public string Text
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return text;
		}
		set
		{
			if (!(Text == value))
			{
				if (!(CellTemplate is DataGridViewLinkCell))
				{
					throw new InvalidOperationException("CellTemplate is null when getting this property.");
				}
				text = value;
				base.DataGridView.InvalidateColumn(base.Index);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the link changes color if it has been visited.</summary>
	/// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool TrackVisitedState
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.TrackVisitedState;
		}
		set
		{
			if (TrackVisitedState == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.TrackVisitedState = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.TrackVisitedState = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; false if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	[DefaultValue(false)]
	public bool UseColumnTextForLinkValue
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.UseColumnTextForLinkValue;
		}
		set
		{
			if (UseColumnTextForLinkValue == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.UseColumnTextForLinkValue = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.UseColumnTextForLinkValue = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color. </returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public Color VisitedLinkColor
	{
		get
		{
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return dataGridViewLinkCell.VisitedLinkColor;
		}
		set
		{
			if (VisitedLinkColor == value)
			{
				return;
			}
			if (!(CellTemplate is DataGridViewLinkCell dataGridViewLinkCell))
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			dataGridViewLinkCell.VisitedLinkColor = value;
			foreach (DataGridViewRow item in (IEnumerable)base.DataGridView.Rows)
			{
				if (item.Cells[base.Index] is DataGridViewLinkCell dataGridViewLinkCell2)
				{
					dataGridViewLinkCell2.VisitedLinkColor = value;
				}
			}
			base.DataGridView.InvalidateColumn(base.Index);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" /> class. </summary>
	public DataGridViewLinkColumn()
	{
		base.CellTemplate = new DataGridViewLinkCell();
	}

	/// <summary>Creates an exact copy of this column.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is null. </exception>
	public override object Clone()
	{
		DataGridViewLinkColumn dataGridViewLinkColumn = (DataGridViewLinkColumn)base.Clone();
		dataGridViewLinkColumn.CellTemplate = (DataGridViewCell)CellTemplate.Clone();
		return dataGridViewLinkColumn;
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString();
	}
}
