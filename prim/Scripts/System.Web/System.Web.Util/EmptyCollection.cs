using System.Collections;

namespace System.Web.Util;

internal class EmptyCollection : ICollection, IEnumerable, IEnumerator
{
	private static EmptyCollection s_theEmptyCollection = new EmptyCollection();

	internal static EmptyCollection Instance => s_theEmptyCollection;

	public int Count => 0;

	bool ICollection.IsSynchronized => true;

	object ICollection.SyncRoot => this;

	object IEnumerator.Current => null;

	private EmptyCollection()
	{
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this;
	}

	public void CopyTo(Array array, int index)
	{
	}

	bool IEnumerator.MoveNext()
	{
		return false;
	}

	void IEnumerator.Reset()
	{
	}
}
