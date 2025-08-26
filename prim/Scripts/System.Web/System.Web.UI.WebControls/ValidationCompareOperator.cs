namespace System.Web.UI.WebControls;

/// <summary>Specifies the validation comparison operators used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> control.</summary>
public enum ValidationCompareOperator
{
	/// <summary>A comparison for equality.</summary>
	Equal,
	/// <summary>A comparison for inequality.</summary>
	NotEqual,
	/// <summary>A comparison for greater than.</summary>
	GreaterThan,
	/// <summary>A comparison for greater than or equal to.</summary>
	GreaterThanEqual,
	/// <summary>A comparison for less than.</summary>
	LessThan,
	/// <summary>A comparison for less than or equal to.</summary>
	LessThanEqual,
	/// <summary>A comparison for data type only.</summary>
	DataTypeCheck
}
