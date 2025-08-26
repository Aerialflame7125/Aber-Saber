namespace System.Web.UI.Design;

/// <summary>Provides the abstract base class for creating formats that can be applied to a custom Web server control at design time.</summary>
public abstract class DesignerAutoFormat
{
	/// <summary>Gets the name of a <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> name.</returns>
	[System.MonoTODO]
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.Design.DesignerAutoFormatStyle" /> object that is used by the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object to render a design-time preview of the associated control.</summary>
	/// <returns>An  object that is used by the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object to render a design-time preview of the associated control.</returns>
	[System.MonoTODO]
	public DesignerAutoFormatStyle Style
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> class.</summary>
	/// <param name="name">A string that identifies a specific <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="name" /> is <see langword="null" />.</exception>
	protected DesignerAutoFormat(string name)
	{
	}

	/// <summary>Applies the associated formatting to the specified control.</summary>
	/// <param name="control">A Web server control to apply the formatting to.</param>
	public abstract void Apply(Control control);

	/// <summary>Returns a copy of the associated control in order to provide a preview before applying the format to the control.</summary>
	/// <param name="runtimeControl">A run-time version of the Web server control.</param>
	/// <returns>The <see cref="M:System.Web.UI.Design.DesignerAutoFormat.GetPreviewControl(System.Web.UI.Control)" /> method returns a copy of the associated Web server control.</returns>
	[System.MonoTODO]
	public virtual Control GetPreviewControl(Control runtimeControl)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object.</summary>
	/// <returns>The <see cref="P:System.Web.UI.Design.DesignerAutoFormat.Name" /> property of the current <see cref="T:System.Web.UI.Design.DesignerAutoFormat" />.</returns>
	[System.MonoTODO]
	public override string ToString()
	{
		return base.ToString();
	}
}
