using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides access to individual files that have been uploaded by a client.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpPostedFileWrapper : HttpPostedFileBase
{
	private HttpPostedFile _file;

	/// <summary>Gets the size of an uploaded file, in bytes.</summary>
	/// <returns>The length of the file, in bytes.</returns>
	public override int ContentLength => _file.ContentLength;

	/// <summary>Gets the MIME content type of an uploaded file.</summary>
	/// <returns>The MIME content type of the file.</returns>
	public override string ContentType => _file.ContentType;

	/// <summary>Gets the fully qualified name of the file on the client.</summary>
	/// <returns>The name of the file on the client, which includes the directory path.</returns>
	public override string FileName => _file.FileName;

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
	/// <returns>An object for reading a file.</returns>
	public override Stream InputStream => _file.InputStream;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpPostedFileWrapper" /> class. </summary>
	/// <param name="httpPostedFile">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpApplicationState" /> is <see langword="null" />.</exception>
	public HttpPostedFileWrapper(HttpPostedFile httpPostedFile)
	{
		if (httpPostedFile == null)
		{
			throw new ArgumentNullException("httpPostedFile");
		}
		_file = httpPostedFile;
	}

	/// <summary>Saves the contents of an uploaded file.</summary>
	/// <param name="filename">The name of the file to save.</param>
	public override void SaveAs(string filename)
	{
		_file.SaveAs(filename);
	}
}
