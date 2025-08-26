using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>Contains extended error information about an error that occurred when the <see cref="M:System.DirectoryServices.DirectoryEntry.Invoke(System.String,System.Object[])" /> method is called.</summary>
[Serializable]
public class DirectoryServicesCOMException : COMException, ISerializable
{
	/// <summary>Gets the extended error code.</summary>
	/// <returns>The extended error code.</returns>
	public int ExtendedError
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the extended error message.</summary>
	/// <returns>The extended error message.</returns>
	public string ExtendedErrorMessage
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class.</summary>
	public DirectoryServicesCOMException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified string.</summary>
	/// <param name="message">The message that describes the error.</param>
	public DirectoryServicesCOMException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified string and exception.</summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="inner">The exception that is the cause of the current exception.</param>
	public DirectoryServicesCOMException(string message, Exception inner)
		: base(message, inner)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified serialization information and streaming context.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> destination for this serialization.</param>
	protected DirectoryServicesCOMException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> object.</summary>
	/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
	/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that is the destination for this serialization.</param>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
	{
		throw new NotImplementedException();
	}
}
