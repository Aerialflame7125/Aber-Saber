using System.Collections;

namespace System.Web.Compilation;

internal class OneNullCollection : ICollection, IEnumerable
{
	public int Count => 1;

	public bool IsSynchronized => false;

	public object SyncRoot => this;

	public void CopyTo(Array array, int index)
	{
		if (array == null)
		{
			throw new ArgumentNullException();
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (array.Rank > 1)
		{
			throw new ArgumentException();
		}
		int length = array.Length;
		if (index >= length || index > length - 1)
		{
			throw new ArgumentException();
		}
		array.SetValue(null, index);
	}

	public IEnumerator GetEnumerator()
	{
		yield return null;
	}
}
