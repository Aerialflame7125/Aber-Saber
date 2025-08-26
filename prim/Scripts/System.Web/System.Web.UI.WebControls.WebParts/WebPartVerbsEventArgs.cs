namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides event data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartZoneBase.CreateVerbs" /> event that is used by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartZoneBase.OnCreateVerbs(System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs)" /> method.</summary>
public class WebPartVerbsEventArgs : EventArgs
{
	private WebPartVerbCollection _verbs;

	/// <summary>Gets or sets the Web Parts verbs used in the event data.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" />.</returns>
	public WebPartVerbCollection Verbs
	{
		get
		{
			if (_verbs == null)
			{
				return WebPartVerbCollection.Empty;
			}
			return _verbs;
		}
		set
		{
			_verbs = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class.</summary>
	public WebPartVerbsEventArgs()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class using the specified Web Parts verb collection.</summary>
	/// <param name="verbs">A Web Parts verb collection.</param>
	public WebPartVerbsEventArgs(WebPartVerbCollection verbs)
	{
		_verbs = verbs;
	}
}
