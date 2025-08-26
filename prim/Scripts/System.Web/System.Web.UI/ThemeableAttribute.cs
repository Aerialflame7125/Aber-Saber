using System.Collections;

namespace System.Web.UI;

/// <summary>Defines the metadata attribute that Web server controls and their members use to indicate whether their rendering can be affected by themes and control skins. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public sealed class ThemeableAttribute : Attribute
{
	/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is affected by themes and control skins.</summary>
	public static readonly ThemeableAttribute Yes;

	/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is not affected by themes and control skins.</summary>
	public static readonly ThemeableAttribute No;

	/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance that represents the application-defined default value of the attribute.</summary>
	public static readonly ThemeableAttribute Default;

	private bool _themeable;

	private static Hashtable _themeableTypes;

	/// <summary>Gets a value indicating whether the current control or member of a control can be affected by themes and control skins defined for the Web application.</summary>
	/// <returns>
	///     <see langword="true" /> if the current type or member can be affected by themes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Themeable => _themeable;

	static ThemeableAttribute()
	{
		Yes = new ThemeableAttribute(themeable: true);
		No = new ThemeableAttribute(themeable: false);
		Default = Yes;
		_themeableTypes = Hashtable.Synchronized(new Hashtable());
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a type or member that is affected by themes and control skins.</summary>
	/// <param name="themeable">
	///       <see langword="true" /> to initialize the <see cref="T:System.Web.UI.ThemeableAttribute" /> to represent a type or member that can be affected by themes; otherwise, <see langword="false" />.</param>
	public ThemeableAttribute(bool themeable)
	{
		_themeable = themeable;
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see langword="object" /> to compare with this instance, or <see langword="null" />.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> is the same instance as the current instance, or if the instances are different, but the attribute values are equivalent; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is ThemeableAttribute themeableAttribute)
		{
			return themeableAttribute.Themeable == _themeable;
		}
		return false;
	}

	/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ThemeableAttribute" /> type. </summary>
	/// <returns>A hash code for the current <see cref="T:System.Web.UI.ThemeableAttribute" />.</returns>
	public override int GetHashCode()
	{
		return _themeable.GetHashCode();
	}

	/// <summary>Gets a value indicating whether the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class.</summary>
	/// <returns>
	///     <see langword="true" /> if the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the class; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}

	/// <summary>Returns a value indicating whether the object passed to the method supports themes. </summary>
	/// <param name="instance">The object to test for themes support.</param>
	/// <returns>
	///     <see langword="true" /> if the object supports themes and control skins; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is <see langword="null" />.</exception>
	public static bool IsObjectThemeable(object instance)
	{
		if (instance == null)
		{
			throw new ArgumentNullException("instance");
		}
		return IsTypeThemeable(instance.GetType());
	}

	/// <summary>Returns a value indicating whether the <see cref="T:System.Type" /> passed to the method supports themes.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> to test for themes support.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Type" /> supports themes and control skins; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
	public static bool IsTypeThemeable(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		object obj = _themeableTypes[type];
		if (obj != null)
		{
			return (bool)obj;
		}
		obj = Attribute.GetCustomAttribute(type, typeof(ThemeableAttribute)) is ThemeableAttribute themeableAttribute && themeableAttribute.Themeable;
		_themeableTypes[type] = obj;
		return (bool)obj;
	}
}
