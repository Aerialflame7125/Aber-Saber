using System.IO;
using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>Provides access to properties and values of the ticket used with forms authentication to identify users. This class cannot be inherited.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class FormsAuthenticationTicket
{
	private int version;

	private bool persistent;

	private DateTime issue_date;

	private DateTime expiration;

	private string name;

	private string cookie_path;

	private string user_data;

	/// <summary>Gets the cookie path for the forms-authentication ticket.</summary>
	/// <returns>The cookie path for the forms-authentication ticket.</returns>
	public string CookiePath => cookie_path;

	/// <summary>Gets the local date and time at which the forms-authentication ticket expires.</summary>
	/// <returns>The <see cref="T:System.DateTime" /> at which the forms-authentication ticket expires.</returns>
	public DateTime Expiration => expiration;

	/// <summary>Gets a value indicating whether the forms-authentication ticket has expired.</summary>
	/// <returns>
	///     <see langword="true" /> if the forms-authentication ticket has expired; otherwise, <see langword="false" />.</returns>
	public bool Expired => DateTime.Now > expiration;

	/// <summary>Gets a value indicating whether the cookie that contains the forms-authentication ticket information is persistent.</summary>
	/// <returns>
	///     <see langword="true" /> if a durable cookie (a cookie that is saved across browser sessions) was issued; otherwise, <see langword="false" />.</returns>
	public bool IsPersistent => persistent;

	/// <summary>Gets the local date and time at which the forms-authentication ticket was originally issued.</summary>
	/// <returns>The <see cref="T:System.DateTime" /> when the forms-authentication ticket was originally issued.</returns>
	public DateTime IssueDate => issue_date;

	/// <summary>Gets the user name associated with the forms-authentication ticket.</summary>
	/// <returns>The user name associated with the forms-authentication ticket.</returns>
	public string Name => name;

	/// <summary>Gets a user-specific string stored with the ticket.</summary>
	/// <returns>A user-specific string stored with the ticket. The default is an empty string ("").</returns>
	public string UserData => user_data;

	/// <summary>Gets the version number of the ticket.</summary>
	/// <returns>The version number of the ticket. The default is 2.</returns>
	public int Version => version;

	internal byte[] ToByteArray()
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(version);
		binaryWriter.Write(persistent);
		binaryWriter.Write(issue_date.Ticks);
		binaryWriter.Write(expiration.Ticks);
		binaryWriter.Write(name != null);
		if (name != null)
		{
			binaryWriter.Write(name);
		}
		binaryWriter.Write(cookie_path != null);
		if (cookie_path != null)
		{
			binaryWriter.Write(cookie_path);
		}
		binaryWriter.Write(user_data != null);
		if (user_data != null)
		{
			binaryWriter.Write(user_data);
		}
		binaryWriter.Flush();
		return memoryStream.ToArray();
	}

	internal static FormsAuthenticationTicket FromByteArray(byte[] bytes)
	{
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
		FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket();
		formsAuthenticationTicket.version = binaryReader.ReadInt32();
		formsAuthenticationTicket.persistent = binaryReader.ReadBoolean();
		formsAuthenticationTicket.issue_date = new DateTime(binaryReader.ReadInt64());
		formsAuthenticationTicket.expiration = new DateTime(binaryReader.ReadInt64());
		if (binaryReader.ReadBoolean())
		{
			formsAuthenticationTicket.name = binaryReader.ReadString();
		}
		if (binaryReader.ReadBoolean())
		{
			formsAuthenticationTicket.cookie_path = binaryReader.ReadString();
		}
		if (binaryReader.ReadBoolean())
		{
			formsAuthenticationTicket.user_data = binaryReader.ReadString();
		}
		return formsAuthenticationTicket;
	}

	private FormsAuthenticationTicket()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, expiration date, issue date, persistence, and user-specific data. The cookie path is set to the default value established in the application's configuration file.</summary>
	/// <param name="version">The version number of the ticket.</param>
	/// <param name="name">The user name associated with the ticket.</param>
	/// <param name="issueDate">The local date and time at which the ticket was issued.</param>
	/// <param name="expiration">The local date and time at which the ticket expires.</param>
	/// <param name="isPersistent">
	///       <see langword="true" /> if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, <see langword="false" />. If the ticket is stored in the URL, this value is ignored.</param>
	/// <param name="userData">The user-specific data to be stored with the ticket.</param>
	public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData)
	{
		this.version = version;
		this.name = name;
		issue_date = issueDate;
		this.expiration = expiration;
		persistent = isPersistent;
		user_data = userData;
		cookie_path = "/";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, directory path, issue date, expiration date, persistence, and user-defined data.</summary>
	/// <param name="version">The version number of the ticket. </param>
	/// <param name="name">The user name associated with the ticket. </param>
	/// <param name="issueDate">The local date and time at which the ticket was issued. </param>
	/// <param name="expiration">The local date and time at which the ticket expires. </param>
	/// <param name="isPersistent">
	///       <see langword="true" /> if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, <see langword="false" />. If the ticket is stored in the URL, this value is ignored.</param>
	/// <param name="userData">The user-specific data to be stored with the ticket. </param>
	/// <param name="cookiePath">The path for the ticket when stored in a cookie. </param>
	public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData, string cookiePath)
	{
		this.version = version;
		this.name = name;
		issue_date = issueDate;
		this.expiration = expiration;
		persistent = isPersistent;
		user_data = userData;
		cookie_path = cookiePath;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class using a cookie name and expiration information.</summary>
	/// <param name="name">The user name associated with the ticket.</param>
	/// <param name="isPersistent">
	///       <see langword="true" /> if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, <see langword="false" />. If the ticket is stored in the URL, this value is ignored.</param>
	/// <param name="timeout">The time, in minutes, for which the authentication ticket is valid.</param>
	public FormsAuthenticationTicket(string name, bool isPersistent, int timeout)
	{
		version = 1;
		this.name = name;
		issue_date = DateTime.Now;
		persistent = isPersistent;
		if (persistent)
		{
			expiration = issue_date.AddYears(50);
		}
		else
		{
			expiration = issue_date.AddMinutes(timeout);
		}
		user_data = "";
		cookie_path = "/";
	}

	internal void SetDates(DateTime issue_date, DateTime expiration)
	{
		this.issue_date = issue_date;
		this.expiration = expiration;
	}

	internal FormsAuthenticationTicket Clone()
	{
		return new FormsAuthenticationTicket(version, name, issue_date, expiration, persistent, user_data, cookie_path);
	}
}
