using System.Collections;

namespace System.Web.UI;

internal class KeyedListEnumerator : IDictionaryEnumerator, IEnumerator
{
	private int index = -1;

	private ArrayList objs;

	public object Current
	{
		get
		{
			if (index < 0 || index >= objs.Count)
			{
				throw new InvalidOperationException();
			}
			return ((DictionaryEntry)objs[index]).Value;
		}
	}

	public DictionaryEntry Entry => (DictionaryEntry)Current;

	public object Key => Entry.Key;

	public object Value => Entry.Value;

	internal KeyedListEnumerator(ArrayList list)
	{
		objs = list;
	}

	public bool MoveNext()
	{
		index++;
		if (index >= objs.Count)
		{
			return false;
		}
		return true;
	}

	public void Reset()
	{
		index = -1;
	}
}
