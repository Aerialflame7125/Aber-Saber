namespace System.Web.Management;

/// <summary>Provides information associated with health events.</summary>
public sealed class WebApplicationInformation
{
	private string application_domain;

	private string application_path;

	private string application_virtual_path;

	private string machine_name;

	private string trust_level;

	/// <summary>Gets the current application domain name.</summary>
	/// <returns>Gets the application domain name.</returns>
	public string ApplicationDomain => application_domain;

	/// <summary>Gets the application physical path.</summary>
	/// <returns>The application physical path.</returns>
	public string ApplicationPath => application_path;

	/// <summary>Gets the application logical path.</summary>
	/// <returns>The application logical path.</returns>
	public string ApplicationVirtualPath => application_virtual_path;

	/// <summary>Gets the application machine name.</summary>
	/// <returns>The name of the machine where the application is running.</returns>
	public string MachineName => machine_name;

	/// <summary>Gets the application trust level.</summary>
	/// <returns>The application trust level.</returns>
	public string TrustLevel => trust_level;

	internal WebApplicationInformation()
	{
	}

	/// <summary>Formats the application information.</summary>
	/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
	public void FormatToString(WebEventFormatter formatter)
	{
		throw new NotImplementedException();
	}

	/// <summary>Formats event information for display purposes.</summary>
	/// <returns>The event information.</returns>
	public override string ToString()
	{
		throw new NotImplementedException();
	}
}
