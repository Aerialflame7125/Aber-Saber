using System;
using Oculus.Platform.Models;

namespace Oculus.Platform;

public class MessageWithLinkedAccountList : Message<LinkedAccountList>
{
	public MessageWithLinkedAccountList(IntPtr c_message)
		: base(c_message)
	{
	}

	public override LinkedAccountList GetLinkedAccountList()
	{
		return base.Data;
	}

	protected override LinkedAccountList GetDataFromMessage(IntPtr c_message)
	{
		IntPtr obj = CAPI.ovr_Message_GetNativeMessage(c_message);
		IntPtr a = CAPI.ovr_Message_GetLinkedAccountArray(obj);
		return new LinkedAccountList(a);
	}
}
