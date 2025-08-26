using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System;

/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
[Serializable]
[ComVisible(true)]
public class ObjectDisposedException : InvalidOperationException
{
	private string objectName;

	/// <summary>Gets the message that describes the error.</summary>
	/// <returns>A string that describes the error.</returns>
	public override string Message
	{
		get
		{
			string text = ObjectName;
			if (text == null || text.Length == 0)
			{
				return base.Message;
			}
			string resourceString = Environment.GetResourceString("Object name: '{0}'.", text);
			return base.Message + Environment.NewLine + resourceString;
		}
	}

	/// <summary>Gets the name of the disposed object.</summary>
	/// <returns>A string containing the name of the disposed object.</returns>
	public string ObjectName
	{
		get
		{
			if (objectName == null && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return string.Empty;
			}
			return objectName;
		}
	}

	private ObjectDisposedException()
		: this(null, Environment.GetResourceString("Cannot access a disposed object."))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
	/// <param name="objectName">A string containing the name of the disposed object.</param>
	public ObjectDisposedException(string objectName)
		: this(objectName, Environment.GetResourceString("Cannot access a disposed object."))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
	/// <param name="objectName">The name of the disposed object.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	public ObjectDisposedException(string objectName, string message)
		: base(message)
	{
		SetErrorCode(-2146232798);
		this.objectName = objectName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	public ObjectDisposedException(string message, Exception innerException)
		: base(message, innerException)
	{
		SetErrorCode(-2146232798);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		objectName = info.GetString("ObjectName");
	}

	/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	[SecurityCritical]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("ObjectName", ObjectName, typeof(string));
	}
}
