namespace System.Web.Configuration;

/// <summary>Maps the configuration file virtual and physical paths.</summary>
public interface IConfigMapPathFactory
{
	/// <summary>Creates the interface for the mapping between configuration-file virtual and physical paths. </summary>
	/// <param name="virtualPath">The configuration-file virtual path.</param>
	/// <param name="physicalPath">The configuration-file physical path.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.IConfigMapPath" /> object associated with the specified configuration-file path mapping.</returns>
	IConfigMapPath Create(string virtualPath, string physicalPath);
}
