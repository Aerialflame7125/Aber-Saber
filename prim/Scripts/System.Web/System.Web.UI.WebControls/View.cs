using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a control that acts as a container for a group of controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
[ParseChildren(false)]
[Designer("System.Web.UI.Design.WebControls.ViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<{0}:View runat=\"server\"></{0}:View>")]
public class View : Control
{
	private static readonly object ActivateEvent;

	private static readonly object DeactivateEvent;

	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(true)]
	public override bool EnableTheming
	{
		get
		{
			return base.EnableTheming;
		}
		set
		{
			base.EnableTheming = value;
		}
	}

	internal bool VisibleInternal
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.View" /> control is visible. </summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.View" /> control is visible; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property at run time.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			throw new InvalidOperationException("The Visible property of a View control can only be set by setting the active View of a MultiView.");
		}
	}

	/// <summary>Occurs when the current <see cref="T:System.Web.UI.WebControls.View" /> control becomes the active view.</summary>
	public event EventHandler Activate
	{
		add
		{
			base.Events.AddHandler(ActivateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ActivateEvent, value);
		}
	}

	/// <summary>Occurs when the current active <see cref="T:System.Web.UI.WebControls.View" /> control becomes inactive.</summary>
	public event EventHandler Deactivate
	{
		add
		{
			base.Events.AddHandler(DeactivateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DeactivateEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.View" /> class. </summary>
	public View()
	{
		base.Visible = false;
	}

	internal void NotifyActivation(bool activated)
	{
		if (activated)
		{
			OnActivate(EventArgs.Empty);
		}
		else
		{
			OnDeactivate(EventArgs.Empty);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Activate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnActivate(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[Activate])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Deactivate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDeactivate(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[Deactivate])?.Invoke(this, e);
		}
	}

	static View()
	{
		Activate = new object();
		Deactivate = new object();
	}
}
