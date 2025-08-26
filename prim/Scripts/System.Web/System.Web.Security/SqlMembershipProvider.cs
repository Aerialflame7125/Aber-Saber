using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Manages storage of membership information for an ASP.NET application in a SQL Server database.</summary>
public class SqlMembershipProvider : MembershipProvider
{
	[Flags]
	private enum DeleteUserTableMask
	{
		MembershipUsers = 1,
		UsersInRoles = 2,
		Profiles = 4,
		WebPartStateUser = 8
	}

	private sealed class PasswordInfo
	{
		private string _password;

		private MembershipPasswordFormat _passwordFormat;

		private string _passwordSalt;

		private int _failedPasswordAttemptCount;

		private int _failedPasswordAnswerAttemptCount;

		private bool _isApproved;

		private DateTime _lastLoginDate;

		private DateTime _lastActivityDate;

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		public MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return _passwordFormat;
			}
			set
			{
				_passwordFormat = value;
			}
		}

		public string PasswordSalt
		{
			get
			{
				return _passwordSalt;
			}
			set
			{
				_passwordSalt = value;
			}
		}

		public int FailedPasswordAttemptCount
		{
			get
			{
				return _failedPasswordAttemptCount;
			}
			set
			{
				_failedPasswordAttemptCount = value;
			}
		}

		public int FailedPasswordAnswerAttemptCount
		{
			get
			{
				return _failedPasswordAnswerAttemptCount;
			}
			set
			{
				_failedPasswordAnswerAttemptCount = value;
			}
		}

		public bool IsApproved
		{
			get
			{
				return _isApproved;
			}
			set
			{
				_isApproved = value;
			}
		}

		public DateTime LastLoginDate
		{
			get
			{
				return _lastLoginDate;
			}
			set
			{
				_lastLoginDate = value;
			}
		}

		public DateTime LastActivityDate
		{
			get
			{
				return _lastActivityDate;
			}
			set
			{
				_lastActivityDate = value;
			}
		}

		internal PasswordInfo(string password, MembershipPasswordFormat passwordFormat, string passwordSalt, int failedPasswordAttemptCount, int failedPasswordAnswerAttemptCount, bool isApproved, DateTime lastLoginDate, DateTime lastActivityDate)
		{
			_password = password;
			_passwordFormat = passwordFormat;
			_passwordSalt = passwordSalt;
			_failedPasswordAttemptCount = failedPasswordAttemptCount;
			_failedPasswordAnswerAttemptCount = failedPasswordAnswerAttemptCount;
			_isApproved = isApproved;
			_lastLoginDate = lastLoginDate;
			_lastActivityDate = lastActivityDate;
		}
	}

	private bool enablePasswordReset;

	private bool enablePasswordRetrieval;

	private int maxInvalidPasswordAttempts;

	private MembershipPasswordFormat passwordFormat;

	private bool requiresQuestionAndAnswer;

	private bool requiresUniqueEmail;

	private int minRequiredNonAlphanumericCharacters;

	private int minRequiredPasswordLength;

	private int passwordAttemptWindow;

	private string passwordStrengthRegularExpression;

	private TimeSpan userIsOnlineTimeWindow;

	private ConnectionStringSettings connectionString;

	private DbProviderFactory factory;

	private string applicationName;

	private bool schemaIsOk;

	/// <summary>Gets or sets the name of the application to store and retrieve membership information for.</summary>
	/// <returns>The name of the application to store and retrieve membership information for. The default is the <see cref="P:System.Web.HttpRequest.ApplicationPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
	/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" /> property to an empty string or <see langword="null" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" /> property to a string that is longer than 256 characters.</exception>
	public override string ApplicationName
	{
		get
		{
			return applicationName;
		}
		set
		{
			applicationName = value;
		}
	}

	/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to allow users to reset their passwords.</summary>
	/// <returns>
	///     <see langword="true" /> if the membership provider supports password reset; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool EnablePasswordReset => enablePasswordReset;

	/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to allow users to retrieve their passwords.</summary>
	/// <returns>
	///     <see langword="true" /> if the membership provider supports password retrieval; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool EnablePasswordRetrieval => enablePasswordRetrieval;

	/// <summary>Gets a value indicating the format for storing passwords in the SQL Server membership database.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values, indicating the format for storing passwords in the SQL Server database.</returns>
	public override MembershipPasswordFormat PasswordFormat => passwordFormat;

	/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to require the user to answer a password question for password reset and retrieval.</summary>
	/// <returns>
	///     <see langword="true" /> if a password answer is required for password reset and retrieval; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool RequiresQuestionAndAnswer => requiresQuestionAndAnswer;

	/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to require a unique e-mail address for each user name.</summary>
	/// <returns>
	///     <see langword="true" /> if the membership provider requires a unique e-mail address; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresUniqueEmail => requiresUniqueEmail;

	/// <summary>Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.</summary>
	/// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
	public override int MaxInvalidPasswordAttempts => maxInvalidPasswordAttempts;

	/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
	/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
	public override int MinRequiredNonAlphanumericCharacters => minRequiredNonAlphanumericCharacters;

	/// <summary>Gets the minimum length required for a password.</summary>
	/// <returns>The minimum length required for a password. </returns>
	public override int MinRequiredPasswordLength => minRequiredPasswordLength;

	/// <summary>Gets the time window between which consecutive failed attempts to provide a valid password or password answers are tracked.</summary>
	/// <returns>The time window, in minutes, during which consecutive failed attempts to provide a valid password or password answers are tracked. The default is 10 minutes. If the interval between the current failed attempt and the last failed attempt is greater than the <see cref="P:System.Web.Security.SqlMembershipProvider.PasswordAttemptWindow" /> property setting, each failed attempt is treated as if it were the first failed attempt.</returns>
	public override int PasswordAttemptWindow => passwordAttemptWindow;

	/// <summary>Gets the regular expression used to evaluate a password.</summary>
	/// <returns>A regular expression used to evaluate a password.</returns>
	public override string PasswordStrengthRegularExpression => passwordStrengthRegularExpression;

	private DbConnection CreateConnection()
	{
		if (!schemaIsOk && !(schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(factory, connectionString.ConnectionString, "membership", "1")))
		{
			throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
		}
		if (connectionString == null)
		{
			throw new ProviderException("Connection string for the SQL Membership Provider has not been provided.");
		}
		try
		{
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = connectionString.ConnectionString;
			dbConnection.Open();
			return dbConnection;
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Unable to open SQL connection for the SQL Membership Provider.", innerException);
		}
	}

	private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
	{
		return AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
	}

	private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}

	private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, DbType type, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		dbParameter.DbType = type;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}

	private static int GetReturnValue(DbParameter returnValue)
	{
		object value = returnValue.Value;
		if (!(value is int))
		{
			return -1;
		}
		return (int)value;
	}

	private void CheckParam(string pName, string p, int length)
	{
		if (p == null)
		{
			throw new ArgumentNullException(pName);
		}
		if (p.Length == 0 || p.Length > length || p.IndexOf(',') != -1)
		{
			throw new ArgumentException($"invalid format for {pName}");
		}
	}

	/// <summary>Modifies a user's password.</summary>
	/// <param name="username">The user to update the password for. </param>
	/// <param name="oldPassword">The current password for the specified user. </param>
	/// <param name="newPassword">The new password for the specified user. </param>
	/// <returns>
	///     <see langword="true" /> if the password was updated successfully. <see langword="false" /> if the supplied old password is invalid, the user is locked out, or the user does not exist in the database.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -
	///         <paramref name="oldPassword" /> is an empty string or longer than 128 characters.- or -
	///         <paramref name="newPassword" /> is an empty string or longer than 128 characters.- or -The encoded version of <paramref name="newPassword" /> is greater than 128 characters.- or -The change-password action was canceled by a subscriber to the <see cref="E:System.Web.Security.Membership.ValidatingPassword" /> event, and the <see cref="P:System.Web.Security.ValidatePasswordEventArgs.FailureInformation" /> property was <see langword="null" />.- or -The length of <paramref name="newPassword" /> is less than the minimum length specified in the <see cref="P:System.Web.Security.SqlMembershipProvider.MinRequiredPasswordLength" /> property.- or -The number of non-alphabetic characters in <paramref name="newPassword" /> is less than the required number of non-alphabetic characters specified in the <see cref="P:System.Web.Security.SqlMembershipProvider.MinRequiredNonAlphanumericCharacters" /> property.- or -
	///         <paramref name="newPassword" /> does not pass the regular expression defined in the <see cref="P:System.Web.Security.SqlMembershipProvider.PasswordStrengthRegularExpression" /> property.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="oldPassword" /> is <see langword="null" />.- or -
	///         <paramref name="newPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.Security.MembershipPasswordException">
	///         <paramref name="username" /> was not found in the database.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An error occurred while setting the new password value at the database. </exception>
	/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
	public override bool ChangePassword(string username, string oldPassword, string newPassword)
	{
		if (username != null)
		{
			username = username.Trim();
		}
		if (oldPassword != null)
		{
			oldPassword = oldPassword.Trim();
		}
		if (newPassword != null)
		{
			newPassword = newPassword.Trim();
		}
		CheckParam("username", username, 256);
		CheckParam("oldPassword", oldPassword, 128);
		CheckParam("newPassword", newPassword, 128);
		if (!CheckPassword(newPassword))
		{
			throw new ArgumentException($"New Password invalid. New Password length minimum: {MinRequiredPasswordLength}. Non-alphanumeric characters required: {MinRequiredNonAlphanumericCharacters}.");
		}
		using DbConnection connection = CreateConnection();
		PasswordInfo passwordInfo = ValidateUsingPassword(username, oldPassword);
		if (passwordInfo != null)
		{
			EmitValidatingPassword(username, newPassword, isNewUser: false);
			string parameterValue = EncodePassword(newPassword, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.Connection = connection;
			dbCommand.CommandText = "aspnet_Membership_SetPassword";
			dbCommand.CommandType = CommandType.StoredProcedure;
			AddParameter(dbCommand, "@ApplicationName", ApplicationName);
			AddParameter(dbCommand, "@UserName", username);
			AddParameter(dbCommand, "@NewPassword", parameterValue);
			AddParameter(dbCommand, "@PasswordFormat", (int)passwordInfo.PasswordFormat);
			AddParameter(dbCommand, "@PasswordSalt", passwordInfo.PasswordSalt);
			AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
			DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
			dbCommand.ExecuteNonQuery();
			if (GetReturnValue(returnValue) != 0)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	/// <summary>Updates the password question and answer for a user in the SQL Server membership database.</summary>
	/// <param name="username">The user to change the password question and answer for. </param>
	/// <param name="password">The password for the specified user. </param>
	/// <param name="newPasswordQuestion">The new password question for the specified user.</param>
	/// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
	/// <returns>
	///     <see langword="true" /> if the update was successful; otherwise, <see langword="false" />. A value of <see langword="false" /> is also returned if the <paramref name="password" /> is incorrect, the user is locked out, or the user does not exist in the database.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -
	///         <paramref name="password" /> is an empty string or is longer than 128 characters.- or -
	///         <paramref name="newPasswordQuestion" /> is an empty string or is longer than 256 characters.- or -
	///         <paramref name="newPasswordAnswer" /> is an empty string or is longer than 128 characters.- or -The encoded version of <paramref name="newPasswordAnswer" /> is longer than 128 characters.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="password" /> is <see langword="null" />.- or -
	///         <paramref name="newPasswordQuestion" /> is <see langword="null" /> and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.- or -
	///         <paramref name="newPasswordAnswer" /> is <see langword="null" /> and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An error occurred when changing the password question and answer in the database.</exception>
	public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
	{
		if (username != null)
		{
			username = username.Trim();
		}
		if (newPasswordQuestion != null)
		{
			newPasswordQuestion = newPasswordQuestion.Trim();
		}
		if (newPasswordAnswer != null)
		{
			newPasswordAnswer = newPasswordAnswer.Trim();
		}
		CheckParam("username", username, 256);
		if (RequiresQuestionAndAnswer)
		{
			CheckParam("newPasswordQuestion", newPasswordQuestion, 128);
		}
		if (RequiresQuestionAndAnswer)
		{
			CheckParam("newPasswordAnswer", newPasswordAnswer, 128);
		}
		using DbConnection connection = CreateConnection();
		PasswordInfo passwordInfo = ValidateUsingPassword(username, password);
		if (passwordInfo != null)
		{
			string parameterValue = EncodePassword(newPasswordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.Connection = connection;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = "aspnet_Membership_ChangePasswordQuestionAndAnswer";
			AddParameter(dbCommand, "@ApplicationName", ApplicationName);
			AddParameter(dbCommand, "@UserName", username);
			AddParameter(dbCommand, "@NewPasswordQuestion", newPasswordQuestion);
			AddParameter(dbCommand, "@NewPasswordAnswer", parameterValue);
			DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
			dbCommand.ExecuteNonQuery();
			if (GetReturnValue(returnValue) != 0)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	/// <summary>Adds a new user to the SQL Server membership database.</summary>
	/// <param name="username">The user name for the new user. </param>
	/// <param name="password">The password for the new user. </param>
	/// <param name="email">The e-mail address for the new user. </param>
	/// <param name="passwordQuestion">The password question for the new user.</param>
	/// <param name="passwordAnswer">The password answer for the new user.</param>
	/// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
	/// <param name="providerUserKey">A <see cref="T:System.Guid" /> that uniquely identifies the membership user in the SQL Server database.</param>
	/// <param name="status">One of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> values, indicating whether the user was created successfully.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns <see langword="null" />.</returns>
	public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
	{
		if (username != null)
		{
			username = username.Trim();
		}
		if (password != null)
		{
			password = password.Trim();
		}
		if (email != null)
		{
			email = email.Trim();
		}
		if (passwordQuestion != null)
		{
			passwordQuestion = passwordQuestion.Trim();
		}
		if (passwordAnswer != null)
		{
			passwordAnswer = passwordAnswer.Trim();
		}
		if (username == null || username.Length == 0 || username.Length > 256 || username.IndexOf(',') != -1)
		{
			status = MembershipCreateStatus.InvalidUserName;
			return null;
		}
		if (password == null || password.Length == 0 || password.Length > 128)
		{
			status = MembershipCreateStatus.InvalidPassword;
			return null;
		}
		if (!CheckPassword(password))
		{
			status = MembershipCreateStatus.InvalidPassword;
			return null;
		}
		EmitValidatingPassword(username, password, isNewUser: true);
		if (RequiresUniqueEmail && (email == null || email.Length == 0))
		{
			status = MembershipCreateStatus.InvalidEmail;
			return null;
		}
		if (RequiresQuestionAndAnswer && (passwordQuestion == null || passwordQuestion.Length == 0 || passwordQuestion.Length > 256))
		{
			status = MembershipCreateStatus.InvalidQuestion;
			return null;
		}
		if (RequiresQuestionAndAnswer && (passwordAnswer == null || passwordAnswer.Length == 0 || passwordAnswer.Length > 128))
		{
			status = MembershipCreateStatus.InvalidAnswer;
			return null;
		}
		if (providerUserKey != null && !(providerUserKey is Guid))
		{
			status = MembershipCreateStatus.InvalidProviderUserKey;
			return null;
		}
		if (providerUserKey == null)
		{
			providerUserKey = Guid.NewGuid();
		}
		string text = "";
		RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
		byte[] array = new byte[16];
		randomNumberGenerator.GetBytes(array);
		text = Convert.ToBase64String(array);
		password = EncodePassword(password, PasswordFormat, text);
		if (RequiresQuestionAndAnswer)
		{
			passwordAnswer = EncodePassword(passwordAnswer, PasswordFormat, text);
		}
		if (password.Length > 128)
		{
			status = MembershipCreateStatus.InvalidPassword;
			return null;
		}
		if (RequiresQuestionAndAnswer && passwordAnswer.Length > 128)
		{
			status = MembershipCreateStatus.InvalidAnswer;
			return null;
		}
		status = MembershipCreateStatus.Success;
		using DbConnection connection = CreateConnection();
		try
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.Connection = connection;
			dbCommand.CommandText = "aspnet_Membership_CreateUser";
			dbCommand.CommandType = CommandType.StoredProcedure;
			DateTime utcNow = DateTime.UtcNow;
			AddParameter(dbCommand, "@ApplicationName", ApplicationName);
			AddParameter(dbCommand, "@UserName", username);
			AddParameter(dbCommand, "@Password", password);
			AddParameter(dbCommand, "@PasswordSalt", text);
			AddParameter(dbCommand, "@Email", email);
			AddParameter(dbCommand, "@PasswordQuestion", passwordQuestion);
			AddParameter(dbCommand, "@PasswordAnswer", passwordAnswer);
			AddParameter(dbCommand, "@IsApproved", isApproved);
			AddParameter(dbCommand, "@CurrentTimeUtc", utcNow);
			AddParameter(dbCommand, "@CreateDate", utcNow);
			AddParameter(dbCommand, "@UniqueEmail", RequiresUniqueEmail);
			AddParameter(dbCommand, "@PasswordFormat", (int)PasswordFormat);
			AddParameter(dbCommand, "@UserId", ParameterDirection.InputOutput, providerUserKey);
			DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
			dbCommand.ExecuteNonQuery();
			switch (GetReturnValue(returnValue))
			{
			case 0:
				return GetUser(username, userIsOnline: false);
			case 6:
				status = MembershipCreateStatus.DuplicateUserName;
				break;
			case 7:
				status = MembershipCreateStatus.DuplicateEmail;
				break;
			case 10:
				status = MembershipCreateStatus.DuplicateProviderUserKey;
				break;
			default:
				status = MembershipCreateStatus.ProviderError;
				break;
			}
			return null;
		}
		catch (Exception)
		{
			status = MembershipCreateStatus.ProviderError;
			return null;
		}
	}

	private bool CheckPassword(string password)
	{
		if (password.Length < MinRequiredPasswordLength)
		{
			return false;
		}
		if (MinRequiredNonAlphanumericCharacters > 0)
		{
			int num = 0;
			for (int i = 0; i < password.Length; i++)
			{
				if (!char.IsLetterOrDigit(password[i]))
				{
					num++;
				}
			}
			return num >= MinRequiredNonAlphanumericCharacters;
		}
		return true;
	}

	/// <summary>Removes a user's membership information from the SQL Server membership database.</summary>
	/// <param name="username">The name of the user to delete.</param>
	/// <param name="deleteAllRelatedData">
	///       <see langword="true" /> to delete data related to the user from the database; <see langword="false" /> to leave data related to the user in the database.</param>
	/// <returns>
	///     <see langword="true" /> if the user was deleted; otherwise, <see langword="false" />. A value of <see langword="false" /> is also returned if the user does not exist in the database.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public override bool DeleteUser(string username, bool deleteAllRelatedData)
	{
		CheckParam("username", username, 256);
		DeleteUserTableMask deleteUserTableMask = DeleteUserTableMask.MembershipUsers;
		if (deleteAllRelatedData)
		{
			deleteUserTableMask |= DeleteUserTableMask.UsersInRoles | DeleteUserTableMask.Profiles | DeleteUserTableMask.WebPartStateUser;
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Users_DeleteUser";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@TablesToDeleteFrom", (int)deleteUserTableMask);
		AddParameter(dbCommand, "@NumTablesDeletedFrom", ParameterDirection.Output, 0);
		DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		if ((int)dbCommand.Parameters["@NumTablesDeletedFrom"].Value == 0)
		{
			return false;
		}
		if (GetReturnValue(returnValue) == 0)
		{
			return true;
		}
		return false;
	}

	/// <summary>Generates a random password that is at least 14 characters long.</summary>
	/// <returns>A random password that is at least 14 characters long.</returns>
	public virtual string GeneratePassword()
	{
		return Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
	}

	/// <summary>Returns a collection of membership users for which the e-mail address field contains the specified e-mail address.</summary>
	/// <param name="emailToMatch">The e-mail address to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">The total number of matched users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="emailToMatch" /> is longer than 256 characters.- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
	public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		CheckParam("emailToMatch", emailToMatch, 256);
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex must be >= 0");
		}
		if (pageSize < 0)
		{
			throw new ArgumentException("pageSize must be >= 0");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_FindUsersByEmail";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@PageIndex", pageIndex);
		AddParameter(dbCommand, "@PageSize", pageSize);
		AddParameter(dbCommand, "@EmailToMatch", emailToMatch);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
		return BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Gets a collection of membership users where the user name contains the specified user name to match.</summary>
	/// <param name="usernameToMatch">The user name to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains the total number of matched users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("") or is longer than 256 characters.- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		CheckParam("usernameToMatch", usernameToMatch, 256);
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex must be >= 0");
		}
		if (pageSize < 0)
		{
			throw new ArgumentException("pageSize must be >= 0");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_FindUsersByName";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@PageIndex", pageIndex);
		AddParameter(dbCommand, "@PageSize", pageSize);
		AddParameter(dbCommand, "@UserNameToMatch", usernameToMatch);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
		return BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Gets a collection of all the users in the SQL Server membership database.</summary>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">The total number of users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all the users in the database for the configured <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
	public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
	{
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex must be >= 0");
		}
		if (pageSize < 0)
		{
			throw new ArgumentException("pageSize must be >= 0");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_GetAllUsers";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@PageIndex", pageIndex);
		AddParameter(dbCommand, "@PageSize", pageSize);
		AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
		return BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
	}

	private MembershipUserCollection BuildMembershipUserCollection(DbCommand command, int pageIndex, int pageSize, out int totalRecords)
	{
		DbDataReader dbDataReader = null;
		try
		{
			MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
			dbDataReader = command.ExecuteReader();
			while (dbDataReader.Read())
			{
				membershipUserCollection.Add(GetUserFromReader(dbDataReader, null, null));
			}
			totalRecords = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);
			return membershipUserCollection;
		}
		catch (Exception)
		{
			totalRecords = 0;
			return null;
		}
		finally
		{
			dbDataReader?.Close();
		}
	}

	/// <summary>Returns the number of users currently accessing the application.</summary>
	/// <returns>The number of users currently accessing the application.</returns>
	public override int GetNumberOfUsersOnline()
	{
		using DbConnection connection = CreateConnection();
		DateTime utcNow = DateTime.UtcNow;
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_GetNumberOfUsersOnline";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@CurrentTimeUtc", utcNow.ToString());
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@MinutesSinceLastInActive", userIsOnlineTimeWindow.Minutes);
		DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteScalar();
		return GetReturnValue(returnValue);
	}

	/// <summary>Returns the password for the specified user name from the SQL Server membership database.</summary>
	/// <param name="username">The user to retrieve the password for. </param>
	/// <param name="passwordAnswer">The password answer for the user. </param>
	/// <returns>The password for the specified user name.</returns>
	/// <exception cref="T:System.Web.Security.MembershipPasswordException">
	///         <paramref name="passwordAnswer" /> is invalid. - or -The membership user identified by <paramref name="username" /> is locked out.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///         <see cref="P:System.Web.Security.SqlMembershipProvider.EnablePasswordRetrieval" /> is set to <see langword="false" />. </exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="username" /> is not found in the membership database.- or -An error occurred while retrieving the password from the database. </exception>
	/// <exception cref="T:System.ArgumentException">One of the parameter values exceeds the maximum allowed length.- or -
	///         <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -
	///         <paramref name="passwordAnswer" /> is an empty string and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.- or -
	///         <paramref name="passwordAnswer" /> is greater than 128 characters.- or -The encoded version of <paramref name="passwordAnswer" /> is greater than 128 characters.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="passwordAnswer" /> is <see langword="null" /> and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.</exception>
	public override string GetPassword(string username, string passwordAnswer)
	{
		if (!EnablePasswordRetrieval)
		{
			throw new NotSupportedException("this provider has not been configured to allow the retrieval of passwords");
		}
		CheckParam("username", username, 256);
		if (RequiresQuestionAndAnswer)
		{
			CheckParam("passwordAnswer", passwordAnswer, 128);
		}
		PasswordInfo passwordInfo = GetPasswordInfo(username);
		if (passwordInfo == null)
		{
			throw new ProviderException("An error occurred while retrieving the password from the database");
		}
		string parameterValue = EncodePassword(passwordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
		string text = null;
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_GetPassword";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", MaxInvalidPasswordAttempts);
		AddParameter(dbCommand, "@PasswordAttemptWindow", PasswordAttemptWindow);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
		AddParameter(dbCommand, "@PasswordAnswer", parameterValue);
		DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		switch (GetReturnValue(returnValue))
		{
		case 3:
			throw new MembershipPasswordException("Password Answer is invalid");
		case 99:
			throw new MembershipPasswordException("The user account is currently locked out");
		default:
			if (dbDataReader.Read())
			{
				text = dbDataReader.GetString(0);
				dbDataReader.Close();
			}
			if (passwordInfo.PasswordFormat == MembershipPasswordFormat.Clear)
			{
				return text;
			}
			if (passwordInfo.PasswordFormat == MembershipPasswordFormat.Encrypted)
			{
				return DecodePassword(text, passwordInfo.PasswordFormat);
			}
			return text;
		}
	}

	private MembershipUser GetUserFromReader(DbDataReader reader, string username, object userId)
	{
		int num = 0;
		if (username == null)
		{
			num = 1;
		}
		if (userId != null)
		{
			username = reader.GetString(8);
		}
		return new MembershipUser(Name, (username == null) ? reader.GetString(0) : username, (userId == null) ? ((object)reader.GetGuid(8 + num)) : userId, reader.IsDBNull(num) ? null : reader.GetString(num), reader.IsDBNull(1 + num) ? null : reader.GetString(1 + num), reader.IsDBNull(2 + num) ? null : reader.GetString(2 + num), reader.GetBoolean(3 + num), reader.GetBoolean(9 + num), reader.GetDateTime(4 + num).ToLocalTime(), reader.GetDateTime(5 + num).ToLocalTime(), reader.GetDateTime(6 + num).ToLocalTime(), reader.GetDateTime(7 + num).ToLocalTime(), reader.GetDateTime(10 + num).ToLocalTime());
	}

	private MembershipUser BuildMembershipUser(DbCommand query, string username, object userId)
	{
		try
		{
			using DbConnection connection = CreateConnection();
			query.Connection = connection;
			using DbDataReader dbDataReader = query.ExecuteReader();
			if (!dbDataReader.Read())
			{
				return null;
			}
			return GetUserFromReader(dbDataReader, username, userId);
		}
		catch (Exception)
		{
			return null;
		}
		finally
		{
			query.Connection = null;
		}
	}

	/// <summary>Returns information from the SQL Server membership database for a user and provides an option to update the last activity date/time stamp for the user.</summary>
	/// <param name="username">The name of the user to get information for. </param>
	/// <param name="userIsOnline">
	///       <see langword="true" /> to update the last activity date/time stamp for the user; <see langword="false" /> to return user information without updating the last activity date/time stamp for the user. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If no user is found in the database for the specified <paramref name="username" /> value, <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> exceeds 256 characters.- or -
	///         <paramref name="username" /> contains a comma.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public override MembershipUser GetUser(string username, bool userIsOnline)
	{
		if (username == null)
		{
			throw new ArgumentNullException("username");
		}
		if (username.Length == 0)
		{
			return null;
		}
		CheckParam("username", username, 256);
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "aspnet_Membership_GetUserByName";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
		AddParameter(dbCommand, "@UpdateLastActivity", userIsOnline);
		return BuildMembershipUser(dbCommand, username, null);
	}

	/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier and updates the last activity date/time stamp for the user, if specified.</summary>
	/// <param name="providerUserKey">The unique identifier for the user.</param>
	/// <param name="userIsOnline">
	///       <see langword="true" /> to update the last-activity date/time stamp for the specified user; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier. If no user is found in the database for the specified <paramref name="providerUserKey" /> value, <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerUserKey" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="providerUserKey" /> is not of type <see cref="T:System.Guid" />.</exception>
	public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
	{
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "aspnet_Membership_GetUserByUserId";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@UserId", providerUserKey);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
		AddParameter(dbCommand, "@UpdateLastActivity", userIsOnline);
		return BuildMembershipUser(dbCommand, string.Empty, providerUserKey);
	}

	/// <summary>Gets the user name associated with the specified e-mail address.</summary>
	/// <param name="email">The e-mail address to search for. </param>
	/// <returns>The user name associated with the specified e-mail address. If no match is found, this method returns <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="email" /> exceeds 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">More than one user with the same e-mail address exists in the database and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is <see langword="true" />.</exception>
	public override string GetUserNameByEmail(string email)
	{
		CheckParam("email", email, 256);
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_GetUserByEmail";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@Email", email);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		string result = null;
		if (dbDataReader.Read())
		{
			result = dbDataReader.GetString(0);
		}
		dbDataReader.Close();
		return result;
	}

	private bool GetBoolConfigValue(NameValueCollection config, string name, bool def)
	{
		bool result = def;
		string text = config[name];
		if (text != null)
		{
			try
			{
				result = bool.Parse(text);
			}
			catch (Exception innerException)
			{
				throw new ProviderException($"{name} must be true or false", innerException);
			}
		}
		return result;
	}

	private int GetIntConfigValue(NameValueCollection config, string name, int def)
	{
		int result = def;
		string text = config[name];
		if (text != null)
		{
			try
			{
				result = int.Parse(text);
			}
			catch (Exception innerException)
			{
				throw new ProviderException($"{name} must be an integer", innerException);
			}
		}
		return result;
	}

	private int GetEnumConfigValue(NameValueCollection config, string name, Type enumType, int def)
	{
		int result = def;
		string text = config[name];
		if (text != null)
		{
			try
			{
				result = (int)Enum.Parse(enumType, text);
			}
			catch (Exception innerException)
			{
				throw new ProviderException(string.Format("{0} must be one of the following values: {1}", name, string.Join(",", Enum.GetNames(enumType))), innerException);
			}
		}
		return result;
	}

	private string GetStringConfigValue(NameValueCollection config, string name, string def)
	{
		string result = def;
		string text = config[name];
		if (text != null)
		{
			result = text;
		}
		return result;
	}

	private void EmitValidatingPassword(string username, string password, bool isNewUser)
	{
		ValidatePasswordEventArgs validatePasswordEventArgs = new ValidatePasswordEventArgs(username, password, isNewUser);
		OnValidatingPassword(validatePasswordEventArgs);
		if (validatePasswordEventArgs.Cancel)
		{
			if (validatePasswordEventArgs.FailureInformation == null)
			{
				throw new ProviderException("Password validation canceled");
			}
			throw validatePasswordEventArgs.FailureInformation;
		}
	}

	/// <summary>Initializes the SQL Server membership provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Security.SqlMembershipProvider" /> instance to initialize. </param>
	/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the membership provider. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="config" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="enablePasswordRetrieval" />, <see langword="enablePasswordReset" />, <see langword="requiresQuestionAndAnswer" />, or <see langword="requiresUniqueEmail" /> attribute is set to a value other than a <see langword="Boolean" />.- or -The <see langword="maxInvalidPasswordAttempts" /> or the <see langword="passwordAttemptWindow" /> attribute is set to a value other than a positive integer.- or -The <see langword="minRequiredPasswordLength" /> attribute is set to a value other than a positive integer, or the value is greater than 128.- or -The <see langword="minRequiredNonalphanumericCharacters" /> attribute is set to a value other than zero or a positive integer, or the value is greater than 128.- or -The value for the <see langword="passwordStrengthRegularExpression" /> attribute is not a valid regular expression.- or -The <see langword="applicationName" /> attribute is set to a value that is greater than 256 characters.- or -The <see langword="passwordFormat" /> attribute specified in the application configuration file is an invalid <see cref="T:System.Web.Security.MembershipPasswordFormat" /> enumeration.- or -The <see langword="passwordFormat" /> attribute is set to <see cref="F:System.Web.Security.MembershipPasswordFormat.Hashed" /> and the <see langword="enablePasswordRetrieval" /> attribute is set to <see langword="true" /> in the application configuration.- or -The <see langword="passwordFormat" /> attribute is set to <see langword="Encrypted" /> and the  configuration element specifies <see langword="AutoGenerate" /> for the <see langword="decryptionKey" /> attribute.- or -The <see langword="connectionStringName" /> attribute is empty or does not exist in the application configuration.- or - The value of the connection string for the <see langword="connectionStringName" /> attribute value is empty, or the specified <see langword="connectionStringName" /> does not exist in the application configuration file.- or - The value for the <see langword="commandTimeout" /> attribute is set to a value other than zero or a positive integer.- or -The application configuration file for this <see cref="T:System.Web.Security.SqlMembershipProvider" /> instance contains an unrecognized attribute.</exception>
	/// <exception cref="T:System.Web.HttpException">The current trust level is less than <see langword="Low" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The provider has already been initialized prior to the current call to the <see cref="M:System.Web.Security.SqlMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</exception>
	public override void Initialize(string name, NameValueCollection config)
	{
		if (config == null)
		{
			throw new ArgumentNullException("config");
		}
		base.Initialize(name, config);
		applicationName = GetStringConfigValue(config, "applicationName", "/");
		enablePasswordReset = GetBoolConfigValue(config, "enablePasswordReset", def: true);
		enablePasswordRetrieval = GetBoolConfigValue(config, "enablePasswordRetrieval", def: false);
		requiresQuestionAndAnswer = GetBoolConfigValue(config, "requiresQuestionAndAnswer", def: true);
		requiresUniqueEmail = GetBoolConfigValue(config, "requiresUniqueEmail", def: false);
		passwordFormat = (MembershipPasswordFormat)GetEnumConfigValue(config, "passwordFormat", typeof(MembershipPasswordFormat), 1);
		maxInvalidPasswordAttempts = GetIntConfigValue(config, "maxInvalidPasswordAttempts", 5);
		minRequiredPasswordLength = GetIntConfigValue(config, "minRequiredPasswordLength", 7);
		minRequiredNonAlphanumericCharacters = GetIntConfigValue(config, "minRequiredNonalphanumericCharacters", 1);
		passwordAttemptWindow = GetIntConfigValue(config, "passwordAttemptWindow", 10);
		passwordStrengthRegularExpression = GetStringConfigValue(config, "passwordStrengthRegularExpression", "");
		MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
		userIsOnlineTimeWindow = membershipSection.UserIsOnlineTimeWindow;
		if (passwordFormat == MembershipPasswordFormat.Hashed && enablePasswordRetrieval)
		{
			throw new ProviderException("password retrieval cannot be used with hashed passwords");
		}
		string text = config["connectionStringName"];
		if (applicationName.Length > 256)
		{
			throw new ProviderException("The ApplicationName attribute must be 256 characters long or less.");
		}
		if (text == null || text.Length == 0)
		{
			throw new ProviderException("The ConnectionStringName attribute must be present and non-zero length.");
		}
		connectionString = WebConfigurationManager.ConnectionStrings[text];
		factory = ((connectionString == null || string.IsNullOrEmpty(connectionString.ProviderName)) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(connectionString.ProviderName));
	}

	/// <summary>Resets a user's password to a new, automatically generated password.</summary>
	/// <param name="username">The user to reset the password for. </param>
	/// <param name="passwordAnswer">The password answer for the specified user. </param>
	/// <returns>The new password for the specified user.</returns>
	/// <exception cref="T:System.Web.Security.MembershipPasswordException">
	///         <paramref name="passwordAnswer" /> is invalid. - or -The user account is currently locked.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///         <see cref="P:System.Web.Security.SqlMembershipProvider.EnablePasswordReset" /> is set to <see langword="false" />. </exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="username" /> is not found in the membership database.- or -The change password action was canceled by a subscriber to the <see cref="E:System.Web.Security.Membership.ValidatingPassword" /> event and the <see cref="P:System.Web.Security.ValidatePasswordEventArgs.FailureInformation" /> property was <see langword="null" />.- or -An error occurred while retrieving the password from the database. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -
	///         <paramref name="passwordAnswer" /> is an empty string, or is longer than 128 characters, and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.- or -
	///         <paramref name="passwordAnswer" /> is longer than 128 characters after encoding.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="passwordAnswer" /> is <see langword="null" /> and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is <see langword="true" />.</exception>
	/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
	public override string ResetPassword(string username, string passwordAnswer)
	{
		if (!EnablePasswordReset)
		{
			throw new NotSupportedException("this provider has not been configured to allow the resetting of passwords");
		}
		CheckParam("username", username, 256);
		if (RequiresQuestionAndAnswer)
		{
			CheckParam("passwordAnswer", passwordAnswer, 128);
		}
		using DbConnection connection = CreateConnection();
		PasswordInfo passwordInfo = GetPasswordInfo(username);
		if (passwordInfo == null)
		{
			throw new ProviderException(username + "is not found in the membership database");
		}
		string text = GeneratePassword();
		EmitValidatingPassword(username, text, isNewUser: false);
		string parameterValue = EncodePassword(text, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
		string parameterValue2 = EncodePassword(passwordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_ResetPassword";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@NewPassword", parameterValue);
		AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", MaxInvalidPasswordAttempts);
		AddParameter(dbCommand, "@PasswordAttemptWindow", PasswordAttemptWindow);
		AddParameter(dbCommand, "@PasswordSalt", passwordInfo.PasswordSalt);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
		AddParameter(dbCommand, "@PasswordFormat", (int)passwordInfo.PasswordFormat);
		AddParameter(dbCommand, "@PasswordAnswer", parameterValue2);
		DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		return GetReturnValue(returnValue) switch
		{
			0 => text, 
			3 => throw new MembershipPasswordException("Password Answer is invalid"), 
			99 => throw new MembershipPasswordException("The user account is currently locked out"), 
			_ => throw new ProviderException("Failed to reset password"), 
		};
	}

	/// <summary>Updates information about a user in the SQL Server membership database.</summary>
	/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="user" /> is <see langword="null" />. - or -The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> is <see langword="null" />.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is <see langword="null" /> and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to <see langword="true" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is longer than 256 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is an empty string and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to <see langword="true" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> was not found in the database.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> was equal to an existing e-mail address in the database and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to true.- or -The user update failed.</exception>
	public override void UpdateUser(MembershipUser user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user");
		}
		if (user.UserName == null)
		{
			throw new ArgumentNullException("user.UserName");
		}
		if (RequiresUniqueEmail && user.Email == null)
		{
			throw new ArgumentNullException("user.Email");
		}
		CheckParam("user.UserName", user.UserName, 256);
		if (user.Email.Length > 256 || (RequiresUniqueEmail && user.Email.Length == 0))
		{
			throw new ArgumentException("invalid format for user.Email");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_Membership_UpdateUser";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@UserName", user.UserName);
		AddParameter(dbCommand, "@Email", (user.Email == null) ? ((IConvertible)DBNull.Value) : ((IConvertible)user.Email));
		AddParameter(dbCommand, "@Comment", (user.Comment == null) ? ((IConvertible)DBNull.Value) : ((IConvertible)user.Comment));
		AddParameter(dbCommand, "@IsApproved", user.IsApproved);
		AddParameter(dbCommand, "@LastLoginDate", DateTime.UtcNow);
		AddParameter(dbCommand, "@LastActivityDate", DateTime.UtcNow);
		AddParameter(dbCommand, "@UniqueEmail", RequiresUniqueEmail);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
		DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		switch (GetReturnValue(returnValue))
		{
		case 1:
			throw new ProviderException("The UserName property of user was not found in the database.");
		case 7:
			throw new ProviderException("The Email property of user was equal to an existing e-mail address in the database and RequiresUniqueEmail is set to true.");
		default:
			throw new ProviderException("Failed to update user");
		case 0:
			break;
		}
	}

	/// <summary>Verifies that the specified user name and password exist in the SQL Server membership database.</summary>
	/// <param name="username">The name of the user to validate. </param>
	/// <param name="password">The password for the specified user. </param>
	/// <returns>
	///     <see langword="true" /> if the specified username and password are valid; otherwise, <see langword="false" />. A value of <see langword="false" /> is also returned if the user does not exist in the database.</returns>
	public override bool ValidateUser(string username, string password)
	{
		if (username.Length == 0)
		{
			return false;
		}
		CheckParam("username", username, 256);
		EmitValidatingPassword(username, password, isNewUser: false);
		PasswordInfo passwordInfo = ValidateUsingPassword(username, password);
		if (passwordInfo != null)
		{
			passwordInfo.LastLoginDate = DateTime.UtcNow;
			UpdateUserInfo(username, passwordInfo, isPasswordCorrect: true, updateLoginActivity: true);
			return true;
		}
		return false;
	}

	/// <summary>Clears the user's locked-out status so that the membership user can be validated.</summary>
	/// <param name="username">The name of the membership user to clear the locked-out status for.</param>
	/// <returns>
	///     <see langword="true" /> if the membership user was successfully unlocked; otherwise, <see langword="false" />. A value of <see langword="false" /> is also returned if the user does not exist in the database.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string, is longer than 256 characters, or contains a comma.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public override bool UnlockUser(string username)
	{
		CheckParam("username", username, 256);
		using (DbConnection connection = CreateConnection())
		{
			try
			{
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.Connection = connection;
				dbCommand.CommandText = "aspnet_Membership_UnlockUser";
				dbCommand.CommandType = CommandType.StoredProcedure;
				AddParameter(dbCommand, "@ApplicationName", ApplicationName);
				AddParameter(dbCommand, "@UserName", username);
				DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				if (GetReturnValue(returnValue) != 0)
				{
					return false;
				}
			}
			catch (Exception innerException)
			{
				throw new ProviderException("Failed to unlock user", innerException);
			}
		}
		return true;
	}

	private void UpdateUserInfo(string username, PasswordInfo pi, bool isPasswordCorrect, bool updateLoginActivity)
	{
		CheckParam("username", username, 256);
		using DbConnection connection = CreateConnection();
		try
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.Connection = connection;
			dbCommand.CommandText = "aspnet_Membership_UpdateUserInfo";
			dbCommand.CommandType = CommandType.StoredProcedure;
			AddParameter(dbCommand, "@ApplicationName", ApplicationName);
			AddParameter(dbCommand, "@UserName", username);
			AddParameter(dbCommand, "@IsPasswordCorrect", isPasswordCorrect);
			AddParameter(dbCommand, "@UpdateLastLoginActivityDate", updateLoginActivity);
			AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", MaxInvalidPasswordAttempts);
			AddParameter(dbCommand, "@PasswordAttemptWindow", PasswordAttemptWindow);
			AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
			AddParameter(dbCommand, "@LastLoginDate", pi.LastLoginDate);
			AddParameter(dbCommand, "@LastActivityDate", pi.LastActivityDate);
			DbParameter returnValue = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
			dbCommand.ExecuteNonQuery();
			GetReturnValue(returnValue);
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Failed to update Membership table", innerException);
		}
	}

	private PasswordInfo ValidateUsingPassword(string username, string password)
	{
		MembershipUser user = GetUser(username, userIsOnline: true);
		if (user == null)
		{
			return null;
		}
		if (!user.IsApproved || user.IsLockedOut)
		{
			return null;
		}
		PasswordInfo passwordInfo = GetPasswordInfo(username);
		if (passwordInfo == null)
		{
			return null;
		}
		if (EncodePassword(password, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt) != passwordInfo.Password)
		{
			UpdateUserInfo(username, passwordInfo, isPasswordCorrect: false, updateLoginActivity: false);
			return null;
		}
		return passwordInfo;
	}

	private PasswordInfo GetPasswordInfo(string username)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Membership_GetPasswordWithFormat";
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@UpdateLastLoginActivityDate", false);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
		AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		if (!dbDataReader.Read())
		{
			return null;
		}
		return new PasswordInfo(dbDataReader.GetString(0), (MembershipPasswordFormat)dbDataReader.GetInt32(1), dbDataReader.GetString(2), dbDataReader.GetInt32(3), dbDataReader.GetInt32(4), dbDataReader.GetBoolean(5), dbDataReader.GetDateTime(6), dbDataReader.GetDateTime(7));
	}

	private string EncodePassword(string password, MembershipPasswordFormat passwordFormat, string salt)
	{
		switch (passwordFormat)
		{
		case MembershipPasswordFormat.Clear:
			return password;
		case MembershipPasswordFormat.Hashed:
		{
			byte[] bytes = Encoding.Unicode.GetBytes(password);
			byte[] array = Convert.FromBase64String(salt);
			byte[] array3 = new byte[array.Length + bytes.Length];
			Buffer.BlockCopy(array, 0, array3, 0, array.Length);
			Buffer.BlockCopy(bytes, 0, array3, array.Length, bytes.Length);
			string text = ((MembershipSection)WebConfigurationManager.GetSection("system.web/membership")).HashAlgorithmType;
			if (text.Length == 0)
			{
				text = MachineKeySection.Config.Validation.ToString();
				if (text.StartsWith("alg:"))
				{
					text = text.Substring(4);
				}
			}
			using HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text);
			if (hashAlgorithm is KeyedHashAlgorithm keyedHashAlgorithm)
			{
				keyedHashAlgorithm.Key = MachineKeySection.Config.GetValidationKey();
			}
			hashAlgorithm.TransformFinalBlock(array3, 0, array3.Length);
			return Convert.ToBase64String(hashAlgorithm.Hash);
		}
		case MembershipPasswordFormat.Encrypted:
		{
			byte[] bytes = Encoding.Unicode.GetBytes(password);
			byte[] array = Convert.FromBase64String(salt);
			byte[] array2 = new byte[bytes.Length + array.Length];
			Array.Copy(array, 0, array2, 0, array.Length);
			Array.Copy(bytes, 0, array2, array.Length, bytes.Length);
			return Convert.ToBase64String(EncryptPassword(array2));
		}
		default:
			return null;
		}
	}

	private string DecodePassword(string password, MembershipPasswordFormat passwordFormat)
	{
		return passwordFormat switch
		{
			MembershipPasswordFormat.Clear => password, 
			MembershipPasswordFormat.Hashed => throw new ProviderException("Hashed passwords cannot be decoded."), 
			MembershipPasswordFormat.Encrypted => Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password))), 
			_ => null, 
		};
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.SqlMembershipProvider" /> class.</summary>
	public SqlMembershipProvider()
	{
	}
}
