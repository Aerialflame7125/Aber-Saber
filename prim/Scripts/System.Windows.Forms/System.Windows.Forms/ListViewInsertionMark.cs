using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Used to indicate the expected drop location when an item is dragged to a new position in a <see cref="T:System.Windows.Forms.ListView" /> control. This functionality is available only on Windows XP and later.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ListViewInsertionMark
{
	private ListView listview_owner;

	private bool appears_after_item;

	private Rectangle bounds;

	private Color? color;

	private int index;

	/// <summary>Gets or sets a value indicating whether the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property.</summary>
	/// <returns>true if the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool AppearsAfterItem
	{
		get
		{
			return appears_after_item;
		}
		set
		{
			if (value != appears_after_item)
			{
				appears_after_item = value;
				listview_owner.item_control.Invalidate(bounds);
				UpdateBounds();
				listview_owner.item_control.Invalidate(bounds);
			}
		}
	}

	/// <summary>Gets the bounding rectangle of the insertion mark.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the position and size of the insertion mark.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Rectangle Bounds => bounds;

	/// <summary>Gets or sets the color of the insertion mark.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the color of the insertion mark. The default value is the value of the <see cref="P:System.Windows.Forms.ListView.ForeColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color Color
	{
		get
		{
			Color? color = this.color;
			return color.HasValue ? this.color.Value : listview_owner.ForeColor;
		}
		set
		{
			color = value;
		}
	}

	/// <summary>Gets or sets the index of the item next to which the insertion mark appears.</summary>
	/// <returns>The index of the item next to which the insertion mark appears or -1 when the insertion mark is hidden.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int Index
	{
		get
		{
			return index;
		}
		set
		{
			if (value != index)
			{
				index = value;
				listview_owner.item_control.Invalidate(bounds);
				UpdateBounds();
				listview_owner.item_control.Invalidate(bounds);
			}
		}
	}

	internal PointF[] TopTriangle
	{
		get
		{
			PointF pointF = new PointF(bounds.X, bounds.Y);
			PointF pointF2 = new PointF(bounds.Right, bounds.Y);
			PointF pointF3 = new PointF(bounds.X + (bounds.Right - bounds.X) / 2, bounds.Y + 5);
			return new PointF[3] { pointF, pointF2, pointF3 };
		}
	}

	internal PointF[] BottomTriangle
	{
		get
		{
			PointF pointF = new PointF(bounds.X, bounds.Bottom);
			PointF pointF2 = new PointF(bounds.Right, bounds.Bottom);
			PointF pointF3 = new PointF(bounds.X + (bounds.Right - bounds.X) / 2, bounds.Bottom - 5);
			return new PointF[3] { pointF, pointF2, pointF3 };
		}
	}

	internal Rectangle Line => new Rectangle(bounds.X + 2, bounds.Y + 2, 2, bounds.Height - 5);

	internal ListViewInsertionMark(ListView listview)
	{
		listview_owner = listview;
	}

	private void UpdateBounds()
	{
		if (index < 0 || index >= listview_owner.Items.Count)
		{
			bounds = Rectangle.Empty;
			return;
		}
		Rectangle rectangle = listview_owner.Items[index].Bounds;
		int x = ((!appears_after_item) ? rectangle.Left : rectangle.Right) - 2;
		int height = rectangle.Height + ThemeEngine.Current.ListViewVerticalSpacing;
		bounds = new Rectangle(x, rectangle.Top, 7, height);
	}

	/// <summary>Retrieves the index of the item closest to the specified point.</summary>
	/// <returns>The index of the item closest to the specified point or -1 if the closest item is the item currently being dragged.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> representing the location from which to find the nearest item. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int NearestIndex(Point pt)
	{
		double num = double.MaxValue;
		int num2 = -1;
		for (int i = 0; i < listview_owner.Items.Count; i++)
		{
			Point itemLocation = listview_owner.GetItemLocation(i);
			double num3 = Math.Pow(itemLocation.X - pt.X, 2.0) + Math.Pow(itemLocation.Y - pt.Y, 2.0);
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (listview_owner.item_control.dragged_item_index == num2)
		{
			return -1;
		}
		return num2;
	}
}
