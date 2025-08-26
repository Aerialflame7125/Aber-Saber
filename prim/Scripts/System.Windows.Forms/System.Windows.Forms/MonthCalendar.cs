using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms;

/// <summary>Represents a Windows control that enables the user to select a date using a visual monthly calendar display.</summary>
/// <filterpriority>1</filterpriority>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultBindingProperty("SelectionRange")]
[DefaultProperty("SelectionRange")]
[Designer("System.Windows.Forms.Design.MonthCalendarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultEvent("DateChanged")]
public class MonthCalendar : Control
{
	/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	public enum HitArea
	{
		/// <summary>The specified point is either not on the month calendar control, or it is in an inactive portion of the control.</summary>
		Nowhere,
		/// <summary>The specified point is over the background of a month's title.</summary>
		TitleBackground,
		/// <summary>The specified point is in a month's title bar, over a month name.</summary>
		TitleMonth,
		/// <summary>The specified point is in a month's title bar, over the year value.</summary>
		TitleYear,
		/// <summary>The specified point is over the button at the upper-right corner of the control. If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
		NextMonthButton,
		/// <summary>The specified point is over the button at the upper-left corner of the control. If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
		PrevMonthButton,
		/// <summary>The specified point is part of the calendar's background.</summary>
		CalendarBackground,
		/// <summary>The specified point is on a date within the calendar. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the date at the specified point.</summary>
		Date,
		/// <summary>The specified point is over a date from the next month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
		NextMonthDate,
		/// <summary>The specified point is over a date from the previous month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
		PrevMonthDate,
		/// <summary>The specified point is over a day abbreviation ("Fri", for example). The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to January 1, 0001.</summary>
		DayOfWeek,
		/// <summary>The specified point is over a week number. This occurs only if the <see cref="P:System.Windows.Forms.MonthCalendar.ShowWeekNumbers" /> property of <see cref="T:System.Windows.Forms.MonthCalendar" /> is enabled. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the corresponding date in the leftmost column.</summary>
		WeekNumbers,
		/// <summary>The specified point is on the today link at the bottom of the month calendar control.</summary>
		TodayLink
	}

	internal enum HitAreaExtra
	{
		YearRectangle,
		UpButton,
		DownButton
	}

	/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control. This class cannot be inherited.</summary>
	public sealed class HitTestInfo
	{
		private HitArea hit_area;

		private Point point;

		private DateTime time;

		internal HitAreaExtra hit_area_extra;

		internal DateTime hit_time;

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> that represents the area of the calendar evaluated by the hit-test operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> values. The default is <see cref="F:System.Windows.Forms.MonthCalendar.HitArea.Nowhere" />.</returns>
		public HitArea HitArea => hit_area;

		/// <summary>Gets the point that was hit-tested.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values tested.</returns>
		public Point Point => point;

		/// <summary>Gets the time information specific to the location that was hit-tested.</summary>
		/// <returns>A <see cref="T:System.DateTime" />.</returns>
		public DateTime Time => time;

		internal HitTestInfo()
		{
			hit_area = HitArea.Nowhere;
			point = new Point(0, 0);
			time = DateTime.Now;
		}

		internal HitTestInfo(HitArea hit_area, Point point, DateTime time)
		{
			this.hit_area = hit_area;
			this.point = point;
			this.time = time;
			hit_time = time;
		}

		internal HitTestInfo(HitArea hit_area, Point point, DateTime time, DateTime hit_time)
		{
			this.hit_area = hit_area;
			this.point = point;
			this.time = time;
			this.hit_time = hit_time;
		}

		internal HitTestInfo(HitArea hit_area, Point point, DateTime time, HitAreaExtra hit_area_extra)
		{
			this.hit_area = hit_area;
			this.hit_area_extra = hit_area_extra;
			this.point = point;
			this.time = time;
		}
	}

	private const int initial_delay = 500;

	private const int subsequent_delay = 100;

	private ArrayList annually_bolded_dates;

	private ArrayList monthly_bolded_dates;

	private ArrayList bolded_dates;

	private Size calendar_dimensions;

	private Day first_day_of_week;

	private DateTime max_date;

	private int max_selection_count;

	private DateTime min_date;

	private int scroll_change;

	private SelectionRange selection_range;

	private bool show_today;

	private bool show_today_circle;

	private bool show_week_numbers;

	private Color title_back_color;

	private Color title_fore_color;

	private DateTime today_date;

	private bool today_date_set;

	private Color trailing_fore_color;

	private ContextMenu today_menu;

	private ContextMenu month_menu;

	private Timer timer;

	private Timer updown_timer;

	private bool is_year_going_up;

	private bool is_year_going_down;

	private bool is_mouse_moving_year;

	private int year_moving_count;

	private bool date_selected_event_pending;

	private bool right_to_left_layout;

	internal bool show_year_updown;

	internal DateTime current_month;

	internal DateTimePicker owner;

	internal int button_x_offset;

	internal Size button_size;

	internal Size title_size;

	internal Size date_cell_size;

	internal Size calendar_spacing;

	internal int divider_line_offset;

	internal DateTime clicked_date;

	internal Rectangle clicked_rect;

	internal bool is_date_clicked;

	internal bool is_previous_clicked;

	internal bool is_next_clicked;

	internal bool is_shift_pressed;

	internal DateTime first_select_start_date;

	internal int last_clicked_calendar_index;

	internal Rectangle last_clicked_calendar_rect;

	internal Font bold_font;

	internal StringFormat centered_format;

	private Point month_title_click_location;

	private bool[] click_state;

	private static object DateChangedEvent;

	private static object DateSelectedEvent;

	private static object RightToLeftLayoutChangedEvent;

	private static object UIAMaxSelectionCountChangedEvent;

	private static object UIASelectionChangedEvent;

	/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which annual days are displayed in bold.</summary>
	/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public DateTime[] AnnuallyBoldedDates
	{
		get
		{
			if (annually_bolded_dates == null || annually_bolded_dates.Count == 0)
			{
				return new DateTime[0];
			}
			DateTime[] array = new DateTime[annually_bolded_dates.Count];
			annually_bolded_dates.CopyTo(array);
			return array;
		}
		set
		{
			if (annually_bolded_dates == null)
			{
				annually_bolded_dates = new ArrayList(value);
			}
			else
			{
				annually_bolded_dates.Clear();
				annually_bolded_dates.AddRange(value);
			}
			UpdateBoldedDates();
		}
	}

	/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /></summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	/// <summary>Gets or sets a value indicating the layout for the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override ImageLayout BackgroundImageLayout
	{
		get
		{
			return base.BackgroundImageLayout;
		}
		set
		{
			base.BackgroundImageLayout = value;
		}
	}

	/// <summary>Gets or sets the background color for the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which nonrecurring dates are displayed in bold.</summary>
	/// <returns>The array of bold dates.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public DateTime[] BoldedDates
	{
		get
		{
			if (bolded_dates == null || bolded_dates.Count == 0)
			{
				return new DateTime[0];
			}
			DateTime[] array = new DateTime[bolded_dates.Count];
			bolded_dates.CopyTo(array);
			return array;
		}
		set
		{
			if (bolded_dates == null)
			{
				bolded_dates = new ArrayList(value);
			}
			else
			{
				bolded_dates.Clear();
				bolded_dates.AddRange(value);
			}
			UpdateBoldedDates();
		}
	}

	/// <summary>Gets or sets the number of columns and rows of months displayed.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> with the number of columns and rows to use to display the calendar.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public Size CalendarDimensions
	{
		get
		{
			return calendar_dimensions;
		}
		set
		{
			if (value.Width < 0 || value.Height < 0)
			{
				throw new ArgumentException();
			}
			if (!(calendar_dimensions != value))
			{
				return;
			}
			if (value.Width * value.Height > 12)
			{
				if (value.Width > 12 && value.Height > 12)
				{
					calendar_dimensions = new Size(4, 3);
				}
				else if (value.Width > 12)
				{
					for (int num = 12; num > 0; num--)
					{
						if (num * value.Height <= 12)
						{
							calendar_dimensions = new Size(num, value.Height);
							break;
						}
					}
				}
				else if (value.Height > 12)
				{
					for (int num2 = 12; num2 > 0; num2--)
					{
						if (num2 * value.Width <= 12)
						{
							calendar_dimensions = new Size(value.Width, num2);
							break;
						}
					}
				}
			}
			else
			{
				calendar_dimensions = value;
			}
			Invalidate();
		}
	}

	/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
	/// <returns>true if the control should use a secondary buffer to redraw; otherwise, false. The default is false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override bool DoubleBuffered
	{
		get
		{
			return base.DoubleBuffered;
		}
		set
		{
			base.DoubleBuffered = value;
		}
	}

	/// <summary>Gets or sets the first day of the week as displayed in the month calendar.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Day" /> values. The default is <see cref="F:System.Windows.Forms.Day.Default" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Day" /> enumeration members. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(Day.Default)]
	public Day FirstDayOfWeek
	{
		get
		{
			return first_day_of_week;
		}
		set
		{
			if (first_day_of_week != value)
			{
				first_day_of_week = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the control.</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ImeMode ImeMode
	{
		get
		{
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	/// <summary>Gets or sets the maximum allowable date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> representing the maximum allowable date. The default is 12/31/9998.</returns>
	/// <exception cref="T:System.ArgumentException">The value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DateTime MaxDate
	{
		get
		{
			return max_date;
		}
		set
		{
			if (value < MinDate)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MaxDate'. 'MaxDate' must be greater than or equal to MinDate.", new object[1] { value.ToString("d", CultureInfo.CurrentCulture) });
				throw new ArgumentOutOfRangeException("MaxDate", message);
			}
			if (!(max_date == value))
			{
				max_date = value;
				if (max_date < selection_range.Start || max_date < selection_range.End)
				{
					DateTime lower = ((!(max_date < selection_range.Start)) ? selection_range.Start : max_date);
					DateTime upper = ((!(max_date < selection_range.End)) ? selection_range.End : max_date);
					SelectionRange = new SelectionRange(lower, upper);
				}
			}
		}
	}

	/// <summary>Gets or sets the maximum number of days that can be selected in a month calendar control.</summary>
	/// <returns>The maximum number of days that you can select. The default is 7.</returns>
	/// <exception cref="T:System.ArgumentException">The value is less than 1.-or- The <see cref="P:System.Windows.Forms.MonthCalendar.MaxSelectionCount" /> cannot be set. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(7)]
	public int MaxSelectionCount
	{
		get
		{
			return max_selection_count;
		}
		set
		{
			if (value < 1)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MaxSelectionCount'. 'MaxSelectionCount' must be greater than or equal to {1}.", new object[2] { value, 1 });
				throw new ArgumentOutOfRangeException("MaxSelectionCount", message);
			}
			if ((SelectionEnd - SelectionStart).Days > value)
			{
				throw new ArgumentException();
			}
			if (max_selection_count != value)
			{
				max_selection_count = value;
				OnUIAMaxSelectionCountChanged();
			}
		}
	}

	/// <summary>Gets or sets the minimum allowable date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> representing the minimum allowable date. The default is 01/01/1753.</returns>
	/// <exception cref="T:System.ArgumentException">The date set is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />.-or-The date set is earlier than 01/01/1753. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DateTime MinDate
	{
		get
		{
			return min_date;
		}
		set
		{
			DateTime dateTime = new DateTime(1753, 1, 1);
			if (value < dateTime)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MinDate'. 'MinDate' must be greater than or equal to {1}.", new object[2]
				{
					value.ToString("d", CultureInfo.CurrentCulture),
					dateTime.ToString("d", CultureInfo.CurrentCulture)
				});
				throw new ArgumentOutOfRangeException("MinDate", message);
			}
			if (value > MaxDate)
			{
				string message2 = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MinDate'. 'MinDate' must be less than MaxDate.", new object[1] { value.ToString("d", CultureInfo.CurrentCulture) });
				throw new ArgumentOutOfRangeException("MinDate", message2);
			}
			if (!(min_date == value))
			{
				min_date = value;
				if (min_date > selection_range.Start || min_date > selection_range.End)
				{
					DateTime lower = ((!(min_date > selection_range.Start)) ? selection_range.Start : min_date);
					DateTime upper = ((!(min_date > selection_range.End)) ? selection_range.End : min_date);
					SelectionRange = new SelectionRange(lower, upper);
				}
			}
		}
	}

	/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determine which monthly days to bold.</summary>
	/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public DateTime[] MonthlyBoldedDates
	{
		get
		{
			if (monthly_bolded_dates == null || monthly_bolded_dates.Count == 0)
			{
				return new DateTime[0];
			}
			DateTime[] array = new DateTime[monthly_bolded_dates.Count];
			monthly_bolded_dates.CopyTo(array);
			return array;
		}
		set
		{
			if (monthly_bolded_dates == null)
			{
				monthly_bolded_dates = new ArrayList(value);
			}
			else
			{
				monthly_bolded_dates.Clear();
				monthly_bolded_dates.AddRange(value);
			}
			UpdateBoldedDates();
		}
	}

	/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control and its contents.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
	/// <returns>true if the control is laid out from right to left; otherwise, false. The default is false.</returns>
	[Localizable(true)]
	[DefaultValue(false)]
	public virtual bool RightToLeftLayout
	{
		get
		{
			return right_to_left_layout;
		}
		set
		{
			right_to_left_layout = value;
		}
	}

	/// <summary>Gets or sets the scroll rate for a month calendar control.</summary>
	/// <returns>A positive number representing the current scroll rate in number of months moved. The default is the number of months currently displayed. The maximum is 20,000.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.-or- The value is greater than 20,000. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	public int ScrollChange
	{
		get
		{
			return scroll_change;
		}
		set
		{
			if (value < 0 || value > 20000)
			{
				throw new ArgumentException();
			}
			if (scroll_change != value)
			{
				scroll_change = value;
			}
		}
	}

	/// <summary>Gets or sets the end date of the selected range of dates.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> indicating the last date in the selection range.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> value.-or- The date value is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> value. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DateTime SelectionEnd
	{
		get
		{
			return SelectionRange.End;
		}
		set
		{
			if (value < MinDate || value > MaxDate)
			{
				throw new ArgumentException();
			}
			if (SelectionRange.End != value)
			{
				DateTime end = SelectionRange.End;
				if (value < SelectionRange.Start)
				{
					SelectionRange.Start = value;
				}
				if (value.AddDays((MaxSelectionCount - 1) * -1) > SelectionRange.Start)
				{
					SelectionRange.Start = value.AddDays((MaxSelectionCount - 1) * -1);
				}
				SelectionRange.End = value;
				InvalidateDateRange(new SelectionRange(end, SelectionRange.End));
				OnDateChanged(new DateRangeEventArgs(SelectionStart, SelectionEnd));
				OnUIASelectionChanged();
			}
		}
	}

	/// <summary>Gets or sets the selected range of dates for a month calendar control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.SelectionRange" /> with the start and end dates of the selected range.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	public SelectionRange SelectionRange
	{
		get
		{
			return selection_range;
		}
		set
		{
			if (selection_range != value)
			{
				if (value.Start < MinDate)
				{
					throw new ArgumentException("SelectionStart cannot be less than MinDate");
				}
				if (value.End > MaxDate)
				{
					throw new ArgumentException("SelectionEnd cannot be greated than MaxDate");
				}
				SelectionRange selectionRange = selection_range;
				if (value.End.AddDays((MaxSelectionCount - 1) * -1) > value.Start)
				{
					selection_range = new SelectionRange(value.End.AddDays((MaxSelectionCount - 1) * -1), value.End);
				}
				else
				{
					selection_range = value;
				}
				SelectionRange displayRange = GetDisplayRange(visible: true);
				if (displayRange.Start > selection_range.End)
				{
					current_month = new DateTime(selection_range.Start.Year, selection_range.Start.Month, 1);
					Invalidate();
				}
				else if (displayRange.End < selection_range.Start)
				{
					int num = selection_range.End.Year - displayRange.End.Year;
					int num2 = selection_range.End.Month - displayRange.End.Month;
					current_month = current_month.AddMonths(num * 12 + num2);
					Invalidate();
				}
				DateTime lower = selectionRange.Start;
				DateTime upper = selectionRange.End;
				if (selectionRange.Start > SelectionRange.Start)
				{
					lower = SelectionRange.Start;
				}
				else if (selectionRange.Start == SelectionRange.Start)
				{
					lower = ((!(selectionRange.End < SelectionRange.End)) ? SelectionRange.End : selectionRange.End);
				}
				if (selectionRange.End < SelectionRange.End)
				{
					upper = SelectionRange.End;
				}
				else if (selectionRange.End == SelectionRange.End)
				{
					upper = ((!(selectionRange.Start < SelectionRange.Start)) ? selectionRange.Start : SelectionRange.Start);
				}
				SelectionRange selectionRange2 = new SelectionRange(lower, upper);
				if (selectionRange2.End != selectionRange.End || selectionRange2.Start != selectionRange.Start)
				{
					InvalidateDateRange(selectionRange2);
				}
				OnDateChanged(new DateRangeEventArgs(SelectionStart, SelectionEnd));
				OnUIASelectionChanged();
			}
		}
	}

	/// <summary>Gets or sets the start date of the selected range of dates.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> indicating the first date in the selection range.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />.-or- The date value is greater than <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DateTime SelectionStart
	{
		get
		{
			return selection_range.Start;
		}
		set
		{
			if (value < MinDate || value > MaxDate)
			{
				throw new ArgumentException();
			}
			if (SelectionRange.Start != value)
			{
				if (value > SelectionRange.End)
				{
					SelectionRange.End = value;
				}
				else if (value.AddDays(MaxSelectionCount - 1) < SelectionRange.End)
				{
					SelectionRange.End = value.AddDays(MaxSelectionCount - 1);
				}
				SelectionRange.Start = value;
				DateTime dateTime = new DateTime(value.Year, value.Month, 1);
				if (current_month != dateTime)
				{
					current_month = dateTime;
				}
				Invalidate();
				OnDateChanged(new DateRangeEventArgs(SelectionStart, SelectionEnd));
				OnUIASelectionChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the date represented by the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property is displayed at the bottom of the control.</summary>
	/// <returns>true if today's date is displayed; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool ShowToday
	{
		get
		{
			return show_today;
		}
		set
		{
			if (show_today != value)
			{
				show_today = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether today's date is identified with a circle or a square.</summary>
	/// <returns>true if today's date is identified with a circle or a square; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool ShowTodayCircle
	{
		get
		{
			return show_today_circle;
		}
		set
		{
			if (show_today_circle != value)
			{
				show_today_circle = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.</summary>
	/// <returns>true if the week numbers are displayed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool ShowWeekNumbers
	{
		get
		{
			return show_week_numbers;
		}
		set
		{
			if (show_week_numbers != value)
			{
				show_week_numbers = value;
				SetBoundsCore(base.Left, base.Top, base.Width, base.Height, BoundsSpecified.Width);
				Invalidate();
			}
		}
	}

	/// <summary>Gets the minimum size to display one month of the calendar.</summary>
	/// <returns>The size, in pixels, necessary to fully display one month in the calendar.</returns>
	/// <exception cref="T:System.InvalidOperationException">The dimensions cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Size SingleMonthSize
	{
		get
		{
			if (Font == null)
			{
				throw new InvalidOperationException();
			}
			int height = Font.Height;
			int num = ((!ShowWeekNumbers) ? 7 : 8);
			int num2 = 7;
			date_cell_size = new Size((int)Math.Ceiling(1.8 * (double)height), height);
			title_size = new Size(date_cell_size.Width * num, 2 * height);
			return new Size(num * date_cell_size.Width, num2 * date_cell_size.Height + title_size.Height);
		}
	}

	/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Size Size
	{
		get
		{
			return base.Size;
		}
		set
		{
			base.Size = value;
		}
	}

	/// <summary>Gets or sets the text to display on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Bindable(false)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>Gets or sets a value indicating the background color of the title area of the calendar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active captions.</returns>
	/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color TitleBackColor
	{
		get
		{
			return title_back_color;
		}
		set
		{
			if (title_back_color != value)
			{
				title_back_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating the foreground color of the title area of the calendar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active caption text.</returns>
	/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color TitleForeColor
	{
		get
		{
			return title_fore_color;
		}
		set
		{
			if (title_fore_color != value)
			{
				title_fore_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the value that is used by <see cref="T:System.Windows.Forms.MonthCalendar" /> as today's date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> representing today's date. The default value is the current system date.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.-or- The value is greater than the maximum allowable date.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DateTime TodayDate
	{
		get
		{
			return today_date;
		}
		set
		{
			today_date_set = true;
			if (today_date != value)
			{
				today_date = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set.</summary>
	/// <returns>true if the value for the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool TodayDateSet => today_date_set;

	/// <summary>Gets or sets a value indicating the color of days in months that are not fully displayed in the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />. The default is <see cref="P:System.Drawing.Color.Gray" />.</returns>
	/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color TrailingForeColor
	{
		get
		{
			return trailing_fore_color;
		}
		set
		{
			if (trailing_fore_color != value)
			{
				trailing_fore_color = value;
				SelectionRange displayRange = GetDisplayRange(visible: false);
				SelectionRange displayRange2 = GetDisplayRange(visible: true);
				InvalidateDateRange(new SelectionRange(displayRange.Start, displayRange2.Start));
				InvalidateDateRange(new SelectionRange(displayRange.End, displayRange2.End));
			}
		}
	}

	/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> with the information for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
	protected override CreateParams CreateParams
	{
		get
		{
			if (owner == null)
			{
				return base.CreateParams;
			}
			CreateParams createParams = base.CreateParams;
			createParams.Style ^= 1073741824;
			createParams.Style |= int.MinValue;
			createParams.ExStyle |= 136;
			return createParams;
		}
	}

	/// <summary>Gets a value indicating the input method editor for the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
	/// <returns>As implemented for this object, always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
	protected override ImeMode DefaultImeMode => base.DefaultImeMode;

	/// <summary>Gets the default margin settings for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> structure with a padding size of 9 pixels, for all of its edges.</returns>
	protected override Padding DefaultMargin => new Padding(9);

	/// <summary>Gets the default size of the calendar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> specifying the height and width, in pixels, of the control.</returns>
	protected override Size DefaultSize
	{
		get
		{
			Size singleMonthSize = SingleMonthSize;
			int num = calendar_dimensions.Width * singleMonthSize.Width;
			if (calendar_dimensions.Width > 1)
			{
				num += (calendar_dimensions.Width - 1) * calendar_spacing.Width;
			}
			int num2 = calendar_dimensions.Height * singleMonthSize.Height;
			if (ShowToday)
			{
				num2 += date_cell_size.Height + 2;
			}
			if (calendar_dimensions.Height > 1)
			{
				num2 += (calendar_dimensions.Height - 1) * calendar_spacing.Height;
			}
			if (num > 0)
			{
				num += 2;
			}
			if (num2 > 0)
			{
				num2 += 2;
			}
			return new Size(num, num2);
		}
	}

	internal bool IsYearGoingUp
	{
		get
		{
			return is_year_going_up;
		}
		set
		{
			if (value)
			{
				is_year_going_down = false;
				year_moving_count = ((!is_year_going_up) ? 1 : (year_moving_count + 1));
				if (is_year_going_up)
				{
					year_moving_count++;
				}
				else
				{
					year_moving_count = 1;
				}
				AddYears(1, year_moving_count > 10);
				if (is_mouse_moving_year)
				{
					StartHideTimer();
				}
			}
			else
			{
				year_moving_count = 0;
			}
			is_year_going_up = value;
			Invalidate();
		}
	}

	internal bool IsYearGoingDown
	{
		get
		{
			return is_year_going_down;
		}
		set
		{
			if (value)
			{
				is_year_going_up = false;
				year_moving_count = ((!is_year_going_down) ? 1 : (year_moving_count + 1));
				if (is_year_going_down)
				{
					year_moving_count++;
				}
				else
				{
					year_moving_count = 1;
				}
				AddYears(-1, year_moving_count > 10);
				if (is_mouse_moving_year)
				{
					StartHideTimer();
				}
			}
			else
			{
				year_moving_count = 0;
			}
			is_year_going_down = value;
			Invalidate();
		}
	}

	internal bool ShowYearUpDown
	{
		get
		{
			return show_year_updown;
		}
		set
		{
			if (show_year_updown != value)
			{
				show_year_updown = value;
				Invalidate();
			}
		}
	}

	internal DateTime CurrentMonth
	{
		get
		{
			return current_month;
		}
		set
		{
			if (!(value < MinDate) && !(value > MaxDate) && (value.Month != current_month.Month || value.Year != current_month.Year))
			{
				SelectionRange = new SelectionRange(SelectionStart.Add(value.Subtract(current_month)), SelectionEnd.Add(value.Subtract(current_month)));
				current_month = value;
				UpdateBoldedDates();
				Invalidate();
			}
		}
	}

	internal override bool InternalCapture
	{
		get
		{
			return base.InternalCapture;
		}
		set
		{
			if (owner == null)
			{
				base.InternalCapture = value;
			}
		}
	}

	/// <summary>Occurs when the date selected in the <see cref="T:System.Windows.Forms.MonthCalendar" /> changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event DateRangeEventHandler DateChanged
	{
		add
		{
			base.Events.AddHandler(DateChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DateChangedEvent, value);
		}
	}

	/// <summary>Occurs when the user makes an explicit date selection using the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	public event DateRangeEventHandler DateSelected
	{
		add
		{
			base.Events.AddHandler(DateSelectedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DateSelectedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged += value;
		}
	}

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler Click
	{
		add
		{
			base.Click += value;
		}
		remove
		{
			base.Click -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DoubleClick
	{
		add
		{
			base.DoubleClick += value;
		}
		remove
		{
			base.DoubleClick -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.ImeMode" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ImeModeChanged
	{
		add
		{
			base.ImeModeChanged += value;
		}
		remove
		{
			base.ImeModeChanged -= value;
		}
	}

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseClick
	{
		add
		{
			base.MouseClick += value;
		}
		remove
		{
			base.MouseClick -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.MouseDoubleClick += value;
		}
		remove
		{
			base.MouseDoubleClick -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Padding" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the control is redrawn.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event PaintEventHandler Paint;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.RightToLeftLayout" /> property changes.</summary>
	public event EventHandler RightToLeftLayoutChanged
	{
		add
		{
			base.Events.AddHandler(RightToLeftLayoutChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RightToLeftLayoutChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	internal event EventHandler UIAMaxSelectionCountChanged
	{
		add
		{
			base.Events.AddHandler(UIAMaxSelectionCountChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAMaxSelectionCountChangedEvent, value);
		}
	}

	internal event EventHandler UIASelectionChanged
	{
		add
		{
			base.Events.AddHandler(UIASelectionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIASelectionChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MonthCalendar" /> class.</summary>
	public MonthCalendar()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, value: false);
		timer = new Timer();
		timer.Interval = 500;
		timer.Enabled = false;
		DateTime date = DateTime.Now.Date;
		selection_range = new SelectionRange(date, date);
		today_date = date;
		current_month = new DateTime(date.Year, date.Month, 1);
		annually_bolded_dates = null;
		bolded_dates = null;
		calendar_dimensions = new Size(1, 1);
		first_day_of_week = Day.Default;
		max_date = new DateTime(9998, 12, 31);
		max_selection_count = 7;
		min_date = new DateTime(1753, 1, 1);
		monthly_bolded_dates = null;
		scroll_change = 0;
		show_today = true;
		show_today_circle = true;
		show_week_numbers = false;
		title_back_color = ThemeEngine.Current.ColorActiveCaption;
		title_fore_color = ThemeEngine.Current.ColorActiveCaptionText;
		today_date_set = false;
		trailing_fore_color = SystemColors.GrayText;
		bold_font = new Font(Font, Font.Style | FontStyle.Bold);
		centered_format = new StringFormat(StringFormat.GenericTypographic);
		centered_format.FormatFlags = centered_format.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
		centered_format.FormatFlags &= ~StringFormatFlags.NoClip;
		centered_format.LineAlignment = StringAlignment.Center;
		centered_format.Alignment = StringAlignment.Center;
		ForeColor = SystemColors.WindowText;
		BackColor = ThemeEngine.Current.ColorWindow;
		button_x_offset = 5;
		button_size = new Size(22, 17);
		date_cell_size = new Size(24, 16);
		divider_line_offset = 4;
		calendar_spacing = new Size(4, 5);
		clicked_date = date;
		is_date_clicked = false;
		is_previous_clicked = false;
		is_next_clicked = false;
		is_shift_pressed = false;
		click_state = new bool[3];
		first_select_start_date = date;
		month_title_click_location = Point.Empty;
		SetUpTodayMenu();
		SetUpMonthMenu();
		timer.Tick += TimerHandler;
		base.MouseMove += MouseMoveHandler;
		base.MouseDown += MouseDownHandler;
		base.KeyDown += KeyDownHandler;
		base.MouseUp += MouseUpHandler;
		base.KeyUp += KeyUpHandler;
		base.Paint += PaintHandler;
		Size = DefaultSize;
	}

	internal MonthCalendar(DateTimePicker owner)
		: this()
	{
		this.owner = owner;
		is_visible = false;
		Size = DefaultSize;
	}

	static MonthCalendar()
	{
		DateChanged = new object();
		DateSelected = new object();
		RightToLeftLayoutChanged = new object();
		UIAMaxSelectionCountChanged = new object();
		UIASelectionChanged = new object();
	}

	/// <summary>Adds a day that is displayed in bold on an annual basis in the month calendar.</summary>
	/// <param name="date">The date to be displayed in bold. </param>
	/// <filterpriority>1</filterpriority>
	public void AddAnnuallyBoldedDate(DateTime date)
	{
		if (annually_bolded_dates == null)
		{
			annually_bolded_dates = new ArrayList();
		}
		if (!annually_bolded_dates.Contains(date))
		{
			annually_bolded_dates.Add(date);
		}
	}

	/// <summary>Adds a day to be displayed in bold in the month calendar.</summary>
	/// <param name="date">The date to be displayed in bold. </param>
	/// <filterpriority>1</filterpriority>
	public void AddBoldedDate(DateTime date)
	{
		if (bolded_dates == null)
		{
			bolded_dates = new ArrayList();
		}
		if (!bolded_dates.Contains(date))
		{
			bolded_dates.Add(date);
		}
	}

	/// <summary>Adds a day that is displayed in bold on a monthly basis in the month calendar.</summary>
	/// <param name="date">The date to be displayed in bold. </param>
	/// <filterpriority>1</filterpriority>
	public void AddMonthlyBoldedDate(DateTime date)
	{
		if (monthly_bolded_dates == null)
		{
			monthly_bolded_dates = new ArrayList();
		}
		if (!monthly_bolded_dates.Contains(date))
		{
			monthly_bolded_dates.Add(date);
		}
	}

	/// <summary>Retrieves date information that represents the low and high limits of the displayed dates of the control.</summary>
	/// <returns>The begin and end dates of the displayed calendar.</returns>
	/// <param name="visible">true to retrieve only the dates that are fully contained in displayed months; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public SelectionRange GetDisplayRange(bool visible)
	{
		DateTime dateTime = new DateTime(current_month.Year, current_month.Month, 1);
		DateTime dateTime2 = dateTime.AddMonths(calendar_dimensions.Width * calendar_dimensions.Height).AddDays(-1.0);
		if (!visible)
		{
			dateTime = GetFirstDateInMonthGrid(dateTime);
			dateTime2 = GetLastDateInMonthGrid(dateTime2);
		}
		return new SelectionRange(dateTime, dateTime2);
	}

	/// <summary>Returns a <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> with information on which portion of a month calendar control is at a specified x- and y-coordinate.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
	/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the point to be hit tested. </param>
	/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the point to be hit tested. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public HitTestInfo HitTest(int x, int y)
	{
		return HitTest(new Point(x, y));
	}

	/// <summary>Returns an object with information on which portion of a month calendar control is at a location specified by a <see cref="T:System.Drawing.Point" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
	/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> coordinates of the point to be hit tested. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public HitTestInfo HitTest(Point point)
	{
		return HitTest(point, out last_clicked_calendar_index, out last_clicked_calendar_rect);
	}

	/// <summary>Removes all the annually bold dates.</summary>
	/// <filterpriority>2</filterpriority>
	public void RemoveAllAnnuallyBoldedDates()
	{
		if (annually_bolded_dates != null)
		{
			annually_bolded_dates.Clear();
		}
	}

	/// <summary>Removes all the nonrecurring bold dates.</summary>
	/// <filterpriority>2</filterpriority>
	public void RemoveAllBoldedDates()
	{
		if (bolded_dates != null)
		{
			bolded_dates.Clear();
		}
	}

	/// <summary>Removes all the monthly bold dates.</summary>
	/// <filterpriority>2</filterpriority>
	public void RemoveAllMonthlyBoldedDates()
	{
		if (monthly_bolded_dates != null)
		{
			monthly_bolded_dates.Clear();
		}
	}

	/// <summary>Removes the specified date from the list of annually bold dates.</summary>
	/// <param name="date">The date to remove from the date list. </param>
	/// <filterpriority>2</filterpriority>
	public void RemoveAnnuallyBoldedDate(DateTime date)
	{
		if (annually_bolded_dates == null)
		{
			return;
		}
		for (int i = 0; i < annually_bolded_dates.Count; i++)
		{
			DateTime dateTime = (DateTime)annually_bolded_dates[i];
			if (dateTime.Day == date.Day && dateTime.Month == date.Month)
			{
				annually_bolded_dates.RemoveAt(i);
				break;
			}
		}
	}

	/// <summary>Removes the specified date from the list of nonrecurring bold dates.</summary>
	/// <param name="date">The date to remove from the date list. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void RemoveBoldedDate(DateTime date)
	{
		if (bolded_dates == null)
		{
			return;
		}
		for (int i = 0; i < bolded_dates.Count; i++)
		{
			DateTime dateTime = (DateTime)bolded_dates[i];
			if (dateTime.Year == date.Year && dateTime.Month == date.Month && dateTime.Day == date.Day)
			{
				bolded_dates.RemoveAt(i);
				break;
			}
		}
	}

	/// <summary>Removes the specified date from the list of monthly bolded dates.</summary>
	/// <param name="date">The date to remove from the date list. </param>
	/// <filterpriority>2</filterpriority>
	public void RemoveMonthlyBoldedDate(DateTime date)
	{
		if (monthly_bolded_dates == null)
		{
			return;
		}
		for (int i = 0; i < monthly_bolded_dates.Count; i++)
		{
			DateTime dateTime = (DateTime)monthly_bolded_dates[i];
			if (dateTime.Day == date.Day && dateTime.Month == date.Month)
			{
				monthly_bolded_dates.RemoveAt(i);
				break;
			}
		}
	}

	/// <summary>Sets the number of columns and rows of months to display.</summary>
	/// <param name="x">The number of columns. </param>
	/// <param name="y">The number of rows. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="x" /> or <paramref name="y" /> is less than 1. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetCalendarDimensions(int x, int y)
	{
		CalendarDimensions = new Size(x, y);
	}

	/// <summary>Sets a date as the currently selected date.</summary>
	/// <param name="date">The date to be selected. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.-or- The value is greater than the maximum allowable date. This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetDate(DateTime date)
	{
		SetSelectionRange(date.Date, date.Date);
	}

	/// <summary>Sets the selected dates in a month calendar control to the specified date range.</summary>
	/// <param name="date1">The beginning date of the selection range. </param>
	/// <param name="date2">The end date of the selection range. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="date1" /> is less than the minimum date allowable for a month calendar control.-or- <paramref name="date1" /> is greater than the maximum allowable date for a month calendar control.-or- <paramref name="date2" /> is less than the minimum date allowable for a month calendar control.-or- <paramref name="date2" /> is greater than the maximum allowable date for a month calendar control. This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetSelectionRange(DateTime date1, DateTime date2)
	{
		SelectionRange = new SelectionRange(date1, date2);
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MonthCalendar" />. </returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return GetType().Name + ", " + SelectionRange.ToString();
	}

	/// <summary>Repaints the bold dates to reflect the dates set in the lists of bold dates.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void UpdateBoldedDates()
	{
		Invalidate();
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.MonthCalendar" />. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
	protected override bool IsInputKey(Keys keyData)
	{
		switch (keyData)
		{
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
			return true;
		default:
			return base.IsInputKey(keyData);
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
		Invalidate();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> event.</summary>
	/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data. </param>
	protected virtual void OnDateChanged(DateRangeEventArgs drevent)
	{
		((DateRangeEventHandler)base.Events[DateChanged])?.Invoke(this, drevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> event.</summary>
	/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data. </param>
	protected virtual void OnDateSelected(DateRangeEventArgs drevent)
	{
		((DateRangeEventHandler)base.Events[DateSelected])?.Invoke(this, drevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		Size = new Size(CalendarDimensions.Width * SingleMonthSize.Width, CalendarDimensions.Height * SingleMonthSize.Height);
		bold_font = new Font(Font, Font.Style | FontStyle.Bold);
		base.OnFontChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnForeColorChanged(EventArgs e)
	{
		base.OnForeColorChanged(e);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.RightToLeftLayoutChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftLayoutChanged])?.Invoke(this, e);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" /> method.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Right" /> property value of the control.</param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		Size defaultSize = DefaultSize;
		Size size = defaultSize;
		Size size2 = new Size(defaultSize.Width + SingleMonthSize.Width + calendar_spacing.Width, defaultSize.Height + SingleMonthSize.Height + calendar_spacing.Height);
		int num = (size2.Width + size.Width) / 2;
		int num2 = (size2.Height + size.Height) / 2;
		width = ((width >= num) ? size2.Width : size.Width);
		height = ((height >= num2) ? size2.Height : size.Height);
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	private void AddYears(int years, bool fast)
	{
		if (fast && CurrentMonth.Year + years * 5 <= MaxDate.Year)
		{
			DateTime dateTime = CurrentMonth.AddYears(years * 5);
			if (MaxDate >= dateTime && MinDate <= dateTime)
			{
				CurrentMonth = dateTime;
				return;
			}
		}
		if (CurrentMonth.Year + years <= MaxDate.Year)
		{
			DateTime dateTime = CurrentMonth.AddYears(years);
			if (MaxDate >= dateTime && MinDate <= dateTime)
			{
				CurrentMonth = dateTime;
			}
		}
	}

	internal HitTestInfo HitTest(Point point, out int calendar_index, out Rectangle calendar_rect)
	{
		calendar_index = -1;
		calendar_rect = Rectangle.Empty;
		Rectangle rectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Bottom - date_cell_size.Height, 7 * date_cell_size.Width, date_cell_size.Height);
		if (rectangle.Contains(point) && ShowToday)
		{
			return new HitTestInfo(HitArea.TodayLink, point, DateTime.Now);
		}
		Size singleMonthSize = SingleMonthSize;
		Rectangle[] array = new Rectangle[CalendarDimensions.Width * CalendarDimensions.Height];
		for (int i = 0; i < CalendarDimensions.Width * CalendarDimensions.Height; i++)
		{
			if (i == 0)
			{
				ref Rectangle reference = ref array[i];
				reference = new Rectangle(new Point(base.ClientRectangle.X + 1, base.ClientRectangle.Y + 1), singleMonthSize);
			}
			else if (i % CalendarDimensions.Width == 0)
			{
				ref Rectangle reference2 = ref array[i];
				reference2 = new Rectangle(new Point(array[i - CalendarDimensions.Width].X, array[i - CalendarDimensions.Width].Bottom + calendar_spacing.Height), singleMonthSize);
			}
			else
			{
				ref Rectangle reference3 = ref array[i];
				reference3 = new Rectangle(new Point(array[i - 1].Right + calendar_spacing.Width, array[i - 1].Y), singleMonthSize);
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (!array[j].Contains(point))
			{
				continue;
			}
			Rectangle title_rect = new Rectangle(array[j].Location, title_size);
			if (title_rect.Contains(point))
			{
				if (j == 0 && new Rectangle(new Point(array[j].X + button_x_offset, (title_size.Height - button_size.Height) / 2), button_size).Contains(point))
				{
					return new HitTestInfo(HitArea.PrevMonthButton, point, new DateTime(1, 1, 1));
				}
				if (j % CalendarDimensions.Height == 0 && j % CalendarDimensions.Width == calendar_dimensions.Width - 1 && new Rectangle(new Point(array[j].Right - button_x_offset - button_size.Width, (title_size.Height - button_size.Height) / 2), button_size).Contains(point))
				{
					return new HitTestInfo(HitArea.NextMonthButton, point, new DateTime(1, 1, 1));
				}
				calendar_index = j;
				calendar_rect = array[j];
				if (GetMonthNameRectangle(title_rect, j).Contains(point))
				{
					return new HitTestInfo(HitArea.TitleMonth, point, new DateTime(1, 1, 1));
				}
				GetYearNameRectangles(title_rect, j, out var year_rect, out var up_rect, out var down_rect);
				if (year_rect.Contains(point))
				{
					return new HitTestInfo(HitArea.TitleYear, point, new DateTime(1, 1, 1), HitAreaExtra.YearRectangle);
				}
				if (up_rect.Contains(point))
				{
					return new HitTestInfo(HitArea.TitleYear, point, new DateTime(1, 1, 1), HitAreaExtra.UpButton);
				}
				if (down_rect.Contains(point))
				{
					return new HitTestInfo(HitArea.TitleYear, point, new DateTime(1, 1, 1), HitAreaExtra.DownButton);
				}
				return new HitTestInfo(HitArea.TitleBackground, point, new DateTime(1, 1, 1));
			}
			Point location = new Point(array[j].X, title_rect.Bottom);
			if (ShowWeekNumbers)
			{
				if (new Rectangle(location, new Size(date_cell_size.Width, Math.Max(array[j].Height - title_rect.Height, 0))).Contains(point))
				{
					return new HitTestInfo(HitArea.WeekNumbers, point, DateTime.Now);
				}
				location.X += date_cell_size.Width;
			}
			Rectangle rectangle2 = new Rectangle(location, new Size(Math.Max(array[j].Right - location.X, 0), date_cell_size.Height));
			if (rectangle2.Contains(point))
			{
				return new HitTestInfo(HitArea.DayOfWeek, point, new DateTime(1, 1, 1));
			}
			Rectangle rectangle3 = new Rectangle(new Point(rectangle2.X, rectangle2.Bottom), new Size(rectangle2.Width, Math.Max(array[j].Bottom - rectangle2.Bottom, 0)));
			if (!rectangle3.Contains(point))
			{
				continue;
			}
			clicked_rect = rectangle3;
			Point point2 = new Point(point.X - rectangle3.X, point.Y - rectangle3.Y);
			int num = point2.Y / date_cell_size.Height;
			int num2 = point2.X / date_cell_size.Width;
			DateTime dateTime = CurrentMonth.AddMonths(j);
			DateTime dateTime2 = GetFirstDateInMonthGrid(dateTime).AddDays(num * 7 + num2);
			if (dateTime2.Year != dateTime.Year || dateTime2.Month != dateTime.Month)
			{
				if (dateTime2 < dateTime && j == 0)
				{
					return new HitTestInfo(HitArea.PrevMonthDate, point, new DateTime(1, 1, 1), dateTime2);
				}
				if (dateTime2 > dateTime && j == CalendarDimensions.Width * CalendarDimensions.Height - 1)
				{
					return new HitTestInfo(HitArea.NextMonthDate, point, new DateTime(1, 1, 1), dateTime2);
				}
				return new HitTestInfo(HitArea.Nowhere, point, new DateTime(1, 1, 1));
			}
			return new HitTestInfo(HitArea.Date, point, dateTime2);
		}
		return new HitTestInfo();
	}

	internal DateTime GetFirstDateInMonthGrid(DateTime month)
	{
		DayOfWeek dayOfWeek = GetDayOfWeek(first_day_of_week);
		DateTime dateTime = new DateTime(month.Year, month.Month, 1);
		DayOfWeek dayOfWeek2 = dateTime.DayOfWeek;
		int num = dayOfWeek2 - dayOfWeek;
		if (num < 0)
		{
			num += 7;
		}
		return dateTime.AddDays(-1 * num);
	}

	internal DateTime GetLastDateInMonthGrid(DateTime month)
	{
		return GetFirstDateInMonthGrid(month).AddDays(41.0);
	}

	internal bool IsBoldedDate(DateTime date)
	{
		if (bolded_dates != null && bolded_dates.Count > 0)
		{
			foreach (DateTime bolded_date in bolded_dates)
			{
				if (bolded_date.Date == date.Date)
				{
					return true;
				}
			}
		}
		if (monthly_bolded_dates != null && monthly_bolded_dates.Count > 0)
		{
			foreach (DateTime monthly_bolded_date in monthly_bolded_dates)
			{
				if (monthly_bolded_date.Day == date.Day)
				{
					return true;
				}
			}
		}
		if (annually_bolded_dates != null && annually_bolded_dates.Count > 0)
		{
			foreach (DateTime annually_bolded_date in annually_bolded_dates)
			{
				if (annually_bolded_date.Month == date.Month && annually_bolded_date.Day == date.Day)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void SetUpTodayMenu()
	{
		today_menu = new ContextMenu();
		MenuItem menuItem = new MenuItem("Go to today");
		menuItem.Click += TodayMenuItemClickHandler;
		today_menu.MenuItems.Add(menuItem);
	}

	private void SetUpMonthMenu()
	{
		month_menu = new ContextMenu();
		for (int i = 0; i < 12; i++)
		{
			MenuItem menuItem = new MenuItem(new DateTime(2000, i + 1, 1).ToString("MMMM"));
			menuItem.Click += MonthMenuItemClickHandler;
			month_menu.MenuItems.Add(menuItem);
		}
	}

	private DateTime GetFirstDateInMonth(DateTime date)
	{
		return new DateTime(date.Year, date.Month, 1);
	}

	private DateTime GetLastDateInMonth(DateTime date)
	{
		return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1.0);
	}

	private void AddTimeToSelection(int delta, bool isDays)
	{
		DateTime dateTime = ((!(SelectionStart != first_select_start_date)) ? SelectionEnd : SelectionStart);
		SelectionRange selectionRange = new SelectionRange(upper: (!isDays) ? dateTime.AddMonths(delta) : dateTime.AddDays(delta), lower: first_select_start_date);
		if (selectionRange.Start.AddDays(MaxSelectionCount - 1) < selectionRange.End)
		{
			if (selectionRange.Start != first_select_start_date)
			{
				selectionRange.Start = selectionRange.End.AddDays((MaxSelectionCount - 1) * -1);
			}
			else
			{
				selectionRange.End = selectionRange.Start.AddDays(MaxSelectionCount - 1);
			}
		}
		if (selectionRange.Start != selection_range.Start || selectionRange.End != selection_range.End)
		{
			SelectionRange = selectionRange;
		}
	}

	private void SelectDate(DateTime date)
	{
		SelectionRange selectionRange = null;
		if (is_shift_pressed || click_state[0])
		{
			selectionRange = new SelectionRange(first_select_start_date, date);
			if (selectionRange.Start.AddDays(MaxSelectionCount - 1) < selectionRange.End)
			{
				if (selectionRange.Start != first_select_start_date)
				{
					selectionRange.Start = selectionRange.End.AddDays((MaxSelectionCount - 1) * -1);
				}
				else
				{
					selectionRange.End = selectionRange.Start.AddDays(MaxSelectionCount - 1);
				}
			}
		}
		else if (date >= MinDate && date <= MaxDate)
		{
			selectionRange = new SelectionRange(date, date);
			first_select_start_date = date;
		}
		if ((selectionRange != null && selectionRange.Start != selection_range.Start) || selectionRange.End != selection_range.End)
		{
			SelectionRange = selectionRange;
		}
	}

	internal int GetWeekOfYear(DateTime date)
	{
		DayOfWeek dayOfWeek = GetDayOfWeek(first_day_of_week);
		DayOfWeek dayOfWeek2 = new DateTime(date.Year, 1, 1).DayOfWeek;
		int num = dayOfWeek2 - dayOfWeek;
		return (date.DayOfYear + num) / 7 + 1;
	}

	internal DayOfWeek GetDayOfWeek(Day day)
	{
		if (day == Day.Default)
		{
			return Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
		}
		return (DayOfWeek)(int)Enum.Parse(typeof(DayOfWeek), day.ToString());
	}

	internal Rectangle GetMonthNameRectangle(Rectangle title_rect, int calendar_index)
	{
		DateTime dateTime = current_month.AddMonths(calendar_index);
		Size size = TextRenderer.MeasureString(dateTime.ToString("MMMM yyyy"), Font).ToSize();
		Size size2 = TextRenderer.MeasureString(dateTime.ToString("MMMM"), Font).ToSize();
		return new Rectangle(new Point(title_rect.X + (title_rect.Width - size.Width) / 2, title_rect.Y + (title_rect.Height - size.Height) / 2), size2);
	}

	internal void GetYearNameRectangles(Rectangle title_rect, int calendar_index, out Rectangle year_rect, out Rectangle up_rect, out Rectangle down_rect)
	{
		DateTime dateTime = current_month.AddMonths(calendar_index);
		SizeF size = TextRenderer.MeasureString(dateTime.ToString("MMMM yyyy"), bold_font, int.MaxValue, centered_format);
		SizeF sizeF = TextRenderer.MeasureString(dateTime.ToString("yyyy"), bold_font, int.MaxValue, centered_format);
		RectangleF rectangleF = new RectangleF(new PointF((float)title_rect.X + ((float)title_rect.Width - size.Width) / 2f, (float)title_rect.Y + ((float)title_rect.Height - size.Height) / 2f), size);
		year_rect = new Rectangle(new Point((int)(rectangleF.Right - sizeF.Width + 1f), (int)rectangleF.Y), new Size((int)(sizeF.Width + 1f), (int)(sizeF.Height + 1f)));
		year_rect.Inflate(0, 1);
		up_rect = default(Rectangle);
		up_rect.Location = new Point(year_rect.X + year_rect.Width + 2, year_rect.Y);
		up_rect.Size = new Size(16, year_rect.Height / 2);
		down_rect = default(Rectangle);
		down_rect.Location = new Point(up_rect.X, up_rect.Y + up_rect.Height + 1);
		down_rect.Size = up_rect.Size;
	}

	internal Rectangle GetYearNameRectangle(Rectangle title_rect, int calendar_index)
	{
		GetYearNameRectangles(title_rect, calendar_index, out var year_rect, out var up_rect, out up_rect);
		return year_rect;
	}

	internal bool IsValidWeekToDraw(DateTime month, DateTime date, int row, int col)
	{
		DateTime dateTime = month.AddMonths(-1);
		if ((month.Year == date.Year && month.Month == date.Month) || (dateTime.Year == date.Year && dateTime.Month == date.Month))
		{
			return true;
		}
		if (row == CalendarDimensions.Height - 1 && col == CalendarDimensions.Width - 1)
		{
			dateTime = month.AddMonths(1);
			return dateTime.Year == date.Year && dateTime.Month == date.Month;
		}
		return false;
	}

	private void SetItemClick(HitTestInfo hti)
	{
		switch (hti.HitArea)
		{
		case HitArea.NextMonthButton:
			is_previous_clicked = false;
			is_next_clicked = true;
			is_date_clicked = false;
			break;
		case HitArea.PrevMonthButton:
			is_previous_clicked = true;
			is_next_clicked = false;
			is_date_clicked = false;
			break;
		case HitArea.Date:
		case HitArea.NextMonthDate:
		case HitArea.PrevMonthDate:
			clicked_date = hti.hit_time;
			is_previous_clicked = false;
			is_next_clicked = false;
			is_date_clicked = true;
			break;
		default:
			is_previous_clicked = false;
			is_next_clicked = false;
			is_date_clicked = false;
			break;
		}
	}

	private void TodayMenuItemClickHandler(object sender, EventArgs e)
	{
		SetSelectionRange(DateTime.Now.Date, DateTime.Now.Date);
		OnDateSelected(new DateRangeEventArgs(SelectionStart, SelectionEnd));
	}

	private void MonthMenuItemClickHandler(object sender, EventArgs e)
	{
		if (!(sender is MenuItem menuItem) || !(month_title_click_location != Point.Empty) || menuItem.Parent == null)
		{
			return;
		}
		int num = menuItem.Parent.MenuItems.IndexOf(menuItem) + 1;
		if (num == 0)
		{
			return;
		}
		Size singleMonthSize = SingleMonthSize;
		for (int i = 0; i < CalendarDimensions.Height; i++)
		{
			for (int j = 0; j < CalendarDimensions.Width; j++)
			{
				int months = i * CalendarDimensions.Width + j;
				Rectangle rectangle = new Rectangle(new Point(0, 0), singleMonthSize);
				if (j == 0)
				{
					rectangle.X = base.ClientRectangle.X + 1;
				}
				else
				{
					rectangle.X = base.ClientRectangle.X + 1 + j * (singleMonthSize.Width + calendar_spacing.Width);
				}
				if (i == 0)
				{
					rectangle.Y = base.ClientRectangle.Y + 1;
				}
				else
				{
					rectangle.Y = base.ClientRectangle.Y + 1 + i * (singleMonthSize.Height + calendar_spacing.Height);
				}
				if (rectangle.Contains(month_title_click_location))
				{
					int months2 = num - CurrentMonth.AddMonths(months).Month;
					CurrentMonth = CurrentMonth.AddMonths(months2);
					break;
				}
			}
		}
		month_title_click_location = Point.Empty;
	}

	private void TimerHandler(object sender, EventArgs e)
	{
		if (base.Capture)
		{
			HitTestInfo hitTestInfo = HitTest(PointToClient(Control.MousePosition));
			if (click_state[1] || click_state[2])
			{
				DoMouseUp();
				if (hitTestInfo.HitArea == HitArea.PrevMonthButton || hitTestInfo.HitArea == HitArea.NextMonthButton)
				{
					DoButtonMouseDown(hitTestInfo);
					click_state[1] = hitTestInfo.HitArea == HitArea.PrevMonthButton;
					click_state[2] = !click_state[1];
				}
				if (timer.Interval != 300)
				{
					timer.Interval = 300;
				}
			}
		}
		else
		{
			timer.Enabled = false;
		}
	}

	private void DoButtonMouseDown(HitTestInfo hti)
	{
		SetItemClick(hti);
		if (hti.HitArea == HitArea.PrevMonthButton)
		{
			Invalidate(new Rectangle(base.ClientRectangle.X + 1 + button_x_offset, base.ClientRectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, button_size.Width, button_size.Height));
			int num = ((scroll_change != 0) ? scroll_change : (CalendarDimensions.Width * CalendarDimensions.Height));
			CurrentMonth = CurrentMonth.AddMonths(-num);
		}
		else
		{
			Invalidate(new Rectangle(base.ClientRectangle.Right - 1 - button_x_offset - button_size.Width, base.ClientRectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, button_size.Width, button_size.Height));
			int months = ((scroll_change != 0) ? scroll_change : (CalendarDimensions.Width * CalendarDimensions.Height));
			CurrentMonth = CurrentMonth.AddMonths(months);
		}
	}

	private void DoDateMouseDown(HitTestInfo hti)
	{
		SetItemClick(hti);
	}

	private void DoMouseUp()
	{
		IsYearGoingDown = false;
		IsYearGoingUp = false;
		is_mouse_moving_year = false;
		if (is_next_clicked)
		{
			Invalidate(new Rectangle(base.ClientRectangle.Right - 1 - button_x_offset - button_size.Width, base.ClientRectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, button_size.Width, button_size.Height));
		}
		if (is_previous_clicked)
		{
			Invalidate(new Rectangle(base.ClientRectangle.X + 1 + button_x_offset, base.ClientRectangle.Y + 1 + (title_size.Height - button_size.Height) / 2, button_size.Width, button_size.Height));
		}
		if (is_date_clicked)
		{
			InvalidateDateRange(new SelectionRange(clicked_date, clicked_date));
		}
		is_previous_clicked = false;
		is_next_clicked = false;
		is_date_clicked = false;
	}

	private void UpDownTimerTick(object sender, EventArgs e)
	{
		if (IsYearGoingUp)
		{
			IsYearGoingUp = true;
		}
		if (IsYearGoingDown)
		{
			IsYearGoingDown = true;
		}
		if (!IsYearGoingDown && !IsYearGoingUp)
		{
			updown_timer.Enabled = false;
		}
		else if (IsYearGoingDown || IsYearGoingUp)
		{
			updown_timer.Interval = 100;
		}
	}

	private void StartHideTimer()
	{
		if (updown_timer == null)
		{
			updown_timer = new Timer();
			updown_timer.Tick += UpDownTimerTick;
		}
		updown_timer.Interval = 500;
		updown_timer.Enabled = true;
	}

	private void MouseMoveHandler(object sender, MouseEventArgs e)
	{
		HitTestInfo hitTestInfo = HitTest(e.X, e.Y);
		if (click_state[0] && (hitTestInfo.HitArea == HitArea.PrevMonthDate || hitTestInfo.HitArea == HitArea.NextMonthDate || hitTestInfo.HitArea == HitArea.Date))
		{
			Rectangle a = clicked_rect;
			DateTime dateTime = clicked_date;
			DoDateMouseDown(hitTestInfo);
			if (owner == null)
			{
				click_state[0] = true;
			}
			else
			{
				click_state[0] = false;
				click_state[1] = false;
				click_state[2] = false;
			}
			if (dateTime != clicked_date)
			{
				SelectDate(clicked_date);
				date_selected_event_pending = true;
				Rectangle rc = Rectangle.Union(a, clicked_rect);
				Invalidate(rc);
			}
		}
	}

	private void MouseDownHandler(object sender, MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left) == 0)
		{
			return;
		}
		click_state[0] = false;
		click_state[1] = false;
		click_state[2] = false;
		if (timer.Enabled)
		{
			timer.Stop();
			timer.Enabled = false;
		}
		Point point = new Point(e.X, e.Y);
		if (owner != null && !base.ClientRectangle.Contains(point))
		{
			owner.HideMonthCalendar();
			return;
		}
		HitTestInfo hitTestInfo = HitTest(point);
		if (ShowYearUpDown && hitTestInfo.HitArea != HitArea.TitleYear)
		{
			ShowYearUpDown = false;
		}
		switch (hitTestInfo.HitArea)
		{
		case HitArea.NextMonthButton:
		case HitArea.PrevMonthButton:
			DoButtonMouseDown(hitTestInfo);
			click_state[1] = hitTestInfo.HitArea == HitArea.PrevMonthDate;
			click_state[2] = !click_state[1];
			timer.Interval = 750;
			timer.Start();
			break;
		case HitArea.Date:
		case HitArea.NextMonthDate:
		case HitArea.PrevMonthDate:
			DoDateMouseDown(hitTestInfo);
			SelectDate(clicked_date);
			date_selected_event_pending = true;
			if (owner == null)
			{
				click_state[0] = true;
				break;
			}
			click_state[0] = false;
			click_state[1] = false;
			click_state[2] = false;
			break;
		case HitArea.TitleMonth:
			month_title_click_location = hitTestInfo.Point;
			month_menu.Show(this, hitTestInfo.Point);
			if (base.Capture && owner != null)
			{
				base.Capture = false;
				base.Capture = true;
			}
			break;
		case HitArea.TitleYear:
			if (ShowYearUpDown)
			{
				if (hitTestInfo.hit_area_extra == HitAreaExtra.UpButton)
				{
					is_mouse_moving_year = true;
					IsYearGoingUp = true;
				}
				else if (hitTestInfo.hit_area_extra == HitAreaExtra.DownButton)
				{
					is_mouse_moving_year = true;
					IsYearGoingDown = true;
				}
			}
			else
			{
				ShowYearUpDown = true;
			}
			break;
		case HitArea.TodayLink:
			SetSelectionRange(DateTime.Now.Date, DateTime.Now.Date);
			OnDateSelected(new DateRangeEventArgs(SelectionStart, SelectionEnd));
			break;
		default:
			is_previous_clicked = false;
			is_next_clicked = false;
			is_date_clicked = false;
			break;
		}
	}

	private void KeyDownHandler(object sender, KeyEventArgs e)
	{
		if (ShowYearUpDown)
		{
			switch (e.KeyCode)
			{
			case Keys.Return:
				ShowYearUpDown = false;
				IsYearGoingDown = false;
				IsYearGoingUp = false;
				break;
			case Keys.Up:
				IsYearGoingUp = true;
				break;
			case Keys.Down:
				IsYearGoingDown = true;
				break;
			}
			return;
		}
		if (!is_shift_pressed && e.Shift)
		{
			first_select_start_date = SelectionStart;
			is_shift_pressed = e.Shift;
			e.Handled = true;
		}
		switch (e.KeyCode)
		{
		case Keys.Home:
			if (is_shift_pressed)
			{
				DateTime dateTime = GetFirstDateInMonth(first_select_start_date);
				if (dateTime < first_select_start_date.AddDays((MaxSelectionCount - 1) * -1))
				{
					dateTime = first_select_start_date.AddDays((MaxSelectionCount - 1) * -1);
				}
				SetSelectionRange(dateTime, first_select_start_date);
			}
			else
			{
				DateTime firstDateInMonth = GetFirstDateInMonth(SelectionStart);
				SetSelectionRange(firstDateInMonth, firstDateInMonth);
			}
			e.Handled = true;
			break;
		case Keys.End:
			if (is_shift_pressed)
			{
				DateTime dateTime4 = GetLastDateInMonth(first_select_start_date);
				if (dateTime4 > first_select_start_date.AddDays(MaxSelectionCount - 1))
				{
					dateTime4 = first_select_start_date.AddDays(MaxSelectionCount - 1);
				}
				SetSelectionRange(dateTime4, first_select_start_date);
			}
			else
			{
				DateTime lastDateInMonth = GetLastDateInMonth(SelectionStart);
				SetSelectionRange(lastDateInMonth, lastDateInMonth);
			}
			e.Handled = true;
			break;
		case Keys.PageUp:
			if (is_shift_pressed)
			{
				AddTimeToSelection(-1, isDays: false);
			}
			else
			{
				DateTime dateTime2 = SelectionStart.AddMonths(-1);
				SetSelectionRange(dateTime2, dateTime2);
			}
			e.Handled = true;
			break;
		case Keys.PageDown:
			if (is_shift_pressed)
			{
				AddTimeToSelection(1, isDays: false);
			}
			else
			{
				DateTime dateTime5 = SelectionStart.AddMonths(1);
				SetSelectionRange(dateTime5, dateTime5);
			}
			e.Handled = true;
			break;
		case Keys.Up:
			if (is_shift_pressed)
			{
				AddTimeToSelection(-7, isDays: true);
			}
			else
			{
				DateTime dateTime8 = SelectionStart.AddDays(-7.0);
				SetSelectionRange(dateTime8, dateTime8);
			}
			e.Handled = true;
			break;
		case Keys.Down:
			if (is_shift_pressed)
			{
				AddTimeToSelection(7, isDays: true);
			}
			else
			{
				DateTime dateTime6 = SelectionStart.AddDays(7.0);
				SetSelectionRange(dateTime6, dateTime6);
			}
			e.Handled = true;
			break;
		case Keys.Left:
			if (is_shift_pressed)
			{
				AddTimeToSelection(-1, isDays: true);
			}
			else
			{
				DateTime dateTime7 = SelectionStart.AddDays(-1.0);
				SetSelectionRange(dateTime7, dateTime7);
			}
			e.Handled = true;
			break;
		case Keys.Right:
			if (is_shift_pressed)
			{
				AddTimeToSelection(1, isDays: true);
			}
			else
			{
				DateTime dateTime3 = SelectionStart.AddDays(1.0);
				SetSelectionRange(dateTime3, dateTime3);
			}
			e.Handled = true;
			break;
		case Keys.F4:
			if (e.Alt && owner != null)
			{
				Hide();
				e.Handled = true;
			}
			break;
		}
	}

	private void MouseUpHandler(object sender, MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left) == 0)
		{
			if (show_today && ContextMenu == null)
			{
				today_menu.Show(this, new Point(e.X, e.Y));
			}
			return;
		}
		if (timer.Enabled)
		{
			timer.Stop();
		}
		click_state[0] = false;
		click_state[1] = false;
		click_state[2] = false;
		DoMouseUp();
		if (date_selected_event_pending)
		{
			OnDateSelected(new DateRangeEventArgs(SelectionStart, SelectionEnd));
			date_selected_event_pending = false;
		}
	}

	private void KeyUpHandler(object sender, KeyEventArgs e)
	{
		is_shift_pressed = e.Shift;
		e.Handled = true;
		IsYearGoingUp = false;
		IsYearGoingDown = false;
	}

	private void PaintHandler(object sender, PaintEventArgs pe)
	{
		if (base.Width > 0 && base.Height > 0 && base.Visible)
		{
			Draw(pe.ClipRectangle, pe.Graphics);
			if (this.Paint != null)
			{
				this.Paint(sender, pe);
			}
		}
	}

	private void InvalidateDateRange(SelectionRange range)
	{
		SelectionRange displayRange = GetDisplayRange(visible: false);
		if (range.End < displayRange.Start || range.Start > displayRange.End)
		{
			return;
		}
		if (range.Start < displayRange.Start)
		{
			range = new SelectionRange(displayRange.Start, range.End);
		}
		if (range.End > displayRange.End)
		{
			range = new SelectionRange(range.Start, displayRange.End);
		}
		DateTime dateTime = current_month.AddMonths(CalendarDimensions.Width * CalendarDimensions.Height).AddDays(-1.0);
		DateTime dateTime2 = range.Start;
		while (dateTime2 <= range.End)
		{
			DateTime dateTime3 = new DateTime(dateTime2.Year, dateTime2.Month, 1).AddMonths(1).AddDays(-1.0);
			Rectangle rectangle;
			Rectangle dateRowRect;
			if (range.End <= dateTime3 && dateTime2 < dateTime)
			{
				rectangle = ((!(dateTime2 < current_month)) ? GetDateRowRect(dateTime2, dateTime2) : GetDateRowRect(current_month, current_month));
				dateRowRect = GetDateRowRect(dateTime2, range.End);
			}
			else if (dateTime2 < dateTime)
			{
				rectangle = GetDateRowRect(dateTime2, dateTime2);
				dateRowRect = GetDateRowRect(dateTime3, dateTime3);
			}
			else
			{
				rectangle = GetDateRowRect(dateTime, dateTime.AddDays(1.0));
				dateRowRect = GetDateRowRect(dateTime, range.End);
			}
			dateTime2 = dateTime3.AddDays(1.0);
			Invalidate(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, Math.Max(dateRowRect.Bottom - rectangle.Y, 0)));
		}
	}

	private Rectangle GetDateRowRect(DateTime month, DateTime date)
	{
		Size singleMonthSize = SingleMonthSize;
		Rectangle rectangle = Rectangle.Empty;
		for (int i = 0; i < CalendarDimensions.Width * CalendarDimensions.Height; i++)
		{
			DateTime dateTime = current_month.AddMonths(i);
			if (month.Year == dateTime.Year && month.Month == dateTime.Month)
			{
				rectangle = new Rectangle(base.ClientRectangle.X + 1 + singleMonthSize.Width * (i % CalendarDimensions.Width) + calendar_spacing.Width * (i % CalendarDimensions.Width), base.ClientRectangle.Y + 1 + singleMonthSize.Height * (i / CalendarDimensions.Width) + calendar_spacing.Height * (i / CalendarDimensions.Width), singleMonthSize.Width, singleMonthSize.Height);
				break;
			}
		}
		if (rectangle == Rectangle.Empty)
		{
			return Rectangle.Empty;
		}
		int num = -1;
		DateTime dateTime2 = GetFirstDateInMonthGrid(month);
		DateTime dateTime3 = dateTime2.AddDays(7.0);
		for (int j = 0; j < 6; j++)
		{
			if (date >= dateTime2 && date < dateTime3)
			{
				num = j;
				break;
			}
			dateTime2 = dateTime3;
			dateTime3 = dateTime3.AddDays(7.0);
		}
		if (num < 0)
		{
			return Rectangle.Empty;
		}
		int num2 = (ShowWeekNumbers ? date_cell_size.Width : 0);
		int num3 = title_size.Height + date_cell_size.Height * (num + 1);
		return new Rectangle(rectangle.X + num2, rectangle.Y + num3, date_cell_size.Width * 7, date_cell_size.Height);
	}

	internal void Draw(Rectangle clip_rect, Graphics dc)
	{
		ThemeEngine.Current.DrawMonthCalendar(dc, clip_rect, this);
	}

	private void OnUIAMaxSelectionCountChanged()
	{
		((EventHandler)base.Events[UIAMaxSelectionCountChanged])?.Invoke(this, EventArgs.Empty);
	}

	private void OnUIASelectionChanged()
	{
		((EventHandler)base.Events[UIASelectionChanged])?.Invoke(this, EventArgs.Empty);
	}
}
