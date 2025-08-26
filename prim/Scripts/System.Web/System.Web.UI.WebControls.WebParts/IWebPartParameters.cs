using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines the contract a Web Parts control implements to pass a parameter value in a Web Parts connection.</summary>
public interface IWebPartParameters
{
	/// <summary>Gets the property descriptors for the data to be received by the consumer.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
	PropertyDescriptorCollection Schema { get; }

	/// <summary>Gets the value of the data from the connection provider.</summary>
	/// <param name="callback">The method to call to process the data from the provider.</param>
	void GetParametersData(ParametersCallback callback);

	/// <summary>Sets the property descriptors for the properties that the consumer receives when calling the <see cref="M:System.Web.UI.WebControls.WebParts.IWebPartParameters.GetParametersData(System.Web.UI.WebControls.WebParts.ParametersCallback)" /> method.</summary>
	/// <param name="schema">The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> returned by <see cref="P:System.Web.UI.WebControls.WebParts.IWebPartParameters.Schema" />.</param>
	void SetConsumerSchema(PropertyDescriptorCollection schema);
}
