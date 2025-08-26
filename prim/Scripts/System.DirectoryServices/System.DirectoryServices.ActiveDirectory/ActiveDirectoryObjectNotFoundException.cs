using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class exception is thrown when a requested object is not found in the underlying directory store.</summary>
[Serializable]
public class ActiveDirectoryObjectNotFoundException : Exception, ISerializable
{
	/// <summary>Gets the name of the requested object.</summary>
	/// <returns>A string that contains the name of the requested object.</returns>
	public string Name
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the type of the requested object.</summary>
	/// <returns>A <see cref="T:System.Type" /> object that represents the type of the requested object.</returns>
	public Type Type
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message and information about the requested object.</summary>
	/// <param name="message">A message that describes the error.</param>
	/// <param name="type">A <see cref="T:System.Type" /> object that describes the type of the requested object.</param>
	/// <param name="name">A <see cref="T:System.String" /> that contains the name of the requested object.</param>
	[System.MonoTODO]
	public ActiveDirectoryObjectNotFoundException(string message, Type type, string name)
		: base(message)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message and an underlying exception object.</summary>
	/// <param name="message">A message that describes the error.</param>
	/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
	[System.MonoTODO]
	public ActiveDirectoryObjectNotFoundException(string message, Exception inner)
		: base(message, inner)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message.</summary>
	/// <param name="message">A message that describes the error.</param>
	[System.MonoTODO]
	public ActiveDirectoryObjectNotFoundException(string message)
		: base(message)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class.</summary>
	[System.MonoTODO]
	public ActiveDirectoryObjectNotFoundException()
		: base("DSUnknownFailure")
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class, using the specified serialization information and streaming context.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the exception.</param>
	[System.MonoTODO]
	protected ActiveDirectoryObjectNotFoundException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
	/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception that is being thrown.</param>
	/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
	[System.MonoTODO]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
	{
		throw new NotImplementedException();
	}
}
