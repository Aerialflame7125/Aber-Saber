using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Represents the primary key field or fields of a record in a data-bound control.</summary>
public class DataKey : IStateManager, IEquatable<DataKey>
{
	private IOrderedDictionary keyTable;

	private string[] keyNames;

	private bool trackViewState;

	private IOrderedDictionary readonlyKeyTable;

	/// <summary>Gets the value of the key field at the specified index from a <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <param name="index">The zero-based index at which to retrieve the key field value.</param>
	/// <returns>The value of the key field at the specified index.</returns>
	public virtual object this[int index] => keyTable[index];

	/// <summary>Gets the value of the key field with the specified field name from a <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <param name="name">The name of the key field for which to retrieve the key field value.</param>
	/// <returns>The value of the key field with the specified field name.</returns>
	public virtual object this[string name] => keyTable[name];

	/// <summary>Gets the value of the key field at index 0 in the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <returns>The value of the key field at index 0 in the <see cref="T:System.Web.UI.WebControls.DataKey" />.</returns>
	public virtual object Value
	{
		get
		{
			if (keyTable.Count == 0)
			{
				return null;
			}
			return keyTable[0];
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains every key field in the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains every key field in the <see cref="T:System.Web.UI.WebControls.DataKey" />.</returns>
	public virtual IOrderedDictionary Values
	{
		get
		{
			if (readonlyKeyTable == null)
			{
				if (keyTable is OrderedDictionary)
				{
					readonlyKeyTable = ((OrderedDictionary)keyTable).AsReadOnly();
				}
				else
				{
					readonlyKeyTable = keyTable;
				}
			}
			return readonlyKeyTable;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKey" /> object is tracking its view-state changes.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the <see cref="T:System.Web.UI.WebControls.DataKey" /> is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	protected virtual bool IsTrackingViewState => trackViewState;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKey" /> object is tracking its view-state changes.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the <see cref="T:System.Web.UI.WebControls.DataKey" /> is tracking its view-state changes; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKey" /> class using the specified dictionary of key field values.</summary>
	/// <param name="keyTable">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />  that contains the key field values.</param>
	public DataKey(IOrderedDictionary keyTable)
		: this(keyTable, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKey" /> class using the specified dictionary of key field values and array of field names.</summary>
	/// <param name="keyTable">The key field values.</param>
	/// <param name="keyNames">An array of strings that contain the names of the key fields.</param>
	public DataKey(IOrderedDictionary keyTable, string[] keyNames)
	{
		this.keyTable = keyTable;
		this.keyNames = keyNames;
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.DataKey" /> array is equal to the current data key.</summary>
	/// <param name="other">The <see cref="T:System.Web.UI.WebControls.DataKey" /> object to compare to the current <see cref="T:System.Web.UI.WebControls.DataKey" />. object.</param>
	public bool Equals(DataKey other)
	{
		if (other == null)
		{
			return false;
		}
		IOrderedDictionary orderedDictionary = other.keyTable;
		if (keyTable != null && orderedDictionary != null)
		{
			if (keyTable.Count != orderedDictionary.Count)
			{
				return false;
			}
			foreach (object key in keyTable.Keys)
			{
				if (!orderedDictionary.Contains(key))
				{
					return false;
				}
				object obj = keyTable[key];
				object obj2 = orderedDictionary[key];
				if ((obj == null) ^ (obj2 == null))
				{
					return false;
				}
				if (!obj.Equals(obj2))
				{
					return false;
				}
			}
		}
		string[] array = other.keyNames;
		if (keyNames != null && array != null)
		{
			int num = keyNames.Length;
			if (num != array.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(keyNames[i], array[i], StringComparison.Ordinal) != 0)
				{
					return false;
				}
			}
		}
		else if ((keyNames == null) ^ (array == null))
		{
			return false;
		}
		return true;
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <param name="state">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKey" />.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="state" /> is not <see langword="null" /> and cannot be resolved to a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
	protected virtual void LoadViewState(object state)
	{
		if (state is Pair)
		{
			Pair obj = (Pair)state;
			object[] array = (object[])obj.First;
			object[] array2 = (object[])obj.Second;
			for (int i = 0; i < array.Length; i++)
			{
				keyTable[array[i]] = array2[i];
			}
		}
		else if (state is object[])
		{
			object[] array3 = (object[])state;
			for (int j = 0; j < array3.Length; j++)
			{
				keyTable[keyNames[j]] = array3[j];
			}
		}
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</returns>
	protected virtual object SaveViewState()
	{
		if (keyTable.Count == 0)
		{
			return null;
		}
		if (keyNames != null)
		{
			object[] array = new object[keyTable.Count];
			int num = 0;
			{
				foreach (object value in keyTable.Values)
				{
					array[num++] = value;
				}
				return array;
			}
		}
		object[] array2 = new object[keyTable.Count];
		object[] array3 = new object[keyTable.Count];
		int num2 = 0;
		foreach (DictionaryEntry item in keyTable)
		{
			array3[num2] = item.Key;
			array2[num2++] = item.Value;
		}
		return new Pair(array3, array2);
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	protected virtual void TrackViewState()
	{
		trackViewState = true;
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKey" />.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}
}
