namespace System.Web.UI;

/// <summary>Stores ASP.NET page view state on the Web server.</summary>
public class SessionPageStatePersister : PageStatePersister
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SessionPageStatePersister" /> class.</summary>
	/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.SessionState.HttpSessionState" /> is null (<see langword="Nothing" /> in Visual Basic)</exception>
	public SessionPageStatePersister(Page page)
		: base(page)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deserializes and loads persisted state from the server-side session object when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
	/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.SessionPageStatePersister.Load" /> method could not successfully deserialize the state contained in the request to the Web server.</exception>
	public override void Load()
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes any object state contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> or the <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> property and writes the state to the session object.</summary>
	public override void Save()
	{
		throw new NotImplementedException();
	}
}
