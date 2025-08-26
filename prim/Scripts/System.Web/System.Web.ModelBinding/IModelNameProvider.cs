namespace System.Web.ModelBinding;

/// <summary>Provides a way to specify an alternate name to use for model binding instead of using the parameter name.</summary>
public interface IModelNameProvider
{
	/// <summary>When implemented in a class, gets the model name.</summary>
	/// <returns>The model name.</returns>
	string GetModelName();
}
