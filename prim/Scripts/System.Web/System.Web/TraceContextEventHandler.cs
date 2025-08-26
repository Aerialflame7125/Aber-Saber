namespace System.Web;

/// <summary>Represents the method that handles the <see cref="E:System.Web.TraceContext.TraceFinished" /> event of a <see cref="T:System.Web.TraceContext" /> object. </summary>
/// <param name="sender">The event source (the <see cref="T:System.Web.TraceContext" />). </param>
/// <param name="e">A <see cref="T:System.Web.TraceContextEventArgs" /> that contains the event data. </param>
public delegate void TraceContextEventHandler(object sender, TraceContextEventArgs e);
