using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that enables information to be shared across multiple requests and sessions within an ASP.NET application.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpApplicationStateWrapper : HttpApplicationStateBase
{
	private HttpApplicationState _application;

	/// <summary>Gets the keys for the objects in the collection.</summary>
	/// <returns>An array of state object keys.</returns>
	public override string[] AllKeys => _application.AllKeys;

	/// <summary>Gets a reference to the <see cref="T:System.Web.HttpApplicationStateBase" /> object.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
	public override HttpApplicationStateBase Contents => this;

	/// <summary>Gets the number of objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	public override int Count => _application.Count;

	/// <summary>Gets a value that indicates whether access to the collection is thread-safe.</summary>
	/// <returns>
	///     <see langword="true" /> if access is synchronized (thread-safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool IsSynchronized => ((ICollection)_application).IsSynchronized;

	/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance.</summary>
	/// <returns>A collection of all the keys in the collection.</returns>
	public override KeysCollection Keys => _application.Keys;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public override object SyncRoot => ((ICollection)_application).SyncRoot;

	/// <summary>Gets a state object by index.</summary>
	/// <param name="index">The index of the object in the collection.</param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override object this[int index] => _application[index];

	/// <summary>Gets a state object by name.</summary>
	/// <param name="name">The name of the object in the collection.</param>
	/// <returns>The object referenced by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override object this[string name]
	{
		get
		{
			return _application[name];
		}
		set
		{
			_application[name] = value;
		}
	}

	/// <summary>Gets all objects that are declared by an <see langword="object" /> element where the scope is set to "Application" in the ASP.NET application.</summary>
	/// <returns>A collection of objects in the application.</returns>
	public override HttpStaticObjectsCollectionBase StaticObjects => new HttpStaticObjectsCollectionWrapper(_application.StaticObjects);

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpApplicationStateWrapper" /> class. </summary>
	/// <param name="httpApplicationState">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpApplicationState" /> is <see langword="null" />.</exception>
	public HttpApplicationStateWrapper(HttpApplicationState httpApplicationState)
	{
		if (httpApplicationState == null)
		{
			throw new ArgumentNullException("httpApplicationState");
		}
		_application = httpApplicationState;
	}

	/// <summary>Adds an object to the collection.</summary>
	/// <param name="name">The name of the object to add to the collection.</param>
	/// <param name="value">The value of the object.</param>
	public override void Add(string name, object value)
	{
		_application.Add(name, value);
	}

	/// <summary>Removes all objects from the collection.</summary>
	public override void Clear()
	{
		_application.Clear();
	}

	/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="array">The one-dimensional array that is the destination for the elements that are copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Web.HttpApplicationStateWrapper" /> object is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Web.HttpApplicationStateWrapper" /> object cannot be cast to the type of the destination array.</exception>
	public override void CopyTo(Array array, int index)
	{
		((ICollection)_application).CopyTo(array, index);
	}

	/// <summary>Returns a state object by index.</summary>
	/// <param name="index">The index of the application state object to get.</param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override object Get(int index)
	{
		return _application.Get(index);
	}

	/// <summary>Returns a state object by name.</summary>
	/// <param name="name">The name of the object to get.</param>
	/// <returns>The object referenced by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override object Get(string name)
	{
		return _application.Get(name);
	}

	/// <summary>Returns an enumerator that can be used to iterate through a collection.</summary>
	/// <returns>An object that can be used to iterate through the collection.</returns>
	public override IEnumerator GetEnumerator()
	{
		return ((IEnumerable)_application).GetEnumerator();
	}

	/// <summary>Returns the name of a state object by index.</summary>
	/// <param name="index">The index of the application state object to get.</param>
	/// <returns>The name of the application state object.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override string GetKey(int index)
	{
		return _application.GetKey(index);
	}

	/// <summary>Returns the data that is necessary to serialize the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream that is associated with the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</param>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		_application.GetObjectData(info, context);
	}

	/// <summary>Locks access to objects in the collection in order to enable synchronized access.</summary>
	public override void Lock()
	{
		_application.Lock();
	}

	/// <summary>Raises the deserialization event when deserialization is finished.</summary>
	/// <param name="sender">The source of the deserialization event.</param>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that is associated with the current <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance is invalid.</exception>
	public override void OnDeserialization(object sender)
	{
		_application.OnDeserialization(sender);
	}

	/// <summary>Removes the object specified by name from the collection.</summary>
	/// <param name="name">The name of the object to remove from the collection.</param>
	public override void Remove(string name)
	{
		_application.Remove(name);
	}

	/// <summary>Removes all objects from the collection.</summary>
	public override void RemoveAll()
	{
		_application.RemoveAll();
	}

	/// <summary>Removes the object specified by index from the collection.</summary>
	/// <param name="index">The position in the collection of the item to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override void RemoveAt(int index)
	{
		_application.RemoveAt(index);
	}

	/// <summary>Updates the value of an object in the collection.</summary>
	/// <param name="name">The name of the object to update.</param>
	/// <param name="value">The updated value of the object.</param>
	public override void Set(string name, object value)
	{
		_application.Set(name, value);
	}

	/// <summary>Unlocks access to objects in the collection to enable synchronized access.</summary>
	public override void UnLock()
	{
		_application.UnLock();
	}
}
