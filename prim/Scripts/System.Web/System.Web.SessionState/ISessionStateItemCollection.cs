using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState;

/// <summary>Defines the contract for the collection used by ASP.NET session state to manage session.</summary>
public interface ISessionStateItemCollection : ICollection, IEnumerable
{
	/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, <see langword="false" />.</returns>
	bool Dirty { get; set; }

	/// <summary>Gets or sets a value in the collection by numerical index.</summary>
	/// <param name="index">The numerical index of the value in the collection.</param>
	/// <returns>The value in the collection stored at the specified index.</returns>
	object this[int index] { get; set; }

	/// <summary>Gets or sets a value in the collection by name.</summary>
	/// <param name="name">The key name of the value in the collection.</param>
	/// <returns>The value in the collection with the specified name.</returns>
	object this[string name] { get; set; }

	/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
	/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the collection keys.</returns>
	NameObjectCollectionBase.KeysCollection Keys { get; }

	/// <summary>Removes all values and keys from the session-state collection.</summary>
	void Clear();

	/// <summary>Deletes an item from the collection.</summary>
	/// <param name="name">The name of the item to delete from the collection.</param>
	void Remove(string name);

	/// <summary>Deletes an item at a specified index from the collection.</summary>
	/// <param name="index">The index of the item to remove from the collection.</param>
	void RemoveAt(int index);
}
