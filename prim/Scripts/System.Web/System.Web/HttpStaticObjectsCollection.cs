using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.Util;

namespace System.Web;

/// <summary>Provides a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpStaticObjectsCollection : ICollection, IEnumerable
{
	private sealed class StaticItem
	{
		private object this_lock = new object();

		private Type type;

		private object instance;

		public object Instance
		{
			get
			{
				lock (this_lock)
				{
					if (instance == null)
					{
						instance = Activator.CreateInstance(type);
					}
				}
				return instance;
			}
		}

		public StaticItem(Type type)
		{
			this.type = type;
		}

		public StaticItem(StaticItem item)
		{
			type = item.type;
		}
	}

	private Dictionary<string, object> objects;

	private Dictionary<string, object> Objects
	{
		get
		{
			if (objects == null)
			{
				objects = new Dictionary<string, object>(StringComparer.Ordinal);
			}
			return objects;
		}
	}

	/// <summary>Gets the object with the specified name from the collection.</summary>
	/// <param name="name">The case-insensitive name of the object to get. </param>
	/// <returns>An object from the collection.</returns>
	public object this[string name]
	{
		get
		{
			if (objects == null)
			{
				return null;
			}
			StaticItem staticItem = null;
			if (Objects.TryGetValue(name, out var value))
			{
				staticItem = value as StaticItem;
			}
			return staticItem?.Instance;
		}
	}

	/// <summary>Gets the number of objects in the collection.</summary>
	/// <returns>The number of objects in the collection.</returns>
	public int Count
	{
		get
		{
			if (objects == null)
			{
				return 0;
			}
			return Objects.Count;
		}
	}

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>Always returns <see langword="true" />.</returns>
	public bool IsReadOnly => true;

	/// <summary>Gets a value indicating whether the collection is synchronized (that is, thread-safe).</summary>
	/// <returns>In this implementation, this property always returns <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a Boolean value indicating whether the collection has been accessed before.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.HttpStaticObjectsCollection" /> has never been accessed; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool NeverAccessed
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>The current <see cref="T:System.Web.HttpStaticObjectsCollection" />.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpStaticObjectsCollection" /> class.</summary>
	public HttpStaticObjectsCollection()
	{
	}

	internal HttpStaticObjectsCollection(HttpApplicationState appstate)
	{
	}

	/// <summary>Returns the object with the specified name from the collection. This property is an alternative to the <see langword="this" /> accessor.</summary>
	/// <param name="name">The case-insensitive name of the object to return. </param>
	/// <returns>An object from the collection.</returns>
	public object GetObject(string name)
	{
		return this[name];
	}

	/// <summary>Returns a dictionary enumerator used for iterating through the key-and-value pairs contained in the collection.</summary>
	/// <returns>The enumerator for the collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return Objects.GetEnumerator();
	}

	/// <summary>Copies members of an <see cref="T:System.Web.HttpStaticObjectsCollection" /> into an array.</summary>
	/// <param name="array">The array to copy the <see cref="T:System.Web.HttpStaticObjectsCollection" /> into. </param>
	/// <param name="index">The member of the collection where copying starts. </param>
	public void CopyTo(Array array, int index)
	{
		if (objects == null)
		{
			return;
		}
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		if (array.Rank > 1)
		{
			throw new ArgumentException("array is multidimensional");
		}
		if (array.Length > 0 && index >= array.Length)
		{
			throw new ArgumentException("index is equal to or greater than array.Length");
		}
		if (index + objects.Count > array.Length)
		{
			throw new ArgumentException("Not enough room from index to end of array for this collection");
		}
		foreach (KeyValuePair<string, object> @object in objects)
		{
			array.SetValue(new DictionaryEntry(@object.Key, @object.Value), index++);
		}
	}

	internal IDictionary GetObjects()
	{
		return Objects;
	}

	internal HttpStaticObjectsCollection Clone()
	{
		HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
		if (objects == null)
		{
			return httpStaticObjectsCollection;
		}
		Dictionary<string, object> dictionary = httpStaticObjectsCollection.Objects;
		foreach (KeyValuePair<string, object> @object in objects)
		{
			StaticItem value = new StaticItem((StaticItem)@object.Value);
			dictionary[@object.Key] = value;
		}
		return httpStaticObjectsCollection;
	}

	internal void Add(ObjectTagBuilder tag)
	{
		Objects.Add(tag.ObjectID, new StaticItem(tag.Type));
	}

	private void Set(string name, object obj)
	{
		Objects[name] = obj;
	}

	/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.IO.BinaryWriter" /> used to write the serialized collection to a stream or encoded string.</param>
	public void Serialize(BinaryWriter writer)
	{
		if (objects == null)
		{
			writer.Write(0);
			return;
		}
		writer.Write(objects.Count);
		foreach (KeyValuePair<string, object> @object in objects)
		{
			writer.Write(@object.Key);
			AltSerialization.Serialize(writer, @object.Value);
		}
	}

	/// <summary>Creates an <see cref="T:System.Web.HttpStaticObjectsCollection" /> object from a binary file that was written by using the <see cref="M:System.Web.HttpStaticObjectsCollection.Serialize(System.IO.BinaryWriter)" /> method.</summary>
	/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> used to read the serialized collection from a stream or encoded string.</param>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> populated with the contents from a binary file written using the <see cref="M:System.Web.HttpStaticObjectsCollection.Serialize(System.IO.BinaryWriter)" /> method.</returns>
	public static HttpStaticObjectsCollection Deserialize(BinaryReader reader)
	{
		HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
		for (int num = reader.ReadInt32(); num > 0; num--)
		{
			httpStaticObjectsCollection.Set(reader.ReadString(), AltSerialization.Deserialize(reader));
		}
		return httpStaticObjectsCollection;
	}

	internal byte[] ToByteArray()
	{
		MemoryStream memoryStream = null;
		try
		{
			memoryStream = new MemoryStream();
			Serialize(new BinaryWriter(memoryStream));
			return memoryStream.GetBuffer();
		}
		catch
		{
			throw;
		}
		finally
		{
			memoryStream?.Close();
		}
	}

	internal static HttpStaticObjectsCollection FromByteArray(byte[] data)
	{
		HttpStaticObjectsCollection httpStaticObjectsCollection = null;
		MemoryStream memoryStream = null;
		try
		{
			memoryStream = new MemoryStream(data);
			return Deserialize(new BinaryReader(memoryStream));
		}
		catch
		{
			throw;
		}
		finally
		{
			memoryStream?.Close();
		}
	}
}
