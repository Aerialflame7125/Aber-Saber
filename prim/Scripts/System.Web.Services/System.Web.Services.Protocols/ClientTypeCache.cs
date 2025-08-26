using System.Collections;

namespace System.Web.Services.Protocols;

internal class ClientTypeCache
{
	private Hashtable cache = new Hashtable();

	internal object this[Type key] => cache[key];

	internal void Add(Type key, object value)
	{
		lock (this)
		{
			if (cache[key] == value)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			foreach (object key2 in cache.Keys)
			{
				hashtable.Add(key2, cache[key2]);
			}
			cache = hashtable;
			cache[key] = value;
		}
	}
}
