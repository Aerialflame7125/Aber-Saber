using System.Collections.Generic;

namespace System.Web.Configuration;

internal class LruCache<TKey, TValue>
{
	private Dictionary<TKey, LinkedListNode<TValue>> dict;

	private Dictionary<LinkedListNode<TValue>, TKey> revdict;

	private LinkedList<TValue> list;

	private int entry_limit;

	private bool eviction_warning_shown;

	private int evictions;

	internal string EvictionWarning { private get; set; }

	public LruCache(int entryLimit)
	{
		entry_limit = entryLimit;
		dict = new Dictionary<TKey, LinkedListNode<TValue>>();
		revdict = new Dictionary<LinkedListNode<TValue>, TKey>();
		list = new LinkedList<TValue>();
	}

	private void Evict()
	{
		LinkedListNode<TValue> last = list.Last;
		if (last != null)
		{
			TKey key = revdict[last];
			dict.Remove(key);
			revdict.Remove(last);
			list.RemoveLast();
			DisposeValue(last.Value);
			evictions++;
			if (!string.IsNullOrEmpty(EvictionWarning) && !eviction_warning_shown && evictions >= entry_limit)
			{
				Console.Error.WriteLine("WARNING: " + EvictionWarning);
				eviction_warning_shown = true;
			}
		}
	}

	public void Clear()
	{
		foreach (TValue item in list)
		{
			DisposeValue(item);
		}
		dict.Clear();
		revdict.Clear();
		list.Clear();
		eviction_warning_shown = false;
		evictions = 0;
	}

	private void DisposeValue(TValue value)
	{
		if (value is IDisposable)
		{
			((IDisposable)(object)value).Dispose();
		}
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		if (dict.TryGetValue(key, out var value2))
		{
			list.Remove(value2);
			list.AddFirst(value2);
			value = value2.Value;
			return true;
		}
		value = default(TValue);
		return false;
	}

	public void Add(TKey key, TValue value)
	{
		if (dict.TryGetValue(key, out var value2))
		{
			list.Remove(value2);
			list.AddFirst(value2);
			DisposeValue(value2.Value);
			value2.Value = value;
			return;
		}
		if (dict.Count >= entry_limit)
		{
			Evict();
		}
		value2 = new LinkedListNode<TValue>(value);
		list.AddFirst(value2);
		dict[key] = value2;
		revdict[value2] = key;
	}

	public override string ToString()
	{
		return "LRUCache dict={0} revdict={1} list={2}";
	}
}
