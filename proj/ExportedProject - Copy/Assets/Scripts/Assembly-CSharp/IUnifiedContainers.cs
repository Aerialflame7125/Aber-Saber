using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.IUnified;

public class IUnifiedContainers<TContainer, TInterface> : IList<TInterface>, ICollection<TInterface>, IEnumerable<TInterface>, IEnumerable where TContainer : IUnifiedContainer<TInterface>, new() where TInterface : class
{
	private readonly Func<IList<TContainer>> _getList;

	public int Count
	{
		get
		{
			return _getList().Count;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			return _getList().IsReadOnly;
		}
	}

	public TInterface this[int index]
	{
		get
		{
			TContainer val = _getList()[index];
			return (val != null) ? val.Result : ((TInterface)null);
		}
		set
		{
			_getList()[index] = new TContainer
			{
				Result = value
			};
		}
	}

	public IUnifiedContainers(Func<IList<TContainer>> getList)
	{
		_getList = getList;
	}

	public IEnumerator<TInterface> GetEnumerator()
	{
		return (from c in _getList()
			select (c != null) ? c.Result : ((TInterface)null)).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add(TInterface item)
	{
		_getList().Add(new TContainer
		{
			Result = item
		});
	}

	public void Clear()
	{
		_getList().Clear();
	}

	public bool Contains(TInterface item)
	{
		return IndexOf(_getList(), item) >= 0;
	}

	public void CopyTo(TInterface[] array, int arrayIndex)
	{
		List<TInterface> list = (from c in _getList()
			select (c != null) ? c.Result : ((TInterface)null)).ToList();
		Array.Copy(list.ToArray(), 0, array, arrayIndex, list.Count);
	}

	public bool Remove(TInterface item)
	{
		IList<TContainer> list = _getList();
		int num = IndexOf(list, item);
		if (num < 0)
		{
			return false;
		}
		list.RemoveAt(num);
		return true;
	}

	public int IndexOf(TInterface item)
	{
		return IndexOf(_getList(), item);
	}

	public void Insert(int index, TInterface item)
	{
		_getList().Insert(index, new TContainer
		{
			Result = item
		});
	}

	public void RemoveAt(int index)
	{
		_getList().RemoveAt(index);
	}

	private static int IndexOf(IList<TContainer> list, TInterface item)
	{
		return list.FirstIndexWhere((TContainer c) => (item == null) ? (c == null || c.Result == null) : (c != null && c.Result == item));
	}
}
