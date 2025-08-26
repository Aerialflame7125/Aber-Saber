using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Manages all <see cref="T:System.Windows.Forms.Binding" /> objects that are bound to the same data source and data member. This class is abstract.</summary>
/// <filterpriority>2</filterpriority>
public abstract class BindingManagerBase
{
	private BindingsCollection bindings;

	internal bool transfering_data;

	/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
	protected EventHandler onCurrentChangedHandler;

	/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
	protected EventHandler onPositionChangedHandler;

	internal EventHandler onCurrentItemChangedHandler;

	/// <summary>Gets the collection of bindings being managed.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
	/// <filterpriority>1</filterpriority>
	public BindingsCollection Bindings
	{
		get
		{
			if (bindings == null)
			{
				bindings = new BindingsCollection();
			}
			return bindings;
		}
	}

	/// <summary>When overridden in a derived class, gets the number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
	/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
	/// <filterpriority>1</filterpriority>
	public abstract int Count { get; }

	/// <summary>When overridden in a derived class, gets the current object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public abstract object Current { get; }

	/// <summary>Gets a value indicating whether binding is suspended.</summary>
	/// <returns>true if binding is suspended; otherwise, false.</returns>
	public bool IsBindingSuspended => IsSuspended;

	/// <summary>When overridden in a derived class, gets or sets the position in the underlying list that controls bound to this data source point to.</summary>
	/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
	/// <filterpriority>1</filterpriority>
	public abstract int Position { get; set; }

	internal virtual bool IsSuspended => false;

	/// <summary>Occurs when the currently bound item changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CurrentChanged
	{
		add
		{
			onCurrentChangedHandler = (EventHandler)Delegate.Combine(onCurrentChangedHandler, value);
		}
		remove
		{
			onCurrentChangedHandler = (EventHandler)Delegate.Remove(onCurrentChangedHandler, value);
		}
	}

	/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler PositionChanged
	{
		add
		{
			onPositionChangedHandler = (EventHandler)Delegate.Combine(onPositionChangedHandler, value);
		}
		remove
		{
			onPositionChangedHandler = (EventHandler)Delegate.Remove(onPositionChangedHandler, value);
		}
	}

	/// <summary>Occurs when the state of the currently bound item changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CurrentItemChanged
	{
		add
		{
			onCurrentItemChangedHandler = (EventHandler)Delegate.Combine(onCurrentItemChangedHandler, value);
		}
		remove
		{
			onCurrentItemChangedHandler = (EventHandler)Delegate.Remove(onCurrentItemChangedHandler, value);
		}
	}

	/// <summary>Occurs at the completion of a data-binding operation.</summary>
	public event BindingCompleteEventHandler BindingComplete;

	/// <summary>Occurs when an <see cref="T:System.Exception" /> is silently handled by the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </summary>
	public event BindingManagerDataErrorEventHandler DataError;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerBase" /> class.</summary>
	public BindingManagerBase()
	{
	}

	/// <summary>When overridden in a derived class, adds a new item to the underlying list.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void AddNew();

	/// <summary>When overridden in a derived class, cancels the current edit.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void CancelCurrentEdit();

	/// <summary>When overridden in a derived class, ends the current edit.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void EndCurrentEdit();

	/// <summary>When overridden in a derived class, gets the collection of property descriptors for the binding.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual PropertyDescriptorCollection GetItemProperties()
	{
		return GetItemPropertiesInternal();
	}

	internal virtual PropertyDescriptorCollection GetItemPropertiesInternal()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, deletes the row at the specified index from the underlying list.</summary>
	/// <param name="index">The index of the row to delete. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />. </exception>
	/// <filterpriority>1</filterpriority>
	public abstract void RemoveAt(int index);

	/// <summary>When overridden in a derived class, resumes data binding.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void ResumeBinding();

	/// <summary>When overridden in a derived class, suspends data binding.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void SuspendBinding();

	/// <summary>Gets the collection of property descriptors for the binding using the specified <see cref="T:System.Collections.ArrayList" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
	/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources. </param>
	/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the list of properties of the items managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
	/// <param name="listType">The <see cref="T:System.Type" /> of the bound list. </param>
	/// <param name="offset">A counter used to recursively call the method. </param>
	/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources. </param>
	/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, gets the name of the list supplying the data for the binding.</summary>
	/// <returns>The name of the list supplying the data for the binding.</returns>
	/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
	protected internal abstract string GetListName(ArrayList listAccessors);

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal abstract void OnCurrentChanged(EventArgs e);

	/// <summary>Pulls data from the data-bound control into the data source, returning no information.</summary>
	protected void PullData()
	{
		try
		{
			if (!transfering_data)
			{
				transfering_data = true;
				UpdateIsBinding();
			}
			foreach (Binding binding in Bindings)
			{
				binding.PullData();
			}
		}
		finally
		{
			transfering_data = false;
		}
	}

	/// <summary>Pushes data from the data source into the data-bound control, returning no information.</summary>
	protected void PushData()
	{
		try
		{
			if (!transfering_data)
			{
				transfering_data = true;
				UpdateIsBinding();
			}
			foreach (Binding binding in Bindings)
			{
				binding.PushData();
			}
		}
		finally
		{
			transfering_data = false;
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.BindingComplete" /> event. </summary>
	/// <param name="args">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
	protected void OnBindingComplete(BindingCompleteEventArgs args)
	{
		if (this.BindingComplete != null)
		{
			this.BindingComplete(this, args);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected abstract void OnCurrentItemChanged(EventArgs e);

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to occur.</param>
	protected void OnDataError(Exception e)
	{
		if (this.DataError != null)
		{
			this.DataError(this, new BindingManagerDataErrorEventArgs(e));
		}
	}

	/// <summary>When overridden in a derived class, updates the binding.</summary>
	protected abstract void UpdateIsBinding();

	internal void AddBinding(Binding binding)
	{
		if (!Bindings.Contains(binding))
		{
			Bindings.Add(binding);
		}
	}
}
