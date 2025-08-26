using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>Provides designer functionality for controls that contain child controls or properties that can be modified at design time.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class ContainerControlDesigner : ControlDesigner
{
	/// <summary>Gets a value indicating if the control can be resized at design time.</summary>
	/// <returns>
	///   <see langword="true" />, if the control can be resized; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool AllowResize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the caption that is displayed for a control at design time.</summary>
	/// <returns>The string used for the control frame caption at design time, if the control has a design-time caption; otherwise, an empty string ("").</returns>
	[System.MonoTODO]
	public virtual string FrameCaption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the style that is applied to the control frame at design time.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> for the control frame at design time.</returns>
	[System.MonoTODO]
	public virtual Style FrameStyle
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ContainerControlDesigner" /> class.</summary>
	public ContainerControlDesigner()
	{
	}

	/// <summary>Adds the style attributes for the control at design time.</summary>
	/// <param name="styleAttributes">A keyed collection of style attributes.</param>
	[System.MonoTODO]
	protected virtual void AddDesignTimeCssAttributes(IDictionary styleAttributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the collection of style attributes for the control at design time.</summary>
	/// <returns>A collection of style attributes applied to the control on the design surface. The style attribute names are keys used to access the style attribute values in the <see cref="T:System.Collections.IDictionary" />.</returns>
	[System.MonoTODO]
	public virtual IDictionary GetDesignTimeCssAttributes()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the HTML markup that is used to represent the control at design time.</summary>
	/// <param name="regions">A collection of designer regions.</param>
	/// <returns>An HTML markup string that represents the control.</returns>
	[System.MonoTODO]
	public override string GetDesignTimeHtml(DesignerRegionCollection regions)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the content for the editable region of the control at design time.</summary>
	/// <param name="region">An editable design region contained within the control.</param>
	/// <returns>The persisted content of the region contained within the <see cref="T:System.Web.UI.Design.ContainerControlDesigner" />.</returns>
	[System.MonoTODO]
	public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the persistable content of the control at design time.</summary>
	/// <returns>
	///   <see langword="null" />.</returns>
	[System.MonoTODO]
	public override string GetPersistenceContent()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the content for the editable region of the control at design time.</summary>
	/// <param name="region">An editable design region contained within the control.</param>
	/// <param name="content">Content to assign for the editable design region.</param>
	[System.MonoTODO]
	public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
	{
		throw new NotImplementedException();
	}
}
