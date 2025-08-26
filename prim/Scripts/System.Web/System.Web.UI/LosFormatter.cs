using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Serializes the view state for a Web Forms page. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class LosFormatter
{
	private ObjectStateFormatter osf;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using default values.</summary>
	public LosFormatter()
	{
		osf = new ObjectStateFormatter();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
	/// <param name="enableMac">
	///       <see langword="true" /> to enable view-state MAC; otherwise, <see langword="false" />. </param>
	/// <param name="macKeyModifier">The modifier for the MAC key. </param>
	public LosFormatter(bool enableMac, string macKeyModifier)
		: this(enableMac, string.IsNullOrEmpty(macKeyModifier) ? null : Encoding.ASCII.GetBytes(macKeyModifier))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
	/// <param name="enableMac">
	///       <see langword="true" /> to enable view-state MAC; otherwise, <see langword="false" />.</param>
	/// <param name="macKeyModifier">The modifier for the MAC key.</param>
	public LosFormatter(bool enableMac, byte[] macKeyModifier)
	{
		osf = new ObjectStateFormatter();
		if (enableMac && macKeyModifier != null)
		{
			SetMacKey(macKeyModifier);
		}
	}

	private void SetMacKey(byte[] macKeyModifier)
	{
		try
		{
			osf.Section.ValidationKey = MachineKeySectionUtils.GetHexString(macKeyModifier);
		}
		catch (ArgumentException)
		{
		}
		catch (ConfigurationErrorsException)
		{
		}
	}

	/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.Stream" /> object to a limited object serialization (LOS)-formatted object.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the view-state value to transform. </param>
	/// <returns>A LOS-formatted object.</returns>
	public object Deserialize(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		using StreamReader streamReader = new StreamReader(stream);
		return Deserialize(streamReader.ReadToEnd());
	}

	/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.TextReader" /> object to a limited object serialization (LOS)-formatted object.</summary>
	/// <param name="input">A <see cref="T:System.IO.TextReader" /> that contains the view-state value to transform. </param>
	/// <returns>A LOS-formatted object.</returns>
	public object Deserialize(TextReader input)
	{
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		return Deserialize(input.ReadToEnd());
	}

	/// <summary>Transforms the specified view-state value to a limited object serialization (LOS)-formatted object.</summary>
	/// <param name="input">The view-state value to transform. </param>
	/// <returns>A LOS-formatted object.</returns>
	/// <exception cref="T:System.Web.HttpException">The view state is invalid. </exception>
	public object Deserialize(string input)
	{
		if (input == null)
		{
			return null;
		}
		return osf.Deserialize(input);
	}

	internal string SerializeToBase64(object value)
	{
		return osf.Serialize(value);
	}

	/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.Stream" /> object.</summary>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the transformed value. </param>
	/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
	public void Serialize(Stream stream, object value)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (!stream.CanSeek)
		{
			throw new NotSupportedException();
		}
		string s = SerializeToBase64(value);
		byte[] bytes = Encoding.ASCII.GetBytes(s);
		stream.Write(bytes, 0, bytes.Length);
	}

	/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.TextWriter" /> object.</summary>
	/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to receive the transformed value. </param>
	/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
	public void Serialize(TextWriter output, object value)
	{
		if (output == null)
		{
			throw new ArgumentNullException("output");
		}
		output.Write(SerializeToBase64(value));
	}
}
