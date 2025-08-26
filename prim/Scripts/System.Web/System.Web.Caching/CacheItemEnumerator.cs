using System.Collections;
using System.Collections.Generic;

namespace System.Web.Caching;

internal sealed class CacheItemEnumerator : IDictionaryEnumerator, IEnumerator
{
	private List<CacheItem> list;

	private int pos = -1;

	private CacheItem Item
	{
		get
		{
			if (pos < 0 || pos >= list.Count)
			{
				throw new InvalidOperationException();
			}
			return list[pos];
		}
	}

	public DictionaryEntry Entry
	{
		get
		{
			CacheItem item = Item;
			if (item == null)
			{
				return new DictionaryEntry(null, null);
			}
			return new DictionaryEntry(item.Key, item.Value);
		}
	}

	public object Key => Item.Key;

	public object Value => Item.Value;

	public object Current => Entry;

	public CacheItemEnumerator(List<CacheItem> list)
	{
		this.list = list;
	}

	public bool MoveNext()
	{
		return ++pos < list.Count;
	}

	public void Reset()
	{
		pos = -1;
	}
}
