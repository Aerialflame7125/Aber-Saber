using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Caching;

/// <summary>Establishes a dependency relationship between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and a file, cache key, an array of either, or another <see cref="T:System.Web.Caching.CacheDependency" /> object. The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CacheDependency : IDisposable
{
	private static readonly object dependencyChangedEvent = new object();

	private string[] cachekeys;

	private CacheDependency dependency;

	private DateTime start;

	private Cache cache;

	private FileSystemWatcher[] watchers;

	private bool hasChanged;

	private bool used;

	private DateTime utcLastModified;

	private object locker = new object();

	private EventHandlerList events = new EventHandlerList();

	internal bool IsUsed => used;

	internal DateTime Start
	{
		get
		{
			return start;
		}
		set
		{
			start = value;
		}
	}

	/// <summary>Gets the time when the dependency was last changed.</summary>
	/// <returns>The time when the dependency was last changed.</returns>
	public DateTime UtcLastModified => utcLastModified;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Caching.CacheDependency" /> object has changed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool HasChanged
	{
		get
		{
			if (hasChanged)
			{
				return true;
			}
			if (DateTime.Now < start)
			{
				return false;
			}
			if (cache != null && cachekeys != null)
			{
				string[] array = cachekeys;
				foreach (string key in array)
				{
					if (cache.GetKeyLastChange(key) > start)
					{
						hasChanged = true;
						break;
					}
				}
			}
			if (hasChanged)
			{
				DisposeWatchers();
			}
			return hasChanged;
		}
	}

	internal event EventHandler DependencyChanged
	{
		add
		{
			events.AddHandler(dependencyChangedEvent, value);
		}
		remove
		{
			events.RemoveHandler(dependencyChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
	protected CacheDependency()
		: this(null, null, null, DateTime.Now)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
	/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
	public CacheDependency(string filename)
		: this(new string[1] { filename }, null, null, DateTime.Now)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	public CacheDependency(string[] filenames)
		: this(filenames, null, null, DateTime.Now)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors a file or directory for changes.</summary>
	/// <param name="filename">The path to a file or directory that the cached object is dependent upon. When this resource changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="start">The time against which to check the last modified date of the directory or file. </param>
	public CacheDependency(string filename, DateTime start)
		: this(new string[1] { filename }, null, null, start)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories) for changes and specifies a time when change monitoring begins.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="start">The time against which to check the last modified date of the objects in the array. </param>
	public CacheDependency(string[] filenames, DateTime start)
		: this(filenames, null, null, start)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
	public CacheDependency(string[] filenames, string[] cachekeys)
		: this(filenames, cachekeys, null, DateTime.Now)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon a separate instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
	/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
	public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency)
		: this(filenames, cachekeys, dependency, DateTime.Now)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
	/// <param name="start">The date and time against which to check the last modified date of the objects passed in the <paramref name="filenames" /> and <paramref name="cachekeys" /> arrays. </param>
	public CacheDependency(string[] filenames, string[] cachekeys, DateTime start)
		: this(filenames, cachekeys, null, start)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that monitors an array of paths (to files or directories), an array of cache keys, or both for changes. It also makes itself dependent upon another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class and a time when the change monitoring begins.</summary>
	/// <param name="filenames">An array of paths (to files or directories) that the cached object is dependent upon. When any of these resources changes, the cached object becomes obsolete and is removed from the cache. </param>
	/// <param name="cachekeys">An array of cache keys that the new object monitors for changes. When any of these cache keys changes, the cached object associated with this dependency object becomes obsolete and is removed from the cache. </param>
	/// <param name="dependency">Another instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class that this instance is dependent upon. </param>
	/// <param name="start">The time against which to check the last modified date of the objects in the arrays and the <see cref="T:System.Web.Caching.CacheDependency" /> object. </param>
	public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency, DateTime start)
	{
		int num = ((filenames != null) ? filenames.Length : 0);
		if (num > 0)
		{
			watchers = new FileSystemWatcher[num];
			for (int i = 0; i < num; i++)
			{
				string text = filenames[i];
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
				if (Directory.Exists(text))
				{
					fileSystemWatcher.Path = text;
				}
				else
				{
					string directoryName = Path.GetDirectoryName(text);
					if (directoryName == null || !Directory.Exists(directoryName))
					{
						continue;
					}
					fileSystemWatcher.Path = directoryName;
					fileSystemWatcher.Filter = Path.GetFileName(text);
				}
				fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
				fileSystemWatcher.Created += OnChanged;
				fileSystemWatcher.Changed += OnChanged;
				fileSystemWatcher.Deleted += OnChanged;
				fileSystemWatcher.Renamed += OnChanged;
				fileSystemWatcher.EnableRaisingEvents = true;
				watchers[i] = fileSystemWatcher;
			}
		}
		this.cachekeys = cachekeys;
		this.dependency = dependency;
		if (dependency != null)
		{
			dependency.DependencyChanged += OnChildDependencyChanged;
		}
		this.start = start;
		FinishInit();
	}

	/// <summary>Retrieves a unique identifier for a <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
	/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.</returns>
	public virtual string GetUniqueID()
	{
		StringBuilder stringBuilder = new StringBuilder();
		lock (locker)
		{
			if (watchers != null)
			{
				FileSystemWatcher[] array = watchers;
				foreach (FileSystemWatcher fileSystemWatcher in array)
				{
					if (fileSystemWatcher != null && fileSystemWatcher.Path != null && fileSystemWatcher.Path.Length != 0)
					{
						stringBuilder.Append("_" + fileSystemWatcher.Path);
					}
				}
			}
		}
		if (cachekeys != null)
		{
			string[] array2 = cachekeys;
			foreach (string text in array2)
			{
				stringBuilder.AppendFormat("_" + text);
			}
		}
		return stringBuilder.ToString();
	}

	private void OnChanged(object sender, FileSystemEventArgs args)
	{
		OnDependencyChanged(sender, args);
	}

	private bool DoOnChanged()
	{
		DateTime now = DateTime.Now;
		if (now < start)
		{
			return false;
		}
		hasChanged = true;
		utcLastModified = now.ToUniversalTime();
		DisposeWatchers();
		if (cache != null)
		{
			cache.CheckDependencies();
		}
		return true;
	}

	private void DisposeWatchers()
	{
		lock (locker)
		{
			if (watchers != null)
			{
				FileSystemWatcher[] array = watchers;
				for (int i = 0; i < array.Length; i++)
				{
					array[i]?.Dispose();
				}
			}
			watchers = null;
		}
	}

	/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
	public void Dispose()
	{
		DependencyDispose();
	}

	internal virtual void DependencyDisposeInternal()
	{
	}

	/// <summary>Releases the resources used by the <see cref="T:System.Web.Caching.CacheDependency" /> class and any classes that derive from <see cref="T:System.Web.Caching.CacheDependency" />.</summary>
	protected virtual void DependencyDispose()
	{
		DependencyDisposeInternal();
		DisposeWatchers();
		if (dependency != null)
		{
			dependency.DependencyChanged -= OnChildDependencyChanged;
			dependency.Dispose();
		}
		cache = null;
	}

	internal void SetCache(Cache c)
	{
		cache = c;
		used = c != null;
	}

	/// <summary>Completes initialization of the <see cref="T:System.Web.Caching.CacheDependency" /> object.</summary>
	protected internal void FinishInit()
	{
		utcLastModified = DateTime.UtcNow;
	}

	/// <summary>Marks the time when a dependency last changed.</summary>
	/// <param name="utcLastModified">The time when the dependency last changed. </param>
	protected void SetUtcLastModified(DateTime utcLastModified)
	{
		this.utcLastModified = utcLastModified;
	}

	private void OnChildDependencyChanged(object o, EventArgs e)
	{
		hasChanged = true;
		OnDependencyChanged(o, e);
	}

	private void OnDependencyChanged(object sender, EventArgs e)
	{
		if (DoOnChanged() && events[dependencyChangedEvent] is EventHandler eventHandler)
		{
			eventHandler(sender, e);
		}
	}

	/// <summary>Notifies the base <see cref="T:System.Web.Caching.CacheDependency" /> object that the dependency represented by a derived <see cref="T:System.Web.Caching.CacheDependency" /> class has changed.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected void NotifyDependencyChanged(object sender, EventArgs e)
	{
		OnDependencyChanged(sender, e);
	}
}
