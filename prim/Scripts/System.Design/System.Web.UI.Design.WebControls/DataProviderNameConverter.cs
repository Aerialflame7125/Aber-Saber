using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Web.UI.Design.WebControls;

/// <summary>Creates a user-selectable list of ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names.</summary>
public class DataProviderNameConverter : StringConverter
{
	/// <summary>Returns a list of the available ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the names of the available ADO.NET providers.</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
		if (factoryClasses == null)
		{
			return new StandardValuesCollection(new string[0]);
		}
		string[] array = new string[factoryClasses.Rows.Count];
		int num = 0;
		foreach (DataRow row in factoryClasses.Rows)
		{
			array[num++] = (string)row["Name"];
		}
		return new StandardValuesCollection(array);
	}

	/// <summary>Gets a value indicating whether the returned ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names are an exclusive list of possible values.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>Always <see langword="false" />.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return true;
	}

	/// <summary>Gets a value indicating whether this object returns a standard set of ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names that can be picked from a list.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>Always <see langword="true" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataProviderNameConverter" /> class.</summary>
	public DataProviderNameConverter()
	{
	}
}
