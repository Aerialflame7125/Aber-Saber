namespace System.Windows.Forms;

/// <summary>Specifies a standard interface for retrieving feature information from the current system.</summary>
/// <filterpriority>2</filterpriority>
public interface IFeatureSupport
{
	/// <summary>Retrieves the version of the specified feature.</summary>
	/// <returns>A <see cref="T:System.Version" /> representing the version number of the specified feature; or null if the feature is not installed.</returns>
	/// <param name="feature">The feature whose version is requested. </param>
	/// <filterpriority>1</filterpriority>
	Version GetVersionPresent(object feature);

	/// <summary>Determines whether any version of the specified feature is currently available on the system.</summary>
	/// <returns>true if the feature is present; otherwise, false.</returns>
	/// <param name="feature">The feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	bool IsPresent(object feature);

	/// <summary>Determines whether the specified or newer version of the specified feature is currently available on the system.</summary>
	/// <returns>true if the requested version of the feature is present; otherwise, false.</returns>
	/// <param name="feature">The feature to look for. </param>
	/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	bool IsPresent(object feature, Version minimumVersion);
}
