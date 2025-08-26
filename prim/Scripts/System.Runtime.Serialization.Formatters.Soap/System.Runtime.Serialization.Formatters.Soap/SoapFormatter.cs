using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Soap;

/// <summary>Serializes and deserializes an object, or an entire graph of connected objects, in SOAP format.</summary>
public sealed class SoapFormatter : IRemotingFormatter, IFormatter
{
	private SerializationBinder _binder;

	private StreamingContext _context;

	private ISurrogateSelector _selector;

	private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

	private FormatterTypeStyle _typeFormat;

	private ISoapMessage _topObject;

	private TypeFilterLevel _filterLevel = TypeFilterLevel.Low;

	/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> that controls type substitution during serialization and deserialization.</summary>
	/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
	public ISurrogateSelector SurrogateSelector
	{
		get
		{
			return _selector;
		}
		set
		{
			_selector = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that controls the binding of a serialized object to a type.</summary>
	/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
	public SerializationBinder Binder
	{
		get
		{
			return _binder;
		}
		set
		{
			_binder = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</summary>
	/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
	public StreamingContext Context
	{
		get
		{
			return _context;
		}
		set
		{
			_context = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.ISoapMessage" /> into which the SOAP top object is deserialized.</summary>
	/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.ISoapMessage" /> into which the SOAP top object is deserialized.</returns>
	public ISoapMessage TopObject
	{
		get
		{
			return _topObject;
		}
		set
		{
			_topObject = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> of automatic deserialization for .NET Framework remoting.</summary>
	/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> that represents the current automatic deserialization level.</returns>
	[System.MonoTODO("Interpret this")]
	public TypeFilterLevel FilterLevel
	{
		get
		{
			return _filterLevel;
		}
		set
		{
			_filterLevel = value;
		}
	}

	/// <summary>Gets or sets the behavior of the deserializer with regards to finding and loading assemblies.</summary>
	/// <returns>One of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values that specifies the deserializer behavior.</returns>
	public FormatterAssemblyStyle AssemblyFormat
	{
		get
		{
			return _assemblyFormat;
		}
		set
		{
			_assemblyFormat = value;
		}
	}

	/// <summary>Gets or sets the format in which type descriptions are laid out in the serialized stream.</summary>
	/// <returns>The format in which type descriptions are laid out in the serialized stream.</returns>
	public FormatterTypeStyle TypeFormat
	{
		get
		{
			return _typeFormat;
		}
		set
		{
			_typeFormat = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> class with default property values.</summary>
	public SoapFormatter()
	{
		_selector = null;
		_context = new StreamingContext(StreamingContextStates.All);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> class with the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
	/// <param name="selector">The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to use with the new instance of <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />. Can be <see langword="null" />.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the source and destination of the serialization. If the <paramref name="context" /> parameter is <see langword="null" />, then the <see cref="P:System.Runtime.Serialization.Formatters.Soap.SoapFormatter.Context" /> defaults to <see cref="F:System.Runtime.Serialization.StreamingContextStates.CrossMachine" />.</param>
	public SoapFormatter(ISurrogateSelector selector, StreamingContext context)
	{
		_selector = selector;
		_context = context;
	}

	/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
	/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
	/// <returns>The top object of the deserialized graph (root).</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serializationStream" /> is <see langword="null" />.</exception>
	public object Deserialize(Stream serializationStream)
	{
		return Deserialize(serializationStream, null);
	}

	/// <summary>Deserializes the stream into an object graph with any headers in that stream being handled by the given <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />.</summary>
	/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
	/// <param name="handler">Delegate to handle any headers found on the stream. Can be <see langword="null" />.</param>
	/// <returns>The top object of the deserialized graph (root).</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serializationStream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">
	///   <paramref name="serializationStream" /> supports seeking, and its length is 0.</exception>
	public object Deserialize(Stream serializationStream, HeaderHandler handler)
	{
		return new SoapReader(_binder, _selector, _context).Deserialize(serializationStream, _topObject);
	}

	/// <summary>Serializes an object or graph of objects with the specified root to the given <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="serializationStream">The stream onto which the formatter puts the data to serialize.</param>
	/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serializationStream" /> is <see langword="null" />.</exception>
	public void Serialize(Stream serializationStream, object graph)
	{
		Serialize(serializationStream, graph, null);
	}

	/// <summary>Serializes an object or graph of objects with the specified root to the given <see cref="T:System.IO.Stream" /> in the SOAP Remote Procedure Call (RPC) format.</summary>
	/// <param name="serializationStream">The stream onto which the formatter puts the data to serialize.</param>
	/// <param name="graph">The object or root of the object graph to serialize. All child objects of this root object are automatically serialized.</param>
	/// <param name="headers">Remoting headers to include in the serialization. Can be <see langword="null" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serializationStream" /> is <see langword="null" />.</exception>
	public void Serialize(Stream serializationStream, object graph, Header[] headers)
	{
		if (serializationStream == null)
		{
			throw new ArgumentNullException("serializationStream");
		}
		if (!serializationStream.CanWrite)
		{
			throw new SerializationException("Can't write in the serialization stream");
		}
		if (graph == null)
		{
			throw new ArgumentNullException("graph");
		}
		new SoapWriter(serializationStream, _selector, _context, _topObject).Serialize(graph, headers, _typeFormat, _assemblyFormat);
	}
}
