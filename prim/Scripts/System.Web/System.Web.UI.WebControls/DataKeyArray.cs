using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects. This class cannot be inherited.</summary>
public sealed class DataKeyArray : ICollection, IEnumerable, IStateManager
{
	private IList keys;

	private bool trackViewState;

	/// <summary>Gets the number of items in the collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	public int Count => keys.Count;

	/// <summary>Gets a value indicating whether the items in the collection can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataKey" /> object from the collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataKey" /> to retrieve from the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.DataKey" /> at the specified index in the collection.</returns>
	public DataKey this[int index] => (DataKey)keys[index];

	/// <summary>Gets the object used to synchronize access to the collection.</summary>
	/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object is tracking its view-state changes.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => trackViewState;

	internal DataKeyArray(IList keys)
	{
		this.keys = keys;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> class.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.ArrayList" /> of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects with which to populate the collection.</param>
	public DataKeyArray(ArrayList keys)
	{
		this.keys = keys;
	}

	/// <summary>Copies all the items from this collection to the specified array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects, starting at the specified index in the array.</summary>
	/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that receives the copied items from the collection.</param>
	/// <param name="index">The first index in the specified array to receive the copied contents.</param>
	public void CopyTo(DataKey[] array, int index)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DataKey dataKey = (DataKey)enumerator.Current;
				array[index++] = dataKey;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	/// <summary>Copies all the items from this collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the collection.</param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				array.SetValue(current, index++);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return keys.GetEnumerator();
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			for (int i = 0; i < array.Length && i < keys.Count; i++)
			{
				((IStateManager)keys[i]).LoadViewState(array[i]);
			}
		}
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</returns>
	object IStateManager.SaveViewState()
	{
		if (keys.Count == 0)
		{
			return null;
		}
		object[] array = new object[keys.Count];
		for (int i = 0; i < keys.Count; i++)
		{
			array[i] = ((IStateManager)keys[i]).SaveViewState();
		}
		return array;
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
	void IStateManager.TrackViewState()
	{
		trackViewState = true;
		foreach (IStateManager key in keys)
		{
			key.TrackViewState();
		}
	}
}
