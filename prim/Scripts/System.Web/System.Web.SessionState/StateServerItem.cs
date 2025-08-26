namespace System.Web.SessionState;

[Serializable]
internal class StateServerItem
{
	public byte[] CollectionData;

	public byte[] StaticObjectsData;

	private DateTime last_access;

	public int Timeout;

	public int LockId;

	public bool Locked;

	public DateTime LockedTime;

	public SessionStateActions Action;

	public StateServerItem(int timeout)
		: this(null, null, timeout)
	{
	}

	public StateServerItem(byte[] collection_data, byte[] sobjs_data, int timeout)
	{
		CollectionData = collection_data;
		StaticObjectsData = sobjs_data;
		Timeout = timeout;
		last_access = DateTime.UtcNow;
		Locked = false;
		LockId = int.MinValue;
		LockedTime = DateTime.MinValue;
		Action = SessionStateActions.None;
	}

	public void Touch()
	{
		last_access = DateTime.UtcNow;
	}

	public bool IsAbandoned()
	{
		if (last_access.AddMinutes(Timeout) < DateTime.UtcNow)
		{
			return true;
		}
		return false;
	}
}
