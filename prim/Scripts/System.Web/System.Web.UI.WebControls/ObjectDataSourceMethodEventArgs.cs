using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserting" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updating" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleting" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
public class ObjectDataSourceMethodEventArgs : CancelEventArgs
{
	private IOrderedDictionary _inputParameters;

	/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
	public IOrderedDictionary InputParameters => _inputParameters;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> class using the specified input parameters collection.</summary>
	/// <param name="inputParameters">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that represent the names of the parameters of the business object method and their associated values. </param>
	public ObjectDataSourceMethodEventArgs(IOrderedDictionary inputParameters)
	{
		_inputParameters = inputParameters;
	}
}
