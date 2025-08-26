using System.Web.Caching;

namespace System.Web.SessionState;

internal class RemoteStateServer : MarshalByRefObject
{
	private const int lockAcquireTimeout = 30000;

	private Cache cache;

	internal RemoteStateServer()
	{
		cache = new Cache();
	}

	private void Insert(string id, LockableStateServerItem item)
	{
		cache.Insert(id, item, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, item.item.Timeout, 0));
	}

	private LockableStateServerItem Retrieve(string id)
	{
		return cache[id] as LockableStateServerItem;
	}

	internal void CreateUninitializedItem(string id, int timeout)
	{
		LockableStateServerItem item = new LockableStateServerItem(new StateServerItem(timeout)
		{
			Action = SessionStateActions.InitializeItem
		});
		Insert(id, item);
	}

	internal StateServerItem GetItem(string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
	{
		locked = false;
		lockAge = TimeSpan.MinValue;
		lockId = int.MinValue;
		actions = SessionStateActions.None;
		LockableStateServerItem lockableStateServerItem = Retrieve(id);
		if (lockableStateServerItem == null || lockableStateServerItem.item.IsAbandoned())
		{
			return null;
		}
		try
		{
			lockableStateServerItem.rwlock.AcquireReaderLock(30000);
			if (lockableStateServerItem.item.Locked)
			{
				locked = true;
				lockAge = DateTime.UtcNow.Subtract(lockableStateServerItem.item.LockedTime);
				lockId = lockableStateServerItem.item.LockId;
				return null;
			}
			lockableStateServerItem.rwlock.ReleaseReaderLock();
			if (exclusive)
			{
				lockableStateServerItem.rwlock.AcquireWriterLock(30000);
				lockableStateServerItem.item.Locked = true;
				lockableStateServerItem.item.LockedTime = DateTime.UtcNow;
				lockableStateServerItem.item.LockId++;
				lockId = lockableStateServerItem.item.LockId;
			}
		}
		catch
		{
			throw;
		}
		finally
		{
			if (lockableStateServerItem.rwlock.IsReaderLockHeld)
			{
				lockableStateServerItem.rwlock.ReleaseReaderLock();
			}
			if (lockableStateServerItem.rwlock.IsWriterLockHeld)
			{
				lockableStateServerItem.rwlock.ReleaseWriterLock();
			}
		}
		actions = lockableStateServerItem.item.Action;
		return lockableStateServerItem.item;
	}

	internal void Remove(string id, object lockid)
	{
		cache.Remove(id);
	}

	internal void ResetItemTimeout(string id)
	{
		Retrieve(id)?.item.Touch();
	}

	internal void ReleaseItemExclusive(string id, object lockId)
	{
		LockableStateServerItem lockableStateServerItem = Retrieve(id);
		if (lockableStateServerItem == null || lockableStateServerItem.item.LockId != (int)lockId)
		{
			return;
		}
		try
		{
			lockableStateServerItem.rwlock.AcquireWriterLock(30000);
			lockableStateServerItem.item.Locked = false;
		}
		catch
		{
			throw;
		}
		finally
		{
			if (lockableStateServerItem.rwlock.IsWriterLockHeld)
			{
				lockableStateServerItem.rwlock.ReleaseWriterLock();
			}
		}
	}

	internal void SetAndReleaseItemExclusive(string id, byte[] collection_data, byte[] sobjs_data, object lockId, int timeout, bool newItem)
	{
		LockableStateServerItem lockableStateServerItem = Retrieve(id);
		bool flag = false;
		if (newItem || lockableStateServerItem == null)
		{
			lockableStateServerItem = new LockableStateServerItem(new StateServerItem(collection_data, sobjs_data, timeout));
			lockableStateServerItem.item.LockId = (int)lockId;
			flag = true;
		}
		else
		{
			if (lockableStateServerItem.item.LockId != (int)lockId)
			{
				return;
			}
			Remove(id, lockId);
		}
		try
		{
			lockableStateServerItem.rwlock.AcquireWriterLock(30000);
			lockableStateServerItem.item.Locked = false;
			if (!flag)
			{
				lockableStateServerItem.item.CollectionData = collection_data;
				lockableStateServerItem.item.StaticObjectsData = sobjs_data;
			}
			Insert(id, lockableStateServerItem);
		}
		catch
		{
			throw;
		}
		finally
		{
			if (lockableStateServerItem.rwlock.IsWriterLockHeld)
			{
				lockableStateServerItem.rwlock.ReleaseWriterLock();
			}
		}
	}

	public override object InitializeLifetimeService()
	{
		return null;
	}
}
