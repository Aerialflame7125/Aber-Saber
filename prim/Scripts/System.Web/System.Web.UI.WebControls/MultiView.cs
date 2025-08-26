using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a control that acts as a container for a group of <see cref="T:System.Web.UI.WebControls.View" /> controls.</summary>
[ControlBuilder(typeof(MultiViewControlBuilder))]
[Designer("System.Web.UI.Design.WebControls.MultiViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<{0}:MultiView runat=\"server\"></{0}:MultiView>")]
[ParseChildren(typeof(View))]
[DefaultEvent("ActiveViewChanged")]
public class MultiView : Control
{
	/// <summary>Represents the command name associated with the next <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
	public static readonly string NextViewCommandName = "NextView";

	/// <summary>Represents the command name associated with the previous <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
	public static readonly string PreviousViewCommandName = "PrevView";

	/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control, based on a specified <see cref="T:System.Web.UI.WebControls.View" /> id. This field is read-only.</summary>
	public static readonly string SwitchViewByIDCommandName = "SwitchViewByID";

	/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control based on a specified <see cref="T:System.Web.UI.WebControls.View" /> index. This field is read-only.</summary>
	public static readonly string SwitchViewByIndexCommandName = "SwitchViewByIndex";

	private static readonly object ActiveViewChangedEvent;

	private int viewIndex = -1;

	private int initialIndex = -1;

	/// <summary>Gets or sets the index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>The zero-based index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is -1, indicating that no view is set as active.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was set to less than -1, or greater than or equal to the number of items on the list. </exception>
	[DefaultValue(-1)]
	public virtual int ActiveViewIndex
	{
		get
		{
			if (Controls.Count == 0)
			{
				return initialIndex;
			}
			return viewIndex;
		}
		set
		{
			if (Controls.Count == 0)
			{
				initialIndex = value;
				return;
			}
			if (value < -1 || value >= Controls.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (viewIndex != -1)
			{
				((View)Controls[viewIndex]).NotifyActivation(activated: false);
			}
			viewIndex = value;
			if (viewIndex != -1)
			{
				((View)Controls[viewIndex]).NotifyActivation(activated: true);
			}
			UpdateViewVisibility();
			OnActiveViewChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a value indicating whether themes apply to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if themes are to be used; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(true)]
	public new virtual bool EnableTheming
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

	/// <summary>Gets the collection of <see cref="T:System.Web.UI.WebControls.View" /> controls in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> that represents a collection of <see cref="T:System.Web.UI.WebControls.View" /> controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is <see langword="null" />.</returns>
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[Browsable(false)]
	public virtual ViewCollection Views => Controls as ViewCollection;

	/// <summary>Occurs when the active <see cref="T:System.Web.UI.WebControls.View" /> control of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control changes between posts to the server.</summary>
	public event EventHandler ActiveViewChanged
	{
		add
		{
			base.Events.AddHandler(ActiveViewChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ActiveViewChangedEvent, value);
		}
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.MultiView" /> control that an XML or HTML element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> collection of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
	/// <exception cref="T:System.Web.HttpException">The specified <see cref="T:System.Object" /> is not a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (obj is View)
		{
			Controls.Add(obj as View);
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.ControlCollection" /> to hold the child controls of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> to contain the <see cref="T:System.Web.UI.WebControls.View" /> controls of the current <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new ViewCollection(this);
	}

	/// <summary>Returns the current active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.View" /> control that represents the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
	/// <exception cref="T:System.Exception">The <see cref="P:System.Web.UI.WebControls.MultiView.ActiveViewIndex" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.View" /> control within the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
	public View GetActiveView()
	{
		if (viewIndex < 0 || viewIndex >= Controls.Count)
		{
			throw new HttpException("The ActiveViewIndex is not set to a valid View control");
		}
		return Controls[viewIndex] as View;
	}

	/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="view">A <see cref="T:System.Web.UI.WebControls.View" /> control to set as the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
	/// <exception cref="T:System.Web.HttpException">The specified <paramref name="view" /> parameter value was not contained in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
	public void SetActiveView(View view)
	{
		int num = Controls.IndexOf(view);
		if (num == -1)
		{
			throw new HttpException("The provided view is not contained in the MultiView control.");
		}
		ActiveViewIndex = num;
	}

	/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.MultiView" /> control is passed to the page's UI server control hierarchy.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.MultiView" /> control cannot find the <see cref="T:System.Web.UI.WebControls.View" /> specified in the <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" />.</exception>
	/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" /> cannot be parsed as an integer.</exception>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs commandEventArgs)
		{
			switch (commandEventArgs.CommandName)
			{
			case "NextView":
				if (viewIndex < Controls.Count - 1 && Controls.Count > 0)
				{
					ActiveViewIndex = viewIndex + 1;
				}
				break;
			case "PrevView":
				if (viewIndex > 0)
				{
					ActiveViewIndex = viewIndex - 1;
				}
				break;
			case "SwitchViewByID":
				foreach (View control in Controls)
				{
					if (control.ID == (string)commandEventArgs.CommandArgument)
					{
						SetActiveView(control);
						break;
					}
				}
				break;
			case "SwitchViewByIndex":
			{
				int activeViewIndex = (int)Convert.ChangeType(commandEventArgs.CommandArgument, typeof(int));
				ActiveViewIndex = activeViewIndex;
				break;
			}
			}
		}
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		if (initialIndex != -1)
		{
			ActiveViewIndex = initialIndex;
			initialIndex = -1;
		}
		base.OnInit(e);
	}

	private void UpdateViewVisibility()
	{
		for (int i = 0; i < Views.Count; i++)
		{
			Views[i].VisibleInternal = i == viewIndex;
		}
	}

	/// <summary>Called after a <see cref="T:System.Web.UI.WebControls.View" /> control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="ctl">The <see cref="T:System.Web.UI.WebControls.View" /> control that has been removed. </param>
	protected internal override void RemovedControl(Control ctl)
	{
		if (viewIndex >= Controls.Count)
		{
			viewIndex = Controls.Count - 1;
			UpdateViewVisibility();
		}
		base.RemovedControl(ctl);
	}

	/// <summary>Loads the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
	protected internal override void LoadControlState(object state)
	{
		if (state != null)
		{
			viewIndex = (int)state;
			UpdateViewVisibility();
		}
		else
		{
			viewIndex = -1;
		}
	}

	/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. If there is no state associated with the <see cref="T:System.Web.UI.WebControls.MultiView" /> control, this method returns <see langword="null" />.</returns>
	protected internal override object SaveControlState()
	{
		if (viewIndex != -1)
		{
			return viewIndex;
		}
		return null;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.MultiView.ActiveViewChanged" /> event of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnActiveViewChanged(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[ActiveViewChanged])?.Invoke(this, e);
		}
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.MultiView" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (Controls.Count == 0 && initialIndex != -1)
		{
			viewIndex = initialIndex;
		}
		if (viewIndex != -1)
		{
			GetActiveView().Render(writer);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MultiView" /> class. </summary>
	public MultiView()
	{
	}

	static MultiView()
	{
		ActiveViewChanged = new object();
	}
}
