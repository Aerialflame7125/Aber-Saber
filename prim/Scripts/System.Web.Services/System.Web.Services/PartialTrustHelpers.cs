using System.Security;
using System.Web.Hosting;

namespace System.Web.Services;

internal static class PartialTrustHelpers
{
	[SecurityCritical]
	private static bool isInPartialTrustOutsideAspNet;

	[SecurityCritical]
	private static bool isInPartialTrustOutsideAspNetInitialized;

	[SecuritySafeCritical]
	internal static void FailIfInPartialTrustOutsideAspNet()
	{
		if (!isInPartialTrustOutsideAspNetInitialized)
		{
			isInPartialTrustOutsideAspNet = !AppDomain.CurrentDomain.IsFullyTrusted && !HostingEnvironment.IsHosted;
			isInPartialTrustOutsideAspNetInitialized = true;
		}
		if (isInPartialTrustOutsideAspNet)
		{
			throw new SecurityException(Res.GetString("CannotRunInPartialTrustOutsideAspNet"));
		}
	}
}
