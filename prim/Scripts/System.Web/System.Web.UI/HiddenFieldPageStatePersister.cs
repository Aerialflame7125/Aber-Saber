using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Stores ASP.NET page view state on the Web client in a hidden HTML element. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HiddenFieldPageStatePersister : PageStatePersister
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HiddenFieldPageStatePersister" /> class.</summary>
	/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
	public HiddenFieldPageStatePersister(Page page)
		: base(page)
	{
	}

	/// <summary>Deserializes and loads persisted state information from an <see cref="T:System.Web.HttpRequest" /> object when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
	/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.HiddenFieldPageStatePersister.Load" /> method could not successfully deserialize the state information contained in the request to the Web server.</exception>
	public override void Load()
	{
		string rawViewState = base.Page.RawViewState;
		IStateFormatter stateFormatter = base.StateFormatter;
		if (!string.IsNullOrEmpty(rawViewState) && stateFormatter.Deserialize(rawViewState) is Pair pair)
		{
			base.ViewState = pair.First;
			base.ControlState = pair.Second;
		}
	}

	/// <summary>Serializes any object state contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> or <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> property and writes the state to the response stream.</summary>
	public override void Save()
	{
		IStateFormatter stateFormatter = base.StateFormatter;
		base.Page.RawViewState = stateFormatter.Serialize(new Pair(base.ViewState, base.ControlState));
	}
}
