using System.Collections;

namespace Mono.WebBrowser.DOM;

public interface INodeList : IList, ICollection, IEnumerable
{
	new INode this[int index] { get; set; }

	new int GetHashCode();
}
