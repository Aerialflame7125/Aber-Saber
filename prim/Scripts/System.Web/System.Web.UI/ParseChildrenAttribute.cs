using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Defines a metadata attribute that you can use when developing ASP.NET server controls. Use the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class to indicate how the page parser should treat content nested inside a server control tag declared on a page. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ParseChildrenAttribute : Attribute
{
	private bool childrenAsProperties;

	private string defaultProperty;

	/// <summary>Defines the default value for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class. This field is read-only.</summary>
	public static readonly ParseChildrenAttribute Default = new ParseChildrenAttribute();

	/// <summary>Indicates that the nested content that is contained within the server control is parsed as controls.</summary>
	public static readonly ParseChildrenAttribute ParseAsChildren = new ParseChildrenAttribute(childrenAsProperties: false);

	/// <summary>Indicates that the nested content that is contained within a server control is parsed as properties of the control. </summary>
	public static readonly ParseChildrenAttribute ParseAsProperties = new ParseChildrenAttribute(childrenAsProperties: true);

	private Type childType = typeof(Control);

	/// <summary>Gets or sets a value indicating whether to parse the elements that are contained within a server control as properties.</summary>
	/// <returns>
	///     <see langword="true" /> to parse the elements as properties; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to <see langword="false" />.</exception>
	public bool ChildrenAsProperties
	{
		get
		{
			return childrenAsProperties;
		}
		set
		{
			childrenAsProperties = value;
		}
	}

	/// <summary>Gets or sets the default property for the server control into which the elements are parsed.</summary>
	/// <returns>The name of the default collection property of the server control into which the elements are parsed.</returns>
	/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to <see langword="false" />.</exception>
	public string DefaultProperty
	{
		get
		{
			return defaultProperty;
		}
		set
		{
			defaultProperty = value;
		}
	}

	/// <summary>Gets a value indicating the allowed type of a control. </summary>
	/// <returns>The control type. The default is <see cref="T:System.Web.UI.Control" />. </returns>
	public Type ChildControlType => childType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class.</summary>
	public ParseChildrenAttribute()
	{
		childrenAsProperties = false;
		defaultProperty = "";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildrenAsProperties" /> property to determine if the elements that are contained within a server control are parsed as properties of the server control.</summary>
	/// <param name="childrenAsProperties">
	///       <see langword="true" /> to parse the elements as properties of the server control; otherwise, <see langword="false" />. </param>
	public ParseChildrenAttribute(bool childrenAsProperties)
	{
		this.childrenAsProperties = childrenAsProperties;
		defaultProperty = "";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <paramref name="childrenAsProperties" /> and <paramref name="defaultProperty" /> parameters.</summary>
	/// <param name="childrenAsProperties">
	///       <see langword="true" /> to parse the elements as properties of the server control; otherwise, <see langword="false" />. </param>
	/// <param name="defaultProperty">A string that defines a collection property of the server control into which nested content is parsed by default. </param>
	public ParseChildrenAttribute(bool childrenAsProperties, string defaultProperty)
	{
		this.childrenAsProperties = childrenAsProperties;
		if (childrenAsProperties)
		{
			this.defaultProperty = defaultProperty;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildControlType" /> property to determine which elements that are contained within a server control are parsed as controls.</summary>
	/// <param name="childControlType">The control type to parse as a property. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="childControlType" /> is <see langword="null" />. </exception>
	public ParseChildrenAttribute(Type childControlType)
	{
		childType = childControlType;
		defaultProperty = "";
	}

	/// <summary>Determines whether the specified object is equal to the current object.</summary>
	/// <param name="obj">The object to compare with the current object.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is ParseChildrenAttribute parseChildrenAttribute))
		{
			return false;
		}
		if (childrenAsProperties == parseChildrenAttribute.childrenAsProperties)
		{
			if (!childrenAsProperties)
			{
				return true;
			}
			return defaultProperty == parseChildrenAttribute.DefaultProperty;
		}
		return false;
	}

	/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object.</summary>
	/// <returns>A hash code for the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object. </returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Returns a value indicating whether the value of the current instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class is the default value of the derived class.</summary>
	/// <returns>
	///     <see langword="true" /> if the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> value is the default instance; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
