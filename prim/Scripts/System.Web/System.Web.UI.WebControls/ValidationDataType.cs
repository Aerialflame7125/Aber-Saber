namespace System.Web.UI.WebControls;

/// <summary>Specifies the validation data types used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> and <see cref="T:System.Web.UI.WebControls.RangeValidator" /> controls.</summary>
public enum ValidationDataType
{
	/// <summary>A string data type. The value is treated as a <see cref="T:System.String" />.</summary>
	String,
	/// <summary>A 32-bit signed integer data type. The value is treated as a <see cref="T:System.Int32" />.</summary>
	Integer,
	/// <summary>A double precision floating point number data type. The value is treated as a <see cref="T:System.Double" />.</summary>
	Double,
	/// <summary>A date data type. Only numeric dates are allowed. The time portion cannot be specified.</summary>
	Date,
	/// <summary>A monetary data type. The value is treated as a <see cref="T:System.Decimal" />. However, currency and grouping symbols are still allowed.</summary>
	Currency
}
