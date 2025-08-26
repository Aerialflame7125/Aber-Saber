using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design;

/// <summary>Imports ActiveX controls and generates a wrapper that can be accessed by a designer.</summary>
[System.MonoTODO]
public class AxImporter
{
	/// <summary>Represents a set of options for an <see cref="T:System.Windows.Forms.Design.AxImporter" />.</summary>
	public sealed class Options
	{
		/// <summary>Specifies whether the generated assembly is strongly named and will be signed later.</summary>
		[System.MonoTODO]
		public bool delaySign;

		/// <summary>Specifies whether sources for the type library wrapper should be generated.</summary>
		[System.MonoTODO]
		public bool genSources;

		/// <summary>Specifies the path to the file that contains the strong name key container for the generated assemblies.</summary>
		[System.MonoTODO]
		public string keyContainer;

		/// <summary>Specifies the path to the file that contains the strong name key for the generated assemblies.</summary>
		[System.MonoTODO]
		public string keyFile;

		/// <summary>Specifies the strong name used for the generated assemblies.</summary>
		[System.MonoTODO]
		public StrongNameKeyPair keyPair;

		/// <summary>Indicates whether the ActiveX importer tool logo will be displayed when the control is imported.</summary>
		[System.MonoTODO]
		public bool noLogo;

		/// <summary>Specifies the path to the directory that the generated assemblies will be created in.</summary>
		[System.MonoTODO]
		public string outputDirectory;

		/// <summary>Specifies the filename to generate the ActiveX control wrapper to.</summary>
		[System.MonoTODO]
		public string outputName;

		/// <summary>Specifies whether to overwrite existing files when generating assemblies.</summary>
		[System.MonoTODO]
		public bool overwriteRCW;

		/// <summary>Specifies the public key used to sign the generated assemblies.</summary>
		[System.MonoTODO]
		public byte[] publicKey;

		/// <summary>Specifies the <see cref="T:System.Windows.Forms.Design.AxImporter.IReferenceResolver" /> to use to resolve types and references when generating assemblies.</summary>
		[System.MonoTODO]
		public IReferenceResolver references;

		/// <summary>Specifies whether to compile in silent mode, which generates less displayed information at compile time.</summary>
		[System.MonoTODO]
		public bool silentMode;

		/// <summary>Specifies whether to compile in verbose mode, which generates more displayed information at compile time.</summary>
		[System.MonoTODO]
		public bool verboseMode;

		/// <summary>Specifies whether errors are output in the Microsoft Build Engine (MSBuild) format.</summary>
		[System.MonoTODO]
		public bool msBuildErrors;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxImporter.Options" /> class.</summary>
		public Options()
		{
		}
	}

	/// <summary>Provides methods to resolve references to ActiveX libraries, COM type libraries or assemblies, or managed assemblies.</summary>
	public interface IReferenceResolver
	{
		/// <summary>Resolves a reference to the specified type library that contains an ActiveX control.</summary>
		/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> to resolve a reference to.</param>
		/// <returns>A fully qualified path to an assembly.</returns>
		string ResolveActiveXReference(UCOMITypeLib typeLib);

		/// <summary>Resolves a reference to the specified assembly that contains a COM component.</summary>
		/// <param name="name">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly to resolve a reference to.</param>
		/// <returns>A fully qualified path to an assembly.</returns>
		string ResolveComReference(AssemblyName name);

		/// <summary>Resolves a reference to the specified type library that contains an COM component.</summary>
		/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> to resolve a reference to.</param>
		/// <returns>A fully qualified path to an assembly.</returns>
		string ResolveComReference(UCOMITypeLib typeLib);

		/// <summary>Resolves a reference to the specified assembly.</summary>
		/// <param name="assemName">The name of the assembly to resolve a reference to.</param>
		/// <returns>A fully qualified path to an assembly.</returns>
		string ResolveManagedReference(string assemName);
	}

	internal Options options;

	/// <summary>Gets the names of the assemblies that are generated for the control.</summary>
	/// <returns>An array of names of the generated assemblies, or an empty string array if no assemblies have been generated.</returns>
	[System.MonoTODO]
	public string[] GeneratedAssemblies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the names of the source files that were generated.</summary>
	/// <returns>An array of file names of the generated source files, or <see langword="null" /> if none exist.</returns>
	[System.MonoTODO]
	public string[] GeneratedSources
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the attributes for the generated type library.</summary>
	/// <returns>An array of type <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> that indicates the attributes for the generated type library.</returns>
	[System.MonoTODO]
	public TYPELIBATTR[] GeneratedTypeLibAttributes
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxImporter" /> class.</summary>
	/// <param name="options">An <see cref="T:System.Windows.Forms.Design.AxImporter.Options" /> that indicates the options for the ActiveX control importer to use.</param>
	[System.MonoTODO]
	public AxImporter(Options options)
	{
		this.options = options;
	}

	/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
	/// <param name="file">A <see cref="T:System.IO.FileInfo" /> indicating the file that contains the control.</param>
	/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
	/// <exception cref="T:System.Exception">A type library could not be loaded from <paramref name="file" />.</exception>
	[System.MonoTODO]
	public string GenerateFromFile(FileInfo file)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
	/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that indicates the type library to generate the control from.</param>
	/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
	/// <exception cref="T:System.Exception">No registered ActiveX control was found in <paramref name="typeLib" />.</exception>
	[System.MonoTODO]
	public string GenerateFromTypeLibrary(UCOMITypeLib typeLib)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a wrapper for an ActiveX control for use in the design-time environment.</summary>
	/// <param name="typeLib">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that indicates the type library to generate the control from.</param>
	/// <param name="clsid">The <see cref="T:System.Guid" /> for the control wrapper.</param>
	/// <returns>An assembly qualified name for the type of ActiveX control for which a wrapper was generated.</returns>
	/// <exception cref="T:System.Exception">No registered ActiveX control was found in <paramref name="typeLib" />.</exception>
	[System.MonoTODO]
	public string GenerateFromTypeLibrary(UCOMITypeLib typeLib, Guid clsid)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the path and file name to the specified type library.</summary>
	/// <param name="tlibattr">A <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> that indicates the type library to retrieve the file name of.</param>
	/// <returns>The path and file name to the specified type library, or <see langword="null" /> if the library could not be located.</returns>
	[System.MonoTODO]
	public static string GetFileOfTypeLib(ref TYPELIBATTR tlibattr)
	{
		throw new NotImplementedException();
	}
}
