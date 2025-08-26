using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Mail;

/// <summary>Provides properties and methods for constructing an e-mail attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
[Obsolete("The recommended alternative is System.Net.Mail.Attachment. http://go.microsoft.com/fwlink/?linkid=14202")]
public class MailAttachment
{
	private string filename;

	private MailEncoding encoding;

	/// <summary>Gets the name of the attachment file. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>The name of the attachment file.</returns>
	public string Filename => filename;

	/// <summary>Gets the type of encoding for the e-mail attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.Mail.MailEncoding" /> values.</returns>
	public MailEncoding Encoding => encoding;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailAttachment" /> class with the specified file name for the attachment. Sets the <see cref="T:System.Text.Encoding" /> property to <see cref="F:System.Web.Mail.MailEncoding.UUEncode" /> by default. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <param name="filename">The name of the attachment file. </param>
	public MailAttachment(string filename)
		: this(filename, MailEncoding.Base64)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailAttachment" /> class with the specified file name and encoding type for the attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	/// <param name="filename">The name of the attachment file. </param>
	/// <param name="encoding">The type of <see cref="T:System.Web.Mail.MailEncoding" /> for the attachment. </param>
	public MailAttachment(string filename, MailEncoding encoding)
	{
		if (SecurityManager.SecurityEnabled)
		{
			new FileIOPermission(FileIOPermissionAccess.Read, filename).Demand();
		}
		if (!File.Exists(filename))
		{
			throw new HttpException(string.Format(Locale.GetText("Cannot find file: '{0}'."), filename));
		}
		this.filename = filename;
		this.encoding = encoding;
	}
}
