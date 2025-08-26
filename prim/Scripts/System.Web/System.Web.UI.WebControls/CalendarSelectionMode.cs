namespace System.Web.UI.WebControls;

/// <summary>Specifies the date selection mode of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
public enum CalendarSelectionMode
{
	/// <summary>No dates can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	None,
	/// <summary>A single date can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	Day,
	/// <summary>A single date or entire week can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	DayWeek,
	/// <summary>A single date, week, or entire month can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	DayWeekMonth
}
