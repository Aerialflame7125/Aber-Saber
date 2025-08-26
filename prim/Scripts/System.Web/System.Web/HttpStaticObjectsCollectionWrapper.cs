using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpStaticObjectsCollectionWrapper : HttpStaticObjectsCollectionBase
{
	private HttpStaticObjectsCollection _collection;

	/// <summary>Gets the number of objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	public override int Count => _collection.Count;

	/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool IsReadOnly => _collection.IsReadOnly;

	/// <summary>Gets a value that indicates whether the collection is thread-safe.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool IsSynchronized => _collection.IsSynchronized;

	/// <summary>Gets the object that has the specified name from the collection.</summary>
	/// <param name="name">The case-insensitive name of the object to get.</param>
	/// <returns>The object that is specified by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override object this[string name] => _collection[name];

	/// <summary>Gets a value that indicates whether the collection has been accessed.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection has never been accessed; otherwise, <see langword="false" />.</returns>
	public override bool NeverAccessed => _collection.NeverAccessed;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>The current instance of the <see cref="T:System.Web.HttpStaticObjectsCollection" /> class that is wrapped by this class.</returns>
	public override object SyncRoot => _collection.SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpStaticObjectsCollectionWrapper" /> class. </summary>
	/// <param name="httpStaticObjectsCollection">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpStaticObjectsCollection" /> is <see langword="null" />.</exception>
	public HttpStaticObjectsCollectionWrapper(HttpStaticObjectsCollection httpStaticObjectsCollection)
	{
		if (httpStaticObjectsCollection == null)
		{
			throw new ArgumentNullException("httpStaticObjectsCollection");
		}
		_collection = httpStaticObjectsCollection;
	}

	/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="array">The one-dimensional array that is the destination of the elements that are copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
	public override void CopyTo(Array array, int index)
	{
		_collection.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
	/// <returns>An object that can be used to iterate through the collection.</returns>
	public override IEnumerator GetEnumerator()
	{
		return _collection.GetEnumerator();
	}

	/// <summary>Returns the object that has the specified name from the collection.</summary>
	/// <param name="name">The case-insensitive name of the object to return.</param>
	/// <returns>The object that is specified by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override object GetObject(string name)
	{
		return _collection.GetObject(name);
	}

	/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
	/// <param name="writer">The object to use to write the serialized collection to a stream or encoded string.</param>
	public override void Serialize(BinaryWriter writer)
	{
		_collection.Serialize(writer);
	}
}
