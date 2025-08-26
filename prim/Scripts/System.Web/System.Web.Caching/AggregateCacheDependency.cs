using System.Collections.Generic;
using System.Text;

namespace System.Web.Caching;

/// <summary>Combines multiple dependencies between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects. This class cannot be inherited.</summary>
public sealed class AggregateCacheDependency : CacheDependency
{
	private object dependenciesLock = new object();

	private List<CacheDependency> dependencies;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> class.</summary>
	public AggregateCacheDependency()
	{
		FinishInit();
	}

	/// <summary>Adds an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects to the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
	/// <param name="dependencies">The array of <see cref="T:System.Web.Caching.CacheDependency" />  objects to add. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="dependencies" /> is <see langword="null" />.- or -A <see cref="T:System.Web.Caching.CacheDependency" /> object in <paramref name="dependencies" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.Caching.CacheDependency" /> object is referenced from more than one <see cref="T:System.Web.Caching.Cache" /> entry.</exception>
	public void Add(params CacheDependency[] dependencies)
	{
		if (dependencies == null)
		{
			throw new ArgumentNullException("dependencies");
		}
		if (dependencies.Length == 0)
		{
			return;
		}
		bool flag = false;
		CacheDependency[] array = dependencies;
		foreach (CacheDependency cacheDependency in array)
		{
			if (cacheDependency == null || cacheDependency.IsUsed)
			{
				throw new InvalidOperationException("Cache dependency already in use");
			}
			if (!flag && cacheDependency != null && cacheDependency.HasChanged)
			{
				flag = true;
			}
		}
		lock (dependenciesLock)
		{
			if (this.dependencies == null)
			{
				this.dependencies = new List<CacheDependency>(dependencies.Length);
			}
			array = dependencies;
			foreach (CacheDependency cacheDependency2 in array)
			{
				if (cacheDependency2 != null)
				{
					cacheDependency2.DependencyChanged += OnAnyChanged;
				}
			}
			this.dependencies.AddRange(dependencies);
			base.Start = DateTime.UtcNow;
		}
		if (flag)
		{
			NotifyDependencyChanged(this, null);
		}
	}

	/// <summary>Retrieves a unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
	/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object. If one of the associated dependency objects does not have a unique identifier, the <see cref="M:System.Web.Caching.AggregateCacheDependency.GetUniqueID" /> method returns <see langword="null" />.</returns>
	public override string GetUniqueID()
	{
		if (dependencies == null || dependencies.Count == 0)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder();
		lock (dependenciesLock)
		{
			string text = null;
			foreach (CacheDependency dependency in dependencies)
			{
				text = dependency.GetUniqueID();
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				stringBuilder.Append(text);
				stringBuilder.Append(';');
			}
		}
		return stringBuilder.ToString();
	}

	protected override void DependencyDispose()
	{
		base.DependencyDispose();
	}

	internal override void DependencyDisposeInternal()
	{
		if (dependencies == null || dependencies.Count <= 0)
		{
			return;
		}
		foreach (CacheDependency dependency in dependencies)
		{
			dependency.DependencyChanged -= OnAnyChanged;
		}
	}

	private void OnAnyChanged(object sender, EventArgs args)
	{
		NotifyDependencyChanged(sender, args);
	}
}
