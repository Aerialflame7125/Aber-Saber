namespace System.Web.Compilation;

/// <summary>Provides flags that determine precompilation behavior.</summary>
[Flags]
public enum PrecompilationFlags
{
	/// <summary>The default value; no special behavior specified for precompilation.</summary>
	Default = 0,
	/// <summary>The deployed application will be updatable. This field corresponds to the <see langword="-u" /> switch on Aspnet_compiler.exe.</summary>
	Updatable = 1,
	/// <summary>The target directory can be overwritten. This field corresponds to the <see langword="-f" /> switch on Aspnet_compiler.exe for a previously precompiled target.</summary>
	OverwriteTarget = 2,
	/// <summary>The compiler will emit debug information. This field corresponds to the <see langword="-d" /> switch on Aspnet_compiler.exe.</summary>
	ForceDebug = 4,
	/// <summary>The application will be built "clean": Any previously compiled components will be recompiled. This field corresponds to the <see langword="-c" /> switch on Aspnet_compiler.exe.</summary>
	Clean = 8,
	/// <summary>The <see langword="/define:CodeAnalysis" /> flag will be added as a compilation symbol.</summary>
	CodeAnalysis = 0x10,
	/// <summary>An <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute is generated for the assemblies, which means the assemblies can be called by partially trusted code. The <see langword="/aptca" /> flag will be added as a compilation symbol.</summary>
	AllowPartiallyTrustedCallers = 0x20,
	/// <summary>The assembly is not fully signed when created. The assembly can be signed later by a signing tool such as Sn.exe. The <see langword="/delaysign" /> flag will be added as a compilation symbol.</summary>
	DelaySign = 0x40,
	/// <summary>The assembly is generated with fixed names for the Web pages. The files are not batched during compilation and instead are compiled individually to produce the fixed names. </summary>
	FixedNames = 0x80
}
