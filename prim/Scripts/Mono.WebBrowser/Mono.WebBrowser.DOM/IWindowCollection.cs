using System.Collections;

namespace Mono.WebBrowser.DOM;

public interface IWindowCollection : IList, ICollection, IEnumerable
{
	new IWindow this[int index] { get; set; }
}
