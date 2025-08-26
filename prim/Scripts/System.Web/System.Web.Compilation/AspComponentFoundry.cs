using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AspComponentFoundry
{
	private abstract class Foundry
	{
		private bool _fromConfig;

		public bool FromConfig
		{
			get
			{
				return _fromConfig;
			}
			set
			{
				_fromConfig = value;
			}
		}

		public abstract Type GetType(string componentName, out string source, out string ns);
	}

	private class TagNameFoundry : Foundry
	{
		private string tagName;

		private Type type;

		private string source;

		public bool FromWebConfig => source != null;

		public string TagName => tagName;

		public TagNameFoundry(string tagName, string source)
		{
			this.tagName = tagName;
			this.source = source;
		}

		public TagNameFoundry(string tagName, Type type)
		{
			this.tagName = tagName;
			this.type = type;
		}

		public override Type GetType(string componentName, out string source, out string ns)
		{
			source = null;
			ns = null;
			if (string.Compare(componentName, tagName, ignoreCase: true, Helpers.InvariantCulture) != 0)
			{
				return null;
			}
			source = this.source;
			return LoadType();
		}

		private Type LoadType()
		{
			if (type != null)
			{
				return type;
			}
			HttpContext current = HttpContext.Current;
			string virtualPath;
			string text;
			if (VirtualPathUtility.IsAppRelative(source))
			{
				virtualPath = source;
				text = current.Request.MapPath(source);
			}
			else
			{
				virtualPath = VirtualPathUtility.ToAppRelative(source);
				text = source;
			}
			if ((type = CachingCompiler.GetTypeFromCache(text)) != null)
			{
				return type;
			}
			type = BuildManager.GetCompiledType(virtualPath);
			if (type != null)
			{
				AspGenerator.AddTypeToCache(null, text, type);
				BuildManager.AddToReferencedAssemblies(type.Assembly);
			}
			return type;
		}
	}

	private class AssemblyFoundry : Foundry
	{
		private string nameSpace;

		private Assembly assembly;

		private string assemblyName;

		private Dictionary<string, Assembly> assemblyCache;

		public AssemblyFoundry(Assembly assembly, string nameSpace)
		{
			this.assembly = assembly;
			this.nameSpace = nameSpace;
			if (assembly != null)
			{
				assemblyName = assembly.FullName;
			}
			else
			{
				assemblyName = null;
			}
		}

		public AssemblyFoundry(string assemblyName, string nameSpace)
		{
			assembly = null;
			this.nameSpace = nameSpace;
			this.assemblyName = assemblyName;
		}

		public override Type GetType(string componentName, out string source, out string ns)
		{
			source = null;
			ns = nameSpace;
			if (this.assembly == null && assemblyName != null)
			{
				this.assembly = GetAssemblyByName(assemblyName, throwOnMissing: true);
			}
			string name = nameSpace + "." + componentName;
			if (this.assembly != null)
			{
				return this.assembly.GetType(name, throwOnError: false, ignoreCase: true);
			}
			IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
			if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
			{
				Type type = null;
				foreach (Assembly item in topLevelAssemblies)
				{
					if (!(item == null))
					{
						type = item.GetType(name, throwOnError: false, ignoreCase: true);
						if (type != null)
						{
							return type;
						}
					}
				}
			}
			return null;
		}

		private Assembly GetAssemblyByName(string name, bool throwOnMissing)
		{
			if (assemblyCache == null)
			{
				assemblyCache = new Dictionary<string, Assembly>();
			}
			if (assemblyCache.ContainsKey(name))
			{
				return assemblyCache[name];
			}
			Assembly assembly = null;
			Exception innerException = null;
			if (name.IndexOf(',') != -1)
			{
				try
				{
					assembly = Assembly.Load(name);
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
					assembly = Assembly.LoadWithPartialName(name);
				}
				catch (Exception ex2)
				{
					innerException = ex2;
				}
			}
			if (assembly == null)
			{
				if (throwOnMissing)
				{
					throw new HttpException("Assembly " + name + " not found", innerException);
				}
				return null;
			}
			assemblyCache.Add(name, assembly);
			return assembly;
		}
	}

	private class CompoundFoundry : Foundry
	{
		private AssemblyFoundry assemblyFoundry;

		private Hashtable tagnames;

		private string tagPrefix;

		public CompoundFoundry(string tagPrefix)
		{
			this.tagPrefix = tagPrefix;
			tagnames = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}

		public void Add(Foundry foundry)
		{
			if (foundry is AssemblyFoundry)
			{
				assemblyFoundry = (AssemblyFoundry)foundry;
				return;
			}
			TagNameFoundry tagNameFoundry = (TagNameFoundry)foundry;
			string tagName = tagNameFoundry.TagName;
			if (tagnames.Contains(tagName))
			{
				if (!tagNameFoundry.FromWebConfig)
				{
					throw new ApplicationException($"{tagPrefix}:{tagName} already registered.");
				}
			}
			else
			{
				tagnames.Add(tagName, foundry);
			}
		}

		public override Type GetType(string componentName, out string source, out string ns)
		{
			source = null;
			ns = null;
			if (tagnames[componentName] is Foundry foundry)
			{
				return foundry.GetType(componentName, out source, out ns);
			}
			if (assemblyFoundry != null)
			{
				try
				{
					return assemblyFoundry.GetType(componentName, out source, out ns);
				}
				catch
				{
				}
			}
			throw new ApplicationException($"Type {componentName} not registered for prefix {tagPrefix}");
		}
	}

	private Hashtable foundries;

	private Dictionary<string, AspComponent> components;

	private Dictionary<string, AspComponent> Components
	{
		get
		{
			if (components == null)
			{
				components = new Dictionary<string, AspComponent>(StringComparer.OrdinalIgnoreCase);
			}
			return components;
		}
	}

	public AspComponentFoundry()
	{
		foundries = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		Assembly assembly = typeof(AspComponentFoundry).Assembly;
		RegisterFoundry("asp", assembly, "System.Web.UI.WebControls");
		RegisterFoundry("", "object", typeof(ObjectTag));
		RegisterConfigControls();
	}

	public AspComponent GetComponent(string tagName)
	{
		if (tagName == null || tagName.Length == 0)
		{
			return null;
		}
		if (components != null && components.TryGetValue(tagName, out var value))
		{
			return value;
		}
		int num = tagName.IndexOf(':');
		string text;
		string tag;
		if (num > -1)
		{
			if (num == 0)
			{
				throw new Exception("Empty TagPrefix is not valid.");
			}
			if (num + 1 == tagName.Length)
			{
				return null;
			}
			text = tagName.Substring(0, num);
			tag = tagName.Substring(num + 1);
		}
		else
		{
			text = string.Empty;
			tag = tagName;
		}
		object obj = foundries[text];
		if (obj == null)
		{
			return null;
		}
		if (obj is Foundry foundry)
		{
			return CreateComponent(foundry, tagName, text, tag);
		}
		if (!(obj is ArrayList arrayList))
		{
			return null;
		}
		AspComponent aspComponent = null;
		Exception ex = null;
		foreach (Foundry item in arrayList)
		{
			try
			{
				aspComponent = CreateComponent(item, tagName, text, tag);
				if (aspComponent != null)
				{
					return aspComponent;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
		}
		if (ex != null)
		{
			throw ex;
		}
		return null;
	}

	private AspComponent CreateComponent(Foundry foundry, string tagName, string prefix, string tag)
	{
		string source;
		string ns;
		Type type = foundry.GetType(tag, out source, out ns);
		if (type == null)
		{
			return null;
		}
		AspComponent aspComponent = new AspComponent(type, ns, prefix, source, foundry.FromConfig);
		Components.Add(tagName, aspComponent);
		return aspComponent;
	}

	public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace)
	{
		RegisterFoundry(foundryName, assembly, nameSpace, fromConfig: false);
	}

	public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace, bool fromConfig)
	{
		AssemblyFoundry assemblyFoundry = new AssemblyFoundry(assembly, nameSpace);
		assemblyFoundry.FromConfig = fromConfig;
		InternalRegister(foundryName, assemblyFoundry, fromConfig);
	}

	public void RegisterFoundry(string foundryName, string tagName, Type type)
	{
		RegisterFoundry(foundryName, tagName, type, fromConfig: false);
	}

	public void RegisterFoundry(string foundryName, string tagName, Type type, bool fromConfig)
	{
		TagNameFoundry tagNameFoundry = new TagNameFoundry(tagName, type);
		tagNameFoundry.FromConfig = fromConfig;
		InternalRegister(foundryName, tagNameFoundry, fromConfig);
	}

	public void RegisterFoundry(string foundryName, string tagName, string source)
	{
		RegisterFoundry(foundryName, tagName, source, fromConfig: false);
	}

	public void RegisterFoundry(string foundryName, string tagName, string source, bool fromConfig)
	{
		TagNameFoundry tagNameFoundry = new TagNameFoundry(tagName, source);
		tagNameFoundry.FromConfig = fromConfig;
		InternalRegister(foundryName, tagNameFoundry, fromConfig);
	}

	public void RegisterAssemblyFoundry(string foundryName, string assemblyName, string nameSpace, bool fromConfig)
	{
		AssemblyFoundry assemblyFoundry = new AssemblyFoundry(assemblyName, nameSpace);
		assemblyFoundry.FromConfig = fromConfig;
		InternalRegister(foundryName, assemblyFoundry, fromConfig);
	}

	private void RegisterConfigControls()
	{
		if (!(WebConfigurationManager.GetSection("system.web/pages") is PagesSection { Controls: { Count: not 0 } controls }))
		{
			return;
		}
		IList codeAssemblies = BuildManager.CodeAssemblies;
		bool flag = codeAssemblies != null && codeAssemblies.Count > 0;
		foreach (TagPrefixInfo item in controls)
		{
			if (!string.IsNullOrEmpty(item.TagName))
			{
				RegisterFoundry(item.TagPrefix, item.TagName, item.Source, fromConfig: true);
			}
			else if (string.IsNullOrEmpty(item.Assembly))
			{
				if (!flag)
				{
					continue;
				}
				foreach (object item2 in codeAssemblies)
				{
					Assembly assembly = item2 as Assembly;
					if (!(assembly == null))
					{
						RegisterFoundry(item.TagPrefix, assembly, item.Namespace, fromConfig: true);
					}
				}
			}
			else if (!string.IsNullOrEmpty(item.Namespace))
			{
				RegisterAssemblyFoundry(item.TagPrefix, item.Assembly, item.Namespace, fromConfig: true);
			}
		}
	}

	private void InternalRegister(string foundryName, Foundry foundry, bool fromConfig)
	{
		object obj = foundries[foundryName];
		Foundry foundry2 = null;
		if (obj is CompoundFoundry)
		{
			((CompoundFoundry)obj).Add(foundry);
			return;
		}
		if (obj == null || obj is ArrayList || (obj is AssemblyFoundry && foundry is AssemblyFoundry))
		{
			foundry2 = foundry;
		}
		else if (obj != null)
		{
			CompoundFoundry compoundFoundry = new CompoundFoundry(foundryName);
			compoundFoundry.Add((Foundry)obj);
			compoundFoundry.Add(foundry);
			foundry2 = foundry;
			foundry2.FromConfig = fromConfig;
		}
		if (foundry2 == null)
		{
			return;
		}
		if (obj == null)
		{
			foundries[foundryName] = foundry2;
			return;
		}
		ArrayList arrayList = obj as ArrayList;
		if (arrayList == null)
		{
			arrayList = new ArrayList(2);
			arrayList.Add(obj);
			foundries[foundryName] = arrayList;
		}
		if (foundry2 is AssemblyFoundry)
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (arrayList[i] is AssemblyFoundry)
				{
					arrayList.Insert(i, foundry2);
					return;
				}
			}
			arrayList.Add(foundry2);
		}
		else
		{
			arrayList.Insert(0, foundry2);
		}
	}

	public bool LookupFoundry(string foundryName)
	{
		return foundries.Contains(foundryName);
	}
}
