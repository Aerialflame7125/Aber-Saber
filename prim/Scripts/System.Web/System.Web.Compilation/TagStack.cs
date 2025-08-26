using System.Collections;
using System.Web.Util;

namespace System.Web.Compilation;

internal class TagStack
{
	private Stack tags;

	public int Count => tags.Count;

	public string Current => (string)tags.Peek();

	public TagStack()
	{
		tags = new Stack();
	}

	public void Push(string tagid)
	{
		tags.Push(tagid);
	}

	public string Pop()
	{
		if (tags.Count == 0)
		{
			return null;
		}
		return (string)tags.Pop();
	}

	public bool CompareTo(string tagid)
	{
		if (tags.Count == 0)
		{
			return false;
		}
		return string.Compare(tagid, (string)tags.Peek(), ignoreCase: true, Helpers.InvariantCulture) == 0;
	}
}
