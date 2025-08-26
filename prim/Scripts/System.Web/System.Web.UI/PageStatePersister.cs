namespace System.Web.UI;

/// <summary>Provides the base functionality for ASP.NET view state persistence mechanisms.</summary>
public abstract class PageStatePersister
{
	private object control_state;

	private object view_state;

	private Page page;

	private IStateFormatter state_formatter;

	/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
	/// <returns>An object that contains view state data.</returns>
	public object ControlState
	{
		get
		{
			return control_state;
		}
		set
		{
			control_state = value;
		}
	}

	/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
	/// <returns>An object that contains view state data.</returns>
	public object ViewState
	{
		get
		{
			return view_state;
		}
		set
		{
			view_state = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.Page" /> object that the view state persistence mechanism is created for.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Page" /> that the <see cref="T:System.Web.UI.PageStatePersister" /> is associated with.</returns>
	protected Page Page
	{
		get
		{
			return page;
		}
		set
		{
			page = value;
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.UI.IStateFormatter" /> object that is used to serialize and deserialize the state information contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> and <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> properties during calls to the <see cref="M:System.Web.UI.PageStatePersister.Save" /> and <see cref="M:System.Web.UI.PageStatePersister.Load" /> methods.</summary>
	/// <returns>An instance of <see cref="T:System.Web.UI.IStateFormatter" /> that is used to serialize and deserialize object state.</returns>
	protected IStateFormatter StateFormatter
	{
		get
		{
			if (state_formatter == null)
			{
				state_formatter = page.GetFormatter();
			}
			return state_formatter;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageStatePersister" /> class.</summary>
	/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="page" /> parameter is <see langword="null" />.</exception>
	protected PageStatePersister(Page page)
	{
		if (page == null)
		{
			throw new ArgumentNullException("page");
		}
		this.page = page;
	}

	/// <summary>Overridden by derived classes to deserialize and load persisted state information when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
	public abstract void Load();

	/// <summary>Overridden by derived classes to serialize persisted state information when a <see cref="T:System.Web.UI.Page" /> object is unloaded from memory.</summary>
	public abstract void Save();
}
