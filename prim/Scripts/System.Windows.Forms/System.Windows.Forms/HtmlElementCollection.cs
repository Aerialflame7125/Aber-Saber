using System.Collections;
using System.Collections.Generic;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Defines a collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</summary>
/// <filterpriority>2</filterpriority>
public sealed class HtmlElementCollection : ICollection, IEnumerable
{
	private List<HtmlElement> elements;

	private IWebBrowser webHost;

	private WebBrowser owner;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.HtmlElementCollection" /> is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets the number of elements in the collection. </summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the number of elements in the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int Count => elements.Count;

	/// <summary>Gets an item from the collection by specifying its name.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />, if the named element is found. Otherwise, null.</returns>
	/// <param name="elementId">The <see cref="P:System.Windows.Forms.HtmlElement.Name" /> or <see cref="P:System.Windows.Forms.HtmlElement.Id" /> attribute of the element.</param>
	/// <filterpriority>1</filterpriority>
	public HtmlElement this[string elementId]
	{
		get
		{
			foreach (HtmlElement element in elements)
			{
				if (element.Id.Equals(elementId))
				{
					return element;
				}
			}
			return null;
		}
	}

	/// <summary>Gets an item from the collection by specifying its numerical index.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />.</returns>
	/// <param name="index">The position from which to retrieve an item from the collection.</param>
	/// <filterpriority>1</filterpriority>
	public HtmlElement this[int index]
	{
		get
		{
			if (index > elements.Count || index < 0)
			{
				return null;
			}
			return elements[index];
		}
	}

	internal HtmlElementCollection(WebBrowser owner, IWebBrowser webHost, IElementCollection col)
	{
		elements = new List<HtmlElement>();
		foreach (IElement item in col)
		{
			elements.Add(new HtmlElement(owner, webHost, item));
		}
		this.webHost = webHost;
		this.owner = owner;
	}

	private HtmlElementCollection(WebBrowser owner, IWebBrowser webHost, List<HtmlElement> elems)
	{
		elements = elems;
		this.webHost = webHost;
		this.owner = owner;
	}

	/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
	/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	void ICollection.CopyTo(Array dest, int index)
	{
		elements.CopyTo(dest as HtmlElement[], index);
	}

	/// <summary>Gets a collection of elements by their name.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing the elements whose <see cref="P:System.Windows.Forms.HtmlElement.Name" /> property match <paramref name="name" />. </returns>
	/// <param name="name">The name or ID of the element. </param>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection GetElementsByName(string name)
	{
		List<HtmlElement> list = new List<HtmlElement>();
		foreach (HtmlElement element in elements)
		{
			if (element.HasAttribute("name") && element.GetAttribute("name").Equals(name))
			{
				list.Add(new HtmlElement(owner, webHost, element.element));
			}
		}
		return new HtmlElementCollection(owner, webHost, list);
	}

	/// <summary>Returns an enumerator that iterates through a collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return elements.GetEnumerator();
	}
}
