namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines the contract a control implements to act as a configuration control for a transformer in a Web Parts connection.</summary>
public interface ITransformerConfigurationControl
{
	/// <summary>Occurs when transformer configuration is not completed.</summary>
	event EventHandler Cancelled;

	/// <summary>Occurs when transformer configuration is successfully completed. </summary>
	event EventHandler Succeeded;
}
