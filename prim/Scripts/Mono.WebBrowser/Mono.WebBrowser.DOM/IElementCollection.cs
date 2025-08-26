using System.Collections;

namespace Mono.WebBrowser.DOM;

public interface IElementCollection : INodeList, IList, ICollection, IEnumerable
{
	new IElement this[int index] { get; set; }

	new int GetHashCode();
}
