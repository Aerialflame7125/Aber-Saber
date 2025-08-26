using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Util;

internal class ObjectSet : ICollection, IEnumerable
{
	private class EmptyEnumerator : IEnumerator
	{
		public object Current => null;

		public bool MoveNext()
		{
			return false;
		}

		public void Reset()
		{
		}
	}

	private static EmptyEnumerator _emptyEnumerator = new EmptyEnumerator();

	private IDictionary _objects;

	protected virtual bool CaseInsensitive => false;

	public int Count
	{
		get
		{
			if (_objects == null)
			{
				return 0;
			}
			return _objects.Keys.Count;
		}
	}

	bool ICollection.IsSynchronized
	{
		get
		{
			if (_objects == null)
			{
				return true;
			}
			return _objects.Keys.IsSynchronized;
		}
	}

	object ICollection.SyncRoot
	{
		get
		{
			if (_objects == null)
			{
				return this;
			}
			return _objects.Keys.SyncRoot;
		}
	}

	internal ObjectSet()
	{
	}

	public void Add(object o)
	{
		if (_objects == null)
		{
			_objects = new HybridDictionary(CaseInsensitive);
		}
		_objects[o] = null;
	}

	public void AddCollection(ICollection c)
	{
		foreach (object item in c)
		{
			Add(item);
		}
	}

	public void Remove(object o)
	{
		if (_objects != null)
		{
			_objects.Remove(o);
		}
	}

	public bool Contains(object o)
	{
		if (_objects == null)
		{
			return false;
		}
		return _objects.Contains(o);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		if (_objects == null)
		{
			return _emptyEnumerator;
		}
		return _objects.Keys.GetEnumerator();
	}

	public void CopyTo(Array array, int index)
	{
		if (_objects != null)
		{
			_objects.Keys.CopyTo(array, index);
		}
	}
}
