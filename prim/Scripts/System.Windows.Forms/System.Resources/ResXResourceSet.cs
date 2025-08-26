using System.Collections;
using System.IO;

namespace System.Resources;

/// <summary>Gathers all items that represent an XML resource (.resx) file into a single object.</summary>
public class ResXResourceSet : ResourceSet
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> to read resources from the specified stream.</summary>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resource file. </param>
	public ResXResourceSet(Stream stream)
	{
		Reader = new ResXResourceReader(stream);
		Table = new Hashtable();
		ReadResources();
	}

	/// <summary>Initializes a new instance of a <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> that opens and reads resources from the specified file.</summary>
	/// <param name="fileName">The name of the file to read resources from. </param>
	public ResXResourceSet(string fileName)
	{
		Reader = new ResXResourceReader(fileName);
		Table = new Hashtable();
		ReadResources();
	}

	/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the preferred resource reader for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
	public override Type GetDefaultReader()
	{
		return typeof(ResXResourceReader);
	}

	/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the preferred resource writer for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
	public override Type GetDefaultWriter()
	{
		return typeof(ResXResourceWriter);
	}
}
