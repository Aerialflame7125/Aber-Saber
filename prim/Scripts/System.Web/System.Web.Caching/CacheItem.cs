namespace System.Web.Caching;

internal class CacheItem
{
	public object Value;

	public string Key;

	public CacheDependency Dependency;

	public DateTime AbsoluteExpiration;

	public TimeSpan SlidingExpiration;

	public CacheItemPriority Priority;

	public CacheItemRemovedCallback OnRemoveCallback;

	public CacheItemUpdateCallback OnUpdateCallback;

	public DateTime LastChange;

	public long ExpiresAt;

	public bool Disabled;

	public bool IsTimedItem;

	public int PriorityQueueIndex = -1;
}
