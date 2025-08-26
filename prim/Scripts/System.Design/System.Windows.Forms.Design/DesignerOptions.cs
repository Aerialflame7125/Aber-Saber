using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design;

/// <summary>Provides access to get and set option values for a designer.</summary>
public class DesignerOptions
{
	/// <summary>Gets or sets a value that enables or disables in-place editing for <see cref="T:System.Windows.Forms.ToolStrip" /> controls.</summary>
	/// <returns>
	///   <see langword="true" /> if in-place editing for <see cref="T:System.Windows.Forms.ToolStrip" /> controls is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoTODO]
	[Browsable(false)]
	public virtual bool EnableInSituEditing
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

	/// <summary>Gets or sets a <see cref="T:System.Drawing.Size" /> representing the dimensions of a grid unit.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the dimensions of a grid unit.</returns>
	[System.MonoTODO]
	public virtual Size GridSize
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

	/// <summary>Gets or sets a value that specifies whether a designer shows a component's smart tag panel automatically on creation.</summary>
	/// <returns>
	///   <see langword="true" /> to allow a component's smart tag panel to open automatically upon creation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoTODO]
	public virtual bool ObjectBoundSmartTagAutoShow
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

	/// <summary>Gets or sets a value that enables or disables the grid in the designer.</summary>
	/// <returns>
	///   <see langword="true" /> if the grid is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoTODO]
	public virtual bool ShowGrid
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

	/// <summary>Gets or sets a value that enables or disables whether controls are automatically placed at grid coordinates.</summary>
	/// <returns>
	///   <see langword="true" /> if snapping is enabled; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool SnapToGrid
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

	/// <summary>Gets or sets a value that enables or disables the component cache.</summary>
	/// <returns>
	///   <see langword="true" /> if the component cache is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoTODO]
	public virtual bool UseOptimizedCodeGeneration
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

	/// <summary>Gets or sets a value that enables or disables smart tags in the designer.</summary>
	/// <returns>
	///   <see langword="true" /> if smart tags in the designer are enabled; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool UseSmartTags
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

	/// <summary>Gets or sets a value that enables or disables snaplines in the designer.</summary>
	/// <returns>
	///   <see langword="true" /> if snaplines in the designer are enabled; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool UseSnapLines
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.DesignerOptions" /> class.</summary>
	public DesignerOptions()
	{
	}
}
