using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Defines the metadata attribute that specifies how an ASP.NET server control property or event is persisted to an ASP.NET page at design time. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.All)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PersistenceModeAttribute : Attribute
{
	private PersistenceMode mode;

	/// <summary>Specifies that the property or event persists in the opening tag of the server control as an attribute. This field is read-only.</summary>
	public static readonly PersistenceModeAttribute Attribute = new PersistenceModeAttribute(PersistenceMode.Attribute);

	/// <summary>Specifies the default type for the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> class. The default is <see langword="PersistenceMode.Attribute" />. This field is read-only.</summary>
	public static readonly PersistenceModeAttribute Default = new PersistenceModeAttribute(PersistenceMode.Attribute);

	/// <summary>Specifies that a property is HTML-encoded and persists as the only inner content of the ASP.NET server control. This field is read-only.</summary>
	public static readonly PersistenceModeAttribute EncodedInnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.EncodedInnerDefaultProperty);

	/// <summary>Specifies that a property persists as the only inner content of the ASP.NET server control. This field is read-only.</summary>
	public static readonly PersistenceModeAttribute InnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.InnerDefaultProperty);

	/// <summary>Specifies that the property persists as a nested tag within the opening and closing tags of the server control. This field is read-only.</summary>
	public static readonly PersistenceModeAttribute InnerProperty = new PersistenceModeAttribute(PersistenceMode.InnerProperty);

	/// <summary>Gets the current value of the <see cref="T:System.Web.UI.PersistenceMode" /> enumeration.</summary>
	/// <returns>A <see cref="T:System.Web.UI.PersistenceMode" /> that represents the current value of the enumeration. This value can be <see langword="Attribute" />, <see langword="InnerProperty" />, <see langword="InnerDefaultProperty" />, or <see langword="EncodedInnerDefaultProperty" />. The default is <see langword="Attribute" />.</returns>
	public PersistenceMode Mode => mode;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> class. </summary>
	/// <param name="mode">The <see cref="T:System.Web.UI.PersistenceMode" /> value to assign to <see cref="P:System.Web.UI.PersistenceModeAttribute.Mode" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="mode" /> is not one of the <see cref="T:System.Web.UI.PersistenceMode" /> values.</exception>
	public PersistenceModeAttribute(PersistenceMode mode)
	{
		this.mode = mode;
	}

	/// <summary>Compares the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> object against another object.</summary>
	/// <param name="obj">The object to compare to.</param>
	/// <returns>
	///     <see langword="true" /> if the objects are considered equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is PersistenceModeAttribute persistenceModeAttribute))
		{
			return false;
		}
		return persistenceModeAttribute.mode == mode;
	}

	/// <summary>Provides a hash value for a <see cref="T:System.Web.UI.PersistenceModeAttribute" /> attribute.</summary>
	/// <returns>The hash value to be assigned to the <see cref="T:System.Web.UI.PersistenceModeAttribute" />.</returns>
	public override int GetHashCode()
	{
		return (int)mode;
	}

	/// <summary>Indicates whether the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> object is of the default type.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> is of the default type; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return mode == PersistenceMode.Attribute;
	}
}
