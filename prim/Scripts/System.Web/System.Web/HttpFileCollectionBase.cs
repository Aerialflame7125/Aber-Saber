using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Serves as the base class for classes that provide access to files that were uploaded by a client.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpFileCollectionBase : NameObjectCollectionBase, ICollection, IEnumerable
{
	/// <summary>When overridden in a derived class, gets an array that contains the keys (names) of all posted file objects in the collection.</summary>
	/// <returns>An array of file names.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string[] AllKeys
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the number of posted file objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public override int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether access to the collection is thread-safe.</summary>
	/// <returns>
	///     <see langword="true" /> if access is synchronized (thread-safe); otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsSynchronized
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

	/// <summary>When overridden in a derived class, gets the posted file object that has the specified name from the collection.</summary>
	/// <param name="name">The name of the object to return.</param>
	/// <returns>The posted file object that is specified by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpPostedFileBase this[string name]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the posted file object at the specified index.</summary>
	/// <param name="index">The index of the object to get.</param>
	/// <returns>The posted file object specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpPostedFileBase this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void CopyTo(Array dest, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns the posted file object at the specified index.</summary>
	/// <param name="index">The index of the object to return.</param>
	/// <returns>The posted file object specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpPostedFileBase Get(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns the posted file object that has the specified name from the collection.</summary>
	/// <param name="name">The name of the object to return.</param>
	/// <returns>The posted file object that is specified by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpPostedFileBase Get(string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>When implemented in a derived class, returns all files that match the specified name.</summary>
	/// <param name="name">The name to match.</param>
	/// <returns>The collection of files.</returns>
	public virtual IList<HttpPostedFileBase> GetMultiple(string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns an enumerator that can be used to iterate through the collection.</summary>
	/// <returns>An object that can be used to iterate through the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public override IEnumerator GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns the name of the posted file object at the specified index.</summary>
	/// <param name="index">The index of the object name to return.</param>
	/// <returns>The name of the posted file object that is specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string GetKey(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpFileCollectionBase()
	{
	}
}
