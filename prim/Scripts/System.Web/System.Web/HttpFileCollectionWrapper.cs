using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides access to files that were uploaded by a client.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpFileCollectionWrapper : HttpFileCollectionBase
{
	private HttpFileCollection w;

	/// <summary>Gets an array that contains the keys (names) of all posted file objects in the collection.</summary>
	/// <returns>An array of file names.</returns>
	public override string[] AllKeys => w.AllKeys;

	/// <summary>Gets the number of objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	public override int Count => w.Count;

	/// <summary>Gets a value that indicates whether access to the collection is thread-safe.</summary>
	/// <returns>
	///     <see langword="true" /> if access is synchronized (thread-safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool IsSynchronized => ((ICollection)w).IsSynchronized;

	/// <summary>Gets the posted file object at the specified index.</summary>
	/// <param name="index">The index of the item to get.</param>
	/// <returns>The posted file object specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override HttpPostedFileBase this[int index] => Get(index);

	/// <summary>Gets the posted file object that has the specified name from the collection.</summary>
	/// <param name="name">The name of the object to get.</param>
	/// <returns>The posted file object specified by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override HttpPostedFileBase this[string name] => Get(name);

	/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance.</summary>
	/// <returns>A collection that contains all the keys in the collection.</returns>
	public override KeysCollection Keys => w.Keys;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public override object SyncRoot => ((ICollection)w).SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpFileCollectionWrapper" /> class. </summary>
	/// <param name="httpFileCollection">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpApplicationState" /> is <see langword="null" />.</exception>
	public HttpFileCollectionWrapper(HttpFileCollection httpFileCollection)
	{
		if (httpFileCollection == null)
		{
			throw new ArgumentNullException("httpFileCollection");
		}
		w = httpFileCollection;
	}

	/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
	/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Web.HttpFileCollectionWrapper" /> object is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Web.HttpFileCollectionWrapper" /> object cannot be cast automatically to the type of the destination array.</exception>
	public override void CopyTo(Array dest, int index)
	{
		w.CopyTo(dest, index);
	}

	/// <summary>Returns the posted file object at the specified index.</summary>
	/// <param name="index">The index of the item to return.</param>
	/// <returns>The posted file object specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override HttpPostedFileBase Get(int index)
	{
		HttpPostedFile httpPostedFile = w.Get(index);
		if (httpPostedFile == null)
		{
			return null;
		}
		return new HttpPostedFileWrapper(httpPostedFile);
	}

	/// <summary>Returns the posted file object that has the specified name from the collection.</summary>
	/// <param name="name">The name of the object to return.</param>
	/// <returns>The posted file object specified by <paramref name="name" />, if found; otherwise, <see langword="null" />.</returns>
	public override HttpPostedFileBase Get(string name)
	{
		HttpPostedFile httpPostedFile = w.Get(name);
		if (httpPostedFile == null)
		{
			return null;
		}
		return new HttpPostedFileWrapper(httpPostedFile);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
	/// <returns>An object that can be used to iterate through the collection.</returns>
	public override IEnumerator GetEnumerator()
	{
		return w.GetEnumerator();
	}

	/// <summary>Returns the name of the posted file object at the specified index.</summary>
	/// <param name="index">The index of the object name to return.</param>
	/// <returns>The name of the posted file object that is specified by <paramref name="index" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override string GetKey(int index)
	{
		return w.GetKey(index);
	}

	/// <summary>Returns the data that is required in order to serialize the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</summary>
	/// <param name="info">The information that is required in order to serialize the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</param>
	/// <param name="context">The source and destination of the serialized stream that is associated with the <see cref="T:System.Web.HttpFileCollectionWrapper" /> object.</param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		w.GetObjectData(info, context);
	}

	/// <summary>Raises the deserialization event when deserialization is finished.</summary>
	/// <param name="sender">The source of the deserialization event.</param>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that is associated with the current <see cref="T:System.Web.HttpFileCollectionWrapper" /> instance is invalid.</exception>
	public override void OnDeserialization(object sender)
	{
		w.OnDeserialization(sender);
	}
}
