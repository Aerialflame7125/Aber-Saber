namespace System.Web.UI;

/// <summary>Allows access to the collection of data-binding expressions on a control at design time.</summary>
public interface IDataBindingsAccessor
{
	/// <summary>Gets a collection of all data bindings on the control. This property is read-only.</summary>
	/// <returns>The collection of data bindings.</returns>
	DataBindingCollection DataBindings { get; }

	/// <summary>Gets a value indicating whether the control contains any data-binding logic.</summary>
	/// <returns>
	///     <see langword="true" /> if the control contains data binding logic.</returns>
	bool HasDataBindings { get; }
}
