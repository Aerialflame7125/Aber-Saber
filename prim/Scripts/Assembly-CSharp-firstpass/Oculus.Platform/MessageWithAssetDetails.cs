using System;
using Oculus.Platform.Models;

namespace Oculus.Platform;

public class MessageWithAssetDetails : Message<AssetDetails>
{
	public MessageWithAssetDetails(IntPtr c_message)
		: base(c_message)
	{
	}

	public override AssetDetails GetAssetDetails()
	{
		return base.Data;
	}

	protected override AssetDetails GetDataFromMessage(IntPtr c_message)
	{
		IntPtr obj = CAPI.ovr_Message_GetNativeMessage(c_message);
		IntPtr o = CAPI.ovr_Message_GetAssetDetails(obj);
		return new AssetDetails(o);
	}
}
