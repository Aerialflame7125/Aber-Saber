using System.Collections;

namespace System.Web.Compilation;

internal class ParserStack
{
	private Hashtable files;

	private Stack parsers;

	private AspParser current;

	public int Count => parsers.Count;

	public AspParser Parser => current;

	public string Filename => current.Filename;

	public ParserStack()
	{
		files = new Hashtable();
		parsers = new Stack();
	}

	public bool Push(AspParser parser)
	{
		if (files.Contains(parser.Filename))
		{
			return false;
		}
		files[parser.Filename] = true;
		parsers.Push(parser);
		current = parser;
		return true;
	}

	public AspParser Pop()
	{
		if (parsers.Count == 0)
		{
			return null;
		}
		files.Remove(current.Filename);
		AspParser result = (AspParser)parsers.Pop();
		if (parsers.Count > 0)
		{
			current = (AspParser)parsers.Peek();
			return result;
		}
		current = null;
		return result;
	}
}
