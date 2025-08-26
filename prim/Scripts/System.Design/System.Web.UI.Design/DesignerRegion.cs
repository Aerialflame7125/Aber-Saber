using System.Drawing;

namespace System.Web.UI.Design;

/// <summary>Defines a region of content within the design-time markup for the associated control.</summary>
public class DesignerRegion : DesignerObject
{
	/// <summary>Defines the HTML attribute name for a designer region.</summary>
	public static readonly string DesignerRegionAttributeName;

	/// <summary>Gets or sets the description for a designer region.</summary>
	/// <returns>A text description of the designer region. The default is an empty string ("").</returns>
	[System.MonoNotSupported("")]
	public virtual string Description
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the friendly display name for a designer region.</summary>
	/// <returns>A text display name for the designer region. The default is an empty string ("").</returns>
	[System.MonoNotSupported("")]
	public virtual string DisplayName
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the region size is to be explicitly set on the designer region by the design host.</summary>
	/// <returns>
	///   <see langword="true" />, if the design host should set the size on the designer region; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool EnsureSize
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether to highlight the designer region on the design surface.</summary>
	/// <returns>
	///   <see langword="true" />, if the visual designer should highlight the designer region on the design surface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool Highlight
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the designer region can be selected by the user on the design surface.</summary>
	/// <returns>
	///   <see langword="true" />, if the designer region can be selected by the user on the design surface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool Selectable
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the designer region is currently selected on the design surface.</summary>
	/// <returns>
	///   <see langword="true" />, if the designer region is currently selected on the design surface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool Selected
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets optional user data to associate with the designer region.</summary>
	/// <returns>An object that contains user data stored for the designer region. The default is <see langword="null" />.</returns>
	[System.MonoNotSupported("")]
	public object UserData
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> class with the specified name for a control designer.</summary>
	/// <param name="designer">The control designer that contains this designer region.</param>
	/// <param name="name">The name of this designer region.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="designer" /> is an empty string ("").  
	/// -or-  
	/// <paramref name="name" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="name" /> is an empty string ("").</exception>
	[System.MonoNotSupported("")]
	public DesignerRegion(ControlDesigner designer, string name)
		: this(designer, name, selectable: false)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> class with the specified name for a control designer, optionally setting the instance as a selectable region in the designer.</summary>
	/// <param name="designer">The control designer that contains this designer region.</param>
	/// <param name="name">The name of this designer region.</param>
	/// <param name="selectable">
	///   <see langword="true" /> to select the region; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="designer" /> is an empty string ("").  
	/// -or-  
	/// <paramref name="name" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="name" /> is an empty string ("").</exception>
	[System.MonoNotSupported("")]
	public DesignerRegion(ControlDesigner designer, string name, bool selectable)
		: base(designer, name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the size of the designer region on the design surface.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the designer region size on the design surface.</returns>
	[System.MonoNotSupported("")]
	public Rectangle GetBounds()
	{
		throw new NotImplementedException();
	}
}
