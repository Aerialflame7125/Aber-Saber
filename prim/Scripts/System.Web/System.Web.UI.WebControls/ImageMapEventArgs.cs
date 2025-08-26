namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ImageMap.Click" /> event of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
public class ImageMapEventArgs : EventArgs
{
	private string _postBackValue;

	/// <summary>Gets the <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</summary>
	/// <returns>The <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</returns>
	public string PostBackValue => _postBackValue;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageMapEventArgs" /> class.</summary>
	/// <param name="value">The <see cref="T:System.String" /> object assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked. </param>
	public ImageMapEventArgs(string value)
	{
		_postBackValue = value;
	}
}
