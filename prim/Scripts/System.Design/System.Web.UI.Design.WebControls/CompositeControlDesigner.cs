using System.ComponentModel;

namespace System.Web.UI.Design.WebControls;

/// <summary>Extends design-time behavior for controls that implement the methods of the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> abstract class.</summary>
public class CompositeControlDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.CompositeControlDesigner" /> class.</summary>
	public CompositeControlDesigner()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates the child controls of this <see cref="T:System.Web.UI.WebControls.CompositeControl" /> control.</summary>
	protected virtual void CreateChildControls()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
	/// <returns>The HTML that is used to represent the control at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	public override string GetDesignTimeHtml(DesignerRegionCollection regions)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the designer with the specified <see cref="T:System.ComponentModel.IComponent" /> object.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" />, which is the control associated with this designer.</param>
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}
}
