using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Configuration;

namespace System.Web.UI.WebControls;

/// <summary>Allows a control to create e-mail messages from text files or strings. This class cannot be inherited.</summary>
[Bindable(false)]
[ParseChildren(true)]
public sealed class MailDefinition : IStateManager
{
	private StateBag _bag = new StateBag();

	/// <summary>Gets or sets the name of the file that contains text for the body of the e-mail message.</summary>
	/// <returns>The name of the file that contains the message body text. The default is <see cref="F:System.String.Empty" />.</returns>
	[Editor("System.Web.UI.Design.MailDefinitionBodyFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	[UrlProperty("*.*")]
	public string BodyFileName
	{
		get
		{
			return _bag.GetString("BodyFileName", string.Empty);
		}
		set
		{
			_bag["BodyFileName"] = value;
		}
	}

	/// <summary>Gets or sets a comma-separated list of e-mail addresses to send a copy (CC) of the message to.</summary>
	/// <returns>A comma-separated list of e-mail addresses to send a copy (CC) of the message to. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	public string CC
	{
		get
		{
			return _bag.GetString("CC", string.Empty);
		}
		set
		{
			_bag["CC"] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> instances, typically used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</summary>
	/// <returns>An <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> instances used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	public EmbeddedMailObjectsCollection EmbeddedObjects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the e-mail address of the message sender.</summary>
	/// <returns>The e-mail address of the message sender. The default is <see cref="F:System.String.Empty" />.</returns>
	[NotifyParentProperty(true)]
	[DefaultValue("")]
	public string From
	{
		get
		{
			return _bag.GetString("From", string.Empty);
		}
		set
		{
			_bag["From"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the body of the e-mail is HTML.</summary>
	/// <returns>
	///     <see langword="true" /> if the body of the e-mail is HTML; otherwise, <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	public bool IsBodyHtml
	{
		get
		{
			return _bag.GetBool("IsBodyHtml", def: false);
		}
		set
		{
			_bag["IsBodyHtml"] = value;
		}
	}

	/// <summary>Gets or sets the priority of the e-mail message.</summary>
	/// <returns>One of the <see cref="T:System.Net.Mail.MailPriority" /> values. The default is <see cref="F:System.Net.Mail.MailPriority.Normal" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Net.Mail.MailPriority" /> values.</exception>
	[DefaultValue(MailPriority.Normal)]
	[NotifyParentProperty(true)]
	public MailPriority Priority
	{
		get
		{
			if (_bag["Priority"] != null)
			{
				return (MailPriority)_bag["Priority"];
			}
			return MailPriority.Normal;
		}
		set
		{
			_bag["Priority"] = value;
		}
	}

	/// <summary>Gets or sets the subject line of the e-mail message.</summary>
	/// <returns>The subject line of the e-mail message. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	public string Subject
	{
		get
		{
			return _bag.GetString("Subject", string.Empty);
		}
		set
		{
			_bag["Subject"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => _bag.IsTrackingViewState;

	/// <summary>Creates an e-mail message from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
	/// <param name="recipients">A comma-separated list of message recipients.</param>
	/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
	/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
	/// <returns>The e-mail message from a text file.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="replacements" /> does not contain strings.</exception>
	/// <exception cref="T:System.Web.HttpException">The <see langword="From" /> value in the SMTP section of the configuration file is <see langword="null" /> or the empty string- or -
	///         <paramref name="recipients" /> contains an incorrect e-mail address.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="owner" /> is <see langword="null" />.</exception>
	public MailMessage CreateMailMessage(string recipients, IDictionary replacements, Control owner)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		string body = null;
		if (BodyFileName.Length > 0)
		{
			string text = null;
			text = ((!Path.IsPathRooted(BodyFileName)) ? HttpContext.Current.Request.MapPath(VirtualPathUtility.Combine(owner.TemplateSourceDirectory, BodyFileName)) : BodyFileName);
			using StreamReader streamReader = new StreamReader(text);
			body = streamReader.ReadToEnd();
		}
		else
		{
			body = "";
		}
		return CreateMailMessage(recipients, replacements, body, owner);
	}

	/// <summary>Creates an e-mail message with replacements from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
	/// <param name="recipients">The comma-separated list of recipients.</param>
	/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
	/// <param name="body">The text of the e-mail message.</param>
	/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
	/// <returns>The e-mail message with replacements from a text file.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="replacements" /> does not contain strings.</exception>
	/// <exception cref="T:System.Web.HttpException">The <see langword="From" /> value in the SMTP section of the configuration file is <see langword="null" /> or an empty string ("").- or -
	///         <paramref name="recipients" /> contains an incorrect e-mail address.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="owner" /> is <see langword="null" />.</exception>
	public MailMessage CreateMailMessage(string recipients, IDictionary replacements, string body, Control owner)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		MailMessage mailMessage = new MailMessage();
		if (CC.Length > 0)
		{
			mailMessage.CC.Add(CC);
		}
		mailMessage.IsBodyHtml = IsBodyHtml;
		mailMessage.Priority = Priority;
		mailMessage.Subject = Subject;
		mailMessage.Body = body;
		if (From.Length > 0)
		{
			mailMessage.From = new MailAddress(From);
		}
		else
		{
			SmtpSection smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
			if (smtpSection != null)
			{
				if (string.IsNullOrEmpty(smtpSection.From))
				{
					throw new HttpException("A from e-mail address must be specified in the From property or the system.net/mailSettings/smtp config section");
				}
				mailMessage.From = new MailAddress(smtpSection.From);
			}
		}
		string[] array = recipients.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			mailMessage.To.Add(array[i]);
		}
		foreach (DictionaryEntry replacement in replacements)
		{
			mailMessage.Body = mailMessage.Body.Replace((string)replacement.Key, (string)replacement.Value);
		}
		return mailMessage;
	}

	/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
	void IStateManager.LoadViewState(object state)
	{
		_bag.LoadViewState(state);
	}

	/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>The server control's current view state.</returns>
	object IStateManager.SaveViewState()
	{
		return _bag.SaveViewState();
	}

	/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	void IStateManager.TrackViewState()
	{
		_bag.TrackViewState();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
	public MailDefinition()
	{
	}
}
