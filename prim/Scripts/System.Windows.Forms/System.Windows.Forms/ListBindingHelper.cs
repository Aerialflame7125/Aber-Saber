using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms;

/// <summary>Provides functionality to discover a bindable list and the properties of the items contained in the list when they differ from the public properties of the object to which they bind.</summary>
public static class ListBindingHelper
{
	/// <summary>Returns a list associated with the specified data source.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the underlying list if it exists; otherwise, the original data source specified by <paramref name="list" />.</returns>
	/// <param name="list">The data source to examine for its underlying list.</param>
	public static object GetList(object list)
	{
		if (list is IListSource)
		{
			return ((IListSource)list).GetList();
		}
		return list;
	}

	/// <summary>Returns an object, typically a list, from the evaluation of a specified data source and optional data member.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the underlying list if it was found; otherwise, <paramref name="dataSource" />.</returns>
	/// <param name="dataSource">The data source from which to find the list.</param>
	/// <param name="dataMember">The name of the data source property that contains the list. This can be null.</param>
	/// <exception cref="T:System.ArgumentException">The specified data member name did not match any of the properties found for the data source.</exception>
	public static object GetList(object dataSource, string dataMember)
	{
		dataSource = GetList(dataSource);
		if (dataSource == null || dataMember == null || dataMember.Length == 0)
		{
			return dataSource;
		}
		PropertyDescriptor propertyDescriptor = GetListItemProperties(dataSource).Find(dataMember, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new ArgumentException("dataMember");
		}
		object obj = null;
		if (dataSource is ICurrencyManagerProvider { CurrencyManager: not null, CurrencyManager: { Count: >0, Current: not null } currencyManager })
		{
			obj = currencyManager.Current;
		}
		if (obj == null)
		{
			if (dataSource is IEnumerable)
			{
				if (dataSource is IList)
				{
					IList list = (IList)dataSource;
					obj = ((list.Count <= 0) ? null : list[0]);
				}
				else
				{
					IEnumerator enumerator = ((IEnumerable)dataSource).GetEnumerator();
					if (enumerator != null && enumerator.MoveNext())
					{
						obj = enumerator.Current;
					}
				}
			}
			else
			{
				obj = dataSource;
			}
		}
		if (obj != null)
		{
			return propertyDescriptor.GetValue(obj);
		}
		return null;
	}

	/// <summary>Returns the data type of the items in the specified list.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the items contained in the list.</returns>
	/// <param name="list">The list to be examined for type information. </param>
	public static Type GetListItemType(object list)
	{
		return GetListItemType(list, string.Empty);
	}

	/// <summary>Returns the data type of the items in the specified data source.</summary>
	/// <returns>For complex data binding, the <see cref="T:System.Type" /> of the items represented by the <paramref name="dataMember" /> in the data source; otherwise, the <see cref="T:System.Type" /> of the item in the list itself.</returns>
	/// <param name="dataSource">The data source to examine for items. </param>
	/// <param name="dataMember">The optional name of the property on the data source that is to be used as the data member. This can be null.</param>
	public static Type GetListItemType(object dataSource, string dataMember)
	{
		if (dataSource == null)
		{
			return null;
		}
		if (dataMember != null && dataMember.Length > 0)
		{
			PropertyDescriptor property = GetProperty(dataSource, dataMember);
			if (property == null)
			{
				return typeof(object);
			}
			return property.PropertyType;
		}
		if (dataSource is Array)
		{
			return dataSource.GetType().GetElementType();
		}
		if (dataSource is IEnumerable)
		{
			IEnumerator enumerator = ((IEnumerable)dataSource).GetEnumerator();
			if (enumerator.MoveNext() && enumerator.Current != null)
			{
				return enumerator.Current.GetType();
			}
			if (dataSource is IList || (object)dataSource.GetType() == typeof(IList<>))
			{
				PropertyInfo propertyByReflection = GetPropertyByReflection(dataSource.GetType(), "Item");
				if ((object)propertyByReflection != null)
				{
					return propertyByReflection.PropertyType;
				}
			}
			return typeof(object);
		}
		return dataSource.GetType();
	}

	/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a specified data source, or properties of the specified data source.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties of the items contained in <paramref name="list" />, or properties of <paramref name="list." /></returns>
	/// <param name="list">The data source to examine for property information.</param>
	public static PropertyDescriptorCollection GetListItemProperties(object list)
	{
		return GetListItemProperties(list, null);
	}

	/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a collection property of a data source. Uses the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> array to indicate which properties to examine.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the properties of the item type contained in a collection property of the data source.</returns>
	/// <param name="list">The data source to be examined for property information.</param>
	/// <param name="listAccessors">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> array describing which properties of the data source to examine. This can be null.</param>
	public static PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors)
	{
		list = GetList(list);
		if (list == null)
		{
			return new PropertyDescriptorCollection(null);
		}
		if (list is ITypedList)
		{
			return ((ITypedList)list).GetItemProperties(listAccessors);
		}
		if (listAccessors == null || listAccessors.Length == 0)
		{
			Type listItemType = GetListItemType(list);
			return TypeDescriptor.GetProperties(listItemType, new Attribute[1]
			{
				new BrowsableAttribute(browsable: true)
			});
		}
		Type propertyType = listAccessors[0].PropertyType;
		if (typeof(IList).IsAssignableFrom(propertyType) || typeof(IList<>).IsAssignableFrom(propertyType))
		{
			PropertyInfo propertyByReflection = GetPropertyByReflection(propertyType, "Item");
			return TypeDescriptor.GetProperties(propertyByReflection.PropertyType);
		}
		return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
	}

	/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in the specified data member of a data source. Uses the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> array to indicate which properties to examine.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the properties of an item type contained in a collection property of the specified data source.</returns>
	/// <param name="dataSource">The data source to be examined for property information.</param>
	/// <param name="dataMember">The optional data member to be examined for property information. This can be null.</param>
	/// <param name="listAccessors">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> array describing which properties of the data member to examine. This can be null.</param>
	/// <exception cref="T:System.ArgumentException">The specified data member could not be found in the specified data source.</exception>
	public static PropertyDescriptorCollection GetListItemProperties(object dataSource, string dataMember, PropertyDescriptor[] listAccessors)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the name of an underlying list, given a data source and optional <see cref="T:System.ComponentModel.PropertyDescriptor" /> array.</summary>
	/// <returns>The name of the list in the data source, as described by <paramref name="listAccessors" />, orthe name of the data source type.</returns>
	/// <param name="list">The data source to examine for the list name.</param>
	/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the data source. This can be null.</param>
	public static string GetListName(object list, PropertyDescriptor[] listAccessors)
	{
		if (list == null)
		{
			return string.Empty;
		}
		Type listItemType = GetListItemType(list);
		return listItemType.Name;
	}

	private static PropertyDescriptor GetProperty(object obj, string property_name)
	{
		return TypeDescriptor.GetProperties(obj, new Attribute[1]
		{
			new BrowsableAttribute(browsable: true)
		})[property_name];
	}

	private static PropertyInfo GetPropertyByReflection(Type type, string property_name)
	{
		PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		foreach (PropertyInfo propertyInfo in properties)
		{
			if (propertyInfo.Name == property_name)
			{
				return propertyInfo;
			}
		}
		return null;
	}
}
