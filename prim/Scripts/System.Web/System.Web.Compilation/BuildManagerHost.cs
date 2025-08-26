using System.Web.Hosting;

namespace System.Web.Compilation;

internal class BuildManagerHost : MarshalByRefObject, IRegisteredObject
{
	protected void RegisterAssembly(string assemblyName, string assemblyLocation)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(assemblyLocation))
		{
			HttpRuntime.RegisteredAssemblies.InsertOrUpdate((uint)assemblyName.GetHashCode(), assemblyName, assemblyLocation, assemblyLocation);
			HttpRuntime.EnableAssemblyMapping(enable: true);
		}
	}

	public void Stop(bool immediate)
	{
	}
}
