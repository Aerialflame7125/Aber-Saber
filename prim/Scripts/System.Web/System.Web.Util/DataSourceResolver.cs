using System.Collections;
using System.ComponentModel;

namespace System.Web.Util;

internal class DataSourceResolver
{
	private DataSourceResolver()
	{
	}

	public static IEnumerable ResolveDataSource(object o, string data_member)
	{
		IEnumerable enumerable = o as IEnumerable;
		if (enumerable != null)
		{
			return enumerable;
		}
		if (!(o is IListSource listSource))
		{
			return null;
		}
		IList list = listSource.GetList();
		if (!listSource.ContainsListCollection)
		{
			return list;
		}
		if (!(list is ITypedList typedList))
		{
			return null;
		}
		PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
		if (itemProperties == null || itemProperties.Count == 0)
		{
			throw new HttpException("The selected data source did not contain any data members to bind to");
		}
		PropertyDescriptor propertyDescriptor = ((data_member == "") ? itemProperties[0] : itemProperties.Find(data_member, ignoreCase: true));
		if (propertyDescriptor != null)
		{
			enumerable = propertyDescriptor.GetValue(list[0]) as IEnumerable;
		}
		if (enumerable == null)
		{
			throw new HttpException("A list corresponding to the selected DataMember was not found");
		}
		return enumerable;
	}
}
