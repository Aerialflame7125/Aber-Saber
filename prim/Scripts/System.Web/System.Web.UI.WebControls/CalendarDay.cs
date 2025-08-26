using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a date in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CalendarDay
{
	private DateTime date;

	private bool isWeekend;

	private bool isToday;

	private bool isSelected;

	private bool isOtherMonth;

	private string dayNumberText;

	private bool isSelectable;

	/// <summary>Gets the date represented by an instance of this class. This property is read-only.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. This allows you to programmatically control the appearance or behavior of the day, based on this value.</returns>
	public DateTime Date => date;

	/// <summary>Gets the string equivalent of the day number for the date represented by an instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class. This property is read-only.</summary>
	/// <returns>The string equivalent of the day number for the date represented by an instance of this class.</returns>
	public string DayNumberText => dayNumberText;

	/// <summary>Gets a value that indicates whether the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, <see langword="false" />.</returns>
	public bool IsOtherMonth => isOtherMonth;

	/// <summary>Gets or sets a value that indicates whether the date represented by an instance of this class can be selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the date can be selected; otherwise, <see langword="false" />.</returns>
	public bool IsSelectable
	{
		get
		{
			return isSelectable;
		}
		set
		{
			isSelectable = value;
		}
	}

	/// <summary>Gets a value that indicates whether the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, <see langword="false" />.</returns>
	public bool IsSelected => isSelected;

	/// <summary>Gets a value that indicates whether the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, <see langword="false" />.</returns>
	public bool IsToday => isToday;

	/// <summary>Gets a value that indicates whether the date represented by an instance of this class is a either Saturday or Sunday. This property is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, <see langword="false" />.</returns>
	public bool IsWeekend => isWeekend;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class.</summary>
	/// <param name="date">A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. </param>
	/// <param name="isWeekend">
	///       <see langword="true" /> to indicate that the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, <see langword="false" />. </param>
	/// <param name="isToday">
	///       <see langword="true" /> to indicate that the date represented by an instance of this class is the current date; otherwise, <see langword="false" />. </param>
	/// <param name="isSelected">
	///       <see langword="true" /> to indicate that the date represented by an instance of this class is selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, <see langword="false" />. </param>
	/// <param name="isOtherMonth">
	///       <see langword="true" /> to indicate that the date represented by an instance of this class is in a month other than the displayed month on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, <see langword="false" />. </param>
	/// <param name="dayNumberText">The day number for the date represented by this class. </param>
	public CalendarDay(DateTime date, bool isWeekend, bool isToday, bool isSelected, bool isOtherMonth, string dayNumberText)
	{
		this.date = date;
		this.isWeekend = isWeekend;
		this.isToday = isToday;
		this.isSelected = isSelected;
		this.isOtherMonth = isOtherMonth;
		this.dayNumberText = dayNumberText;
		isSelectable = false;
	}
}
