using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>References the method to call when retrieving table data from a provider.</summary>
/// <param name="tableData">The data to retrieve from the provider.</param>
public delegate void TableCallback(ICollection tableData);
