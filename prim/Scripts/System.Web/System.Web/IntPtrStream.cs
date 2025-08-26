using System.IO;
using System.Runtime.InteropServices;

namespace System.Web;

internal class IntPtrStream : Stream
{
	private unsafe byte* base_address;

	private int size;

	private int position;

	private bool owns;

	protected unsafe IntPtr BaseAddress => (IntPtr)base_address;

	protected int Size => size;

	public override bool CanRead => true;

	public override bool CanSeek => true;

	public override bool CanWrite => false;

	public override long Position
	{
		get
		{
			return position;
		}
		set
		{
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException("Position", "Can not be negative");
			}
			if (position > size)
			{
				throw new ArgumentOutOfRangeException("Position", "Pointer falls out of range");
			}
			position = (int)value;
		}
	}

	public override long Length => size;

	public unsafe IntPtrStream(IntPtr base_address, int size)
	{
		this.base_address = (byte*)(void*)base_address;
		this.size = size;
		owns = true;
	}

	public unsafe IntPtrStream(Stream stream)
	{
		IntPtrStream intPtrStream = (IntPtrStream)stream;
		size = intPtrStream.size;
		base_address = intPtrStream.base_address;
	}

	public unsafe override int Read(byte[] buffer, int offset, int count)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (offset < 0 || count < 0)
		{
			throw new ArgumentOutOfRangeException("offset or count less than zero.");
		}
		if (buffer.Length - offset < count)
		{
			throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
		}
		if (base_address == null)
		{
			throw new ObjectDisposedException("Stream has been closed");
		}
		if (position >= size || count == 0)
		{
			return 0;
		}
		if (position > size - count)
		{
			count = size - position;
		}
		Marshal.Copy((IntPtr)(base_address + position), buffer, offset, count);
		position += count;
		return count;
	}

	public unsafe override int ReadByte()
	{
		if (position >= size)
		{
			return -1;
		}
		if (base_address == null)
		{
			throw new ObjectDisposedException("Stream has been closed");
		}
		return base_address[position++];
	}

	public unsafe override long Seek(long offset, SeekOrigin loc)
	{
		if (offset > int.MaxValue)
		{
			throw new ArgumentOutOfRangeException("Offset out of range. " + offset);
		}
		if (base_address == null)
		{
			throw new ObjectDisposedException("Stream has been closed");
		}
		int num;
		switch (loc)
		{
		case SeekOrigin.Begin:
			if (offset < 0)
			{
				throw new IOException("Attempted to seek before start of MemoryStream.");
			}
			num = 0;
			break;
		case SeekOrigin.Current:
			num = position;
			break;
		case SeekOrigin.End:
			num = size;
			break;
		default:
			throw new ArgumentException("loc", "Invalid SeekOrigin");
		}
		try
		{
			num = checked(num + (int)offset);
		}
		catch
		{
			throw new ArgumentOutOfRangeException("Too large seek destination");
		}
		if (num < 0)
		{
			throw new IOException("Attempted to seek before start of MemoryStream.");
		}
		position = num;
		return position;
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

	public unsafe override void Close()
	{
		if (owns)
		{
			IntPtr intPtr = (IntPtr)base_address;
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			base_address = null;
		}
	}

	unsafe ~IntPtrStream()
	{
		if (owns)
		{
			IntPtr intPtr = (IntPtr)base_address;
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			base_address = null;
		}
	}
}
