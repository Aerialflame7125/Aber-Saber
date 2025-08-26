using System.Collections;
using System.Collections.Generic;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Represents the windows contained within another <see cref="T:System.Windows.Forms.HtmlWindow" />.</summary>
/// <filterpriority>2</filterpriority>
public class HtmlWindowCollection : ICollection, IEnumerable
{
	private List<HtmlWindow> windows;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets the number of elements in the collection. </summary>
	/// <returns>The number of <see cref="T:System.Windows.Forms.HtmlWindow" /> objects in the current <see cref="T:System.Windows.Forms.HtmlWindowCollection" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int Count => windows.Count;

	/// <summary>Retrieves a frame window by supplying the frame's name.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> element corresponding to the supplied name. </returns>
	/// <param name="windowId">The name of the <see cref="T:System.Windows.Forms.HtmlWindow" /> to retrieve.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="windowId" /> is not the name of a Frame object in the current document or in any of its children.</exception>
	/// <filterpriority>1</filterpriority>
	public HtmlWindow this[string windowId]
	{
		get
		{
			foreach (HtmlWindow window in windows)
			{
				if (window.Name.Equals(windowId))
				{
					return window;
				}
			}
			return null;
		}
	}

	/// <summary>Retrieves a frame window by supplying the frame's position in the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> corresponding to the requested frame.</returns>
	/// <param name="index">The position of the <see cref="T:System.Windows.Forms.HtmlWindow" /> within the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is greater than the number of items in the collection.</exception>
	/// <filterpriority>1</filterpriority>
	public HtmlWindow this[int index]
	{
		get
		{
			if (index > windows.Count || index < 0)
			{
				return null;
			}
			return windows[index];
		}
	}

	internal HtmlWindowCollection(WebBrowser owner, IWebBrowser webHost, IWindowCollection col)
	{
		windows = new List<HtmlWindow>();
		foreach (IWindow item in col)
		{
			windows.Add(new HtmlWindow(owner, webHost, item));
		}
	}

	/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
	/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in Array at which copying begins.</param>
	void ICollection.CopyTo(Array dest, int index)
	{
		windows.CopyTo(dest as HtmlWindow[], index);
	}

	/// <summary>Returns an enumerator that can iterate through all elements in the <see cref="T:System.Windows.Forms.HtmlWindowCollection" />.</summary>
	/// <returns>The <see cref="T:System.Collections.IEnumerator" /> that enables enumeration of this collection's elements.</returns>
	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return windows.GetEnumerator();
	}
}
