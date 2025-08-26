using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Serves as the base class for classes that provide access to individual files that have been uploaded by a client.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpPostedFileBase
{
	/// <summary>When overridden in a derived class, gets the size of an uploaded file, in bytes.</summary>
	/// <returns>The length of the file, in bytes.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ContentLength
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the MIME content type of an uploaded file.</summary>
	/// <returns>The MIME content type of the file.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string ContentType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the fully qualified name of the file on the client.</summary>
	/// <returns>The name of the file on the client, which includes the directory path.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string FileName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
	/// <returns>An object for reading a file.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Stream InputStream
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, saves the contents of an uploaded file.</summary>
	/// <param name="filename">The name of the file to save.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void SaveAs(string filename)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpPostedFileBase()
	{
	}
}
