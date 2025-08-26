namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectCreating" /> and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectCreated" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
public class ObjectDataSourceEventArgs : EventArgs
{
	private object _objectInstance;

	/// <summary>Gets or sets an object that represents the business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs data operations.</summary>
	/// <returns>The business object the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to perform data operations; otherwise, <see langword="null" />, if <see langword="null" /> is passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" />.</returns>
	public object ObjectInstance
	{
		get
		{
			return _objectInstance;
		}
		set
		{
			_objectInstance = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> class using the specified object.</summary>
	/// <param name="objectInstance">The business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> interacts to perform data operations.</param>
	public ObjectDataSourceEventArgs(object objectInstance)
	{
		_objectInstance = objectInstance;
	}
}
