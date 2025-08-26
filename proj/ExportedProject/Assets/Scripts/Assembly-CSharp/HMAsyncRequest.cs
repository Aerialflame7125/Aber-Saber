public class HMAsyncRequest : HMAutoincrementedRequestId
{
	public delegate void CancelHander(HMAsyncRequest request);

	private CancelHander _cancelHander;

	public CancelHander CancelHandler
	{
		get
		{
			return _cancelHander;
		}
		set
		{
			_cancelHander = value;
		}
	}

	public virtual void Cancel()
	{
		if (_cancelHander != null)
		{
			_cancelHander(this);
		}
	}
}
