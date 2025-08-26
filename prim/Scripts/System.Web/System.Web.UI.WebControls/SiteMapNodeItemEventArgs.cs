namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemDataBound" /> events.</summary>
public class SiteMapNodeItemEventArgs : EventArgs
{
	private SiteMapNodeItem _item;

	/// <summary>Gets the node item that is the source of the event.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event.</returns>
	public SiteMapNodeItem Item => _item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object as the source of the event.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event. </param>
	public SiteMapNodeItemEventArgs(SiteMapNodeItem item)
	{
		_item = item;
	}
}
