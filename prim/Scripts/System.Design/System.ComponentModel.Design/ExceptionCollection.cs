using System.Collections;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design;

/// <summary>Represents the collection of exceptions.</summary>
[Serializable]
public sealed class ExceptionCollection : Exception
{
	private ArrayList exceptions;

	/// <summary>Gets the array of <see cref="T:System.Exception" /> objects that represent the collection of exceptions.</summary>
	/// <returns>An <see cref="T:System.Exception" /> array that represent the collection of exceptions.</returns>
	public ArrayList Exceptions => exceptions;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ExceptionCollection" /> class.</summary>
	/// <param name="exceptions">An array of type <see cref="T:System.Exception" />, containing the objects to populate the collection.</param>
	[System.MonoTODO]
	public ExceptionCollection(ArrayList exceptions)
	{
		this.exceptions = exceptions;
		throw new NotImplementedException();
	}

	/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the <see cref="T:System.ComponentModel.Design.ExceptionCollection" />.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
	/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="info" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		throw new NotImplementedException();
	}
}
