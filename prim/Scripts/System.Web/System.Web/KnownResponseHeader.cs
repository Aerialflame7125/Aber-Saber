namespace System.Web;

internal sealed class KnownResponseHeader : BaseResponseHeader
{
	public int ID;

	internal KnownResponseHeader(int ID, string val)
		: base(val)
	{
		this.ID = ID;
	}

	internal override void SendContent(HttpWorkerRequest wr)
	{
		wr.SendKnownResponseHeader(ID, base.Value);
	}
}
