using System.IO;

namespace System.Web;

internal class InputFilterStream : Stream
{
	private Stream stream;

	internal Stream BaseStream
	{
		set
		{
			stream = value;
		}
	}

	public override bool CanRead => true;

	public override bool CanSeek => true;

	public override bool CanWrite => false;

	public override long Position
	{
		get
		{
			return stream.Position;
		}
		set
		{
			stream.Position = value;
		}
	}

	public override long Length => stream.Length;

	public override int Read(byte[] buffer, int offset, int count)
	{
		return stream.Read(buffer, offset, count);
	}

	public override int ReadByte()
	{
		return stream.ReadByte();
	}

	public override long Seek(long offset, SeekOrigin loc)
	{
		return stream.Seek(offset, loc);
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException("This stream can not change its size");
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException("This stream can not change its size");
	}

	public override void WriteByte(byte value)
	{
		throw new NotSupportedException("This stream can not change its size");
	}

	public override void Flush()
	{
	}

	public override void Close()
	{
		stream.Close();
	}
}
