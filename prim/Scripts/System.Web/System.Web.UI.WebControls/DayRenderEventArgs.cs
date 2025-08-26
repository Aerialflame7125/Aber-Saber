namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This class cannot be inherited.</summary>
public sealed class DayRenderEventArgs
{
	private TableCell cell;

	private CalendarDay day;

	private string _selectUrl;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
	public TableCell Cell => cell;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> object that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
	public CalendarDay Day => day;

	/// <summary>Gets the script used to post the page back to the server when the date being rendered is selected in a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>The script used to post the page back to the server when the date being rendered is selected.</returns>
	public string SelectUrl => _selectUrl;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell and calendar day.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
	/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
	public DayRenderEventArgs(TableCell cell, CalendarDay day)
	{
		this.cell = cell;
		this.day = day;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell, calendar day, and selection URL.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
	/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
	/// <param name="selectUrl">The script used to post the page back to the server when the user selects the date being rendered.</param>
	public DayRenderEventArgs(TableCell cell, CalendarDay day, string selectUrl)
		: this(cell, day)
	{
		_selectUrl = selectUrl;
	}
}
