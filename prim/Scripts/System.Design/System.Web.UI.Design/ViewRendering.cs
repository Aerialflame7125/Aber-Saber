namespace System.Web.UI.Design;

/// <summary>Contains the design-time markup for content and regions.</summary>
public class ViewRendering
{
	/// <summary>Gets the design-time HTML markup.</summary>
	/// <returns>The HTML markup to display at design time.</returns>
	[System.MonoNotSupported("")]
	public string Content
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects at design time.</summary>
	/// <returns>A collection of regions.</returns>
	[System.MonoNotSupported("")]
	public DesignerRegionCollection Regions
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ViewRendering" /> class by using the specified content and regions.</summary>
	/// <param name="content">HTML markup.</param>
	/// <param name="regions">A collection that contains the regions.</param>
	[System.MonoNotSupported("")]
	public ViewRendering(string content, DesignerRegionCollection regions)
	{
		throw new NotImplementedException();
	}
}
