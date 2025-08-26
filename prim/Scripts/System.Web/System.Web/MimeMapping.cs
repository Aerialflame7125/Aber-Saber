namespace System.Web;

/// <summary>Maps document extensions to content MIME types.</summary>
public static class MimeMapping
{
	/// <summary>Returns the MIME mapping for the specified file name.</summary>
	/// <param name="fileName">The file name that is used to determine the MIME type.</param>
	public static string GetMimeMapping(string fileName)
	{
		return MimeTypes.GetMimeType(fileName);
	}
}
