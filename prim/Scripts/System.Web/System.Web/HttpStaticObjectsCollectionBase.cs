using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Serves as the base class for classes that provide a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpStaticObjectsCollectionBase : ICollection, IEnumerable
{
	/// <summary>When overridden in a derived class, gets the number of objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the collection is thread-safe.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is thread-safe; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the object that has the specified name from the collection.</summary>
	/// <param name="name">The case-insensitive name of the object to get.</param>
	/// <returns>The object that is specified by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object this[string name]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the collection has been accessed.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection has never been accessed; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool NeverAccessed
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="array">The one-dimensional array that is the destination of the elements that are copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns an enumerator that can be used to iterate through the collection.</summary>
	/// <returns>An object that can be used to iterate through the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IEnumerator GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns the object that has the specified name from the collection.</summary>
	/// <param name="name">The case-insensitive name of the object to return.</param>
	/// <returns>The object that is specified by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetObject(string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
	/// <param name="writer">The object to use to write the serialized collection to a stream or encoded string.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Serialize(BinaryWriter writer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpStaticObjectsCollectionBase()
	{
	}
}
