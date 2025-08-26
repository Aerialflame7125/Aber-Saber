using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace System.Web.Compilation;

internal sealed class BuildManagerCacheItem
{
	public readonly string CompiledCustomString;

	public readonly Assembly BuiltAssembly;

	public readonly string VirtualPath;

	public readonly Type Type;

	public BuildManagerCacheItem(Assembly assembly, BuildProvider bp, CompilerResults results)
	{
		BuiltAssembly = assembly;
		CompiledCustomString = bp.GetCustomString(results);
		VirtualPath = bp.VirtualPath;
		Type = bp.GetGeneratedType(results);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("BuildCacheItem [");
		bool flag = true;
		if (!string.IsNullOrEmpty(CompiledCustomString))
		{
			stringBuilder.Append("compiledCustomString: " + CompiledCustomString);
			flag = false;
		}
		if (BuiltAssembly != null)
		{
			stringBuilder.Append((flag ? string.Empty : "; ") + "assembly: " + BuiltAssembly.ToString());
			flag = false;
		}
		if (!string.IsNullOrEmpty(VirtualPath))
		{
			stringBuilder.Append((flag ? string.Empty : "; ") + "virtualPath: " + VirtualPath);
			flag = false;
		}
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}
}
