namespace System.Web.UI.Design;

/// <summary>Represents an editable content region within the design-time markup for the associated control.</summary>
public class EditableDesignerRegion : DesignerRegion
{
	/// <summary>Gets or sets the HTML markup for the content of the region.</summary>
	/// <returns>HTML markup representing the content of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> object.</returns>
	[System.MonoNotSupported("")]
	public virtual string Content
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the region can accept only Web server controls.</summary>
	/// <returns>
	///   <see langword="true" /> if the region can contain only Web server controls; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool ServerControlsOnly
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the region can be bound to a data source.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> content supports binding to a data source; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool SupportsDataBinding
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> class using the given owner and name.</summary>
	/// <param name="owner">A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object, or a designer that derives from <see cref="T:System.Web.UI.Design.ControlDesigner" />.</param>
	/// <param name="name">The name of the region.</param>
	[System.MonoNotSupported("")]
	public EditableDesignerRegion(ControlDesigner owner, string name)
		: base(owner, name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.EditableDesignerRegion" /> class using the given owner and name and the initial value of the <see cref="P:System.Web.UI.Design.EditableDesignerRegion.ServerControlsOnly" /> property.</summary>
	/// <param name="owner">A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object, or a designer that derives from <see cref="T:System.Web.UI.Design.ControlDesigner" />.</param>
	/// <param name="name">The name of the region.</param>
	/// <param name="serverControlsOnly">
	///   <see langword="true" /> to have the region accept only Web server controls for content; otherwise, <see langword="false" />.</param>
	[System.MonoNotSupported("")]
	public EditableDesignerRegion(ControlDesigner owner, string name, bool serverControlsOnly)
		: base(owner, name, serverControlsOnly)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a <see cref="T:System.Web.UI.Design.ViewRendering" /> object containing the design-time HTML markup for the given control.</summary>
	/// <param name="control">The control for which to get the <see cref="T:System.Web.UI.Design.ViewRendering" /> object for the current region.</param>
	/// <returns>A <see cref="T:System.Web.UI.Design.ViewRendering" /> object.</returns>
	[System.MonoNotSupported("")]
	public virtual ViewRendering GetChildViewRendering(Control control)
	{
		throw new NotImplementedException();
	}
}
