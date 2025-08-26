namespace System.Web.UI.Design;

/// <summary>Defines an editable region of content within the design-time markup for the associated control.</summary>
public class TemplatedEditableDesignerRegion : EditableDesignerRegion
{
	/// <summary>Gets or sets whether the template occurs only once per instance of the containing control, such as a <see langword="header" /> template, or can appear many times according to available data, such as an <see langword="item" /> template.</summary>
	/// <returns>
	///   <see langword="true" /> if the template appears only once; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool IsSingleInstanceTemplate
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that indicates whether the template can be bound to a data source.</summary>
	/// <returns>
	///   <see langword="true" /> if the template represented by the region can be bound to a data source; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property.</exception>
	[System.MonoNotSupported("")]
	public override bool SupportsDataBinding
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.Design.TemplateDefinition" /> object describing the template that is referenced by the region.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.TemplateDefinition" /> object.</returns>
	[System.MonoNotSupported("")]
	public TemplateDefinition TemplateDefinition
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplatedEditableDesignerRegion" /> class using the provided template definition.</summary>
	/// <param name="templateDefinition">A <see cref="T:System.Web.UI.Design.TemplateDefinition" /> instance for the template to edit.</param>
	[System.MonoNotSupported("")]
	public TemplatedEditableDesignerRegion(TemplateDefinition templateDefinition)
		: base(null, null)
	{
		throw new NotImplementedException();
	}
}
