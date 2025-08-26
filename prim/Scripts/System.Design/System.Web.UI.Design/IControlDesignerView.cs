using System.ComponentModel.Design;
using System.Drawing;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for access to the visual representation and content of a control at design time.</summary>
public interface IControlDesignerView
{
	/// <summary>Gets the designer region that contains the associated control, if any.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.DesignerRegion" /> object if the associated control is contained in a designer region; otherwise <see langword="null" />.</returns>
	DesignerRegion ContainingRegion { get; }

	/// <summary>Gets the designer component for the naming container of the associated control, if any.</summary>
	/// <returns>An IDesigner object representing the designer component for the naming container for the associated control; otherwise <see langword="null" />.</returns>
	IDesigner NamingContainerDesigner { get; }

	/// <summary>Gets a value indicating whether designer regions are supported.</summary>
	/// <returns>
	///   <see langword="true" /> if designer regions are supported; otherwise <see langword="false" />.</returns>
	bool SupportsRegions { get; }

	/// <summary>An event raised by the design host for the view and designer component.</summary>
	event ViewEventHandler ViewEvent;

	/// <summary>Retrieves the outer bounds of the designer view.</summary>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> for which you want to retrieve the bounds.</param>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing information about the location and measurements of the view at design time.</returns>
	Rectangle GetBounds(DesignerRegion region);

	/// <summary>Notifies the design host that the area represented by the provided rectangle needs to be repainted on the design surface.</summary>
	/// <param name="rectangle">A <see cref="T:System.Drawing.Rectangle" /> representing the location and outer measurements of the view on the design surface. The coordinate-system origin for this rectangle is the top-left corner of the element to which the behavior is attached.</param>
	void Invalidate(Rectangle rectangle);

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IControlDesignerView.SetFlags(System.Web.UI.Design.ViewFlags,System.Boolean)" />.</summary>
	/// <param name="viewFlags">A member of the <see cref="T:System.Web.UI.Design.ViewFlags" /> enumeration.</param>
	/// <param name="setFlag">
	///   <see langword="true" /> to set the flag, <see langword="false" /> to cancel the flag.</param>
	void SetFlags(ViewFlags viewFlags, bool setFlag);

	/// <summary>Puts the provided content into the provided designer region.</summary>
	/// <param name="region">A <see cref="T:System.Web.UI.Design.DesignerRegion" /> into which the content is to be put.</param>
	/// <param name="content">The HTML markup to be put into the designer region.</param>
	void SetRegionContent(EditableDesignerRegion region, string content);

	/// <summary>Causes the associated control to redraw the invalidated regions within its client area.</summary>
	void Update();
}
