using System.ComponentModel;

namespace System.DirectoryServices;

/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.</summary>
[AttributeUsage(AttributeTargets.All)]
public class DSDescriptionAttribute : DescriptionAttribute
{
	/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.</summary>
	/// <returns>A string that contains a description of a property or other element.  The <see cref="P:System.DirectoryServices.DSDescriptionAttribute.Description" /> property contains a description that is meaningful to the user.</returns>
	public override string Description => base.Description;

	/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.</summary>
	/// <param name="description">The description text.</param>
	public DSDescriptionAttribute(string description)
		: base(description)
	{
	}
}
