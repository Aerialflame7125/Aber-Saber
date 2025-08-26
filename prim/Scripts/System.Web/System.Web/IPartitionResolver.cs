namespace System.Web;

/// <summary>Defines methods that must be implemented for custom session-state partition resolution.</summary>
public interface IPartitionResolver
{
	/// <summary>Initializes the custom partition resolver. </summary>
	void Initialize();

	/// <summary>Resolves the partition based on a key parameter.</summary>
	/// <param name="key">An identifier used to determine which partition to use for the current session state.</param>
	/// <returns>A string with connection information.</returns>
	string ResolvePartition(object key);
}
