namespace System.Web.UI;

/// <summary>Defines the attribute that controls use to identify string properties containing URL values. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class UrlPropertyAttribute : Attribute
{
	private string filter;

	/// <summary>Gets a file filter associated with the URL-specific property. </summary>
	/// <returns>A file filter associated with the URL-specific property. The default is "*.*".</returns>
	public string Filter => filter;

	/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class.</summary>
	public UrlPropertyAttribute()
		: this("*.*")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class, setting the <see cref="P:System.Web.UI.UrlPropertyAttribute.Filter" /> property to the specified string.</summary>
	/// <param name="filter">A file filter associated with the URL-specific property.</param>
	public UrlPropertyAttribute(string filter)
	{
		this.filter = filter;
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is UrlPropertyAttribute urlPropertyAttribute))
		{
			return false;
		}
		return filter.Equals(urlPropertyAttribute.Filter);
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return filter.GetHashCode();
	}
}
