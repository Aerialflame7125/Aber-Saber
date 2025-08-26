using System.Collections;

namespace System.Web.Util;

internal class SimpleRecyclingCache
{
	private const int MAX_SIZE = 100;

	private static Hashtable _hashtable;

	internal object this[object key]
	{
		get
		{
			return _hashtable[key];
		}
		set
		{
			lock (this)
			{
				if (_hashtable.Count >= 100)
				{
					_hashtable.Clear();
				}
				_hashtable[key] = value;
			}
		}
	}

	internal SimpleRecyclingCache()
	{
		CreateHashtable();
	}

	private void CreateHashtable()
	{
		_hashtable = new Hashtable(100, StringComparer.OrdinalIgnoreCase);
	}
}
