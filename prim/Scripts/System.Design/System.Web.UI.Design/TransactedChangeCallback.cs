namespace System.Web.UI.Design;

/// <summary>A delegate that refers to a method in a custom <see cref="T:System.ComponentModel.Design.DesignerActionList" /> object that is to be called by the <see cref="Overload:System.Web.UI.Design.ControlDesigner.InvokeTransactedChange" /> method for implementing property changes in the designer's associated control.</summary>
/// <param name="context">The method to call when the transaction is invoked.</param>
/// <returns>
///   <see langword="true" /> if the transaction completed successfully; <see langword="false" /> if the transaction should be rolled back.</returns>
public delegate bool TransactedChangeCallback(object context);
