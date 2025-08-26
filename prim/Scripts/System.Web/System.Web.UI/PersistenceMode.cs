namespace System.Web.UI;

/// <summary>Specifies how an ASP.NET server control property or event is persisted declaratively in an .aspx or .ascx file.</summary>
public enum PersistenceMode
{
	/// <summary>Specifies that the property or event persists as an attribute.</summary>
	Attribute,
	/// <summary>Specifies that the property persists in the ASP.NET server control as a nested tag. This is commonly used for complex objects, those that have persistable properties of their own.</summary>
	InnerProperty,
	/// <summary>Specifies that the property persists in the ASP.NET server control as inner text. Also indicates that this property is defined as the element's default property. Only one property can be designated the default property.</summary>
	InnerDefaultProperty,
	/// <summary>Specifies that the property persists as the only inner text of the ASP.NET server control. The property value is HTML encoded. Only a string can be given this designation.</summary>
	EncodedInnerDefaultProperty
}
