using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices;

/// <summary>Creates and caches binding rules.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[DebuggerStepThrough]
public static class CallSiteOps
{
	/// <summary>Creates an instance of a dynamic call site used for cache lookup.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	/// <returns>The new call site.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CallSite<T> CreateMatchmaker<T>(CallSite<T> site) where T : class
	{
		CallSite<T> callSite = site.CreateMatchMaker();
		callSite._match = true;
		return callSite;
	}

	/// <summary>Checks if a dynamic site requires an update.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <returns>true if rule does not need updating, false otherwise.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool SetNotMatched(CallSite site)
	{
		bool match = site._match;
		site._match = false;
		return match;
	}

	/// <summary>Checks whether the executed rule matched</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <returns>true if rule matched, false otherwise.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool GetMatch(CallSite site)
	{
		return site._match;
	}

	/// <summary>Clears the match flag on the matchmaker call site.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static void ClearMatch(CallSite site)
	{
		site._match = true;
	}

	/// <summary>Adds a rule to the cache maintained on the dynamic call site.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <param name="rule">An instance of the call site rule.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static void AddRule<T>(CallSite<T> site, T rule) where T : class
	{
		site.AddRule(rule);
	}

	/// <summary>Updates rules in the cache.</summary>
	/// <param name="this">An instance of the dynamic call site.</param>
	/// <param name="matched">The matched rule index.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static void UpdateRules<T>(CallSite<T> @this, int matched) where T : class
	{
		if (matched > 1)
		{
			@this.MoveRule(matched);
		}
	}

	/// <summary>Gets the dynamic binding rules from the call site.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	/// <returns>An array of dynamic binding rules.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static T[] GetRules<T>(CallSite<T> site) where T : class
	{
		return site.Rules;
	}

	/// <summary>Retrieves binding rule cache.</summary>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	/// <returns>The cache.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static RuleCache<T> GetRuleCache<T>(CallSite<T> site) where T : class
	{
		return site.Binder.GetRuleCache<T>();
	}

	/// <summary>Moves the binding rule within the cache.</summary>
	/// <param name="cache">The call site rule cache.</param>
	/// <param name="rule">An instance of the call site rule.</param>
	/// <param name="i">An index of the call site rule.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />. </typeparam>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static void MoveRule<T>(RuleCache<T> cache, T rule, int i) where T : class
	{
		if (i > 1)
		{
			cache.MoveRule(rule, i);
		}
	}

	/// <summary>Searches the dynamic rule cache for rules applicable to the dynamic operation.</summary>
	/// <param name="cache">The cache.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />. </typeparam>
	/// <returns>The collection of applicable rules.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static T[] GetCachedRules<T>(RuleCache<T> cache) where T : class
	{
		return cache.GetRules();
	}

	/// <summary>Updates the call site target with a new rule based on the arguments.</summary>
	/// <param name="binder">The call site binder.</param>
	/// <param name="site">An instance of the dynamic call site.</param>
	/// <param name="args">Arguments to the call site.</param>
	/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
	/// <returns>The new call site target.</returns>
	[Obsolete("do not use this method", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static T Bind<T>(CallSiteBinder binder, CallSite<T> site, object[] args) where T : class
	{
		return binder.BindCore(site, args);
	}
}
