using System;
using Oculus.Platform.Models;

namespace Oculus.Platform;

public class MessageWithAssetDetailsList : Message<AssetDetailsList>
{
	public MessageWithAssetDetailsList(IntPtr c_message)
		: base(c_message)
	{
	}

	public override AssetDetailsList GetAssetDetailsList()
	{
		return base.Data;
	}

	protected override AssetDetailsList GetDataFromMessage(IntPtr c_message)
	{
		IntPtr obj = CAPI.ovr_Message_GetNativeMessage(c_message);
		IntPtr a = CAPI.ovr_Message_GetAssetDetailsArray(obj);
		return new AssetDetailsList(a);
	}
}
