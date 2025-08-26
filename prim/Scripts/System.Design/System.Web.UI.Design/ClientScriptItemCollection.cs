using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Represents a read-only collection of client script blocks that are contained within a Web Form or user control at design time. This class cannot be inherited.</summary>
public sealed class ClientScriptItemCollection : ReadOnlyCollectionBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ClientScriptItemCollection" /> class.</summary>
	/// <param name="clientScriptItems">An array of <see cref="T:System.Web.UI.Design.ClientScriptItem" /> elements used to initialize the collection.</param>
	public ClientScriptItemCollection(ClientScriptItem[] clientScriptItems)
	{
		if (clientScriptItems == null)
		{
			throw new ArgumentNullException("clientScriptItems");
		}
		base.InnerList.AddRange(clientScriptItems);
	}
}
