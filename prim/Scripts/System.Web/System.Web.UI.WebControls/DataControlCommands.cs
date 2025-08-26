using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>The <see cref="T:System.Web.UI.WebControls.DataControlCommands" /> class contains public fields that all ASP.NET data-bound controls use, to promote a consistent user interface (UI). This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataControlCommands
{
	/// <summary>Represents the string "Cancel".</summary>
	public const string CancelCommandName = "Cancel";

	/// <summary>Represents the string "Delete".</summary>
	public const string DeleteCommandName = "Delete";

	/// <summary>Represents the string "Edit".</summary>
	public const string EditCommandName = "Edit";

	/// <summary>Represents the string "First".</summary>
	public const string FirstPageCommandArgument = "First";

	/// <summary>Represents the string "Insert".</summary>
	public const string InsertCommandName = "Insert";

	/// <summary>Represents the string "Last".</summary>
	public const string LastPageCommandArgument = "Last";

	/// <summary>Represents the string "Next".</summary>
	public const string NextPageCommandArgument = "Next";

	/// <summary>Represents the string "New".</summary>
	public const string NewCommandName = "New";

	/// <summary>Represents the string "Page".</summary>
	public const string PageCommandName = "Page";

	/// <summary>Represents the string "Prev".</summary>
	public const string PreviousPageCommandArgument = "Prev";

	/// <summary>Represents the string "Select".</summary>
	public const string SelectCommandName = "Select";

	/// <summary>Represents the string "Sort".</summary>
	public const string SortCommandName = "Sort";

	/// <summary>Represents the string "Update".</summary>
	public const string UpdateCommandName = "Update";

	private DataControlCommands()
	{
	}
}
