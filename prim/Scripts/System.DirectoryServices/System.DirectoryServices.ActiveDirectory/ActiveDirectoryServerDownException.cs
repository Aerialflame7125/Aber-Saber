using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class exception is thrown when a server is unavailable to respond to a service request.</summary>
[Serializable]
public class ActiveDirectoryServerDownException : Exception, ISerializable
{
	/// <summary>Gets the error code for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object.</summary>
	/// <returns>An <see cref="T:System.Int32" /> value that identifies the error.</returns>
	public int ErrorCode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the server that is associated with the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the name of the server that caused this error.</returns>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the message that describes the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object error.</summary>
	/// <returns>A <see cref="T:System.String" /> that describes the error.</returns>
	public override string Message
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message, an underlying exception object, a specified error code, and a specified server name.</summary>
	/// <param name="message">A message that describes the error.</param>
	/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
	/// <param name="errorCode">An error code that identifies the error.</param>
	/// <param name="name">The name of the server that caused the error.</param>
	public ActiveDirectoryServerDownException(string message, Exception inner, int errorCode, string name)
		: base(message, inner)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message, a specified error code, and a specified server name.</summary>
	/// <param name="message">A message that describes the error.</param>
	/// <param name="errorCode">An error code that identifies the error.</param>
	/// <param name="name">The name of the server that caused the error.</param>
	public ActiveDirectoryServerDownException(string message, int errorCode, string name)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message and an underlying exception object.</summary>
	/// <param name="message">A message that describes the error.</param>
	/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
	public ActiveDirectoryServerDownException(string message, Exception inner)
		: base(message, inner)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message.</summary>
	/// <param name="message">A message that describes the error.</param>
	public ActiveDirectoryServerDownException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class.</summary>
	public ActiveDirectoryServerDownException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class, using the specified serialization information and streaming context.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the exception.</param>
	protected ActiveDirectoryServerDownException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
	/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception that is being thrown.</param>
	/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
	{
		throw new NotImplementedException();
	}
}
