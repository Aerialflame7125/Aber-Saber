namespace System.Windows.Forms;

/// <summary>Contains information that enables a <see cref="T:System.Windows.Forms.Binding" /> to resolve a data binding to either the property of an object or the property of the current object in a list of objects.</summary>
/// <filterpriority>2</filterpriority>
public struct BindingMemberInfo
{
	private string data_member;

	private string data_field;

	private string data_path;

	/// <summary>Gets the property name of the data-bound object.</summary>
	/// <returns>The property name of the data-bound object. This can be an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	public string BindingField => data_field;

	/// <summary>Gets the information that is used to specify the property name of the data-bound object.</summary>
	/// <returns>An empty string (""), a single property name, or a hierarchy of period-delimited property names that resolves to the property name of the final data-bound object.</returns>
	/// <filterpriority>1</filterpriority>
	public string BindingMember => data_member;

	/// <summary>Gets the property name, or the period-delimited hierarchy of property names, that comes before the property name of the data-bound object.</summary>
	/// <returns>The property name, or the period-delimited hierarchy of property names, that comes before the data-bound object property name.</returns>
	/// <filterpriority>1</filterpriority>
	public string BindingPath => data_path;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingMemberInfo" /> class.</summary>
	/// <param name="dataMember">A navigation path that resolves to either the property of an object or the property of the current object in a list of objects. </param>
	public BindingMemberInfo(string dataMember)
	{
		if (dataMember != null)
		{
			data_member = dataMember;
		}
		else
		{
			data_member = string.Empty;
		}
		int num = data_member.LastIndexOf('.');
		if (num != -1)
		{
			data_field = data_member.Substring(num + 1);
			data_path = data_member.Substring(0, num);
		}
		else
		{
			data_field = data_member;
			data_path = string.Empty;
		}
	}

	/// <summary>Determines whether the specified object is equal to this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
	/// <returns>true if <paramref name="otherObject" /> is a <see cref="T:System.Windows.Forms.BindingMemberInfo" /> and both <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings are equal; otherwise false.</returns>
	/// <param name="otherObject">The object to compare for equality.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object otherObject)
	{
		if (otherObject is BindingMemberInfo)
		{
			return data_field == ((BindingMemberInfo)otherObject).data_field && data_path == ((BindingMemberInfo)otherObject).data_path && data_member == ((BindingMemberInfo)otherObject).data_member;
		}
		return false;
	}

	/// <summary>Returns the hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
	/// <returns>The hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return data_member.GetHashCode();
	}

	/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are equal.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are equal; otherwise false.</returns>
	/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
	/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
	public static bool operator ==(BindingMemberInfo a, BindingMemberInfo b)
	{
		return a.Equals(b);
	}

	/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are not equal.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise false.</returns>
	/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
	/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
	public static bool operator !=(BindingMemberInfo a, BindingMemberInfo b)
	{
		return !a.Equals(b);
	}
}
