using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Used by data source controls when implementing the members defined by the <see cref="T:System.ComponentModel.IListSource" /> interface. This class cannot be inherited.</summary>
public static class ListSourceHelper
{
	private sealed class ListSourceList : List<IDataSource>, ITypedList
	{
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICollection viewNames = base[0].GetViewNames();
			PropertyDescriptor[] array = new PropertyDescriptor[viewNames.Count];
			int num = 0;
			foreach (string item in viewNames)
			{
				array[num++] = new ListSourcePropertyDescriptor(item, null);
			}
			return new PropertyDescriptorCollection(array);
		}

		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}
	}

	private sealed class ListSourcePropertyDescriptor : PropertyDescriptor
	{
		public override Type ComponentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool IsReadOnly => true;

		public override Type PropertyType => typeof(IEnumerable);

		public ListSourcePropertyDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		public ListSourcePropertyDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		public ListSourcePropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		public override bool CanResetValue(object component)
		{
			throw new NotImplementedException();
		}

		public override object GetValue(object component)
		{
			if (!(component is IDataSource dataSource))
			{
				return null;
			}
			return dataSource.GetView(Name).ExecuteSelect(DataSourceSelectArguments.Empty);
		}

		public override void ResetValue(object component)
		{
			throw new NotImplementedException();
		}

		public override void SetValue(object component, object value)
		{
			throw new NotImplementedException();
		}

		public override bool ShouldSerializeValue(object component)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Indicates whether the specified data source control contains a collection of data source view objects.</summary>
	/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that specifies the data source control to test for associated data source view objects.</param>
	/// <returns>
	///     <see langword="true" /> if the data source control contains a collection of data source view objects; otherwise, <see langword="false" />.</returns>
	public static bool ContainsListCollection(IDataSource dataSource)
	{
		return dataSource.GetViewNames().Count > 0;
	}

	/// <summary>Retrieves an <see cref="T:System.Collections.IList" /> collection of data source objects.</summary>
	/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that contains one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects, which are retrieved by a call to <see cref="M:System.Web.UI.DataSourceControl.GetViewNames" />.</param>
	/// <returns>An <see cref="T:System.Collections.IList" /> of one <see cref="T:System.Web.UI.IDataSource" />, if the <see cref="T:System.Web.UI.IDataSource" /> has one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects; otherwise, returns <see langword="null" />. </returns>
	public static IList GetList(IDataSource dataSource)
	{
		if (dataSource.GetViewNames().Count == 0)
		{
			return null;
		}
		return new ListSourceList { dataSource };
	}
}
