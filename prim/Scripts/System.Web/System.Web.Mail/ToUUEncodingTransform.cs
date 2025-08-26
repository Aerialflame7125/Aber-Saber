using System.Security.Cryptography;

namespace System.Web.Mail;

internal class ToUUEncodingTransform : ICryptoTransform, IDisposable
{
	public int InputBlockSize => 45;

	public int OutputBlockSize => 61;

	public bool CanTransformMultipleBlocks => true;

	public bool CanReuseTransform => true;

	public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
	{
		outputBuffer[0] = 77;
		for (int i = 0; i < 15; i++)
		{
			TransformTriplet(inputBuffer, inputOffset + i * 3, 3, outputBuffer, outputOffset + i * 4 + 1);
		}
		return OutputBlockSize;
	}

	public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
	{
		int num = inputCount / 3 + 1;
		byte[] dst = new byte[num * 3];
		Buffer.BlockCopy(inputBuffer, inputOffset, dst, 0, inputCount);
		byte[] array = new byte[num * 4 + 1];
		array[0] = (byte)(inputCount + 32);
		for (int i = 0; i < num; i++)
		{
			TransformTriplet(inputBuffer, inputOffset + i * 3, 3, array, i * 4 + 1);
		}
		return array;
	}

	protected int TransformTriplet(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
	{
		byte b = inputBuffer[inputOffset];
		byte b2 = inputBuffer[inputOffset + 1];
		byte b3 = inputBuffer[inputOffset + 2];
		outputBuffer[outputOffset] = (byte)(32 + ((b >> 2) & 0x3F));
		outputBuffer[outputOffset + 1] = (byte)(32 + (((b << 4) | ((b2 >> 4) & 0xF)) & 0x3F));
		outputBuffer[outputOffset + 2] = (byte)(32 + (((b2 << 2) | ((b3 >> 6) & 3)) & 0x3F));
		outputBuffer[outputOffset + 3] = (byte)(32 + (b3 & 0x3F));
		for (int i = 0; i < 4; i++)
		{
			if (outputBuffer[outputOffset + i] == 32)
			{
				outputBuffer[outputOffset + i] = 96;
			}
		}
		return 4;
	}

	public void Dispose()
	{
	}
}
