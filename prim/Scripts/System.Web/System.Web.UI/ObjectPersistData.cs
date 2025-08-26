using System.Collections;

namespace System.Web.UI;

/// <summary>During the build process, retains information about property entries.</summary>
public class ObjectPersistData
{
	/// <summary>Gets all the property entries for the control being built.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the property entries for the control.</returns>
	public ICollection AllPropertyEntries
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of the objects that have been built by the control builder.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the items that have been built by the control builder.</returns>
	public IDictionary BuiltObjects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets items that are collection types.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing items of type <see cref="T:System.Collections.ICollection" />.</returns>
	public ICollection CollectionItems
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets event entries for the control being built.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the event entries.</returns>
	public ICollection EventEntries
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the persisted data is for a collection.</summary>
	/// <returns>
	///     <see langword="true" /> if this persisted data is for a collection; otherwise, <see langword="false" />.</returns>
	public bool IsCollection
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the control created by the control builder object is localized.</summary>
	/// <returns>
	///     <see langword="true" /> if the control created by the control builder object is localized; otherwise, <see langword="false" />.</returns>
	public bool Localize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the type of the object associated with the persisted properties.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the object being built.</returns>
	public Type ObjectType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the resource key for the control builder object.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the resource key for the control builder.</returns>
	public string ResourceKey
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectPersistData" /> class. </summary>
	/// <param name="builder">The object for building the control.</param>
	/// <param name="builtObjects">A collection of objects that have been built by this builder.</param>
	public ObjectPersistData(ControlBuilder builder, IDictionary builtObjects)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds built objects to a collection.</summary>
	/// <param name="table">A collection for the control builder.</param>
	public void AddToObjectControlBuilderTable(IDictionary table)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the property entries with the specified filter.</summary>
	/// <param name="filter">The <see cref="P:System.Web.UI.PropertyEntry.Filter" /> on an expression.</param>
	/// <returns>The property entries with the specified filter.</returns>
	public IDictionary GetFilteredProperties(string filter)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets all property entries for the specified filter and property name.</summary>
	/// <param name="filter">The <see cref="P:System.Web.UI.PropertyEntry.Filter" /> on an expression.</param>
	/// <param name="name">The <see cref="P:System.Web.UI.PropertyEntry.Name" /> on an expression.</param>
	/// <returns>All property entries for the specified filter and property name.</returns>
	public PropertyEntry GetFilteredProperty(string filter, string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns all filtered property entries for a specified property name.</summary>
	/// <param name="name">The <see cref="P:System.Web.UI.PropertyEntry.Name" /> on an expression.</param>
	/// <returns>All filtered property entries for a specified property name.</returns>
	public ICollection GetPropertyAllFilters(string name)
	{
		throw new NotImplementedException();
	}
}
