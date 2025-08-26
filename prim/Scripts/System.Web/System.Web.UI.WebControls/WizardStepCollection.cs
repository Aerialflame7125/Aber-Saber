using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in a control that acts as a wizard. This class cannot be inherited.</summary>
public sealed class WizardStepCollection : IList, ICollection, IEnumerable
{
	private ArrayList list = new ArrayList();

	private Wizard wizard;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control's <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	public int Count => list.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection can be modified.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection can be modified; otherwise, <see langword="false" />. </returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection at the specified index location.</returns>
	public WizardStepBase this[int index] => (WizardStepBase)list[index];

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
	public object SyncRoot => this;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets the object at the specified index in the collection.</summary>
	/// <param name="index">The index of the object to get from the collection.</param>
	/// <returns>The object to be retrieved.</returns>
	object IList.this[int index]
	{
		get
		{
			return list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	internal WizardStepCollection(Wizard wizard)
	{
		this.wizard = wizard;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the end of the collection.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is <see langword="null" />.</exception>
	public void Add(WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		wizardStep.SetWizard(wizard);
		list.Add(wizardStep);
		wizard.UpdateViews();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the collection at the specified index location.</summary>
	/// <param name="index">The index location at which to insert <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is <see langword="null" />.</exception>
	public void AddAt(int index, WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		wizardStep.SetWizard(wizard);
		list.Insert(index, wizardStep);
		wizard.UpdateViews();
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects from the collection.</summary>
	public void Clear()
	{
		list.Clear();
		wizard.UpdateViews();
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to find in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object is found in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="wizardStep" /> is <see langword="null" />.</exception>
	public bool Contains(WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		return list.Contains(wizardStep);
	}

	/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that receives the items copied from the collection.</param>
	/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
	public void CopyTo(WizardStepBase[] array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Determines the index value that represents the position of the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the collection.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to search for in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	/// <returns>If found, the zero-based index of the first occurrence of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in within the current <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, -1.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is <see langword="null" />.</exception>
	public int IndexOf(WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		return list.IndexOf(wizardStep);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object into the collection at the specified index location.</summary>
	/// <param name="index">The index location at which to insert the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to insert into the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	public void Insert(int index, WizardStepBase wizardStep)
	{
		AddAt(index, wizardStep);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove from the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is <see langword="null" />.</exception>
	public void Remove(WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		list.Remove(wizardStep);
		wizard.UpdateViews();
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified location.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove.</param>
	public void RemoveAt(int index)
	{
		list.RemoveAt(index);
		wizard.UpdateViews();
	}

	/// <summary>Appends the specified object to the end of the collection.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> to append to the end of the collection.</param>
	/// <returns>The position into which the new element was inserted.</returns>
	int IList.Add(object ob)
	{
		int result = list.Add((WizardStepBase)ob);
		wizard.UpdateViews();
		return result;
	}

	/// <summary>Determines whether the collection contains the specified object.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise <see langword="false" />.</returns>
	bool IList.Contains(object ob)
	{
		return Contains((WizardStepBase)ob);
	}

	/// <summary>Determines the index value that represents the position of the specified object in the collection.</summary>
	/// <param name="value">The object to search for in the collection.</param>
	/// <returns>The index value of the specified object in the collection.</returns>
	int IList.IndexOf(object ob)
	{
		return IndexOf((WizardStepBase)ob);
	}

	/// <summary>Inserts the specified object in the collection at the specified position.</summary>
	/// <param name="index">The index at which to insert the object into the collection.</param>
	/// <param name="value">The object to insert into the collection.</param>
	void IList.Insert(int index, object ob)
	{
		AddAt(index, (WizardStepBase)ob);
	}

	/// <summary>Removes the specified object from the collection.</summary>
	/// <param name="value">The object to remove from the collection.</param>
	void IList.Remove(object ob)
	{
		Remove((WizardStepBase)ob);
	}

	/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the items copied from the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}
}
