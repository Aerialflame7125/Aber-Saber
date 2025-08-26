using System.Threading;

namespace System.Web.SessionState;

internal sealed class InProcSessionItem
{
	public bool locked;

	public bool cookieless;

	public ISessionStateItemCollection items;

	public DateTime lockedTime;

	public DateTime expiresAt;

	public ReaderWriterLockSlim rwlock;

	public int lockId;

	public int timeout;

	public bool resettingTimeout;

	public HttpStaticObjectsCollection staticItems;

	internal InProcSessionItem()
	{
		locked = false;
		cookieless = false;
		items = null;
		staticItems = null;
		lockedTime = DateTime.MinValue;
		expiresAt = DateTime.MinValue;
		rwlock = new ReaderWriterLockSlim();
		lockId = int.MinValue;
		timeout = 0;
		resettingTimeout = false;
	}

	public void Dispose()
	{
		if (rwlock != null)
		{
			rwlock.Dispose();
			rwlock = null;
		}
		staticItems = null;
		if (items != null)
		{
			items.Clear();
		}
		items = null;
	}

	~InProcSessionItem()
	{
		Dispose();
	}
}
