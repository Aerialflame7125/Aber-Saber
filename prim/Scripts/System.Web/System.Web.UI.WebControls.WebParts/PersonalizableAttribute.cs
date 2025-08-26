using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Represents the personalization attribute. This class cannot be inherited.</summary>
/// <exception cref="T:System.Web.HttpException">The property is a read-only or write-only public property.- or -The property is a private or protected read/write property.- or -The property has index parameters.</exception>
[AttributeUsage(AttributeTargets.Property)]
public sealed class PersonalizableAttribute : Attribute
{
	/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
	public static readonly PersonalizableAttribute Default;

	/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
	public static readonly PersonalizableAttribute NotPersonalizable;

	/// <summary>Returns an attribute instance that indicates support for personalization. This field is read-only.</summary>
	public static readonly PersonalizableAttribute Personalizable;

	/// <summary>Returns an attribute instance that indicates support for personalization with a shared scope. This field is read-only.</summary>
	public static readonly PersonalizableAttribute SharedPersonalizable;

	/// <summary>Returns an attribute instance that indicates support for personalization in <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope. This field is read-only.</summary>
	public static readonly PersonalizableAttribute UserPersonalizable;

	private bool isPersonalizable;

	private bool isSensitive;

	private PersonalizationScope scope;

	/// <summary>Gets the setting that indicates whether the attribute can be personalized, as established by one of the constructors.</summary>
	/// <returns>
	///     <see langword="true" /> if the property can be personalized; otherwise, <see langword="false" />.</returns>
	public bool IsPersonalizable => isPersonalizable;

	/// <summary>Gets the setting that indicates whether the attribute is sensitive, as established by one of the constructors.</summary>
	/// <returns>
	///     <see langword="true" /> if the property is sensitive; otherwise, <see langword="false" />.</returns>
	public bool IsSensitive => isSensitive;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value for the class instance, as set by one of the constructors.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value.</returns>
	public PersonalizationScope Scope => scope;

	static PersonalizableAttribute()
	{
		Default = new PersonalizableAttribute(isPersonalizable: false);
		NotPersonalizable = Default;
		Personalizable = new PersonalizableAttribute(PersonalizationScope.User, isSensitive: false);
		SharedPersonalizable = new PersonalizableAttribute(PersonalizationScope.Shared, isSensitive: false);
		UserPersonalizable = new PersonalizableAttribute(PersonalizationScope.User, isSensitive: false);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class. </summary>
	public PersonalizableAttribute()
		: this(isPersonalizable: true)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
	/// <param name="isPersonalizable">A Boolean value indicating whether the property can be personalized.</param>
	public PersonalizableAttribute(bool isPersonalizable)
	{
		this.isPersonalizable = isPersonalizable;
		scope = PersonalizationScope.User;
		isSensitive = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
	/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
	public PersonalizableAttribute(PersonalizationScope scope)
		: this(scope, isSensitive: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameters.</summary>
	/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
	/// <param name="isSensitive">A Boolean value indicating whether the property information is considered sensitive.</param>
	public PersonalizableAttribute(PersonalizationScope scope, bool isSensitive)
	{
		isPersonalizable = true;
		this.scope = scope;
		this.isSensitive = isSensitive;
	}

	/// <summary>When overridden, returns a Boolean evaluation of the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and another <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> instance supplied as a parameter.</summary>
	/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
	/// <returns>
	///     <see langword="true" /> if the values are equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is PersonalizableAttribute personalizableAttribute))
		{
			return false;
		}
		if (isPersonalizable == personalizableAttribute.IsPersonalizable && isSensitive == personalizableAttribute.IsSensitive)
		{
			return scope == personalizableAttribute.Scope;
		}
		return false;
	}

	/// <summary>When overridden, returns a hash code of the attribute.</summary>
	/// <returns>A hash code in the form of an integer.</returns>
	public override int GetHashCode()
	{
		return isPersonalizable.GetHashCode() ^ isSensitive.GetHashCode() ^ scope.GetHashCode();
	}

	/// <summary>Returns a collection of <see cref="T:System.Reflection.PropertyInfo" /> objects for the properties that match the parameter type and are marked as personalizable.</summary>
	/// <param name="type">The type on which to look for <see langword="Personalizable" /> properties.</param>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of personalizable properties.</returns>
	/// <exception cref="T:System.Web.HttpException">A public property on the type is marked as personalizable but is read-only.</exception>
	public static ICollection GetPersonalizableProperties(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		PropertyInfo[] properties = type.GetProperties();
		if (properties == null || properties.Length == 0)
		{
			return new PropertyInfo[0];
		}
		List<PropertyInfo> list = null;
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			if (PropertyQualifies(propertyInfo))
			{
				if (list == null)
				{
					list = new List<PropertyInfo>();
				}
				list.Add(propertyInfo);
			}
		}
		return list;
	}

	private static bool PropertyQualifies(PropertyInfo pi)
	{
		object[] customAttributes = pi.GetCustomAttributes(inherit: false);
		if (customAttributes == null || customAttributes.Length == 0)
		{
			return false;
		}
		object[] array = customAttributes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is PersonalizableAttribute { IsPersonalizable: not false })
			{
				if (pi.GetSetMethod(nonPublic: false) == null)
				{
					throw new HttpException("A public property on the type is marked as personalizable but is read-only.");
				}
				return true;
			}
		}
		return false;
	}

	/// <summary>When overridden, returns a value that indicates whether the attribute instance equals the value of the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> if the attribute instance equals the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return object.Equals(this, Default);
	}

	/// <summary>Returns a value that indicates whether the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and the specified <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> property value.</summary>
	/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
	/// <returns>
	///     <see langword="true" /> if the two attributes have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> value; otherwise, <see langword="false" />.</returns>
	public override bool Match(object obj)
	{
		PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
		if (obj == null)
		{
			return false;
		}
		return isPersonalizable == personalizableAttribute.IsPersonalizable;
	}
}
