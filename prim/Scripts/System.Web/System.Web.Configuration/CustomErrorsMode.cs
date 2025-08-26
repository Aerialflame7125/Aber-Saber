namespace System.Web.Configuration;

/// <summary>Specifies the values for the custom errors modality.</summary>
public enum CustomErrorsMode
{
	/// <summary>Enables custom errors on remote clients only. Custom errors are shown only to remote clients and ASP.NET errors are shown to the local host.</summary>
	RemoteOnly,
	/// <summary>Enables custom errors. If no <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> is specified, standard errors are issued. </summary>
	On,
	/// <summary>Disables custom errors, allowing display of standard errors.</summary>
	Off
}
