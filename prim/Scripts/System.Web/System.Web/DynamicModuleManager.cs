using System.Collections.Generic;

namespace System.Web;

internal sealed class DynamicModuleManager
{
	private const string moduleNameFormat = "__Module__{0}_{1}";

	private readonly List<DynamicModuleInfo> entries = new List<DynamicModuleInfo>();

	private bool entriesAreReadOnly;

	private readonly object mutex = new object();

	public void Add(Type moduleType)
	{
		if (moduleType == null)
		{
			throw new ArgumentException("moduleType");
		}
		if (!typeof(IHttpModule).IsAssignableFrom(moduleType))
		{
			throw new ArgumentException("Given object does not implement IHttpModule.", "moduleType");
		}
		lock (mutex)
		{
			if (entriesAreReadOnly)
			{
				throw new InvalidOperationException("A module was to be added to the dynamic module list, but the list was already initialized. The dynamic module list can only be initialized once.");
			}
			entries.Add(new DynamicModuleInfo(moduleType, $"__Module__{moduleType.AssemblyQualifiedName}_{Guid.NewGuid()}"));
		}
	}

	public ICollection<DynamicModuleInfo> LockAndGetModules()
	{
		lock (mutex)
		{
			entriesAreReadOnly = true;
			return entries;
		}
	}
}
