using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>The exception that is thrown when a user is not successfully created by a membership provider.</summary>
[Serializable]
[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class MembershipCreateUserException : Exception
{
	private MembershipCreateStatus _StatusCode = MembershipCreateStatus.ProviderError;

	/// <summary>Gets a description of the reason for the exception.</summary>
	/// <returns>A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value that describes the reason for the exception.</returns>
	public MembershipCreateStatus StatusCode => _StatusCode;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class with the specified <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> value.</summary>
	/// <param name="statusCode">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value that describes the reason for the exception.</param>
	public MembershipCreateUserException(MembershipCreateStatus statusCode)
		: base(GetMessageFromStatusCode(statusCode))
	{
		_StatusCode = statusCode;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" /> parameter value</summary>
	/// <param name="message">A description of the reason for the exception.</param>
	public MembershipCreateUserException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class with the supplied serialization information and context.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	protected MembershipCreateUserException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		_StatusCode = (MembershipCreateStatus)info.GetInt32("_StatusCode");
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class.</summary>
	public MembershipCreateUserException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" /> and the <see cref="P:System.Exception.InnerException" /> property to the supplied <paramref name="innerException" />.</summary>
	/// <param name="message">A description of the reason for the exception.</param>
	/// <param name="innerException">The exception that caused the <see cref="T:System.Web.Security.MembershipCreateUserException" />.</param>
	public MembershipCreateUserException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("_StatusCode", _StatusCode);
	}

	internal static string GetMessageFromStatusCode(MembershipCreateStatus statusCode)
	{
		return statusCode switch
		{
			MembershipCreateStatus.Success => "No Error.", 
			MembershipCreateStatus.InvalidUserName => "The username supplied is invalid.", 
			MembershipCreateStatus.InvalidPassword => "The password supplied is invalid.  Passwords must conform to the password strength requirements configured for the default provider.", 
			MembershipCreateStatus.InvalidQuestion => "The password-question supplied is invalid.  Note that the current provider configuration requires a valid password question and answer.  As a result, a CreateUser overload that accepts question and answer parameters must also be used.", 
			MembershipCreateStatus.InvalidAnswer => "The password-answer supplied is invalid.", 
			MembershipCreateStatus.InvalidEmail => "The E-mail supplied is invalid.", 
			MembershipCreateStatus.InvalidProviderUserKey => "The provider user key supplied is invalid. It must be of type System.Guid.", 
			MembershipCreateStatus.DuplicateUserName => "The username is already in use.", 
			MembershipCreateStatus.DuplicateEmail => "The E-mail address is already in use.", 
			MembershipCreateStatus.DuplicateProviderUserKey => "The provider user key is already in use.", 
			MembershipCreateStatus.UserRejected => "The user was rejected.", 
			_ => "The Provider encountered an unknown error.", 
		};
	}
}
