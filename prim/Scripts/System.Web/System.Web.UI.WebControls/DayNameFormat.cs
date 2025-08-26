namespace System.Web.UI.WebControls;

/// <summary>Specifies the display format for the days of the week on a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
public enum DayNameFormat
{
	/// <summary>The days of the week displayed in full format. For example, Monday.</summary>
	Full,
	/// <summary>The days of the week displayed in abbreviated format. For example, Mon represents Monday.</summary>
	Short,
	/// <summary>The days of the week displayed with just the first letter. For example, M represents Monday.</summary>
	FirstLetter,
	/// <summary>The days of the week displayed with just the first two letters. For example, Mo represents Monday.</summary>
	FirstTwoLetters,
	/// <summary>The days of the week displayed in the shortest abbreviation format possible for the current culture.</summary>
	Shortest
}
