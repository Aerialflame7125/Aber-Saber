using System.IO;
using System.Text;

namespace System.Web.Mail;

internal class UUAttachmentEncoder : IAttachmentEncoder
{
	protected byte[] beginTag;

	protected byte[] endTag;

	protected byte[] endl;

	public UUAttachmentEncoder(int mode, string fileName)
	{
		string text = "\r\n";
		beginTag = Encoding.ASCII.GetBytes("begin " + mode + " " + fileName + text);
		endTag = Encoding.ASCII.GetBytes("`" + text + "end" + text);
		endl = Encoding.ASCII.GetBytes(text);
	}

	public void EncodeStream(Stream ins, Stream outs)
	{
		outs.Write(beginTag, 0, beginTag.Length);
		ToUUEncodingTransform toUUEncodingTransform = new ToUUEncodingTransform();
		byte[] array = new byte[toUUEncodingTransform.InputBlockSize];
		byte[] array2 = new byte[toUUEncodingTransform.OutputBlockSize];
		while (true)
		{
			int num = ins.Read(array, 0, array.Length);
			if (num < 1)
			{
				break;
			}
			if (num == toUUEncodingTransform.InputBlockSize)
			{
				toUUEncodingTransform.TransformBlock(array, 0, num, array2, 0);
				outs.Write(array2, 0, array2.Length);
				outs.Write(endl, 0, endl.Length);
				continue;
			}
			byte[] array3 = toUUEncodingTransform.TransformFinalBlock(array, 0, num);
			outs.Write(array3, 0, array3.Length);
			outs.Write(endl, 0, endl.Length);
			break;
		}
		outs.Write(endTag, 0, endTag.Length);
	}
}
