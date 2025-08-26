using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selected" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserted" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updated" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
public class ObjectDataSourceStatusEventArgs : EventArgs
{
	private object _returnValue;

	private IDictionary _outputParameters;

	private Exception _exception;

	private bool _exceptionHandled;

	private int _affectedRows = -1;

	/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
	public IDictionary OutputParameters => _outputParameters;

	/// <summary>Gets a wrapper for any exceptions that are thrown by the method that is called by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control during a data operation.</summary>
	/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the business object in its <see cref="P:System.Exception.InnerException" />.</returns>
	public Exception Exception => _exception;

	/// <summary>Gets or sets a value indicating whether an exception that was thrown by the business object has been handled.</summary>
	/// <returns>
	///     <see langword="true" /> if an exception thrown by the business object has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />; otherwise, <see langword="false" />.</returns>
	public bool ExceptionHandled
	{
		get
		{
			return _exceptionHandled;
		}
		set
		{
			_exceptionHandled = value;
		}
	}

	/// <summary>Gets the return value that is returned by the business object method, if any, as an object.</summary>
	/// <returns>An object that represents the return value returned by the business object method; otherwise, <see langword="null" />, if the business object method returns no value.</returns>
	public object ReturnValue => _returnValue;

	/// <summary>Gets or sets the number of rows that are affected by the data operation.</summary>
	/// <returns>The number of rows affected by the data operation.</returns>
	public int AffectedRows
	{
		get
		{
			return _affectedRows;
		}
		set
		{
			_affectedRows = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters and return value.</summary>
	/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
	/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
	public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters)
		: this(returnValue, outputParameters, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters, return value, and exception.</summary>
	/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
	/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
	/// <param name="exception">An <see cref="T:System.Exception" /> that wraps any internal exceptions thrown during the method call. </param>
	public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters, Exception exception)
	{
		_returnValue = returnValue;
		_outputParameters = outputParameters;
		_exception = exception;
	}
}
