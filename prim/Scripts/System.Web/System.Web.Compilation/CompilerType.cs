using System.CodeDom.Compiler;

namespace System.Web.Compilation;

/// <summary>Represents the compiler settings used within the ASP.NET build environment to generate and compile source code from a virtual path. This class cannot be inherited.</summary>
public sealed class CompilerType
{
	private Type type;

	private CompilerParameters parameters;

	/// <summary>Gets a <see cref="T:System.Type" /> for the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
	/// <returns>A read-only <see cref="T:System.Type" /> that represents the configured code provider type.</returns>
	public Type CodeDomProviderType => type;

	/// <summary>Gets the settings and options used to compile source code into an assembly.</summary>
	/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that represents the settings and options of the code compiler.</returns>
	public CompilerParameters CompilerParameters => parameters;

	internal CompilerType(Type type, CompilerParameters parameters)
	{
		this.type = type;
		this.parameters = parameters;
	}

	/// <summary>Determines whether the specified object represents the same code provider and compiler settings as the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</summary>
	/// <param name="o">The object to compare with the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Web.Compilation.CompilerType" /> object and its value is the same as this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object o)
	{
		if (!(o is CompilerType))
		{
			return false;
		}
		CompilerType compilerType = (CompilerType)o;
		if (compilerType.type == type)
		{
			return compilerType.parameters == parameters;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code for the current instance of <see cref="T:System.Web.Compilation.CompilerType" />, suitable for use in hashing algorithms and data structures, such as a hash table.</returns>
	public override int GetHashCode()
	{
		return (type.GetHashCode() << 6) ^ parameters.GetHashCode();
	}
}
