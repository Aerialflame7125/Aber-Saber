using System.ComponentModel;
using System.IO;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays a text box control and a browse button that enable users to select a file to upload to the server.</summary>
[ControlValueProperty("FileBytes")]
[ValidationProperty("FileName")]
[Designer("DesignerBaseTypeNameSystem.ComponentModel.Design.IDesignerDesignerTypeNameSystem.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
public class FileUpload : WebControl
{
	private byte[] cachedBytes;

	/// <summary>Gets an array of the bytes in a file that is specified by using a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
	/// <returns>A <see cref="T:System.Byte" /> array that contains the contents of the specified file.</returns>
	/// <exception cref="T:System.Web.HttpException">The entire file was not read.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Bindable(true, BindingDirection.OneWay)]
	[Browsable(false)]
	public byte[] FileBytes
	{
		get
		{
			if (cachedBytes == null)
			{
				cachedBytes = new byte[FileContent.Length];
				FileContent.Read(cachedBytes, 0, cachedBytes.Length);
			}
			return (byte[])cachedBytes.Clone();
		}
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Stream FileContent
	{
		get
		{
			if (PostedFile == null)
			{
				return Stream.Null;
			}
			Stream inputStream = PostedFile.InputStream;
			if (inputStream != null)
			{
				inputStream.Position = 0L;
			}
			return inputStream;
		}
	}

	/// <summary>Gets the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
	/// <returns>A string that specifies the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string FileName
	{
		get
		{
			if (PostedFile == null)
			{
				return string.Empty;
			}
			return Path.GetFileName(PostedFile.FileName);
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control contains a file.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.FileUpload" /> contains a file; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool HasFile
	{
		get
		{
			HttpPostedFile postedFile = PostedFile;
			if (postedFile != null)
			{
				return !string.IsNullOrEmpty(postedFile.FileName);
			}
			return false;
		}
	}

	/// <summary>Gets the underlying <see cref="T:System.Web.HttpPostedFile" /> object for a file that is uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> for a file uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpPostedFile PostedFile
	{
		get
		{
			Page page = Page;
			if (page == null || !page.IsPostBack)
			{
				return null;
			}
			if (Context == null || Context.Request == null)
			{
				return null;
			}
			return Context.Request.Files[UniqueID];
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FileUpload" /> class.</summary>
	public FileUpload()
		: base(HtmlTextWriterTag.Input)
	{
	}

	/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		writer.AddAttribute(HtmlTextWriterAttribute.Type, "file", fEncode: false);
		if (!string.IsNullOrEmpty(UniqueID))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null)
		{
			page.Form.Enctype = "multipart/form-data";
		}
	}

	/// <summary>Sends the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to render on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		Page?.VerifyRenderingInServerForm(this);
		base.Render(writer);
	}

	/// <summary>Saves the contents of an uploaded file to a specified path on the Web server.</summary>
	/// <param name="filename">A string that specifies the full path of the location of the server on which to save the uploaded file. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="filename" /> is not a full path.</exception>
	public void SaveAs(string filename)
	{
		PostedFile?.SaveAs(filename);
	}
}
