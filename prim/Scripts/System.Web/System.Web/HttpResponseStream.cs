using System.IO;
using System.Runtime.InteropServices;

namespace System.Web;

internal class HttpResponseStream : Stream
{
	private sealed class BlockManager
	{
		private const int PreferredLength = 131072;

		private unsafe byte* data;

		private int position;

		private int block_size;

		public int Position => position;

		private unsafe void EnsureCapacity(int capacity)
		{
			if (block_size < capacity)
			{
				capacity += 131072;
				capacity = capacity / 131072 * 131072;
				data = (byte*)((data == null) ? ((void*)Marshal.AllocHGlobal(capacity)) : ((void*)Marshal.ReAllocHGlobal((IntPtr)data, (IntPtr)capacity)));
				block_size = capacity;
			}
		}

		public unsafe void Write(byte[] buffer, int offset, int count)
		{
			if (count != 0)
			{
				EnsureCapacity(position + count);
				Marshal.Copy(buffer, offset, (IntPtr)(data + position), count);
				position += count;
			}
		}

		public unsafe void Write(IntPtr ptr, int count)
		{
			if (count == 0)
			{
				return;
			}
			EnsureCapacity(position + count);
			byte* src = (byte*)ptr.ToPointer();
			if (count < 32)
			{
				byte* ptr2 = data + position;
				for (int i = 0; i < count; i++)
				{
					*(ptr2++) = *(src++);
				}
			}
			else
			{
				memcpy(data + position, src, count);
			}
			position += count;
		}

		public unsafe void Send(HttpWorkerRequest wr, int start, int end)
		{
			if (end - start > 0)
			{
				wr.SendResponseFromMemory((IntPtr)(data + start), end - start);
			}
		}

		public unsafe void Send(Stream stream, int start, int end)
		{
			int num = end - start;
			if (num <= 0)
			{
				return;
			}
			byte[] array = new byte[Math.Min(num, 32768)];
			int num2 = array.Length;
			while (num > 0)
			{
				Marshal.Copy((IntPtr)(data + start), array, 0, num2);
				stream.Write(array, 0, num2);
				start += num2;
				num -= num2;
				if (num > 0 && num < num2)
				{
					num2 = num;
				}
			}
		}

		public unsafe void Dispose()
		{
			if ((IntPtr)data != IntPtr.Zero)
			{
				Marshal.FreeHGlobal((IntPtr)data);
				data = (byte*)(void*)IntPtr.Zero;
			}
		}
	}

	private abstract class Bucket
	{
		public Bucket Next;

		public abstract int Length { get; }

		public virtual void Dispose()
		{
		}

		public abstract void Send(HttpWorkerRequest wr);

		public abstract void Send(Stream stream);
	}

	private class ByteBucket : Bucket
	{
		private int start;

		private int length;

		public BlockManager blocks;

		public bool Expandable = true;

		public override int Length => length;

		public ByteBucket()
			: this(null)
		{
		}

		public ByteBucket(BlockManager blocks)
		{
			if (blocks == null)
			{
				blocks = new BlockManager();
			}
			this.blocks = blocks;
			start = blocks.Position;
		}

		public unsafe int Write(byte[] buf, int offset, int count)
		{
			if (!Expandable)
			{
				throw new Exception("This should not happen.");
			}
			fixed (byte* ptr = &buf[0])
			{
				IntPtr ptr2 = new IntPtr(ptr + offset);
				blocks.Write(ptr2, count);
			}
			length += count;
			return count;
		}

		public int Write(IntPtr ptr, int count)
		{
			if (!Expandable)
			{
				throw new Exception("This should not happen.");
			}
			blocks.Write(ptr, count);
			length += count;
			return count;
		}

		public override void Dispose()
		{
			blocks.Dispose();
		}

		public override void Send(HttpWorkerRequest wr)
		{
			if (length != 0)
			{
				blocks.Send(wr, start, length);
			}
		}

		public override void Send(Stream stream)
		{
			if (length != 0)
			{
				blocks.Send(stream, start, length);
			}
		}
	}

	private class BufferedFileBucket : Bucket
	{
		private string file;

		private long offset;

		private long length;

		public override int Length => (int)length;

		public BufferedFileBucket(string f, long off, long len)
		{
			file = f;
			offset = off;
			length = len;
		}

		public override void Send(HttpWorkerRequest wr)
		{
			wr.SendResponseFromFile(file, offset, length);
		}

		public override void Send(Stream stream)
		{
			using FileStream fileStream = File.OpenRead(file);
			byte[] buffer = new byte[Math.Min(fileStream.Length, 32768L)];
			long num = fileStream.Length;
			int num2;
			while (num > 0 && (num2 = fileStream.Read(buffer, 0, (int)Math.Min(num, 32768L))) != 0)
			{
				num -= num2;
				stream.Write(buffer, 0, num2);
			}
		}

