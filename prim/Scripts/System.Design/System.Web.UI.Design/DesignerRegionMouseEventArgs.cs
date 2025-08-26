using System.Drawing;

namespace System.Web.UI.Design;

/// <summary>Provides data for a <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event that is raised when you click on a selected control or a designer region in a selected control. This class cannot be inherited.</summary>
public sealed class DesignerRegionMouseEventArgs : EventArgs
{
	/// <summary>Gets the location within the control that was clicked.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> identifying the location within the region that was clicked.</returns>
	[System.MonoNotSupported("")]
	public Point Location
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the designer region that was clicked, if any.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> that the click event applies to, or <see langword="null" /> if no region was clicked.</returns>
	[System.MonoNotSupported("")]
	public DesignerRegion Region
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionMouseEventArgs" /> class with the specified region and location.</summary>
	/// <param name="region">The designer region that was clicked; used to initialize the <see cref="P:System.Web.UI.Design.DesignerRegionMouseEventArgs.Region" />.</param>
	/// <param name="location">The location that was clicked, relative to the upper left corner of the region; used to initialize the <see cref="P:System.Web.UI.Design.DesignerRegionMouseEventArgs.Location" />.</param>
	[System.MonoNotSupported("")]
	public DesignerRegionMouseEventArgs(DesignerRegion region, Point location)
	{
		throw new NotImplementedException();
	}
}
