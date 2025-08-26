using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>A collection of <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects that represent the columns in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited. </summary>
public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
{
	private DataGrid owner;

	private ArrayList columns;

	private bool track;

	/// <summary>Gets the number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <returns>The number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
	[Browsable(false)]
	public int Count => columns.Count;

	/// <summary>Gets a value indicating whether the collection is tracking its view-state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if a <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> object is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => track;

	/// <summary>Gets a value that indicates whether the columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection can be modified.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	[Browsable(false)]
	public bool IsReadOnly => columns.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	[Browsable(false)]
	public bool IsSynchronized => columns.IsSynchronized;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to retrieve. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at the specified index.</returns>
	[Browsable(false)]
	public DataGridColumn this[int index] => (DataGridColumn)columns[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
	[Browsable(false)]
	public object SyncRoot => columns.SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> class.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that corresponds with this collection. </param>
	/// <param name="columns">A <see cref="T:System.Collections.ArrayList" /> that stores the collection of columns. </param>
	public DataGridColumnCollection(DataGrid owner, ArrayList columns)
	{
		this.owner = owner;
		this.columns = columns;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to the end of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to append to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
	public void Add(DataGridColumn column)
	{
		columns.Add(column);
		column.Set_Owner(owner);
		if (track)
		{
			((IStateManager)column).TrackViewState();
		}
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
	/// <param name="index">The index location in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at which to insert the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column. </param>
	/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to insert in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="column" /> is <see langword="null" />.</exception>
	public void AddAt(int index, DataGridColumn column)
	{
		columns.Insert(index, column);
		column.Set_Owner(owner);
		if (track)
		{
			((IStateManager)column).TrackViewState();
		}
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	public void Clear()
	{
		columns.Clear();
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	public void CopyTo(Array array, int index)
	{
		columns.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that contains all the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return columns.GetEnumerator();
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to search for in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
	/// <returns>The index position of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. The default value is <see langword="-1" />, which indicates that the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived object is not found.</returns>
	public int IndexOf(DataGridColumn column)
	{
		return columns.IndexOf(column);
	}

	[Obsolete("figure out what you need with me")]
	internal void OnColumnsChanged()
	{
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
	/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to remove from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
	public void Remove(DataGridColumn column)
	{
		columns.Remove(column);
	}

	/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to remove. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than <see langword="0" /> or greater than <see cref="P:System.Web.UI.WebControls.DataGridColumnCollection.Count" />.</exception>
	public void RemoveAt(int index)
	{
		columns.RemoveAt(index);
	}

	/// <summary>Loads the previously saved state.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		object[] array = (object[])savedState;
		if (array == null)
		{
			return;
		}
		int num = 0;
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				((IStateManager)enumerator.Current).LoadViewState(array[num++]);
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

	/// <summary>Returns an object containing state changes.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
	object IStateManager.SaveViewState()
	{
		object[] array = new object[Count];
		int num = 0;
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				IStateManager stateManager = (IStateManager)enumerator.Current;
				array[num++] = stateManager.SaveViewState();
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
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Starts tracking state changes.</summary>
	void IStateManager.TrackViewState()
	{
		track = true;
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				((IStateManager)enumerator.Current).TrackViewState();
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
}
