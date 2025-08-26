using System;

namespace Oculus.Platform.Models;

public class AssetDetails
{
	public readonly ulong AssetId;

	public readonly string DownloadStatus;

	public readonly string Filepath;

	public readonly string IapStatus;

	public AssetDetails(IntPtr o)
	{
		AssetId = CAPI.ovr_AssetDetails_GetAssetId(o);
		DownloadStatus = CAPI.ovr_AssetDetails_GetDownloadStatus(o);
		Filepath = CAPI.ovr_AssetDetails_GetFilepath(o);
		IapStatus = CAPI.ovr_AssetDetails_GetIapStatus(o);
	}
}
