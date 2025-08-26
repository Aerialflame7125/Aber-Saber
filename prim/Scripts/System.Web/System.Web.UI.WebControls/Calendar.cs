using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Displays a single-month calendar that enables the user to select dates and move to the next or previous month.</summary>
[DataBindingHandler("System.Web.UI.Design.WebControls.CalendarDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("SelectionChanged")]
[DefaultProperty("SelectedDate")]
[Designer("System.Web.UI.Design.WebControls.CalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlValueProperty("SelectedDate", "1/1/0001 12:00:00 AM")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Calendar : WebControl, IPostBackEventHandler
{
	private TableItemStyle dayHeaderStyle;

	private TableItemStyle dayStyle;

	private TableItemStyle nextPrevStyle;

	private TableItemStyle otherMonthDayStyle;

	private TableItemStyle selectedDayStyle;

	private TableItemStyle titleStyle;

	private TableItemStyle todayDayStyle;

	private TableItemStyle selectorStyle;

	private TableItemStyle weekendDayStyle;

	private DateTimeFormatInfo dateInfo;

	private SelectedDatesCollection selectedDatesCollection;

	private ArrayList dateList;

	private DateTime today = DateTime.Today;

	private static DateTime dateZenith = new DateTime(2000, 1, 1);

	private const int daysInAWeek = 7;

	private static readonly object DayRenderEvent;

	private static readonly object SelectionChangedEvent;

	private static readonly object VisibleMonthChangedEvent;

	/// <summary>Gets or sets a text value that is rendered as a caption for the calendar.</summary>
	/// <returns>The table caption.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Caption
	{
		get
		{
			return ViewState.GetString("Caption", string.Empty);
		}
		set
		{
			ViewState["Caption"] = value;
		}
	}

	/// <summary>Gets or sets the alignment of the text that is rendered as a caption for the calendar.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> value that indicates the alignment of the caption. </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values.</exception>
	[DefaultValue(TableCaptionAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual TableCaptionAlign CaptionAlign
	{
		get
		{
			return (TableCaptionAlign)ViewState.GetInt("CaptionAlign", 0);
		}
		set
		{
			ViewState["CaptionAlign"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
	/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is <see langword="2" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell padding is less than -1. </exception>
	[DefaultValue(2)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public int CellPadding
	{
		get
		{
			return ViewState.GetInt("CellPadding", 2);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("The specified cell padding is less than -1.");
			}
			ViewState["CellPadding"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space between cells.</summary>
	/// <returns>The amount of space (in pixels) between cells. The default value is <see langword="0" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell spacing is less than -1. </exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public int CellSpacing
	{
		get
		{
			return ViewState.GetInt("CellSpacing", 0);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("The specified cell spacing is less than -1");
			}
			ViewState["CellSpacing"] = value;
		}
	}

	/// <summary>Gets the style properties for the section that displays the day of the week.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the section that displays the day of the week. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle DayHeaderStyle
	{
		get
		{
			if (dayHeaderStyle == null)
			{
				dayHeaderStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					dayHeaderStyle.TrackViewState();
				}
			}
			return dayHeaderStyle;
		}
	}

	/// <summary>Gets or sets the name format for days of the week.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. The default value is <see langword="Short" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified day name format is not one of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. </exception>
	[DefaultValue(DayNameFormat.Short)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public DayNameFormat DayNameFormat
	{
		get
		{
			return (DayNameFormat)ViewState.GetInt("DayNameFormat", 1);
		}
		set
		{
			if (value != DayNameFormat.FirstLetter && value != DayNameFormat.FirstTwoLetters && value != 0 && value != DayNameFormat.Short && value != DayNameFormat.Shortest)
			{
				throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
			}
			ViewState["DayNameFormat"] = value;
		}
	}

	/// <summary>Gets the style properties for the days in the displayed month.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle DayStyle
	{
		get
		{
			if (dayStyle == null)
			{
				dayStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					dayStyle.TrackViewState();
				}
			}
			return dayStyle;
		}
	}

	/// <summary>Gets or sets the day of the week to display in the first day column of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. The default is <see langword="Default" />, which indicates that the day specified in the system setting is used.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The date specified is not one of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. </exception>
	[DefaultValue(FirstDayOfWeek.Default)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public FirstDayOfWeek FirstDayOfWeek
	{
		get
		{
			return (FirstDayOfWeek)ViewState.GetInt("FirstDayOfWeek", 7);
		}
		set
		{
			if (value < FirstDayOfWeek.Sunday || value > FirstDayOfWeek.Default)
			{
				throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
			}
			ViewState["FirstDayOfWeek"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed for the next month navigation control.</summary>
	/// <returns>The caption text for the next month navigation control. The default value is <see langword="&quot;&amp;gt;&quot;" />, which is rendered as the greater than sign (&gt;).</returns>
	[DefaultValue("&gt;")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string NextMonthText
	{
		get
		{
			return ViewState.GetString("NextMonthText", "&gt;");
		}
		set
		{
			ViewState["NextMonthText"] = value;
		}
	}

	/// <summary>Gets or sets the format of the next and previous month navigation elements in the title section of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. The default value is <see langword="CustomText" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified format is not one of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. </exception>
	[DefaultValue(NextPrevFormat.CustomText)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public NextPrevFormat NextPrevFormat
	{
		get
		{
			return (NextPrevFormat)ViewState.GetInt("NextPrevFormat", 0);
		}
		set
		{
			if (value != 0 && value != NextPrevFormat.ShortMonth && value != NextPrevFormat.FullMonth)
			{
				throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
			}
			ViewState["NextPrevFormat"] = value;
		}
	}

	/// <summary>Gets the style properties for the next and previous month navigation elements.</summary>
	/// <returns>The style properties for the next and previous month navigation elements. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle NextPrevStyle
	{
		get
		{
			if (nextPrevStyle == null)
			{
				nextPrevStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					nextPrevStyle.TrackViewState();
				}
			}
			return nextPrevStyle;
		}
	}

	/// <summary>Gets the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle OtherMonthDayStyle
	{
		get
		{
			if (otherMonthDayStyle == null)
			{
				otherMonthDayStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					otherMonthDayStyle.TrackViewState();
				}
			}
			return otherMonthDayStyle;
		}
	}

	/// <summary>Gets or sets the text displayed for the previous month navigation control.</summary>
	/// <returns>The caption text for the previous month navigation control. The default value is <see langword="&quot;&amp;lt;&quot;" />, which is rendered as the less than sign (&lt;).</returns>
	[DefaultValue("&lt;")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string PrevMonthText
	{
		get
		{
			return ViewState.GetString("PrevMonthText", "&lt;");
		}
		set
		{
			ViewState["PrevMonthText"] = value;
		}
	}

	/// <summary>Gets or sets the selected date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that represents the selected date. The default value is <see cref="F:System.DateTime.MinValue" />.</returns>
	[Bindable(true, BindingDirection.TwoWay)]
	[DefaultValue("1/1/0001 12:00:00 AM")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public DateTime SelectedDate
	{
		get
		{
			if (SelectedDates.Count > 0)
			{
				return SelectedDates[0];
			}
			return DateTime.MinValue;
		}
		set
		{
			SelectedDates.SelectRange(value, value);
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.DateTime" /> objects that represent the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> that contains a collection of <see cref="T:System.DateTime" /> objects representing the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public SelectedDatesCollection SelectedDates
	{
		get
		{
			if (dateList == null)
			{
				dateList = new ArrayList();
			}
			if (selectedDatesCollection == null)
			{
				selectedDatesCollection = new SelectedDatesCollection(dateList);
			}
			return selectedDatesCollection;
		}
	}

	/// <summary>Gets the style properties for the selected dates.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the selected dates. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle SelectedDayStyle
	{
		get
		{
			if (selectedDayStyle == null)
			{
				selectedDayStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					selectedDayStyle.TrackViewState();
				}
			}
			return selectedDayStyle;
		}
	}

	/// <summary>Gets or sets the date selection mode on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that specifies whether the user can select a single day, a week, or an entire month.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. The default value is <see langword="Day" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </exception>
	[DefaultValue(CalendarSelectionMode.Day)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public CalendarSelectionMode SelectionMode
	{
		get
		{
			return (CalendarSelectionMode)ViewState.GetInt("SelectionMode", 1);
		}
		set
		{
			if (value != CalendarSelectionMode.Day && value != CalendarSelectionMode.DayWeek && value != CalendarSelectionMode.DayWeekMonth && value != 0)
			{
				throw new ArgumentOutOfRangeException("The specified selection mode is not one of the CalendarSelectionMode values.");
			}
			ViewState["SelectionMode"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed for the month selection element in the selector column.</summary>
	/// <returns>The text displayed for the month selection element in the selector column. The default value is <see langword="&quot;&amp;gt;&amp;gt;&quot;" />, which is rendered as two greater than signs (&gt;&gt;).</returns>
	[DefaultValue("&gt;&gt;")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string SelectMonthText
	{
		get
		{
			return ViewState.GetString("SelectMonthText", "&gt;&gt;");
		}
		set
		{
			ViewState["SelectMonthText"] = value;
		}
	}

	/// <summary>Gets the style properties for the week and month selector column.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the week and month selector column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle SelectorStyle
	{
		get
		{
			if (selectorStyle == null)
			{
				selectorStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					selectorStyle.TrackViewState();
				}
			}
			return selectorStyle;
		}
	}

	/// <summary>Gets or sets the text displayed for the week selection element in the selector column.</summary>
	/// <returns>The text displayed for the week selection element in the selector column. The default value is <see langword="&quot;&amp;gt;&quot;" />, which is rendered as a greater than sign (&gt;).</returns>
	[DefaultValue("&gt;")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string SelectWeekText
	{
		get
		{
			return ViewState.GetString("SelectWeekText", "&gt;");
		}
		set
		{
			ViewState["SelectWeekText"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the heading for the days of the week is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the heading for the days of the week is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public bool ShowDayHeader
	{
		get
		{
			return ViewState.GetBool("ShowDayHeader", def: true);
		}
		set
		{
			ViewState["ShowDayHeader"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines.</summary>
	/// <returns>
	///     <see langword="true" /> if the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public bool ShowGridLines
	{
		get
		{
			return ViewState.GetBool("ShowGridLines", def: false);
		}
		set
		{
			ViewState["ShowGridLines"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Calendar" /> control displays the next and previous month navigation elements in the title section.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the next and previous month navigation elements in the title section; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public bool ShowNextPrevMonth
	{
		get
		{
			return ViewState.GetBool("ShowNextPrevMonth", def: true);
		}
		set
		{
			ViewState["ShowNextPrevMonth"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the title section is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the title section; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public bool ShowTitle
	{
		get
		{
			return ViewState.GetBool("ShowTitle", def: true);
		}
		set
		{
			ViewState["ShowTitle"] = value;
		}
	}

	/// <summary>Gets or sets the format for the title section.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. The default value is <see langword="MonthYear" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified title format is not one of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. </exception>
	[DefaultValue(TitleFormat.MonthYear)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public TitleFormat TitleFormat
	{
		get
		{
			return (TitleFormat)ViewState.GetInt("TitleFormat", 1);
		}
		set
		{
			if (value != 0 && value != TitleFormat.MonthYear)
			{
				throw new ArgumentOutOfRangeException("The specified title format is not one of the TitleFormat values.");
			}
			ViewState["TitleFormat"] = value;
		}
	}

	/// <summary>Gets the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle TitleStyle
	{
		get
		{
			if (titleStyle == null)
			{
				titleStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					titleStyle.TrackViewState();
				}
			}
			return titleStyle;
		}
	}

	/// <summary>Gets the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle TodayDayStyle
	{
		get
		{
			if (todayDayStyle == null)
			{
				todayDayStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					todayDayStyle.TrackViewState();
				}
			}
			return todayDayStyle;
		}
	}

	/// <summary>Gets or sets the value for today's date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that contains the value that the <see cref="T:System.Web.UI.WebControls.Calendar" /> considers to be today's date. If this property is not explicitly set, this date will be the date on the server.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public DateTime TodaysDate
	{
		get
		{
			object obj = ViewState["TodaysDate"];
			if (obj != null)
			{
				return (DateTime)obj;
			}
			return today;
		}
		set
		{
			ViewState["TodaysDate"] = value.Date;
		}
	}

	/// <summary>Gets or sets a value that indicates whether to render the table header <see langword="&lt;th&gt;" /> HTML element for the day headers instead of the table data <see langword="&lt;td&gt;" /> HTML element.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see langword="&lt;th&gt;" /> element is used for day header cells; <see langword="false" /> if the <see langword="&lt;td&gt;" /> element is used for day headers.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual bool UseAccessibleHeader
	{
		get
		{
			return ViewState.GetBool("UseAccessibleHeader", def: true);
		}
		set
		{
			ViewState["UseAccessibleHeader"] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.DateTime" /> value that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is <see cref="F:System.DateTime.MinValue" />, which displays the month that contains the date specified by <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" />.</returns>
	[Bindable(true)]
	[DefaultValue("1/1/0001 12:00:00 AM")]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public DateTime VisibleDate
	{
		get
		{
			object obj = ViewState["VisibleDate"];
			if (obj != null)
			{
				return (DateTime)obj;
			}
			return DateTime.MinValue;
		}
		set
		{
			ViewState["VisibleDate"] = value.Date;
		}
	}

	/// <summary>Gets the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public TableItemStyle WeekendDayStyle
	{
		get
		{
			if (weekendDayStyle == null)
			{
				weekendDayStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					weekendDayStyle.TrackViewState();
				}
			}
			return weekendDayStyle;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	private DateTimeFormatInfo DateInfo
	{
		get
		{
			if (dateInfo == null)
			{
				dateInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
			}
			return dateInfo;
		}
	}

	private DateTime DisplayDate
	{
		get
		{
			DateTime dateTime = VisibleDate;
			if (dateTime == DateTime.MinValue)
			{
				dateTime = TodaysDate;
			}
			return dateTime;
		}
	}

	private DayOfWeek DisplayFirstDayOfWeek
	{
		get
		{
			if (FirstDayOfWeek != FirstDayOfWeek.Default)
			{
				return (DayOfWeek)FirstDayOfWeek;
			}
			return DateInfo.FirstDayOfWeek;
		}
	}

	/// <summary>Occurs when each day is created in the control hierarchy for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DayRenderEventHandler DayRender
	{
		add
		{
			base.Events.AddHandler(DayRenderEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DayRenderEvent, value);
		}
	}

	/// <summary>Occurs when the user selects a day, a week, or an entire month by clicking the date selector controls.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler SelectionChanged
	{
		add
		{
			base.Events.AddHandler(SelectionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectionChangedEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks the next or previous month navigation controls on the title heading.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event MonthChangedEventHandler VisibleMonthChanged
	{
		add
		{
			base.Events.AddHandler(VisibleMonthChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(VisibleMonthChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Calendar" /> class.</summary>
	public Calendar()
	{
	}

	/// <summary>Creates a collection to store child controls.</summary>
	/// <returns>Always returns an <see langword="InternalControlCollection" /> object.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return base.CreateControlCollection();
	}

	/// <summary>Determines whether a <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> object contains week selectors.</summary>
	/// <param name="selectionMode">One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> contains week selectors; otherwise, <see langword="false" />.</returns>
	protected bool HasWeekSelectors(CalendarSelectionMode selectionMode)
	{
		if (selectionMode == CalendarSelectionMode.DayWeek || selectionMode == CalendarSelectionMode.DayWeekMonth)
		{
			return true;
		}
		return false;
	}

	/// <summary>Raises events on postback for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <param name="eventArgument">The argument for the event. </param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises an event for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler. </param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (eventArgument.Length >= 1)
		{
			if (eventArgument[0] == 'V')
			{
				DateTime visibleDate = VisibleDate;
				int days = int.Parse(eventArgument.Substring(1));
				DateTime visibleDate2 = GetGlobalCalendar().AddDays(dateZenith, days);
				VisibleDate = visibleDate2;
				OnVisibleMonthChanged(VisibleDate, visibleDate);
			}
			else if (eventArgument[0] == 'R')
			{
				string text = eventArgument.Substring(1);
				string s = text.Substring(text.Length - 2, 2);
				string s2 = text.Substring(0, text.Length - 2);
				DateTime fromDate = GetGlobalCalendar().AddDays(dateZenith, int.Parse(s2));
				SelectedDates.SelectRange(fromDate, fromDate.AddDays(int.Parse(s)));
				OnSelectionChanged();
			}
			else
			{
				int days2 = int.Parse(eventArgument);
				DateTime dateTime = GetGlobalCalendar().AddDays(dateZenith, days2);
				SelectedDates.SelectRange(dateTime, dateTime);
				OnSelectionChanged();
			}
		}
	}

	/// <summary>Loads a saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <param name="savedState">A <see cref="T:System.Object" /> that contains the saved condition of the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
	protected override void LoadViewState(object savedState)
	{
		object[] array = (object[])savedState;
		if (array[0] != null)
		{
			base.LoadViewState(array[0]);
		}
		if (array[1] != null)
		{
			DayHeaderStyle.LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			DayStyle.LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			NextPrevStyle.LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			OtherMonthDayStyle.LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			SelectedDayStyle.LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			TitleStyle.LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			TodayDayStyle.LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			SelectorStyle.LoadViewState(array[8]);
		}
		if (array[9] != null)
		{
			WeekendDayStyle.LoadViewState(array[9]);
		}
		ArrayList arrayList = (ArrayList)ViewState["SelectedDates"];
		if (arrayList != null)
		{
			dateList = arrayList;
			selectedDatesCollection = new SelectedDatesCollection(dateList);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to render. </param>
	/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that contains information about the day to render. </param>
	protected virtual void OnDayRender(TableCell cell, CalendarDay day)
	{
		DayRenderEventHandler dayRenderEventHandler = (DayRenderEventHandler)base.Events[DayRender];
		if (dayRenderEventHandler != null)
		{
			Page page = Page;
			if (page != null)
			{
				dayRenderEventHandler(this, new DayRenderEventArgs(cell, day, page.ClientScript.GetPostBackClientHyperlink(this, GetDaysFromZenith(day.Date).ToString(), registerForEventValidation: true)));
			}
			else
			{
				dayRenderEventHandler(this, new DayRenderEventArgs(cell, day));
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event.</summary>
	protected virtual void OnSelectionChanged()
	{
		((EventHandler)base.Events[SelectionChanged])?.Invoke(this, EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event.</summary>
	/// <param name="newDate">A <see cref="T:System.DateTime" /> that represents the month currently displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
	/// <param name="previousDate">A <see cref="T:System.DateTime" /> that represents the previous month displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
	protected virtual void OnVisibleMonthChanged(DateTime newDate, DateTime previousDate)
	{
		((MonthChangedEventHandler)base.Events[VisibleMonthChanged])?.Invoke(this, new MonthChangedEventArgs(newDate, previousDate));
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.Calendar" /> control on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		TableStyle tableStyle = new TableStyle();
		tableStyle.CellSpacing = CellSpacing;
		tableStyle.CellPadding = CellPadding;
		tableStyle.BorderWidth = 1;
		if (base.ControlStyleCreated)
		{
			tableStyle.CopyFrom(base.ControlStyle);
		}
		if (ShowGridLines)
		{
			tableStyle.GridLines = GridLines.Both;
		}
		tableStyle.AddAttributesToRender(writer);
		writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
		writer.RenderBeginTag(HtmlTextWriterTag.Table);
		if (!string.IsNullOrEmpty(Caption))
		{
			WriteCaption(writer);
		}
		bool isEnabled = base.IsEnabled;
		if (ShowTitle)
		{
			WriteTitle(writer, isEnabled);
		}
		if (ShowDayHeader)
		{
			WriteDayHeader(writer, isEnabled);
		}
		WriteDays(writer, isEnabled);
		writer.RenderEndTag();
	}

	/// <summary>Stores the state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[10];
		if (dayHeaderStyle != null)
		{
			array[1] = dayHeaderStyle.SaveViewState();
		}
		if (dayStyle != null)
		{
			array[2] = dayStyle.SaveViewState();
		}
		if (nextPrevStyle != null)
		{
			array[3] = nextPrevStyle.SaveViewState();
		}
		if (otherMonthDayStyle != null)
		{
			array[4] = otherMonthDayStyle.SaveViewState();
		}
		if (selectedDayStyle != null)
		{
			array[5] = selectedDayStyle.SaveViewState();
		}
		if (titleStyle != null)
		{
			array[6] = titleStyle.SaveViewState();
		}
		if (todayDayStyle != null)
		{
			array[7] = todayDayStyle.SaveViewState();
		}
		if (selectorStyle != null)
		{
			array[8] = selectorStyle.SaveViewState();
		}
		if (weekendDayStyle != null)
		{
			array[9] = weekendDayStyle.SaveViewState();
		}
		if (SelectedDates.Count > 0)
		{
			ViewState["SelectedDates"] = dateList;
		}
		array[0] = base.SaveViewState();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Marks the starting point to begin tracking and saving changes to the control as part of the control view state.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (dayHeaderStyle != null)
		{
			dayHeaderStyle.TrackViewState();
		}
		if (dayStyle != null)
		{
			dayStyle.TrackViewState();
		}
		if (nextPrevStyle != null)
		{
			nextPrevStyle.TrackViewState();
		}
		if (otherMonthDayStyle != null)
		{
			otherMonthDayStyle.TrackViewState();
		}
		if (selectedDayStyle != null)
		{
			selectedDayStyle.TrackViewState();
		}
		if (titleStyle != null)
		{
			titleStyle.TrackViewState();
		}
		if (todayDayStyle != null)
		{
			todayDayStyle.TrackViewState();
		}
		if (selectorStyle != null)
		{
			selectorStyle.TrackViewState();
		}
		if (weekendDayStyle != null)
		{
			weekendDayStyle.TrackViewState();
		}
	}

	private void WriteDayHeader(HtmlTextWriter writer, bool enabled)
	{
		int displayFirstDayOfWeek;
		int num = (displayFirstDayOfWeek = (int)DisplayFirstDayOfWeek);
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		if (SelectionMode == CalendarSelectionMode.DayWeek)
		{
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.ApplyStyle(DayHeaderStyle);
			tableCell.RenderBeginTag(writer);
			tableCell.RenderEndTag(writer);
		}
		else if (SelectionMode == CalendarSelectionMode.DayWeekMonth)
		{
			TableCell tableCell2 = new TableCell();
			tableCell2.ApplyStyle(SelectorStyle);
			tableCell2.HorizontalAlign = HorizontalAlign.Center;
			DateTime date = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
			int num2 = DateTime.DaysInMonth(DisplayDate.Year, DisplayDate.Month);
			tableCell2.RenderBeginTag(writer);
			writer.Write(BuildLink("R" + GetDaysFromZenith(date) + num2, SelectMonthText, DayHeaderStyle.ForeColor, enabled));
			tableCell2.RenderEndTag(writer);
		}
		DateTimeFormatInfo dateTimeFormatInfo = DateInfo;
		do
		{
			DayOfWeek dayOfWeek = (DayOfWeek)num;
			string text = dateTimeFormatInfo.GetDayName(dayOfWeek);
			TableCell tableCell;
			if (UseAccessibleHeader)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Abbr, text);
				writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", fEncode: false);
				tableCell = new TableHeaderCell();
			}
			else
			{
				tableCell = new TableCell();
			}
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.ApplyStyle(DayHeaderStyle);
			tableCell.RenderBeginTag(writer);
			switch (DayNameFormat)
			{
			case DayNameFormat.FirstLetter:
				text = text.Substring(0, 1);
				break;
			case DayNameFormat.FirstTwoLetters:
				text = text.Substring(0, 2);
				break;
			case DayNameFormat.Shortest:
				text = dateTimeFormatInfo.GetShortestDayName(dayOfWeek);
				break;
			default:
				text = dateTimeFormatInfo.GetAbbreviatedDayName(dayOfWeek);
				break;
			case DayNameFormat.Full:
				break;
			}
			writer.Write(text);
			tableCell.RenderEndTag(writer);
			num = ((num < 6) ? (num + 1) : 0);
		}
		while (num != displayFirstDayOfWeek);
		writer.RenderEndTag();
	}

	private void WriteDay(DateTime date, HtmlTextWriter writer, bool enabled)
	{
		TableItemStyle tableItemStyle = new TableItemStyle();
		TableCell tableCell = new TableCell();
		CalendarDay calendarDay = new CalendarDay(date, IsWeekEnd(date.DayOfWeek), date == TodaysDate, SelectedDates.Contains(date), GetGlobalCalendar().GetMonth(DisplayDate) != GetGlobalCalendar().GetMonth(date), date.Day.ToString());
		calendarDay.IsSelectable = SelectionMode != CalendarSelectionMode.None;
		tableCell.HorizontalAlign = HorizontalAlign.Center;
		tableCell.Width = Unit.Percentage(GetCellWidth());
		LiteralControl literalControl = new LiteralControl(calendarDay.DayNumberText);
		tableCell.Controls.Add(literalControl);
		OnDayRender(tableCell, calendarDay);
		if (dayStyle != null && !dayStyle.IsEmpty)
		{
			tableItemStyle.CopyFrom(dayStyle);
		}
		if (calendarDay.IsWeekend && weekendDayStyle != null && !weekendDayStyle.IsEmpty)
		{
			tableItemStyle.CopyFrom(weekendDayStyle);
		}
		if (calendarDay.IsToday && todayDayStyle != null && !todayDayStyle.IsEmpty)
		{
			tableItemStyle.CopyFrom(todayDayStyle);
		}
		if (calendarDay.IsOtherMonth && otherMonthDayStyle != null && !otherMonthDayStyle.IsEmpty)
		{
			tableItemStyle.CopyFrom(otherMonthDayStyle);
		}
		if (enabled && calendarDay.IsSelected)
		{
			tableItemStyle.BackColor = Color.Silver;
			tableItemStyle.ForeColor = Color.White;
			if (selectedDayStyle != null && !selectedDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(selectedDayStyle);
			}
		}
		tableCell.ApplyStyle(tableItemStyle);
		literalControl.Text = BuildLink(GetDaysFromZenith(date).ToString(), calendarDay.DayNumberText, tableCell.ForeColor, enabled && calendarDay.IsSelectable);
		tableCell.RenderControl(writer);
	}

	private void WriteDays(HtmlTextWriter writer, bool enabled)
	{
		DateTime dateTime = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
		TableCell tableCell = null;
		int i;
		for (i = 0; i < 7; i++)
		{
			if (dateTime.DayOfWeek == DisplayFirstDayOfWeek)
			{
				break;
			}
			dateTime = GetGlobalCalendar().AddDays(dateTime, -1);
		}
		if (i == 0)
		{
			dateTime = GetGlobalCalendar().AddDays(dateTime, -7);
		}
		DateTime dateTime2 = GetGlobalCalendar().AddDays(dateTime, 42);
		do
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (HasWeekSelectors(SelectionMode))
			{
				if (tableCell == null)
				{
					tableCell = new TableCell();
					tableCell.ApplyStyle(SelectorStyle);
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					tableCell.Width = Unit.Percentage(GetCellWidth());
				}
				tableCell.RenderBeginTag(writer);
				writer.Write(BuildLink("R" + GetDaysFromZenith(dateTime) + "07", SelectWeekText, tableCell.ForeColor, enabled));
				tableCell.RenderEndTag(writer);
			}
			for (int j = 0; j < 7; j++)
			{
				WriteDay(dateTime, writer, enabled);
				dateTime = GetGlobalCalendar().AddDays(dateTime, 1);
			}
			writer.RenderEndTag();
		}
		while (!(dateTime >= dateTime2));
	}

	private string BuildLink(string arg, string text, Color foreColor, bool hasLink)
	{
		StringBuilder stringBuilder = new StringBuilder();
		Page page = Page;
		hasLink = ((page != null && hasLink) ? true : false);
		if (hasLink)
		{
			stringBuilder.Append("<a href=\"");
			stringBuilder.Append(page.ClientScript.GetPostBackClientHyperlink(this, arg, registerForEventValidation: true));
			stringBuilder.Append('"');
			Color c = ((!foreColor.IsEmpty) ? foreColor : ((!ForeColor.IsEmpty) ? ForeColor : Color.Black));
			stringBuilder.Append(" style=\"color:" + ColorTranslator.ToHtml(c));
			stringBuilder.Append("\">");
			stringBuilder.Append(text);
			stringBuilder.Append("</a>");
		}
		else
		{
			stringBuilder.Append(text);
		}
		return stringBuilder.ToString();
	}

	private int GetDaysFromZenith(DateTime date)
	{
		return date.Subtract(dateZenith).Days;
	}

	private void WriteCaption(HtmlTextWriter writer)
	{
		if (CaptionAlign != 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Align, CaptionAlign.ToString(Helpers.InvariantCulture));
		}
		writer.RenderBeginTag(HtmlTextWriterTag.Caption);
		writer.Write(Caption);
		writer.RenderEndTag();
	}

	private void WriteTitle(HtmlTextWriter writer, bool enabled)
	{
		TableCell tableCell = null;
		TableCell tableCell2 = new TableCell();
		Table table = new Table();
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		tableCell2.ColumnSpan = (HasWeekSelectors(SelectionMode) ? 8 : 7);
		if (titleStyle != null && !titleStyle.IsEmpty && !titleStyle.BackColor.IsEmpty)
		{
			tableCell2.BackColor = titleStyle.BackColor;
		}
		else
		{
			tableCell2.BackColor = Color.Silver;
		}
		tableCell2.RenderBeginTag(writer);
		table.Width = Unit.Percentage(100.0);
		if (titleStyle != null && !titleStyle.IsEmpty)
		{
			table.ApplyStyle(titleStyle);
		}
		table.RenderBeginTag(writer);
		writer.RenderBeginTag(HtmlTextWriterTag.Tr);
		if (ShowNextPrevMonth)
		{
			tableCell = new TableCell();
			tableCell.ApplyStyle(nextPrevStyle);
			tableCell.Width = Unit.Percentage(15.0);
			DateTime time = GetGlobalCalendar().AddMonths(DisplayDate, -1);
			time = GetGlobalCalendar().AddDays(time, -time.Day + 1);
			tableCell.RenderBeginTag(writer);
			writer.Write(BuildLink("V" + GetDaysFromZenith(time), GetNextPrevFormatText(time, next: false), tableCell.ForeColor, enabled));
			tableCell.RenderEndTag(writer);
		}
		DateTimeFormatInfo dateTimeFormatInfo = DateInfo;
		TableCell tableCell3 = new TableCell();
		tableCell3.Width = Unit.Percentage(70.0);
		tableCell3.HorizontalAlign = HorizontalAlign.Center;
		tableCell3.RenderBeginTag(writer);
		string value = ((TitleFormat != TitleFormat.MonthYear) ? dateTimeFormatInfo.GetMonthName(GetGlobalCalendar().GetMonth(DisplayDate)) : DisplayDate.ToString(dateTimeFormatInfo.YearMonthPattern, dateTimeFormatInfo));
		writer.Write(value);
		tableCell3.RenderEndTag(writer);
		if (ShowNextPrevMonth)
		{
			DateTime time2 = GetGlobalCalendar().AddMonths(DisplayDate, 1);
			time2 = GetGlobalCalendar().AddDays(time2, -time2.Day + 1);
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableCell.RenderBeginTag(writer);
			writer.Write(BuildLink("V" + GetDaysFromZenith(time2), GetNextPrevFormatText(time2, next: true), tableCell.ForeColor, enabled));
			tableCell.RenderEndTag(writer);
		}
		writer.RenderEndTag();
		table.RenderEndTag(writer);
		tableCell2.RenderEndTag(writer);
		writer.RenderEndTag();
	}

	private string GetNextPrevFormatText(DateTime date, bool next)
	{
		DateTimeFormatInfo dateTimeFormatInfo = DateInfo;
		return NextPrevFormat switch
		{
			NextPrevFormat.FullMonth => dateTimeFormatInfo.GetMonthName(GetGlobalCalendar().GetMonth(date)), 
			NextPrevFormat.ShortMonth => dateTimeFormatInfo.GetAbbreviatedMonthName(GetGlobalCalendar().GetMonth(date)), 
			_ => next ? NextMonthText : PrevMonthText, 
		};
	}

	private bool IsWeekEnd(DayOfWeek day)
	{
		if (day != DayOfWeek.Saturday)
		{
			return day == DayOfWeek.Sunday;
		}
		return true;
	}

	private double GetCellWidth()
	{
		return HasWeekSelectors(SelectionMode) ? 12 : 14;
	}

	private System.Globalization.Calendar GetGlobalCalendar()
	{
		return DateTimeFormatInfo.CurrentInfo.Calendar;
	}

	static Calendar()
	{
		DayRender = new object();
		SelectionChanged = new object();
		VisibleMonthChanged = new object();
	}
}
