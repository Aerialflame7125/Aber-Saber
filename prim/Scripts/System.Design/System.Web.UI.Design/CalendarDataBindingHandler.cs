using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a data-binding handler for a calendar.</summary>
public class CalendarDataBindingHandler : DataBindingHandler
{
	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.CalendarDataBindingHandler" /> class.</summary>
	public CalendarDataBindingHandler()
	{
	}

	/// <summary>Sets the calendar's date to the current day if the <see cref="P:System.Web.UI.WebControls.Calendar.SelectedDate" /> property is data-bound.</summary>
	/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to which data binding will be added.</param>
	[System.MonoTODO]
	public override void DataBindControl(IDesignerHost designerHost, Control control)
	{
		throw new NotImplementedException();
	}
}
