using System.Runtime.InteropServices;

namespace System.Text;

/// <summary>Provides the base class for an encoding provider, which supplies encodings that are unavailable on a particular platform.</summary>
[ComVisible(true)]
public abstract class EncodingProvider
{
	private static object s_InternalSyncObject = new object();

	private static volatile EncodingProvider[] s_providers;

	/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncodingProvider" /> class.</summary>
	public EncodingProvider()
	{
	}

	/// <summary>Returns the encoding with the specified name.</summary>
	/// <param name="name">The name of the requested encoding.</param>
	/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
	public abstract Encoding GetEncoding(string name);

	/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
	/// <param name="codepage">The code page identifier of the requested encoding.</param>
	/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
	public abstract Encoding GetEncoding(int codepage);

	/// <summary>Returns the encoding associated with the specified name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
	/// <param name="name">The name of the preferred encoding.</param>
	/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
	/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
	/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
	public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
	{
		Encoding encoding = GetEncoding(name);
		if (encoding != null)
		{
			encoding = (Encoding)GetEncoding(name).Clone();
			encoding.EncoderFallback = encoderFallback;
			encoding.DecoderFallback = decoderFallback;
		}
		return encoding;
	}

	/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
	/// <param name="codepage">The code page identifier of the requested encoding.</param>
	/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
	/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with this encoding.</param>
	/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
	public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
	{
		Encoding encoding = GetEncoding(codepage);
		if (encoding != null)
		{
			encoding = (Encoding)GetEncoding(codepage).Clone();
			encoding.EncoderFallback = encoderFallback;
			encoding.DecoderFallback = decoderFallback;
		}
		return encoding;
	}

	internal static void AddProvider(EncodingProvider provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		lock (s_InternalSyncObject)
		{
			if (s_providers == null)
			{
				s_providers = new EncodingProvider[1] { provider };
			}
			else if (Array.IndexOf(s_providers, provider) < 0)
			{
				EncodingProvider[] array = new EncodingProvider[s_providers.Length + 1];
				Array.Copy(s_providers, array, s_providers.Length);
				array[array.Length - 1] = provider;
				s_providers = array;
			}
		}
	}

	internal static Encoding GetEncodingFromProvider(int codepage)
	{
		if (s_providers == null)
		{
			return null;
		}
		EncodingProvider[] array = s_providers;
		for (int i = 0; i < array.Length; i++)
		{
			Encoding encoding = array[i].GetEncoding(codepage);
			if (encoding != null)
			{
				return encoding;
			}
		}
		return null;
	}

	internal static Encoding GetEncodingFromProvider(string encodingName)
	{
		if (s_providers == null)
		{
			return null;
		}
		EncodingProvider[] array = s_providers;
		for (int i = 0; i < array.Length; i++)
		{
			Encoding encoding = array[i].GetEncoding(encodingName);
			if (encoding != null)
			{
				return encoding;
			}
		}
		return null;
	}

	internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
	{
		if (s_providers == null)
		{
			return null;
		}
		EncodingProvider[] array = s_providers;
		for (int i = 0; i < array.Length; i++)
		{
			Encoding encoding = array[i].GetEncoding(codepage, enc, dec);
			if (encoding != null)
			{
				return encoding;
			}
		}
		return null;
	}

	internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
	{
		if (s_providers == null)
		{
			return null;
		}
		EncodingProvider[] array = s_providers;
		for (int i = 0; i < array.Length; i++)
		{
			Encoding encoding = array[i].GetEncoding(encodingName, enc, dec);
			if (encoding != null)
			{
				return encoding;
			}
		}
		return null;
	}
}
