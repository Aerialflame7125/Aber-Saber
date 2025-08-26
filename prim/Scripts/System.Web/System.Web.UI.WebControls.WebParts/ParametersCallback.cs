using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>References the method to call when retrieving row data from a provider.</summary>
/// <param name="parametersData">The data to retrieve from the provider. </param>
public delegate void ParametersCallback(IDictionary parametersData);
