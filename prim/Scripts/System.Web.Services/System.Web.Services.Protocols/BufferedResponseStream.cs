using System.IO;

namespace System.Web.Services.Protocols;

internal class BufferedResponseStream : Stream
{
	private Stream outputStream;

	private byte[] buffer;

	private int position;

	private bool flushEnabled = true;

	public override bool CanRead => false;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Length
	{
		get
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
		}
	}

	public override long Position
	{
		get
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
		}
		set
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
		}
	}

	internal bool FlushEnabled
	{
		set
		{
			flushEnabled = value;
		}
	}

	internal BufferedResponseStream(Stream outputStream, int buffersize)
	{
		buffer = new byte[buffersize];
		this.outputStream = outputStream;
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing)
			{
				outputStream.Close();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	public override void Flush()
	{
		if (flushEnabled)
		{
			FlushWrite();
		}
	}

	public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
	}

	public override int EndRead(IAsyncResult asyncResult)
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
	}

	public override int ReadByte()
	{
		throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
	}

	public override void Write(byte[] array, int offset, int count)
	{
		if (position > 0)
		{
			int num = buffer.Length - position;
			if (num > 0)
			{
				if (num > count)
				{
					num = count;
				}
				Array.Copy(array, offset, buffer, position, num);
				position += num;
				if (count == num)
				{
					return;
				}
				offset += num;
				count -= num;
			}
			FlushWrite();
		}
		if (count >= buffer.Length)
		{
			outputStream.Write(array, offset, count);
			return;
		}
		Array.Copy(array, offset, buffer, position, count);
		position = count;
	}

	private void FlushWrite()
	{
		if (position > 0)
		{
			outputStream.Write(buffer, 0, position);
			position = 0;
		}
		outputStream.Flush();
	}

	public override void WriteByte(byte value)
	{
		if (position == buffer.Length)
		{
			FlushWrite();
		}
		buffer[position++] = value;
	}
}
