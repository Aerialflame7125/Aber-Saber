using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Ookii.Dialogs.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[CompilerGenerated]
[DebuggerNonUserCode]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("Ookii.Dialogs.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static string AnimationLoadErrorFormat => ResourceManager.GetString("AnimationLoadErrorFormat", resourceCulture);

	internal static string CredentialEmptyTargetError => ResourceManager.GetString("CredentialEmptyTargetError", resourceCulture);

	internal static string CredentialError => ResourceManager.GetString("CredentialError", resourceCulture);

	internal static string CredentialPromptNotCalled => ResourceManager.GetString("CredentialPromptNotCalled", resourceCulture);

	internal static string DuplicateButtonTypeError => ResourceManager.GetString("DuplicateButtonTypeError", resourceCulture);

	internal static string DuplicateItemIdError => ResourceManager.GetString("DuplicateItemIdError", resourceCulture);

	internal static string FileNotFoundFormat => ResourceManager.GetString("FileNotFoundFormat", resourceCulture);

	internal static string GlassNotSupportedError => ResourceManager.GetString("GlassNotSupportedError", resourceCulture);

	internal static string Help => ResourceManager.GetString("Help", resourceCulture);

	internal static string InvalidFilterString => ResourceManager.GetString("InvalidFilterString", resourceCulture);

	internal static string InvalidTaskDialogItemIdError => ResourceManager.GetString("InvalidTaskDialogItemIdError", resourceCulture);

	internal static string NoAssociatedTaskDialogError => ResourceManager.GetString("NoAssociatedTaskDialogError", resourceCulture);

	internal static string NonCustomTaskDialogButtonIdError => ResourceManager.GetString("NonCustomTaskDialogButtonIdError", resourceCulture);

	internal static string Preview => ResourceManager.GetString("Preview", resourceCulture);

	internal static string ProgressDialogNotRunningError => ResourceManager.GetString("ProgressDialogNotRunningError", resourceCulture);

	internal static string ProgressDialogRunning => ResourceManager.GetString("ProgressDialogRunning", resourceCulture);

	internal static string TaskDialogEmptyButtonLabelError => ResourceManager.GetString("TaskDialogEmptyButtonLabelError", resourceCulture);

	internal static string TaskDialogIllegalCrossThreadCallError => ResourceManager.GetString("TaskDialogIllegalCrossThreadCallError", resourceCulture);

	internal static string TaskDialogItemHasOwnerError => ResourceManager.GetString("TaskDialogItemHasOwnerError", resourceCulture);

	internal static string TaskDialogNoButtonsError => ResourceManager.GetString("TaskDialogNoButtonsError", resourceCulture);

	internal static string TaskDialogNotRunningError => ResourceManager.GetString("TaskDialogNotRunningError", resourceCulture);

	internal static string TaskDialogRunningError => ResourceManager.GetString("TaskDialogRunningError", resourceCulture);

	internal static string TaskDialogsNotSupportedError => ResourceManager.GetString("TaskDialogsNotSupportedError", resourceCulture);

	[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
	internal Resources()
	{
	}
}
