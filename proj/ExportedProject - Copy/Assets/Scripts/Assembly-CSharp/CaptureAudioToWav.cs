using System;
using System.IO;
using System.Text;
using UnityEngine;

public class CaptureAudioToWav : MonoBehaviour
{
	[SerializeField]
	private string _fileName = "recTest.wav";

	private int _sampleRate;

	private int _headerSize = 44;

	private bool _recording;

	private FileStream _fileStream;

	private void Awake()
	{
		_sampleRate = AudioSettings.GetConfiguration().sampleRate;
	}

	private void Update()
	{
		if (Input.GetKeyDown("r"))
		{
			MonoBehaviour.print("rec");
			if (!_recording)
			{
				StartWriting(_fileName);
				_recording = true;
			}
			else
			{
				_recording = false;
				WriteHeader();
				MonoBehaviour.print("rec stop");
			}
		}
	}

	private void StartWriting(string name)
	{
		_fileStream = new FileStream(name, FileMode.Create);
		byte value = 0;
		for (int i = 0; i < _headerSize; i++)
		{
			_fileStream.WriteByte(value);
		}
	}

	private void OnAudioFilterRead(float[] data, int channels)
	{
		if (_recording)
		{
			ConvertAndWrite(data);
		}
	}

	private void ConvertAndWrite(float[] dataSource)
	{
		short[] array = new short[dataSource.Length];
		byte[] array2 = new byte[dataSource.Length * 2];
		int num = 32767;
		for (int i = 0; i < dataSource.Length; i++)
		{
			array[i] = (short)(dataSource[i] * (float)num);
			byte[] array3 = new byte[2];
			array3 = BitConverter.GetBytes(array[i]);
			array3.CopyTo(array2, i * 2);
		}
		_fileStream.Write(array2, 0, array2.Length);
	}

	private void WriteHeader()
	{
		_fileStream.Seek(0L, SeekOrigin.Begin);
		byte[] bytes = Encoding.UTF8.GetBytes("RIFF");
		_fileStream.Write(bytes, 0, 4);
		byte[] bytes2 = BitConverter.GetBytes(_fileStream.Length - 8);
		_fileStream.Write(bytes2, 0, 4);
		byte[] bytes3 = Encoding.UTF8.GetBytes("WAVE");
		_fileStream.Write(bytes3, 0, 4);
		byte[] bytes4 = Encoding.UTF8.GetBytes("fmt ");
		_fileStream.Write(bytes4, 0, 4);
		byte[] bytes5 = BitConverter.GetBytes(16);
		_fileStream.Write(bytes5, 0, 4);
		ushort value = 2;
		ushort value2 = 1;
		byte[] bytes6 = BitConverter.GetBytes(value2);
		_fileStream.Write(bytes6, 0, 2);
		byte[] bytes7 = BitConverter.GetBytes(value);
		_fileStream.Write(bytes7, 0, 2);
		byte[] bytes8 = BitConverter.GetBytes(_sampleRate);
		_fileStream.Write(bytes8, 0, 4);
		byte[] bytes9 = BitConverter.GetBytes(_sampleRate * 4);
		_fileStream.Write(bytes9, 0, 4);
		ushort value3 = 4;
		byte[] bytes10 = BitConverter.GetBytes(value3);
		_fileStream.Write(bytes10, 0, 2);
		ushort value4 = 16;
		byte[] bytes11 = BitConverter.GetBytes(value4);
		_fileStream.Write(bytes11, 0, 2);
		byte[] bytes12 = Encoding.UTF8.GetBytes("data");
		_fileStream.Write(bytes12, 0, 4);
		byte[] bytes13 = BitConverter.GetBytes(_fileStream.Length - _headerSize);
		_fileStream.Write(bytes13, 0, 4);
		_fileStream.Close();
	}
}
