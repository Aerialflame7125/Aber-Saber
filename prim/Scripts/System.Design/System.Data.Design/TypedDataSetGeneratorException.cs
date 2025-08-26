using System.Collections;
using System.Runtime.Serialization;

namespace System.Data.Design;

/// <summary>The exception that is thrown when a name conflict occurs while a strongly typed <see cref="T:System.Data.DataSet" /> is being generated.</summary>
[Serializable]
public class TypedDataSetGeneratorException : DataException
{
	private IList errorList;

	/// <summary>Gets a dynamic list of generated errors.</summary>
	/// <returns>The error list.</returns>
	public IList ErrorList => errorList;

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with a system-supplied message that describes the error.</summary>
	public TypedDataSetGeneratorException()
		: base(global::Locale.GetText("System error."))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class by passing in a collection of errors.</summary>
	/// <param name="list">An <see cref="T:System.Collections.IList" /> of errors.</param>
	public TypedDataSetGeneratorException(IList list)
		: base(global::Locale.GetText("System error."))
	{
		errorList = list;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class, using the specified serialization information and streaming context.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
	protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		int @int = info.GetInt32("KEY_ARRAYCOUNT");
		errorList = new ArrayList(@int);
		for (int i = 0; i < @int; i++)
		{
			errorList.Add(info.GetString("KEY_ARRAYVALUES" + i));
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with a specified message that describes the error.</summary>
	/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	public TypedDataSetGeneratorException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with the specified string and inner exception.</summary>
	/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	public TypedDataSetGeneratorException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	/// <summary>Implements the <see langword="ISerializable" /> interface and returns the data that you must have to serialize the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> object.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		int num = ((errorList != null) ? ErrorList.Count : 0);
		info.AddValue("KEY_ARRAYCOUNT", num);
		for (int i = 0; i < num; i++)
		{
			info.AddValue("KEY_ARRAYVALUES" + i, ErrorList[i]);
		}
	}
}
