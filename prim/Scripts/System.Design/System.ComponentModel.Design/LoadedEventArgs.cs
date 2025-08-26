using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurface.Loaded" /> event. This class cannot be inherited.</summary>
public sealed class LoadedEventArgs : EventArgs
{
	private ICollection _errors;

	private bool _succeeded;

	/// <summary>Gets a collection of errors that occurred while the designer was loading.</summary>
	/// <returns>A collection of errors that occurred while the designer was loading.</returns>
	public ICollection Errors => _errors;

	/// <summary>Gets a value that indicates whether the designer load was successful.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer load was successful; otherwise, <see langword="false" />.</returns>
	public bool HasSucceeded => _succeeded;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.LoadedEventArgs" /> class.</summary>
	/// <param name="succeeded">
	///   <see langword="true" /> to indicate that the designer load was successful; otherwise, <see langword="false" />.</param>
	/// <param name="errors">A collection of errors that occurred while the designer was loading.</param>
	public LoadedEventArgs(bool succeeded, ICollection errors)
	{
		_succeeded = succeeded;
		_errors = errors;
	}
}
