namespace System.Web.UI.WebControls.WebParts;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" /> event, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> object. </summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
public delegate void WebPartEventHandler(object sender, WebPartEventArgs e);
