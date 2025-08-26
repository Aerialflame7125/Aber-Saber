using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Maintains a <see cref="T:System.Windows.Forms.Binding" /> between an object's property and a data-bound control property.</summary>
/// <filterpriority>2</filterpriority>
public class PropertyManager : BindingManagerBase
{
	internal string property_name;

	private PropertyDescriptor prop_desc;

	private object data_source;

	private EventDescriptor changed_event;

	private EventHandler property_value_changed_handler;

	/// <summary>Gets the object to which the data-bound property belongs.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the object to which the property belongs.</returns>
	/// <filterpriority>1</filterpriority>
	public override object Current => (prop_desc != null) ? prop_desc.GetValue(data_source) : data_source;

	/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
	/// <filterpriority>1</filterpriority>
	public override int Position
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int Count => 1;

	internal override bool IsSuspended => data_source == null;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyManager" /> class.</summary>
	public PropertyManager()
	{
	}

	internal PropertyManager(object data_source)
	{
		SetDataSource(data_source);
	}

	internal PropertyManager(object data_source, string property_name)
	{
		this.property_name = property_name;
		SetDataSource(data_source);
	}

	internal void SetDataSource(object new_data_source)
	{
		if (changed_event != null)
		{
			changed_event.RemoveEventHandler(data_source, property_value_changed_handler);
		}
		data_source = new_data_source;
		if (property_name == null)
		{
			return;
		}
		prop_desc = TypeDescriptor.GetProperties(data_source).Find(property_name, ignoreCase: true);
		if (prop_desc != null)
		{
			changed_event = TypeDescriptor.GetEvents(data_source).Find(property_name + "Changed", ignoreCase: false);
			if (changed_event != null)
			{
				property_value_changed_handler = PropertyValueChanged;
				changed_event.AddEventHandler(data_source, property_value_changed_handler);
			}
		}
	}

	private void PropertyValueChanged(object sender, EventArgs args)
	{
		OnCurrentChanged(args);
	}

	/// <filterpriority>1</filterpriority>
	public override void AddNew()
	{
		throw new NotSupportedException("AddNew is not supported for property to property binding");
	}

	/// <filterpriority>1</filterpriority>
	public override void CancelCurrentEdit()
	{
		if (data_source is IEditableObject editableObject)
		{
			editableObject.CancelEdit();
			PushData();
		}
	}

	/// <filterpriority>1</filterpriority>
	public override void EndCurrentEdit()
	{
		PullData();
		if (data_source is IEditableObject editableObject)
		{
			editableObject.EndEdit();
		}
	}

	internal override PropertyDescriptorCollection GetItemPropertiesInternal()
	{
		return TypeDescriptor.GetProperties(data_source);
	}

	/// <param name="index">The index of the row to delete. </param>
	/// <filterpriority>1</filterpriority>
	public override void RemoveAt(int index)
	{
		throw new NotSupportedException("RemoveAt is not supported for property to property binding");
	}

	/// <filterpriority>1</filterpriority>
	public override void ResumeBinding()
	{
	}

	/// <summary>Suspends the data binding between a data source and a data-bound property.</summary>
	/// <filterpriority>1</filterpriority>
	public override void SuspendBinding()
	{
	}

	/// <returns>The name of the list supplying the data for the binding.</returns>
	/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
	protected internal override string GetListName(ArrayList listAccessors)
	{
		return string.Empty;
	}

	/// <summary>Updates the current <see cref="T:System.Windows.Forms.Binding" /> between a data binding and a data-bound property.</summary>
	[System.MonoTODO("Stub, does nothing")]
	protected override void UpdateIsBinding()
	{
	}

	/// <param name="ea"></param>
	protected internal override void OnCurrentChanged(EventArgs ea)
	{
		PushData();
		if (onCurrentChangedHandler != null)
		{
			onCurrentChangedHandler(this, ea);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
	/// <param name="ea">An <see cref="T:System.EventArgs" /> containing the event data.</param>
	protected override void OnCurrentItemChanged(EventArgs ea)
	{
		throw new NotImplementedException();
	}
}
