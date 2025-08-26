namespace System.Web.UI.WebControls.WebParts;

/// <summary>Serves as the base class for all Web Parts part controls, which render a modular user interface on a Web Forms page. </summary>
public abstract class Part : Panel, INamingContainer, ICompositeControlDesignerAccessor
{
	private string description;

	private string title;

	private PartChromeState chrome_state;

	private PartChromeType chrome_type;

	private ControlCollection controls;

	/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
	public virtual PartChromeState ChromeState
	{
		get
		{
			return chrome_state;
		}
		set
		{
			chrome_state = value;
		}
	}

	/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
	public virtual PartChromeType ChromeType
	{
		get
		{
			return chrome_type;
		}
		set
		{
			chrome_type = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for a specified server control in the user interface hierarchy.</summary>
	/// <returns>The collection of child controls for the specified server control.</returns>
	public new virtual ControlCollection Controls
	{
		get
		{
			if (controls == null)
			{
				controls = new ControlCollection(this);
			}
			return controls;
		}
	}

	/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
	/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
	public virtual string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}

	/// <summary>Gets or sets the title of a part control.</summary>
	/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
	public virtual string Title
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	internal Part()
	{
		description = "";
		title = "";
		chrome_state = PartChromeState.Normal;
		chrome_type = PartChromeType.Default;
	}

	/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
	[MonoTODO("Not implemented")]
	public override void DataBind()
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows the developer of a designer for a composite part control to recreate the control's child controls on the design surface.</summary>
	[MonoTODO("not sure exactly what this one does..")]
	void ICompositeControlDesignerAccessor.RecreateChildControls()
	{
		CreateChildControls();
	}
}