		public override string ToString()
		{
			return "file " + file + " " + length + " bytes from position " + offset;
		}
	}

	private Bucket first_bucket;

	private Bucket cur_bucket;

	private HttpResponse response;

	internal long total;

	private Stream filter;

	private byte[] chunk_buffer = new byte[24];

	private bool filtering;

	private const string notsupported = "HttpResponseStream is a forward, write-only stream";

	internal bool HaveFilter => filter != null;

	public Stream Filter
	{
		get
		{
			if (filter == null)
			{
				filter = new OutputFilterStream(this);
			}
			return filter;
		}
		set
		{
			filter = value;
		}
	}

	public override bool CanRead => false;

	public override bool CanSeek => false;

	public override bool CanWrite => true;

	public override long Length
	{
		get
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}
	}

	public override long Position
	{
		get
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}
		set
		{
			throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
		}
	}

	public HttpResponseStream(HttpResponse response)
	{
		this.response = response;
	}

	private void AppendBucket(Bucket b)
	{
		if (first_bucket == null)
		{
			cur_bucket = (first_bucket = b);
			return;
		}
		cur_bucket.Next = b;
		cur_bucket = b;
	}

	public override void Flush()
	{
	}

	private void SendChunkSize(long l, bool last)
	{
		if (l == 0L && !last)
		{
			return;
		}
		int i = 0;
		if (l >= 0)
		{
			for (string text = l.ToString("x"); i < text.Length; i++)
			{
				chunk_buffer[i] = (byte)text[i];
			}
		}
		chunk_buffer[i++] = 13;
		chunk_buffer[i++] = 10;
		if (last)
		{
			chunk_buffer[i++] = 13;
			chunk_buffer[i++] = 10;
		}
		response.WorkerRequest.SendResponseFromMemory(chunk_buffer, i);
	}

	internal void Flush(HttpWorkerRequest wr, bool final_flush)
	{
		if (total == 0L && !final_flush)
		{
			return;
		}
		if (response.use_chunked)
		{
			SendChunkSize(total, last: false);
		}
		for (Bucket next = first_bucket; next != null; next = next.Next)
		{
			next.Send(wr);
		}
		if (response.use_chunked)
		{
			SendChunkSize(-1L, last: false);
			if (final_flush)
			{
				SendChunkSize(0L, last: true);
			}
		}
		wr.FlushResponse(final_flush);
		Clear();
	}

	internal int GetTotalLength()
	{
		int num = 0;
		for (Bucket next = first_bucket; next != null; next = next.Next)
		{
			num += next.Length;
		}
		return num;
	}

	internal MemoryStream GetData()
	{
		MemoryStream memoryStream = new MemoryStream();
		for (Bucket next = first_bucket; next != null; next = next.Next)
		{
			next.Send(memoryStream);
		}
		return memoryStream;
	}

	public void WriteFile(string f, long offset, long length)
	{
		if (length != 0L)
		{
			ByteBucket byteBucket = cur_bucket as ByteBucket;
			if (byteBucket != null)
			{
				byteBucket.Expandable = false;
				byteBucket = new ByteBucket(byteBucket.blocks);
			}
			total += length;
			AppendBucket(new BufferedFileBucket(f, offset, length));
			if (byteBucket != null)
			{
				AppendBucket(byteBucket);
			}
		}
	}

	internal void ApplyFilter(bool close)
	{
		if (filter != null)
		{
			filtering = true;
			Bucket bucket = first_bucket;
			first_bucket = null;
			cur_bucket = null;
			total = 0L;
			for (Bucket bucket2 = bucket; bucket2 != null; bucket2 = bucket2.Next)
			{
				bucket2.Send(filter);
			}
			for (Bucket bucket3 = bucket; bucket3 != null; bucket3 = bucket3.Next)
			{
				bucket3.Dispose();
			}
			if (close)
			{
				filter.Flush();
				filter.Close();
				filter = null;
			}
			else
			{
				filter.Flush();
			}
			filtering = false;
		}
	}

	public void WritePtr(IntPtr ptr, int length)
	{
		if (length == 0)
		{
			return;
		}
		if (response.BufferOutput)
		{
			AppendBuffer(ptr, length);
			return;
		}
		if (filter == null || filtering)
		{
			response.WriteHeaders(final_flush: false);
			HttpWorkerRequest workerRequest = response.WorkerRequest;
			workerRequest.SendResponseFromMemory(ptr, length);
			workerRequest.FlushResponse(finalFlush: false);
			return;
		}
		filtering = true;
		try
		{
			byte[] array = new byte[length];
			Marshal.Copy(ptr, array, 0, length);
			filter.Write(array, 0, length);
			array = null;
		}
		finally
		{
			filtering = false;
		}
		Flush(response.WorkerRequest, final_flush: false);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		bool bufferOutput = response.BufferOutput;
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		int num = buffer.Length - offset;
		if (offset < 0 || num <= 0)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException("count");
		}
		if (count > num)
		{
			count = num;
		}
		if (bufferOutput)
		{
			AppendBuffer(buffer, offset, count);
		}
		else if (filter == null || filtering)
		{
			response.WriteHeaders(final_flush: false);
			HttpWorkerRequest workerRequest = response.WorkerRequest;
			if (offset == 0)
			{
				workerRequest.SendResponseFromMemory(buffer, count);
			}
			else
			{
				UnsafeWrite(workerRequest, buffer, offset, count);
			}
			workerRequest.FlushResponse(finalFlush: false);
		}
		else
		{
			filtering = true;
			try
			{
				filter.Write(buffer, offset, count);
			}
			finally
			{
				filtering = false;
			}
			Flush(response.WorkerRequest, final_flush: false);
		}
	}

	private unsafe void UnsafeWrite(HttpWorkerRequest wr, byte[] buffer, int offset, int count)
	{
		fixed (byte* ptr = buffer)
		{
			wr.SendResponseFromMemory((IntPtr)(ptr + offset), count);
		}
	}

	private void AppendBuffer(byte[] buffer, int offset, int count)
	{
		if (!(cur_bucket is ByteBucket))
		{
			AppendBucket(new ByteBucket());
		}
		total += count;
		((ByteBucket)cur_bucket).Write(buffer, offset, count);
	}

	private void AppendBuffer(IntPtr ptr, int count)
	{
		if (!(cur_bucket is ByteBucket))
		{
			AppendBucket(new ByteBucket());
		}
		total += count;
		((ByteBucket)cur_bucket).Write(ptr, count);
	}

	internal void ReleaseResources(bool close_filter)
	{
		if (close_filter && filter != null)
		{
			filter.Close();
			filter = null;
		}
		for (Bucket next = first_bucket; next != null; next = next.Next)
		{
			next.Dispose();
		}
		first_bucket = null;
		cur_bucket = null;
	}

	public void Clear()
	{
		ReleaseResources(close_filter: false);
		total = 0L;
	}

	private unsafe static void memcpy4(byte* dest, byte* src, int size)
	{
		while (size >= 16)
		{
			*(int*)dest = *(int*)src;
			*(int*)(dest + 4) = *(int*)(src + 4);
			*(int*)(dest + (nint)2 * (nint)4) = *(int*)(src + (nint)2 * (nint)4);
			*(int*)(dest + (nint)3 * (nint)4) = *(int*)(src + (nint)3 * (nint)4);
			dest += 16;
			src += 16;
			size -= 16;
		}
		while (size >= 4)
		{
			*(int*)dest = *(int*)src;
			dest += 4;
			src += 4;
			size -= 4;
		}
		while (size > 0)
		{
			*dest = *src;
			dest++;
			src++;
			size--;
		}
	}

	private unsafe static void memcpy2(byte* dest, byte* src, int size)
	{
		while (size >= 8)
		{
			*(short*)dest = *(short*)src;
			*(short*)(dest + 2) = *(short*)(src + 2);
			*(short*)(dest + (nint)2 * (nint)2) = *(short*)(src + (nint)2 * (nint)2);
			*(short*)(dest + (nint)3 * (nint)2) = *(short*)(src + (nint)3 * (nint)2);
			dest += 8;
			src += 8;
			size -= 8;
		}
		while (size >= 2)
		{
			*(short*)dest = *(short*)src;
			dest += 2;
			src += 2;
			size -= 2;
		}
		if (size > 0)
		{
			*dest = *src;
		}
	}

	private unsafe static void memcpy1(byte* dest, byte* src, int size)
	{
		while (size >= 8)
		{
			*dest = *src;
			dest[1] = src[1];
			dest[2] = src[2];
			dest[3] = src[3];
			dest[4] = src[4];
			dest[5] = src[5];
			dest[6] = src[6];
			dest[7] = src[7];
			dest += 8;
			src += 8;
			size -= 8;
		}
		while (size >= 2)
		{
			*dest = *src;
			dest[1] = src[1];
			dest += 2;
			src += 2;
			size -= 2;
		}
		if (size > 0)
		{
			*dest = *src;
		}
	}

	private unsafe static void memcpy(byte* dest, byte* src, int size)
	{
		if ((((int)dest | (int)src) & 3) != 0)
		{
			if (((int)dest & 1) != 0 && ((int)src & 1) != 0 && size >= 1)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
			if (((int)dest & 2) != 0 && ((int)src & 2) != 0 && size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if ((((int)dest | (int)src) & 1) != 0)
			{
				memcpy1(dest, src, size);
				return;
			}
			if ((((int)dest | (int)src) & 2) != 0)
			{
				memcpy2(dest, src, size);
				return;
			}
		}
		memcpy4(dest, src, size);
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException("HttpResponseStream is a forward, write-only stream");
	}
}
