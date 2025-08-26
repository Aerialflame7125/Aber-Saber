using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI;

internal class KeyedList : IOrderedDictionary, IDictionary, ICollection, IEnumerable
{
	private Hashtable objectTable = new Hashtable();

	private ArrayList objectList = new ArrayList();

	public int Count => objectList.Count;

	public bool IsFixedSize => false;

	public bool IsReadOnly => false;

	public bool IsSynchronized => false;

	public object this[int idx]
	{
		get
		{
			return ((DictionaryEntry)objectList[idx]).Value;
		}
		set
		{
			if (idx < 0 || idx >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object key = ((DictionaryEntry)objectList[idx]).Key;
			objectList[idx] = new DictionaryEntry(key, value);
			objectTable[key] = value;
		}
	}

	public object this[object key]
	{
		get
		{
			return objectTable[key];
		}
		set
		{
			if (objectTable.Contains(key))
			{
				objectTable[key] = value;
				objectTable[IndexOf(key)] = new DictionaryEntry(key, value);
			}
			else
			{
				Add(key, value);
			}
		}
	}

	public ICollection Keys
	{
		get
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < objectList.Count; i++)
			{
				arrayList.Add(((DictionaryEntry)objectList[i]).Key);
			}
			return arrayList;
		}
	}

	public ICollection Values
	{
		get
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < objectList.Count; i++)
			{
				arrayList.Add(((DictionaryEntry)objectList[i]).Value);
			}
			return arrayList;
		}
	}

	public object SyncRoot => this;

	public void Add(object key, object value)
	{
		objectTable.Add(key, value);
		objectList.Add(new DictionaryEntry(key, value));
	}

	public void Clear()
	{
		objectTable.Clear();
		objectList.Clear();
	}

	public bool Contains(object key)
	{
		return objectTable.Contains(key);
	}

	public void CopyTo(Array array, int idx)
	{
		objectTable.CopyTo(array, idx);
	}

	public void Insert(int idx, object key, object value)
	{
		if (idx > Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		objectTable.Add(key, value);
		objectList.Insert(idx, new DictionaryEntry(key, value));
	}

	public void Remove(object key)
	{
		objectTable.Remove(key);
		int num = IndexOf(key);
		if (num >= 0)
		{
			objectList.RemoveAt(num);
		}
	}

	public void RemoveAt(int idx)
	{
		if (idx >= Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		objectTable.Remove(((DictionaryEntry)objectList[idx]).Key);
		objectList.RemoveAt(idx);
	}

	IDictionaryEnumerator IDictionary.GetEnumerator()
	{
		return new KeyedListEnumerator(objectList);
	}

	IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
	{
		return new KeyedListEnumerator(objectList);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return new KeyedListEnumerator(objectList);
	}

	private int IndexOf(object key)
	{
		for (int i = 0; i < objectList.Count; i++)
		{
			if (((DictionaryEntry)objectList[i]).Key.Equals(key))
			{
				return i;
			}
		}
		return -1;
	}
}
