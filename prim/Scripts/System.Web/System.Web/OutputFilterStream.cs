using System.IO;

namespace System.Web;

internal sealed class OutputFilterStream : Stream
{
	private HttpResponseStream stream;

	public override bool CanRead => false;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Position
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public override long Length
	{
		get
		{
			throw new NotSupportedException();
		}
	}

	public OutputFilterStream(HttpResponseStream stream)
	{
		this.stream = stream;
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException();
	}

	public override int ReadByte()
	{
		throw new NotSupportedException();
	}

	public override long Seek(long offset, SeekOrigin loc)
	{
		throw new NotSupportedException();
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException("This stream can not change its size");
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		stream.Write(buffer, offset, count);
	}

	public override void Flush()
	{
	}
}
