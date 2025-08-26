using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreateUserError" /> event.</summary>
public class CreateUserErrorEventArgs : EventArgs
{
	private MembershipCreateStatus _error;

	/// <summary>Gets or sets a value indicating the result of a <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatingUser" /> event.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration values.</returns>
	public MembershipCreateStatus CreateUserError
	{
		get
		{
			return _error;
		}
		set
		{
			_error = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> class.</summary>
	/// <param name="s">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> object that describes the result of a <see cref="Overload:System.Web.Security.Membership.CreateUser" /> attempt.</param>
	public CreateUserErrorEventArgs(MembershipCreateStatus s)
	{
		_error = s;
	}
}
