using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls;

/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Calendar" /> Web server control.</summary>
public class CalendarDesigner : ControlDesigner
{
	public override DesignerVerbCollection Verbs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.CalendarDesigner" /> class.</summary>
	public CalendarDesigner()
	{
	}

	/// <summary>Initializes the designer with the specified component.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> object for this designer.</param>
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when an auto-format scheme has been applied to the control.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected void OnAutoFormat(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
