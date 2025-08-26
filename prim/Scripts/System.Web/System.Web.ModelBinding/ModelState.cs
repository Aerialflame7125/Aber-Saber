namespace System.Web.ModelBinding;

/// <summary>Encapsulates the state of model binding.</summary>
[Serializable]
public class ModelState
{
	private ModelErrorCollection _errors = new ModelErrorCollection();

	/// <summary>Gets or sets an object that encapsulates the value that was being bound during model binding.</summary>
	/// <returns>The value of the model.</returns>
	public ValueProviderResult Value { get; set; }

	/// <summary>Gets a collection of errors that occurred during model binding.</summary>
	/// <returns>The collection of errors.</returns>
	public ModelErrorCollection Errors => _errors;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelState" /> class.</summary>
	public ModelState()
	{
	}
}
