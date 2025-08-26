namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ModelDataSource.CallingDataMethods" /> event.</summary>
public class CallingDataMethodsEventArgs : EventArgs
{
	/// <summary>The type that contains the data methods to call, when the data methods are static methods.</summary>
	/// <returns>The type that contains the static data methods to call, or <see langword="null" /> if the data methods are not static methods.</returns>
	public Type DataMethodsType { get; set; }

	/// <summary>An object that contains the data methods to call, when the data methods are not static methods on a type.</summary>
	/// <returns>The instance that contains the data methods to call, or <see langword="null" /> if the data methods are static methods.</returns>
	public object DataMethodsObject { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CallingDataMethodsEventArgs" /> class.</summary>
	public CallingDataMethodsEventArgs()
	{
	}
}
