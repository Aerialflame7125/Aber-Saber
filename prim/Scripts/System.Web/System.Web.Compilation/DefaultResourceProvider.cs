using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace System.Web.Compilation;

internal sealed class DefaultResourceProvider : IResourceProvider
{
	private sealed class ResourceManagerCacheKey
	{
		private readonly string _name;

		private readonly Assembly _asm;

		public ResourceManagerCacheKey(string name, Assembly asm)
		{
			_name = name;
			_asm = asm;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ResourceManagerCacheKey))
			{
				return false;
			}
			ResourceManagerCacheKey resourceManagerCacheKey = (ResourceManagerCacheKey)obj;
			if (resourceManagerCacheKey._asm == _asm)
			{
				return _name.Equals(resourceManagerCacheKey._name, StringComparison.Ordinal);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode() + _asm.GetHashCode();
		}
	}

	[ThreadStatic]
	private static Dictionary<ResourceManagerCacheKey, ResourceManager> resourceManagerCache;

	private string resource;

	private bool isGlobal;

	public IResourceReader ResourceReader
	{
		get
		{
			Assembly assembly;
			string name;
			if (isGlobal)
			{
				assembly = HttpContext.AppGlobalResourcesAssembly;
				name = resource;
			}
			else
			{
				assembly = GetLocalResourcesAssembly();
				name = Path.GetFileName(resource);
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
				name += ".resources";
			}
			if (assembly == null)
			{
				return null;
			}
			Stream manifestResourceStream = assembly.GetManifestResourceStream(name);
			if (manifestResourceStream == null)
			{
				return null;
			}
			return new ResourceReader(manifestResourceStream);
		}
	}

	public DefaultResourceProvider(string resource, bool isGlobal)
	{
		if (string.IsNullOrEmpty(resource))
		{
			throw new ArgumentNullException("resource");
		}
		this.resource = resource;
		this.isGlobal = isGlobal;
	}

	public object GetObject(string resourceKey, CultureInfo culture)
	{
		if (string.IsNullOrEmpty(resourceKey))
		{
			return null;
		}
		return GetResourceManager()?.GetObject(resourceKey, culture);
	}

	private Assembly GetLocalResourcesAssembly()
	{
		string directory = VirtualPathUtility.GetDirectory(resource);
		Assembly assembly = AppResourcesCompiler.GetCachedLocalResourcesAssembly(directory);
		if (assembly == null)
		{
			assembly = new AppResourcesCompiler(directory).Compile();
			if (assembly == null)
			{
				throw new MissingManifestResourceException("A resource object was not found at the specified virtualPath.");
			}
		}
		return assembly;
	}

	private ResourceManager GetResourceManager()
	{
		Assembly assembly;
		string fileName;
		if (isGlobal)
		{
			assembly = HttpContext.AppGlobalResourcesAssembly;
			fileName = resource;
		}
		else
		{
			assembly = GetLocalResourcesAssembly();
			fileName = Path.GetFileName(resource);
			if (string.IsNullOrEmpty(fileName))
			{
				return null;
			}
		}
		if (assembly == null)
		{
			return null;
		}
		try
		{
			if (resourceManagerCache == null)
			{
				resourceManagerCache = new Dictionary<ResourceManagerCacheKey, ResourceManager>();
			}
			ResourceManagerCacheKey key = new ResourceManagerCacheKey(fileName, assembly);
			if (!resourceManagerCache.TryGetValue(key, out var value))
			{
				value = new ResourceManager(fileName, assembly);
				value.IgnoreCase = true;
				resourceManagerCache.Add(key, value);
			}
			return value;
		}
		catch (MissingManifestResourceException)
		{
			throw;
		}
		catch (Exception innerException)
		{
			throw new HttpException("Failed to retrieve the specified global resource object.", innerException);
		}
	}
}
