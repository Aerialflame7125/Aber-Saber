using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Binding" /> objects for a control.</summary>
/// <filterpriority>2</filterpriority>
[DefaultEvent("CollectionChanged")]
public class BindingsCollection : BaseCollection
{
	/// <summary>Gets the total number of bindings in the collection.</summary>
	/// <returns>The total number of bindings in the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public override int Count => base.Count;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</returns>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to find. </param>
	/// <exception cref="T:System.IndexOutOfRangeException">The collection doesn't contain an item at the specified index. </exception>
	/// <filterpriority>1</filterpriority>
	public Binding this[int index] => (Binding)base.List[index];

	/// <summary>Gets the bindings in the collection as an object.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing all of the collection members.</returns>
	protected override ArrayList List => base.List;

	/// <summary>Occurs when the collection has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	/// <summary>Occurs when the collection is about to change.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanging;

	internal BindingsCollection()
	{
	}

	/// <summary>Adds the specified binding to the collection.</summary>
	/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection. </param>
	protected internal void Add(Binding binding)
	{
		AddCore(binding);
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
	/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataBinding" /> argument was null. </exception>
	protected virtual void AddCore(Binding dataBinding)
	{
		CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, dataBinding);
		OnCollectionChanging(collectionChangeEventArgs);
		base.List.Add(dataBinding);
		OnCollectionChanged(collectionChangeEventArgs);
	}

	/// <summary>Clears the collection of binding objects.</summary>
	protected internal void Clear()
	{
		ClearCore();
	}

	/// <summary>Clears the collection of any members.</summary>
	protected virtual void ClearCore()
	{
		CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
		OnCollectionChanging(collectionChangeEventArgs);
		base.List.Clear();
		OnCollectionChanged(collectionChangeEventArgs);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanged" /> event.</summary>
	/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
	protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
	{
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, ccevent);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanging" /> event. </summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains event data.</param>
	protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
	{
		if (this.CollectionChanging != null)
		{
			this.CollectionChanging(this, e);
		}
	}

	/// <summary>Deletes the specified binding from the collection.</summary>
	/// <param name="binding">The Binding to remove from the collection. </param>
	protected internal void Remove(Binding binding)
	{
		RemoveCore(binding);
	}

	/// <summary>Deletes the binding from the collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to remove. </param>
	protected internal void RemoveAt(int index)
	{
		base.List.RemoveAt(index);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, base.List));
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.Binding" /> from the collection.</summary>
	/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to remove. </param>
	protected virtual void RemoveCore(Binding dataBinding)
	{
		CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataBinding);
		OnCollectionChanging(collectionChangeEventArgs);
		base.List.Remove(dataBinding);
		OnCollectionChanged(collectionChangeEventArgs);
	}

	/// <summary>Gets a value that indicates whether the collection should be serialized.</summary>
	/// <returns>true if the collection count is greater than zero; otherwise, false.</returns>
	protected internal bool ShouldSerializeMyAll()
	{
		if (Count > 0)
		{
			return true;
		}
		return false;
	}

	internal bool Contains(Binding binding)
	{
		return List.Contains(binding);
	}
}
