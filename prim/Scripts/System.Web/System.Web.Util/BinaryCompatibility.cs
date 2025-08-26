using System.Runtime.Versioning;

namespace System.Web.Util;

internal sealed class BinaryCompatibility
{
	internal const string TargetFrameworkKey = "ASPNET_TARGETFRAMEWORK";

	public static readonly BinaryCompatibility Current;

	public bool TargetsAtLeastFramework45 { get; private set; }

	public bool TargetsAtLeastFramework451 { get; private set; }

	public bool TargetsAtLeastFramework452 { get; private set; }

	public bool TargetsAtLeastFramework46 { get; private set; }

	public bool TargetsAtLeastFramework461 { get; private set; }

	public bool TargetsAtLeastFramework463 { get; private set; }

	public Version TargetFramework { get; private set; }

	static BinaryCompatibility()
	{
		Current = new BinaryCompatibility(AppDomain.CurrentDomain.GetData("ASPNET_TARGETFRAMEWORK") as FrameworkName);
		TelemetryLogger.LogTargetFramework(Current.TargetFramework);
	}

	public BinaryCompatibility(FrameworkName frameworkName)
	{
		Version version = VersionUtil.FrameworkDefault;
		if (frameworkName != null && frameworkName.Identifier == ".NETFramework")
		{
			version = frameworkName.Version;
		}
		TargetFramework = version;
		TargetsAtLeastFramework45 = version >= VersionUtil.Framework45;
		TargetsAtLeastFramework451 = version >= VersionUtil.Framework451;
		TargetsAtLeastFramework452 = version >= VersionUtil.Framework452;
		TargetsAtLeastFramework46 = version >= VersionUtil.Framework46;
		TargetsAtLeastFramework461 = version >= VersionUtil.Framework461;
		TargetsAtLeastFramework463 = version >= VersionUtil.Framework463;
	}
}
