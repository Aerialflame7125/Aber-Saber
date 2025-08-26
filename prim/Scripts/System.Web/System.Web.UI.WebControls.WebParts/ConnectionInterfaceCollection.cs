using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Represents a collection of interfaces for use in Web Parts connections.</summary>
public sealed class ConnectionInterfaceCollection : ReadOnlyCollectionBase
{
	/// <summary>References a static, read-only instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class.</summary>
	public static readonly ConnectionInterfaceCollection Empty = new ConnectionInterfaceCollection();

	/// <summary>Gets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element to get.</param>
	/// <returns>The element at the specified index.</returns>
	public Type this[int index] => (Type)base.InnerList[index];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class. </summary>
	public ConnectionInterfaceCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class with the specified collection. </summary>
	/// <param name="connectionInterfaces">A collection of objects to convert into a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />.</param>
	/// <exception cref="T:System.ArgumentException">An object in <paramref name="connectionInterfaces" /> cannot be added to a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection.</exception>
	public ConnectionInterfaceCollection(ICollection connectionInterfaces)
	{
		base.InnerList.AddRange(connectionInterfaces);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> class by combining the two specified collections. </summary>
	/// <param name="existingConnectionInterfaces">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> to combine with the <paramref name="connectionInterfaces" /> object.</param>
	/// <param name="connectionInterfaces">A collection to combine with the <paramref name="existingConnectionInterfaces" /> object.</param>
	/// <exception cref="T:System.ArgumentException">An object in <paramref name="connectionInterfaces" /> cannot be added to a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection.</exception>
	public ConnectionInterfaceCollection(ConnectionInterfaceCollection existingConnectionInterfaces, ICollection connectionInterfaces)
		: this()
	{
		base.InnerList.AddRange(existingConnectionInterfaces);
		base.InnerList.AddRange(connectionInterfaces);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> object contains a specific value.</summary>
	/// <param name="value">The type to locate in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(Type value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>Copies the entire <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ReadOnlyCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(Type[] array, int index)
	{
		base.InnerList.CopyTo(array, index);
	}

	/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> collection. </summary>
	/// <param name="value">The type to locate in the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(Type value)
	{
		return base.InnerList.IndexOf(value);
	}
}
