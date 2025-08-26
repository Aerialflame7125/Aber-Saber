using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DetailsViewPagerRow : DetailsViewRow, INamingContainer, INonBindingContainer
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewPagerRow" /> class.</summary>
	/// <param name="rowIndex">The index of the row in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
	/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
	[MonoTODO("why this class exists at all?")]
	public DetailsViewPagerRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		: base(rowIndex, rowType, rowState)
	{
	}
}
