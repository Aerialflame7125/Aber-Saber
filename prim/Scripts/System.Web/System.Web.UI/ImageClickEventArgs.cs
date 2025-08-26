namespace System.Web.UI;

/// <summary>Provides data for any events that occur when a user clicks an image-based ASP.NET server control, such as the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> or <see cref="T:System.Web.UI.WebControls.ImageButton" /> server controls. This class cannot be inherited.</summary>
public sealed class ImageClickEventArgs : EventArgs
{
	/// <summary>An integer that represents the x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
	public int X;

	/// <summary>An integer that represents the y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
	public int Y;

	/// <summary>An integer that represents the raw x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
	public double XRaw;

	/// <summary>An integer that represents the raw y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
	public double YRaw;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" /> and <paramref name="y" /> parameters.</summary>
	/// <param name="x">The x-coordinate where the user clicked an image-based ASP.NET server control. </param>
	/// <param name="y">The y-coordinate where the user clicked an image-based ASP.NET server control. </param>
	public ImageClickEventArgs(int x, int y)
	{
		X = x;
		Y = y;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" />, <paramref name="y" />, <paramref name="xRaw" />, and <paramref name="yRaw" /> parameters.</summary>
	/// <param name="x">The x-coordinate where a user clicked an image-based ASP.NET server control.</param>
	/// <param name="y">The y-coordinate where a user clicked an image-based ASP.NET server control.</param>
	/// <param name="xRaw">The raw x-coordinate where a user clicked an image-based ASP.NET server control.</param>
	/// <param name="yRaw">The raw y-coordinate where a user clicked an image-based ASP.NET server control.</param>
	public ImageClickEventArgs(int x, int y, double xRaw, double yRaw)
	{
		X = x;
		Y = y;
		XRaw = xRaw;
		YRaw = yRaw;
	}
}
