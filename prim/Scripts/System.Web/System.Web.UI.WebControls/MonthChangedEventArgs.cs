namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
public class MonthChangedEventArgs
{
	private DateTime newDate;

	private DateTime previousDate;

	/// <summary>Gets the date that determines the currently displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
	public DateTime NewDate => newDate;

	/// <summary>Gets the date that determines the previously displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
	public DateTime PreviousDate => previousDate;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MonthChangedEventArgs" /> class.</summary>
	/// <param name="newDate">The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
	/// <param name="previousDate">The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
	public MonthChangedEventArgs(DateTime newDate, DateTime previousDate)
	{
		this.newDate = newDate;
		this.previousDate = previousDate;
	}
}
