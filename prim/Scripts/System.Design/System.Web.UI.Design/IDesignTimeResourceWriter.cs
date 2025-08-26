using System.Resources;

namespace System.Web.UI.Design;

/// <summary>Used by the <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> class to localize data at design time.</summary>
public interface IDesignTimeResourceWriter : IResourceWriter, IDisposable
{
	/// <summary>Creates a key, using the provided string, to use to retrieve data from the given resource.</summary>
	/// <param name="resourceName">The name of the resource.</param>
	/// <param name="obj">The object to localize.</param>
	/// <returns>The key used to write or retrieve <paramref name="obj" /> from <paramref name="resourceName" />.</returns>
	string CreateResourceKey(string resourceName, object obj);
}
