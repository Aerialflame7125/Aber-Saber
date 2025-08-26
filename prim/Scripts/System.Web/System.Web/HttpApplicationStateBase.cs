using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Serves as the base class for classes that enable information to be shared across multiple sessions and requests within an ASP.NET application.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpApplicationStateBase : NameObjectCollectionBase, ICollection, IEnumerable
{
	/// <summary>When overridden in a derived class, gets the access keys for the objects in the collection.</summary>
	/// <returns>An array of state object keys.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string[] AllKeys
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a reference to the <see cref="T:System.Web.HttpApplicationStateBase" /> object.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpApplicationStateBase Contents
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the number of objects in the collection.</summary>
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

	/// <summary>When overridden in a derived class, gets a state object by index.</summary>
	/// <param name="index">The index of the object in the collection.</param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a state object by name.</summary>
	/// <param name="name">The name of the object in the collection.</param>
	/// <returns>The object referenced by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object this[string name]
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets all objects that are declared by an <see langword="object" /> element where the scope is set to "Application" in the ASP.NET application.</summary>
	/// <returns>A collection of objects in the application.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpStaticObjectsCollectionBase StaticObjects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, adds a new object to the collection.</summary>
	/// <param name="name">The name of the object to add to the collection.</param>
	/// <param name="value">The value of the object.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Add(string name, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes all objects from the collection.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="array">The one-dimensional array that is the destination for the elements that are copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying. </param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, gets a state object by index.</summary>
	/// <param name="index">The index of the application state object to get.</param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object Get(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, gets a state object by name.</summary>
	/// <param name="name">The name of the object to get.</param>
	/// <returns>The object referenced by <paramref name="name" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object Get(string name)
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

	/// <summary>When overridden in a derived class, gets the name of a state object by index.</summary>
	/// <param name="index">The index of the application state object to get.</param>
	/// <returns>The name of the application state object.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string GetKey(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, locks access to objects in the collection in order to enable synchronized access.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Lock()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes the named object from the collection.</summary>
	/// <param name="name">The name of the object to remove from the collection.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Remove(string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes all objects from the collection.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveAll()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes a state object specified by index from the collection.</summary>
	/// <param name="index">The position in the collection of the item to remove.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, updates the value of an object in the collection.</summary>
	/// <param name="name">The name of the object to update.</param>
	/// <param name="value">The updated value of the object.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Set(string name, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, unlocks access to objects in the collection to enable synchronized access.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void UnLock()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpApplicationStateBase()
	{
	}
}
