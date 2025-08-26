using System.Collections.Generic;

namespace System.Web.Caching;

internal sealed class CacheItemLRU
{
	public delegate bool SelectItemsQualifier(CacheItem item);

	private Dictionary<string, LinkedListNode<CacheItem>> dict;

	private Dictionary<LinkedListNode<CacheItem>, string> revdict;

	private LinkedList<CacheItem> list;

	private Cache owner;

	private int highWaterMark;

	private int lowWaterMark;

	private bool needsEviction;

	public int Count => dict.Count;

	public CacheItem this[string key]
	{
		get
		{
			if (key == null)
			{
				return null;
			}
			if (dict.TryGetValue(key, out var value))
			{
				CacheItem value2 = value.Value;
				if (value2 == null || value2.Priority != CacheItemPriority.NotRemovable)
				{
					list.Remove(value);
					list.AddFirst(value);
				}
				return value2;
			}
			return null;
		}
		set
		{
			if (dict.TryGetValue(key, out var value2))
			{
				list.Remove(value2);
				if (value == null || value.Priority != CacheItemPriority.NotRemovable)
				{
					list.AddFirst(value2);
				}
				else
				{
					revdict.Remove(value2);
				}
				value2.Value = value;
				return;
			}
			needsEviction = dict.Count >= highWaterMark;
			value2 = new LinkedListNode<CacheItem>(value);
			if (value == null || value.Priority != CacheItemPriority.NotRemovable)
			{
				list.AddFirst(value2);
				revdict[value2] = key;
			}
			dict[key] = value2;
		}
	}

	public CacheItemLRU(Cache owner, int highWaterMark, int lowWaterMark)
	{
		list = new LinkedList<CacheItem>();
		dict = new Dictionary<string, LinkedListNode<CacheItem>>(StringComparer.Ordinal);
		revdict = new Dictionary<LinkedListNode<CacheItem>, string>();
		this.highWaterMark = highWaterMark;
		this.lowWaterMark = lowWaterMark;
		this.owner = owner;
	}

	public bool TryGetValue(string key, out CacheItem value)
	{
		if (dict.TryGetValue(key, out var value2))
		{
			value = value2.Value;
			return true;
		}
		value = null;
		return false;
	}

	public void EvictIfNecessary()
	{
		if (needsEviction)
		{
			for (int num = dict.Count; num > lowWaterMark; num--)
			{
				string key = revdict[list.Last];
				owner.Remove(key, CacheItemRemovedReason.Underused, doLock: false, invokeCallback: true);
			}
		}
	}

	public void InvokePrivateCallbacks()
	{
		foreach (KeyValuePair<string, LinkedListNode<CacheItem>> item in dict)
		{
			CacheItem value = item.Value.Value;
			if (value != null && !value.Disabled && value.OnRemoveCallback != null)
			{
				try
				{
					value.OnRemoveCallback(item.Key, value.Value, CacheItemRemovedReason.Removed);
				}
				catch
				{
				}
			}
		}
	}

	public List<CacheItem> SelectItems(SelectItemsQualifier qualifier)
	{
		List<CacheItem> list = new List<CacheItem>();
		foreach (LinkedListNode<CacheItem> value2 in dict.Values)
		{
			CacheItem value = value2.Value;
			if (qualifier(value))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<CacheItem> ToList()
	{
		List<CacheItem> list = new List<CacheItem>();
		if (dict.Count == 0)
		{
			return list;
		}
		foreach (LinkedListNode<CacheItem> value in dict.Values)
		{
			list.Add(value.Value);
		}
		return list;
	}

	public void Remove(string key)
	{
		if (key != null && dict.TryGetValue(key, out var value))
		{
			CacheItem value2 = value.Value;
			dict.Remove(key);
			if (value2 == null || value2.Priority != CacheItemPriority.NotRemovable)
			{
				revdict.Remove(value);
				list.Remove(value);
			}
		}
	}
}
