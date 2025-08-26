using System.Collections;

namespace System.Web.Compilation;

/// <summary>Represents dependencies returned by the build manager.</summary>
public sealed class BuildDependencySet
{
	/// <summary>Gets a string representing the hash code of the dependent virtual paths.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the hash code of the dependent virtual paths.</returns>
	[MonoTODO("Not implemented")]
	public string HashCode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a list of virtual path dependencies.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> containing the virtual path dependencies.</returns>
	[MonoTODO("Not implemented")]
	public IEnumerable VirtualPaths
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	internal BuildDependencySet()
	{
	}
}
