using System.Collections;

namespace Mono.WebBrowser.DOM;

public interface IAttributeCollection : INodeList, IList, ICollection, IEnumerable
{
	IAttribute this[string name] { get; }

	bool Exists(string name);

	new int GetHashCode();
}
