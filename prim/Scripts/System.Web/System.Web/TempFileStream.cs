using System.IO;
using System.Runtime.InteropServices;

namespace System.Web;

internal class TempFileStream : FileStream
{
	private bool read_mode;

	private bool disposed;

	private long saved_position;

	public override bool CanRead => read_mode;

	public override bool CanWrite => !read_mode;

	public TempFileStream(string name)
		: base(name, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192)
	{
	}

	public void SavePosition()
	{
		saved_position = Position;
		Position = 0L;
	}

	public void RestorePosition()
	{
		Position = saved_position;
		saved_position = -1L;
	}

	public void SetReadOnly()
	{
		read_mode = true;
		Position = 0L;
	}

	public void SetWriteOnly()
	{
		read_mode = false;
		Position = 0L;
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (read_mode)
		{
			throw new InvalidOperationException("mode read");
		}
		base.Write(buffer, offset, count);
	}

	public override int Read([In][Out] byte[] buffer, int offset, int count)
	{
		if (!read_mode)
		{
			throw new InvalidOperationException("mode write");
		}
		return base.Read(buffer, offset, count);
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposed)
		{
			disposed = true;
			base.Dispose(disposing);
			try
			{
				File.Delete(base.Name);
			}
			catch
			{
			}
		}
	}
}
