using System.Runtime.Serialization;

namespace System.Web.UI;

/// <summary>Represents the exception that is thrown when the view state cannot be loaded or validated. This class cannot be inherited.</summary>
[Serializable]
public sealed class ViewStateException : Exception, ISerializable
{
	/// <summary>Gets a value indicating whether the client is currently connected to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is still connected to the server; otherwise, <see langword="false" />.</returns>
	public bool IsConnected
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets debugging information about the HTTP request that resulted in a view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the formatted message with information about the exception.</returns>
	public override string Message
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the path of the HTTP request that resulted in a view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the path from the request.</returns>
	public string Path
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the contents of the view-state string that, when read, caused the view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the view-state values that caused the view-state exception.</returns>
	public string PersistedState
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the URL of the page that linked to the page where the view-state exception occurred.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the HTTP referrer.</returns>
	public string Referer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the IP address of the HTTP request that resulted in a view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the remote IP address of the client.</returns>
	public string RemoteAddress
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the port number of the HTTP request that resulted in a view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the remote port number.</returns>
	public string RemotePort
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the browser type of the HTTP request that resulted in a view-state exception.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the user agent, which is typically the browser type.</returns>
	public string UserAgent
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class. </summary>
	public ViewStateException()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class with serialized data.</summary>
	/// <param name="info">The object that holds the serialized object data. </param>
	/// <param name="context">The contextual information about the source or destination. </param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		throw new NotImplementedException();
	}
}
