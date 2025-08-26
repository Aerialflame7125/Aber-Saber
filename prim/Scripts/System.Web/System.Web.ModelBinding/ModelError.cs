namespace System.Web.ModelBinding;

/// <summary>Represents an error that occurs during model binding.</summary>
[Serializable]
public class ModelError
{
	/// <summary>Gets the exception object.</summary>
	/// <returns>The exception object.</returns>
	public Exception Exception { get; private set; }

	/// <summary>Gets the error message.</summary>
	/// <returns>The error message.</returns>
	public string ErrorMessage { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception.</summary>
	/// <param name="exception">The exception.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is <see langword="null" />.</exception>
	public ModelError(Exception exception)
		: this(exception, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception and error message.</summary>
	/// <param name="exception">The exception.</param>
	/// <param name="errorMessage">The error message.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is <see langword="null" />.</exception>
	public ModelError(Exception exception, string errorMessage)
		: this(errorMessage)
	{
		if (exception == null)
		{
			throw new ArgumentNullException("exception");
		}
		Exception = exception;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified error message.</summary>
	/// <param name="errorMessage">The error message.</param>
	public ModelError(string errorMessage)
	{
		ErrorMessage = errorMessage ?? string.Empty;
	}
}
