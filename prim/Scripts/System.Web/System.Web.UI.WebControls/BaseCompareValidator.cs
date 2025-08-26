using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the abstract base class for validation controls that perform typed comparisons. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class BaseCompareValidator : BaseValidator
{
	/// <summary>Gets or sets a value indicating whether values are converted to a culture-neutral format before being compared.</summary>
	/// <returns>
	///     <see langword="true" /> to convert values to a culture-neutral format before they are compared; otherwise, <see langword="false" />.The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public bool CultureInvariantValues
	{
		get
		{
			return ViewState.GetBool("CultureInvariantValues", def: false);
		}
		set
		{
			ViewState["CultureInvariantValues"] = value;
		}
	}

	/// <summary>Gets the maximum year that can be represented by a two-digit year.</summary>
	/// <returns>The maximum year that can be represented by a two-digit year.</returns>
	protected static int CutoffYear => CultureInfo.CurrentCulture.Calendar.TwoDigitYearMax;

	/// <summary>Gets or sets the data type that the values being compared are converted to before the comparison is made.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> enumeration values. The default value is <see langword="String" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified data type is not one of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</exception>
	[DefaultValue(ValidationDataType.String)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public ValidationDataType Type
	{
		get
		{
			if (ViewState["Type"] != null)
			{
				return (ValidationDataType)ViewState["Type"];
			}
			return ValidationDataType.String;
		}
		set
		{
			ViewState["Type"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseCompareValidator" /> class.</summary>
	protected BaseCompareValidator()
	{
	}

	/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (base.RenderUplevel && Page != null)
		{
			RegisterExpandoAttribute(ClientID, "type", Type.ToString());
			switch (Type)
			{
			case ValidationDataType.Date:
			{
				DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
				string shortDatePattern = dateTimeFormat.ShortDatePattern;
				string attributeValue = (shortDatePattern.StartsWith("y", ignoreCase: true, Helpers.InvariantCulture) ? "ymd" : (shortDatePattern.StartsWith("m", ignoreCase: true, Helpers.InvariantCulture) ? "mdy" : "dmy"));
				RegisterExpandoAttribute(ClientID, "dateorder", attributeValue);
				RegisterExpandoAttribute(ClientID, "cutoffyear", dateTimeFormat.Calendar.TwoDigitYearMax.ToString());
				break;
			}
			case ValidationDataType.Currency:
			{
				NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
				RegisterExpandoAttribute(ClientID, "decimalchar", numberFormat.CurrencyDecimalSeparator, encode: true);
				RegisterExpandoAttribute(ClientID, "groupchar", numberFormat.CurrencyGroupSeparator, encode: true);
				RegisterExpandoAttribute(ClientID, "digits", numberFormat.CurrencyDecimalDigits.ToString());
				RegisterExpandoAttribute(ClientID, "groupsize", numberFormat.CurrencyGroupSizes[0].ToString());
				break;
			}
			}
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Determines whether the specified string can be converted to the specified data type. This version of the overloaded method tests currency, double, and date values using the format used by the current culture.</summary>
	/// <param name="text">The string to test.</param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
	/// <returns>
	///     <see langword="true" /> if the specified data string can be converted to the specified data type; otherwise, <see langword="false" />.</returns>
	public static bool CanConvert(string text, ValidationDataType type)
	{
		object value;
		return Convert(text, type, out value);
	}

	/// <summary>Converts the specified text into an object of the specified data type. This version of the overloaded method converts currency, double, and date values using the format used by the current culture.</summary>
	/// <param name="text">The text to convert.</param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
	/// <param name="value">When this method returns, contains an object with the conversion result. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> if the conversion is successful; otherwise, <see langword="false" />.</returns>
	protected static bool Convert(string text, ValidationDataType type, out object value)
	{
		return Convert(text, type, cultureInvariant: false, out value);
	}

	/// <summary>Compares two strings using the specified operator and data type. This version of the overloaded method compares currency, double, and date values using the format used by the current culture.</summary>
	/// <param name="leftText">The value on the left side of the operator.</param>
	/// <param name="rightText">The value on the right side of the operator.</param>
	/// <param name="op">One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. </param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="leftValue" /> parameter relates to the <paramref name="rightValue" /> parameter in the manner specified by the <paramref name="op" /> parameter; otherwise, <see langword="false" />.</returns>
	protected static bool Compare(string leftText, string rightText, ValidationCompareOperator op, ValidationDataType type)
	{
		return Compare(leftText, cultureInvariantLeftText: false, rightText, cultureInvariantRightText: false, op, type);
	}

	/// <summary>Determines whether the validation control can be rendered for a newer ("uplevel") browser.</summary>
	/// <returns>
	///     <see langword="true" /> if the validation control can be rendered for an "uplevel" browser; otherwise, <see langword="false" />.</returns>
	protected override bool DetermineRenderUplevel()
	{
		return base.DetermineRenderUplevel();
	}

	/// <summary>Determines the order in which the month, day, and year appear in a date value for the current culture.</summary>
	/// <returns>A string that represents the order in which the month, day, and year appear in a date value for the current culture.</returns>
	protected static string GetDateElementOrder()
	{
		string shortDatePattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		shortDatePattern = shortDatePattern.ToLower(Helpers.InvariantCulture);
		foreach (char c in shortDatePattern)
		{
			if (c != 'm' && c != 'd' && c != 'y')
			{
				continue;
			}
			switch (c)
			{
			case 'm':
				if (!flag3)
				{
					stringBuilder.Append("m");
				}
				flag3 = true;
				break;
			case 'y':
				if (!flag2)
				{
					stringBuilder.Append("y");
				}
				flag2 = true;
				break;
			default:
				if (!flag)
				{
					stringBuilder.Append("d");
				}
				flag = true;
				break;
			}
		}
		return stringBuilder.ToString();
	}

	/// <summary>Generates the four-digit year representation of the specified two-digit year.</summary>
	/// <param name="shortYear">A two-digit year.</param>
	/// <returns>The four-digit year representation of the specified two-digit year.</returns>
	protected static int GetFullYear(int shortYear)
	{
		int cutoffYear = CutoffYear;
		int num = cutoffYear % 100;
		if (shortYear <= num)
		{
			return cutoffYear - num + shortYear;
		}
		return cutoffYear - num - 100 + shortYear;
	}

	/// <summary>Determines whether the specified string can be converted to the specified data type. This version of the overloaded method allows you to specify whether values are tested using a culture-neutral format.</summary>
	/// <param name="text">The string to test.</param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> enumeration values.</param>
	/// <param name="cultureInvariant">
	///       <see langword="true" /> to test values using a culture-neutral format; otherwise, <see langword="false" />.</param>
	/// <returns>
	///     <see langword="true" /> if the specified data string can be converted to the specified data type; otherwise, <see langword="false" />.</returns>
	public static bool CanConvert(string text, ValidationDataType type, bool cultureInvariant)
	{
		object value;
		return Convert(text, type, cultureInvariant, out value);
	}

	/// <summary>Compares two strings using the specified operator and validation data type. This version of the overload allows you to specify whether values are compared using a culture-neutral format.</summary>
	/// <param name="leftText">The value on the left side of the operator.</param>
	/// <param name="cultureInvariantLeftText">
	///       <see langword="true" /> to convert the left side value to a culture-neutral format; otherwise, <see langword="false" />.</param>
	/// <param name="rightText">The value on the right side of the operator.</param>
	/// <param name="cultureInvariantRightText">
	///       <see langword="true" /> to convert the right side value to a culture-neutral format; otherwise, <see langword="false" />.</param>
	/// <param name="op">One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values.</param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="leftValue" /> parameter relates to the <paramref name="rightValue" /> parameter in the manner specified by the <paramref name="op" /> parameter; otherwise, <see langword="false" />.</returns>
	protected static bool Compare(string leftText, bool cultureInvariantLeftText, string rightText, bool cultureInvariantRightText, ValidationCompareOperator op, ValidationDataType type)
	{
		if (!Convert(leftText, type, cultureInvariantLeftText, out var value))
		{
			return false;
		}
		if (op == ValidationCompareOperator.DataTypeCheck)
		{
			return true;
		}
		if (!Convert(rightText, type, cultureInvariantRightText, out var value2))
		{
			return true;
		}
		int num = ((IComparable)value).CompareTo((IComparable)value2);
		return op switch
		{
			ValidationCompareOperator.Equal => num == 0, 
			ValidationCompareOperator.NotEqual => num != 0, 
			ValidationCompareOperator.LessThan => num < 0, 
			ValidationCompareOperator.LessThanEqual => num <= 0, 
			ValidationCompareOperator.GreaterThan => num > 0, 
			ValidationCompareOperator.GreaterThanEqual => num >= 0, 
			_ => false, 
		};
	}

	/// <summary>Converts the specified text into an object of the specified data type. This version of the overloaded method allows you to specify whether values are converted using a culture-neutral format.</summary>
	/// <param name="text">The text to convert.</param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
	/// <param name="cultureInvariant">
	///       <see langword="true" /> to convert values to a culture-neutral format; otherwise, <see langword="false" />.</param>
	/// <param name="value">When this method returns, contains an object with the conversion result. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> if the conversion is successful; otherwise, <see langword="false" />.</returns>
	protected static bool Convert(string text, ValidationDataType type, bool cultureInvariant, out object value)
	{
		try
		{
			switch (type)
			{
			case ValidationDataType.String:
				value = text;
				return value != null;
			case ValidationDataType.Integer:
			{
				IFormatProvider provider3 = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
				value = int.Parse(text, provider3);
				return true;
			}
			case ValidationDataType.Double:
			{
				IFormatProvider provider2 = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
				value = double.Parse(text, provider2);
				return true;
			}
			case ValidationDataType.Date:
			{
				IFormatProvider provider4 = (cultureInvariant ? DateTimeFormatInfo.InvariantInfo : DateTimeFormatInfo.CurrentInfo);
				value = DateTime.Parse(text, provider4);
				return true;
			}
			case ValidationDataType.Currency:
			{
				IFormatProvider provider = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
				value = decimal.Parse(text, NumberStyles.Currency, provider);
				return true;
			}
			default:
				value = null;
				return false;
			}
		}
		catch
		{
			value = null;
			return false;
		}
	}
}
