using System.Collections.ObjectModel;

namespace System.Web.ModelBinding;

/// <summary>Provides a container for model validation errors.</summary>
[Serializable]
public class ModelErrorCollection : Collection<ModelError>
{
	/// <summary>Adds a validation error to the collection using the specified exception.</summary>
	/// <param name="exception">The exception.</param>
	public void Add(Exception exception)
	{
		Add(new ModelError(exception));
	}

	/// <summary>Adds a validation error to the collection using the specified error message string.</summary>
	/// <param name="errorMessage">The error message string.</param>
	public void Add(string errorMessage)
	{
		Add(new ModelError(errorMessage));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelErrorCollection" /> class.</summary>
	public ModelErrorCollection()
	{
	}
}
