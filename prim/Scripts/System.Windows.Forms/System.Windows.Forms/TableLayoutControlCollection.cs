using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms;

/// <summary>Represents a collection of child controls in a table layout container.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
[DesignerSerializer("System.Windows.Forms.Design.TableLayoutControlCollectionCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class TableLayoutControlCollection : Control.ControlCollection
{
	private TableLayoutPanel panel;

	/// <summary>Gets the parent <see cref="T:System.Windows.Forms.TableLayoutPanel" /> that contains the controls in the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> that contains the controls in the current collection.</returns>
	/// <filterpriority>1</filterpriority>
	public TableLayoutPanel Container => panel;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutControlCollection" /> class.</summary>
	/// <param name="container">The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control that contains the control collection.</param>
	public TableLayoutControlCollection(TableLayoutPanel container)
		: base(container)
	{
		panel = container;
	}

	/// <summary>Adds the specified control to the collection and positions it at the specified cell.</summary>
	/// <param name="control">The control to add.</param>
	/// <param name="column">The column in which <paramref name="control" /> will be placed.</param>
	/// <param name="row">The row in which <paramref name="control" /> will be placed.</param>
	/// <exception cref="T:System.ArgumentException">Either<paramref name=" column" /> or <paramref name="row" /> is less than -1.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Add(Control control, int column, int row)
	{
		if (column < -1)
		{
			throw new ArgumentException("column");
		}
		if (row < -1)
		{
			throw new ArgumentException("row");
		}
		base.Add(control);
		panel.SetCellPosition(control, new TableLayoutPanelCellPosition(column, row));
	}
}
