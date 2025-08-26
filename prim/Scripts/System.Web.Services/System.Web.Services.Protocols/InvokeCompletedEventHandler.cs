namespace System.Web.Services.Protocols;

/// <summary>Represents an event handler that accepts the results of asynchronously invoked Web methods. This class cannot be inherited.</summary>
/// <param name="sender">A reference to the Web service proxy.</param>
/// <param name="e">An <see cref="T:System.Web.Services.Protocols.InvokeCompletedEventArgs" /> containing the results of the method invocation.</param>
public delegate void InvokeCompletedEventHandler(object sender, InvokeCompletedEventArgs e);
