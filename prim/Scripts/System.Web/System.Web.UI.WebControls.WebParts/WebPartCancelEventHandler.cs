namespace System.Web.UI.WebControls.WebParts;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" /> event, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> class. </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> that contains the event data. </param>
public delegate void WebPartCancelEventHandler(object sender, WebPartCancelEventArgs e);
