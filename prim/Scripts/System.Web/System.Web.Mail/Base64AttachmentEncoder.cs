using System.IO;
using System.Security.Cryptography;

namespace System.Web.Mail;

internal class Base64AttachmentEncoder : IAttachmentEncoder
{
	public void EncodeStream(Stream ins, Stream outs)
	{
		if (ins == null || outs == null)
		{
			throw new ArgumentNullException("The input and output streams may not be null.");
		}
		ICryptoTransform cryptoTransform = new ToBase64Transform();
		byte[] array = new byte[cryptoTransform.InputBlockSize];
		byte[] array2 = new byte[cryptoTransform.OutputBlockSize];
		int num = 0;
		int num2 = 0;
		byte[] array3 = new byte[2] { 13, 10 };
		while (true)
		{
			num = ins.Read(array, 0, array.Length);
			if (num < 1)
			{
				break;
			}
			if (num == array.Length)
			{
				cryptoTransform.TransformBlock(array, 0, array.Length, array2, 0);
				outs.Write(array2, 0, array2.Length);
				num2 += array2.Length;
				if (num2 == 60)
				{
					outs.Write(array3, 0, array3.Length);
					num2 = 0;
				}
			}
			else
			{
				array2 = cryptoTransform.TransformFinalBlock(array, 0, num);
				outs.Write(array2, 0, array2.Length);
			}
		}
		outs.Write(array3, 0, array3.Length);
	}
}
