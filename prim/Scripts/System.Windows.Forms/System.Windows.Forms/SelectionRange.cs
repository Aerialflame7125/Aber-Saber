using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a date selection range in a month calendar control.</summary>
/// <filterpriority>2</filterpriority>
[TypeConverter(typeof(SelectionRangeConverter))]
public sealed class SelectionRange
{
	private DateTime end;

	private DateTime start;

	/// <summary>Gets or sets the ending date and time of the selection range.</summary>
	/// <returns>The ending <see cref="T:System.DateTime" /> value of the range.</returns>
	/// <filterpriority>1</filterpriority>
	public DateTime End
	{
		get
		{
			return end;
		}
		set
		{
			if (end != value)
			{
				end = value;
			}
		}
	}

	/// <summary>Gets or sets the starting date and time of the selection range.</summary>
	/// <returns>The starting <see cref="T:System.DateTime" /> value of the range.</returns>
	/// <filterpriority>1</filterpriority>
	public DateTime Start
	{
		get
		{
			return start;
		}
		set
		{
			if (start != value)
			{
				start = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class.</summary>
	public SelectionRange()
	{
		end = DateTime.MaxValue.Date;
		start = DateTime.MinValue.Date;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified selection range.</summary>
	/// <param name="range">The existing <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
	public SelectionRange(SelectionRange range)
	{
		end = range.End;
		start = range.Start;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified beginning and ending dates.</summary>
	/// <param name="lower">The starting date in the <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
	/// <param name="upper">The ending date in the <see cref="T:System.Windows.Forms.SelectionRange" />. </param>
	public SelectionRange(DateTime lower, DateTime upper)
	{
		if (lower <= upper)
		{
			end = upper.Date;
			start = lower.Date;
		}
		else
		{
			end = lower.Date;
			start = upper.Date;
		}
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.SelectionRange" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.SelectionRange" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return "SelectionRange: Start: " + Start.ToString() + ", End: " + End.ToString();
	}
}
