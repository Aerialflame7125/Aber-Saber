using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Compilation;

internal class AssemblyPathResolver
{
	private static Dictionary<string, string> assemblyCache;

	static AssemblyPathResolver()
	{
		assemblyCache = new Dictionary<string, string>();
	}

	public static string GetAssemblyPath(string assemblyName)
	{
		lock (assemblyCache)
		{
			if (assemblyCache.ContainsKey(assemblyName))
			{
				return assemblyCache[assemblyName];
			}
			Assembly assembly = null;
			Exception innerException = null;
			if (assemblyName.IndexOf(',') != -1)
			{
				try
				{
					assembly = Assembly.Load(assemblyName);
				}
				catch (Exception ex)
				{
					innerException = ex;
				}
			}
			if (assembly == null)
			{
				try
				{
					assembly = Assembly.LoadWithPartialName(assemblyName);
				}
				catch (Exception ex2)
				{
					innerException = ex2;
				}
			}
			if (assembly == null)
			{
				throw new HttpException($"Unable to find assembly {assemblyName}", innerException);
			}
			string localPath = new Uri(assembly.CodeBase).LocalPath;
			assemblyCache.Add(assemblyName, localPath);
			return localPath;
		}
	}
}
