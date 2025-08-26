using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.UI;

internal class ResourceBasedLiteralControl : LiteralControl
{
	private IntPtr ptr;

	private int length;

	public override string Text
	{
		get
		{
			if (length == -1)
			{
				return base.Text;
			}
			byte[] array = new byte[length];
			Marshal.Copy(ptr, array, 0, length);
			return Encoding.UTF8.GetString(array);
		}
		set
		{
			length = -1;
			base.Text = value;
		}
	}

	public ResourceBasedLiteralControl(IntPtr ptr, int length)
	{
		EnableViewState = false;
		base.AutoID = false;
		this.ptr = ptr;
		this.length = length;
	}

	protected internal override void Render(HtmlTextWriter writer)
	{
		if (length == -1)
		{
			writer.Write(base.Text);
			return;
		}
		HttpWriter httpWriter = writer.GetHttpWriter();
		if (httpWriter == null || httpWriter.Response.ContentEncoding.CodePage != 65001)
		{
			byte[] array = new byte[length];
			Marshal.Copy(ptr, array, 0, length);
			writer.Write(Encoding.UTF8.GetString(array));
			array = null;
		}
		else
		{
			httpWriter.WriteUTF8Ptr(ptr, length);
		}
	}
}
