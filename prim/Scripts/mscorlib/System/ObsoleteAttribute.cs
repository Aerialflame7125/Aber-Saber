using System.Runtime.InteropServices;

namespace System;

/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
[ComVisible(true)]
public sealed class ObsoleteAttribute : Attribute
{
	private string _message;

	private bool _error;

	/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
	/// <returns>The workaround text string.</returns>
	public string Message => _message;

	/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
	/// <returns>
	///   <see langword="true" /> if the obsolete element usage is considered an error; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool IsError => _error;

	/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
	public ObsoleteAttribute()
	{
		_message = null;
		_error = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
	/// <param name="message">The text string that describes alternative workarounds.</param>
	public ObsoleteAttribute(string message)
	{
		_message = message;
		_error = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
	/// <param name="message">The text string that describes alternative workarounds.</param>
	/// <param name="error">
	///   <see langword="true" /> if the obsolete element usage generates a compiler error; <see langword="false" /> if it generates a compiler warning.</param>
	public ObsoleteAttribute(string message, bool error)
	{
		_message = message;
		_error = error;
	}
}
