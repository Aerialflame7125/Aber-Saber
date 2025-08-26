using System.ComponentModel;

namespace System.Web.UI.Design.WebControls;

/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Xml" /> Web server control.</summary>
public class XmlDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.XmlDesigner" /> class.</summary>
	public XmlDesigner()
	{
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebControls.XmlDesigner" /> control and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the HTML markup that is used to represent the control at design time.</summary>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the HTML that is used to fill an empty control.</summary>
	/// <returns>The HTML used to fill an empty control.</returns>
	protected override string GetEmptyDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the designer with the control that this instance of the designer is associated with.</summary>
	/// <param name="component">The associated control.</param>
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}
}
