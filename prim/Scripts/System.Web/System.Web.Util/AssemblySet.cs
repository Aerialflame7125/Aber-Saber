using System.Collections;

namespace System.Web.Util;

internal class AssemblySet : ObjectSet
{
	internal AssemblySet()
	{
	}

	internal static AssemblySet Create(ICollection c)
	{
		AssemblySet assemblySet = new AssemblySet();
		assemblySet.AddCollection(c);
		return assemblySet;
	}
}
