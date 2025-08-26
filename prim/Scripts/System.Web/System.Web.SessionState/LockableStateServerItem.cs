using System.Threading;

namespace System.Web.SessionState;

internal class LockableStateServerItem
{
	public StateServerItem item;

	public ReaderWriterLock rwlock;

	public LockableStateServerItem(StateServerItem item)
	{
		this.item = item;
		rwlock = new ReaderWriterLock();
	}
}
