using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Util;

namespace System.Web.SessionState;

/// <summary>A collection of objects stored in session state. This class cannot be inherited.</summary>
public sealed class SessionStateItemCollection : NameObjectCollectionBase, ISessionStateItemCollection, ICollection, IEnumerable
{
	private bool is_dirty;

	/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, <see langword="false" />.</returns>
	public bool Dirty
	{
		get
		{
			return is_dirty;
		}
		set
		{
			is_dirty = value;
		}
	}

	/// <summary>Gets or sets a value in the collection by numerical index.</summary>
	/// <param name="index">The numerical index of the value in the collection.</param>
	/// <returns>The value in the collection stored at the specified index. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
	public object this[int index]
	{
		get
		{
			object obj = BaseGet(index);
			if (IsMutable(obj))
			{
				is_dirty = true;
			}
			return obj;
		}
		set
		{
			BaseSet(index, value);
			is_dirty = true;
		}
	}

	/// <summary>Gets or sets a value in the collection by name.</summary>
	/// <param name="name">The key name of the value in the collection.</param>
	/// <returns>The value in the collection with the specified name. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
	public object this[string name]
	{
		get
		{
			object obj = BaseGet(name);
			if (IsMutable(obj))
			{
				is_dirty = true;
			}
			return obj;
		}
		set
		{
			BaseSet(name, value);
			is_dirty = true;
		}
	}

	/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
	/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> collection that contains all the collection keys. </returns>
	public override KeysCollection Keys => base.Keys;

	private static bool IsMutable(object o)
	{
		if (o != null)
		{
			return Type.GetTypeCode(o.GetType()) == TypeCode.Object;
		}
		return false;
	}

	/// <summary>Creates a new, empty <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> object.</summary>
	public SessionStateItemCollection()
	{
	}

	internal SessionStateItemCollection(int capacity)
		: base(capacity)
	{
	}

	/// <summary>Removes all values and keys from the session-state collection.</summary>
	public void Clear()
	{
		if (Count > 0)
		{
			BaseClear();
			is_dirty = true;
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</summary>
	/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> used to read the serialized collection from a stream or encoded string.</param>
	/// <returns>A <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection populated with the contents from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</returns>
	/// <exception cref="T:System.Web.HttpException">The session state information is invalid or corrupted</exception>
	public static SessionStateItemCollection Deserialize(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection(num);
		while (num > 0)
		{
			sessionStateItemCollection[reader.ReadString()] = AltSerialization.Deserialize(reader);
			num--;
		}
		return sessionStateItemCollection;
	}

	/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" />.</summary>
	/// <param name="writer">The <see cref="T:System.IO.BinaryWriter" /> used to write the serialized collection to a stream or encoded string.</param>
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(Count);
		foreach (string key in base.Keys)
		{
			writer.Write(key);
			AltSerialization.Serialize(writer, BaseGet(key));
		}
	}

	/// <summary>Returns an enumerator that can be used to read all the key names in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
	public override IEnumerator GetEnumerator()
	{
		return base.GetEnumerator();
	}

	/// <summary>Deletes an item from the collection.</summary>
	/// <param name="name">The name of the item to delete from the collection. </param>
	public void Remove(string name)
	{
		BaseRemove(name);
		is_dirty = true;
	}

	/// <summary>Deletes an item at a specified index from the collection.</summary>
	/// <param name="index">The index of the item to remove from the collection. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
		is_dirty = true;
	}
}
