using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding;

/// <summary>Represents the state of model binding.</summary>
[Serializable]
public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
{
	private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);

	/// <summary>Gets the number of key/value pairs in the collection.</summary>
	/// <returns>The number of key/value pairs in the collection.</returns>
	public int Count => _innerDictionary.Count;

	/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the dictionary is read-only; otherwise, <see langword="false" />.</returns>
	public bool IsReadOnly => ((ICollection<KeyValuePair<string, ModelState>>)_innerDictionary).IsReadOnly;

	/// <summary>Gets a value that indicates whether there are any errors in any of the model state objects in the dictionary.</summary>
	/// <returns>
	///     <see langword="false" /> if any errors were found; otherwise, <see langword="true" />.</returns>
	public bool IsValid => Values.All((ModelState modelState) => modelState.Errors.Count == 0);

	/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
	/// <returns>The keys of the dictionary.</returns>
	public ICollection<string> Keys => _innerDictionary.Keys;

	/// <summary>Gets or sets the value that is associated with the specified key.</summary>
	/// <param name="key">The key.</param>
	/// <returns>The item.</returns>
	public ModelState this[string key]
	{
		get
		{
			_innerDictionary.TryGetValue(key, out var value);
			return value;
		}
		set
		{
			_innerDictionary[key] = value;
		}
	}

	/// <summary>Gets a collection that contains the values of the dictionary.</summary>
	/// <returns>The values of the dictionary.</returns>
	public ICollection<ModelState> Values => _innerDictionary.Values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class.</summary>
	public ModelStateDictionary()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class using an existing dictionary collection.</summary>
	/// <param name="dictionary">The dictionary.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is <see langword="null" />.</exception>
	public ModelStateDictionary(ModelStateDictionary dictionary)
	{
		if (dictionary == null)
		{
			throw new ArgumentNullException("dictionary");
		}
		foreach (KeyValuePair<string, ModelState> item in dictionary)
		{
			_innerDictionary.Add(item.Key, item.Value);
		}
	}

	/// <summary>Adds the specified item to the dictionary.</summary>
	/// <param name="item">The item.</param>
	public void Add(KeyValuePair<string, ModelState> item)
	{
		((ICollection<KeyValuePair<string, ModelState>>)_innerDictionary).Add(item);
	}

	/// <summary>Adds an item that has the specified key and value to the dictionary.</summary>
	/// <param name="key">The key.</param>
	/// <param name="value">The value.</param>
	public void Add(string key, ModelState value)
	{
		_innerDictionary.Add(key, value);
	}

	/// <summary>Adds a model error to the errors collection using the specified key and using the specified exception for the value.</summary>
	/// <param name="key">The key.</param>
	/// <param name="exception">The exception object.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
	public void AddModelError(string key, Exception exception)
	{
		GetModelStateForKey(key).Errors.Add(exception);
	}

	/// <summary>Adds the specified model error to the errors collection using the specified key and using the specified error message string for the value.</summary>
	/// <param name="key">The key.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
	public void AddModelError(string key, string errorMessage)
	{
		GetModelStateForKey(key).Errors.Add(errorMessage);
	}

	/// <summary>Removes all items from the dictionary.</summary>
	public void Clear()
	{
		_innerDictionary.Clear();
	}

	/// <summary>Determines whether the model-state dictionary contains a specific item.</summary>
	/// <param name="item">The item to locate in the model-state dictionary.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="item" /> is found in the dictionary; otherwise, <see langword="false" />.</returns>
	public bool Contains(KeyValuePair<string, ModelState> item)
	{
		return ((ICollection<KeyValuePair<string, ModelState>>)_innerDictionary).Contains(item);
	}

	/// <summary>Determines whether the model-state dictionary contains the specified key.</summary>
	/// <param name="key">The key.</param>
	/// <returns>
	///     <see langword="true" /> if the dictionary contains the specified key; otherwise, <see langword="false" />.</returns>
	public bool ContainsKey(string key)
	{
		return _innerDictionary.ContainsKey(key);
	}

	/// <summary>Copies the elements of the dictionary to an array, starting at a specified index.</summary>
	/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
	/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying starts.</param>
	public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
	{
		((ICollection<KeyValuePair<string, ModelState>>)_innerDictionary).CopyTo(array, arrayIndex);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the dictionary.</summary>
	/// <returns>An enumerator that can be used to iterate through the collection.</returns>
	public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
	{
		return _innerDictionary.GetEnumerator();
	}

	private ModelState GetModelStateForKey(string key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (!TryGetValue(key, out var value))
		{
			value = (this[key] = new ModelState());
		}
		return value;
	}

	/// <summary>Determines whether there are any <see cref="T:System.Web.ModelBinding.ModelError" /> objects that are associated with the specified key or that are prefixed with the specified key.</summary>
	/// <param name="key">The key.</param>
	/// <returns>
	///     <see langword="true" /> if any <see cref="T:System.Web.ModelBinding.ModelError" /> objects are associated with the specified key or prefixed with the specified key; otherwise, <see langword="false" />. If the key is not found in the dictionary, this method returns <see langword="true" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
	public bool IsValidField(string key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		return DictionaryHelpers.FindKeysWithPrefix(this, key).All((KeyValuePair<string, ModelState> entry) => entry.Value.Errors.Count == 0);
	}

	/// <summary>Copies the values from the specified model-state dictionary object into this dictionary, overwriting existing values if the keys are the same.</summary>
	/// <param name="dictionary">The model-state dictionary to be merged into this one.</param>
	public void Merge(ModelStateDictionary dictionary)
	{
		if (dictionary == null)
		{
			return;
		}
		foreach (KeyValuePair<string, ModelState> item in dictionary)
		{
			this[item.Key] = item.Value;
		}
	}

	/// <summary>Removes the first occurrence of the specified item from the model-state dictionary.</summary>
	/// <param name="item">The item to remove.</param>
	/// <returns>
	///     <see langword="true" /> if the item was successfully removed from the dictionary, or <see langword="false" /> if the item was not removed or was not found in the dictionary.</returns>
	public bool Remove(KeyValuePair<string, ModelState> item)
	{
		return ((ICollection<KeyValuePair<string, ModelState>>)_innerDictionary).Remove(item);
	}

	/// <summary>Removes the item that has the specified key from the dictionary.</summary>
	/// <param name="key">The key of the item to remove.</param>
	/// <returns>
	///     <see langword="true" /> if the item was successfully removed from the dictionary, or <see langword="false" /> if the item was not removed or was not found in the dictionary.</returns>
	public bool Remove(string key)
	{
		return _innerDictionary.Remove(key);
	}

	/// <summary>Sets the value for the specified key.</summary>
	/// <param name="key">The key.</param>
	/// <param name="value">The value.</param>
	public void SetModelValue(string key, ValueProviderResult value)
	{
		GetModelStateForKey(key).Value = value;
	}

	/// <summary>Attempts to gets the value that is associated with the specified key.</summary>
	/// <param name="key">The key of the value to get.</param>
	/// <param name="value">When this method returns, contains the value that is associated with the specified key, if the key was found; otherwise, contains the default value for the type of this parameter. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> if the dictionary contains an item that has the specified key; otherwise, <see langword="false" />.</returns>
	public bool TryGetValue(string key, out ModelState value)
	{
		return _innerDictionary.TryGetValue(key, out value);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
	/// <returns>An enumerator that can be used to iterate through the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)_innerDictionary).GetEnumerator();
	}
}
