namespace System.Web.Configuration;

/// <summary>Used to determine session-state activation for a single Web page or an entire Web application.</summary>
public enum PagesEnableSessionState
{
	/// <summary>Session state is disabled.</summary>
	False,
	/// <summary>Session state is enabled, but not writable.</summary>
	ReadOnly,
	/// <summary>Session state is enabled.</summary>
	True
}
