using System.Diagnostics;

namespace System.ComponentModel.Design;

/// <summary>Provides debugging services in a design-time environment.</summary>
public interface IComponentDesignerDebugService
{
	/// <summary>Gets or sets the indent level for debug output.</summary>
	/// <returns>The indent level for debug output.</returns>
	int IndentLevel { get; set; }

	/// <summary>Gets a collection of trace listeners for monitoring design-time debugging output.</summary>
	/// <returns>A collection of trace listeners</returns>
	TraceListenerCollection Listeners { get; }

	/// <summary>Asserts on a condition inside a design-time environment.</summary>
	/// <param name="condition">
	///   <see langword="true" /> to prevent <paramref name="message" /> from being displayed; otherwise, <see langword="false" />.</param>
	/// <param name="message">The message to display.</param>
	void Assert(bool condition, string message);

	/// <summary>Logs a failure message inside a design-time environment.</summary>
	/// <param name="message">The message to log.</param>
	void Fail(string message);

	/// <summary>Logs a debug message inside a design-time environment.</summary>
	/// <param name="message">The message to log.</param>
	/// <param name="category">The category of <paramref name="message" />.</param>
	void Trace(string message, string category);
}
