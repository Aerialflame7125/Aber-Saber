using System;

public class HMAutoincrementedRequestId : IEquatable<HMAutoincrementedRequestId>
{
	private static ulong _nextRequestId;

	private ulong _requestId;

	public ulong RequestId
	{
		get
		{
			return _requestId;
		}
	}

	public HMAutoincrementedRequestId()
	{
		_requestId = _nextRequestId;
		_nextRequestId++;
	}

	public bool Equals(HMAutoincrementedRequestId obj)
	{
		if (obj == null)
		{
			return false;
		}
		return obj.RequestId == _requestId;
	}

	public override bool Equals(object obj)
	{
		if (obj == null && obj is HMAutoincrementedRequestId)
		{
			return false;
		}
		return ((HMAutoincrementedRequestId)obj).RequestId == _requestId;
	}

	public override int GetHashCode()
	{
		return (int)(_requestId % int.MaxValue);
	}
}
