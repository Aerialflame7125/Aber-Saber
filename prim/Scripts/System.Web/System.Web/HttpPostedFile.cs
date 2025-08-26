using System.IO;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides access to individual files that have been uploaded by a client.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpPostedFile
{
	private class ReadSubStream : Stream
	{
		private Stream s;

		private long offset;

		private long end;

		private long position;

		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => false;

		public override long Length => end - offset;

		public override long Position
		{
			get
			{
				return position - offset;
			}
			set
			{
				if (value > Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				position = Seek(value, SeekOrigin.Begin);
			}
		}

		public ReadSubStream(Stream s, long offset, long length)
		{
			this.s = s;
			this.offset = offset;
			end = offset + length;
			position = offset;
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int dest_offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (dest_offset < 0)
			{
				throw new ArgumentOutOfRangeException("dest_offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			int num = buffer.Length;
			if (dest_offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (dest_offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (count > end - position)
			{
				count = (int)(end - position);
			}
			if (count <= 0)
			{
				return 0;
			}
			s.Position = position;
			int num2 = s.Read(buffer, dest_offset, count);
			if (num2 > 0)
			{
				position += num2;
			}
			else
			{
				position = end;
			}
			return num2;
		}

		public override int ReadByte()
		{
			if (position >= end)
			{
				return -1;
			}
			s.Position = position;
			int num = s.ReadByte();
			if (num < 0)
			{
				position = end;
				return num;
			}
			position++;
			return num;
		}

		public override long Seek(long d, SeekOrigin origin)
		{
			long num = origin switch
			{
				SeekOrigin.Begin => offset + d, 
				SeekOrigin.End => end + d, 
				SeekOrigin.Current => position + d, 
				_ => throw new ArgumentException(), 
			};
			long num2 = num - offset;
			if (num2 < 0 || num2 > Length)
			{
				throw new ArgumentException();
			}
			position = s.Seek(num, SeekOrigin.Begin);
			return position;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}

	private string name;

	private string content_type;

	private Stream stream;

	/// <summary>Gets the MIME content type of a file sent by a client.</summary>
	/// <returns>The MIME content type of the uploaded file.</returns>
	public string ContentType => content_type;

	/// <summary>Gets the size of an uploaded file, in bytes.</summary>
	/// <returns>The file length, in bytes.</returns>
	public int ContentLength => (int)stream.Length;

	/// <summary>Gets the fully qualified name of the file on the client.</summary>
	/// <returns>The name of the client's file, including the directory path.</returns>
	public string FileName => name;

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> pointing to a file.</returns>
	public Stream InputStream => stream;

	internal HttpPostedFile(string name, string content_type, Stream base_stream, long offset, long length)
	{
		this.name = name;
		this.content_type = content_type;
		stream = new ReadSubStream(base_stream, offset, length);
	}

	/// <summary>Saves the contents of an uploaded file.</summary>
	/// <param name="filename">The name of the saved file. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath" /> property of the <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> object is set to <see langword="true" />, but <paramref name="filename" /> is not an absolute path.</exception>
	public void SaveAs(string filename)
	{
		byte[] buffer = new byte[16384];
		long position = stream.Position;
		try
		{
			File.Delete(filename);
			using FileStream fileStream = File.Create(filename);
			stream.Position = 0L;
			int count;
			while ((count = stream.Read(buffer, 0, 16384)) != 0)
			{
				fileStream.Write(buffer, 0, count);
			}
		}
		finally
		{
			stream.Position = position;
		}
	}
}
