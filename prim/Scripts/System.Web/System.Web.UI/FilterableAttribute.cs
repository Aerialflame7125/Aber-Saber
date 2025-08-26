using System.Collections;
using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Specifies whether the property to which the attribute is applied supports device filtering. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public sealed class FilterableAttribute : Attribute
{
	/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property supports device filtering. This field is read-only.</summary>
	public static readonly FilterableAttribute Yes;

	/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property does not support device filtering. This field is read-only.</summary>
	public static readonly FilterableAttribute No;

	/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object with default property settings. This field is read-only.</summary>
	public static readonly FilterableAttribute Default;

	private bool _filterable;

	private static Hashtable _filterableTypes;

	/// <summary>Gets a value indicating whether the property to which the <see cref="T:System.Web.UI.FilterableAttribute" /> attribute is applied supports device filtering.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the property to which the attribute is applied supports device filtering; otherwise, <see langword="false" />.</returns>
	public bool Filterable => _filterable;

	static FilterableAttribute()
	{
		Yes = new FilterableAttribute(filterable: true);
		No = new FilterableAttribute(filterable: false);
		Default = Yes;
		_filterableTypes = Hashtable.Synchronized(new Hashtable());
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class.</summary>
	/// <param name="filterable">
	///       <see langword="true" /> to indicate that the property to which the attribute is applied supports device filtering; otherwise, <see langword="false" />.</param>
	public FilterableAttribute(bool filterable)
	{
		_filterable = filterable;
	}

	/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the specified object.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
	/// <returns>
	///     <see langword="true" /> if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is FilterableAttribute filterableAttribute)
		{
			return filterableAttribute.Filterable == _filterable;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return _filterable.GetHashCode();
	}

	/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the <see cref="F:System.Web.UI.FilterableAttribute.Default" /> attribute.</summary>
	/// <returns>
	///     <see langword="true" /> if the current instance of <see cref="T:System.Web.UI.FilterableAttribute" /> is equal to <see cref="F:System.Web.UI.FilterableAttribute.Default" />; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Object" /> supports device filtering.</summary>
	/// <param name="instance">The <see cref="T:System.Object" /> to test.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Object" /> contained in the <paramref name="instance" /> parameter supports device filtering; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is <see langword="null" />.</exception>
	public static bool IsObjectFilterable(object instance)
	{
		if (instance == null)
		{
			throw new ArgumentNullException("instance");
		}
		return IsTypeFilterable(instance.GetType());
	}

	/// <summary>Determines whether a property supports device filtering.</summary>
	/// <param name="propertyDescriptor">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the properties of the property to test.</param>
	/// <returns>
	///     <see langword="true" /> if the property represented by the <see cref="T:System.ComponentModel.PropertyDescriptor" /> object contained in the <paramref name="propertyDescriptor" /> parameter supports device filtering; otherwise, <see langword="false" />.</returns>
	public static bool IsPropertyFilterable(PropertyDescriptor propertyDescriptor)
	{
		return ((FilterableAttribute)propertyDescriptor.Attributes[typeof(FilterableAttribute)])?.Filterable ?? true;
	}

	/// <summary>Determines whether the specified data type supports device filtering.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to test.</param>
	/// <returns>
	///     <see langword="true" /> if the data type contained in the <paramref name="type" /> parameter supports device filtering; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
	public static bool IsTypeFilterable(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		object obj = _filterableTypes[type];
		if (obj != null)
		{
			return (bool)obj;
		}
		obj = ((FilterableAttribute)TypeDescriptor.GetAttributes(type)[typeof(FilterableAttribute)])?.Filterable ?? false;
		_filterableTypes[type] = obj;
		return (bool)obj;
	}
}
