using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectDisposing" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
public class ObjectDataSourceDisposingEventArgs : CancelEventArgs
{
	private object _objectInstance;

	/// <summary>Gets an object that represents the business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs data operations.</summary>
	/// <returns>The business object the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to data operations; otherwise, <see langword="null" />, if <see langword="null" /> is passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" />.</returns>
	public object ObjectInstance => _objectInstance;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> class using the specified object.</summary>
	/// <param name="objectInstance">The business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> interacts to perform data operations.</param>
	public ObjectDataSourceDisposingEventArgs(object objectInstance)
	{
		_objectInstance = objectInstance;
	}
}
