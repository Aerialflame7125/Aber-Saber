using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MIDIParser
{
	private const int _noteLinesCount = 4;

	private const int _firstLineNoteKey = 48;

	private int _firstLineNoteWhiteKeyIndex;

	private short _format;

	private int _division;

	private int _tempo;

	private int _tracksCount;

	private List<BeatmapObjectData>[] _bottomNoteLines;

	private List<BeatmapObjectData>[] _topNoteLines;

	private List<ObstacleData>[] _obstacleLines;

	private List<BeatmapEventData> _beatmapEventsData;

	private BinaryReader _binReader;

	private bool _haveFirstNote;

	private int _nextBeatmapObjectId;

	private static readonly byte[] _fileHeaderID = new byte[8] { 77, 84, 104, 100, 0, 0, 0, 6 };

	private static readonly byte[] _trackHeaderID = new byte[4] { 77, 84, 114, 107 };

	public BeatmapData GetBeatmapDataFromFile(string filenamePath)
	{
		byte[] bytes = File.ReadAllBytes(filenamePath);
		return GetBeatmapDataFromBytes(bytes);
	}

	public BeatmapData GetBeatmapDataFromTextAsset(TextAsset textAsset)
	{
		return GetBeatmapDataFromBytes(textAsset.bytes);
	}

	public BeatmapData GetBeatmapDataFromBytes(byte[] bytes)
	{
		ReadMidi(bytes);
		BeatmapLineData[] array = null;
		BeatmapEventData[] array2 = null;
		if (_bottomNoteLines != null)
		{
			array = new BeatmapLineData[4];
			for (int i = 0; i < 4; i++)
			{
				List<BeatmapObjectData> list = new List<BeatmapObjectData>();
				foreach (BeatmapObjectData item in _bottomNoteLines[i])
				{
					list.Add(item);
				}
				foreach (BeatmapObjectData item2 in _topNoteLines[i])
				{
					list.Add(item2);
				}
				foreach (ObstacleData item3 in _obstacleLines[i])
				{
					list.Add(item3);
				}
				list.Sort((BeatmapObjectData x, BeatmapObjectData y) => (x.time != y.time) ? ((x.time > y.time) ? 1 : (-1)) : 0);
				array[i] = new BeatmapLineData();
				array[i].beatmapObjectsData = new BeatmapObjectData[list.Count];
				for (int j = 0; j < list.Count; j++)
				{
					array[i].beatmapObjectsData[j] = list[j];
				}
			}
			array2 = new BeatmapEventData[_beatmapEventsData.Count];
			for (int k = 0; k < _beatmapEventsData.Count; k++)
			{
				array2[k] = _beatmapEventsData[k];
			}
		}
		return new BeatmapData(array, array2);
	}

	private void ReadMidi(byte[] bytes)
	{
		_firstLineNoteWhiteKeyIndex = GetWhiteKeyIndexForKey(48);
		_bottomNoteLines = new List<BeatmapObjectData>[4];
		for (int i = 0; i < 4; i++)
		{
			_bottomNoteLines[i] = new List<BeatmapObjectData>();
		}
		_topNoteLines = new List<BeatmapObjectData>[4];
		for (int j = 0; j < 4; j++)
		{
			_topNoteLines[j] = new List<BeatmapObjectData>();
		}
		_obstacleLines = new List<ObstacleData>[4];
		for (int k = 0; k < 4; k++)
		{
			_obstacleLines[k] = new List<ObstacleData>();
		}
		_beatmapEventsData = new List<BeatmapEventData>();
		_tempo = 120;
		_nextBeatmapObjectId = 0;
		Stream input = new MemoryStream(bytes);
		_binReader = new BinaryReader(input);
		try
		{
			if (VerifyFileType() && ReadFormat() && ReadTrackCount())
			{
				ReadDivision();
				ReadTracks();
			}
			else
			{
				_bottomNoteLines = null;
			}
		}
		catch (EndOfStreamException)
		{
			_bottomNoteLines = null;
		}
		finally
		{
			_binReader.Close();
		}
	}

	private bool VerifyFileType()
	{
		for (int i = 0; i < _fileHeaderID.Length; i++)
		{
			if (_binReader.ReadByte() != _fileHeaderID[i])
			{
				return false;
			}
		}
		return true;
	}

	private bool ReadFormat()
	{
		byte[] array = _binReader.ReadBytes(2);
		ConvertByteArray(array);
		_format = BitConverter.ToInt16(array, 0);
		return _format == 0 || _format == 1;
	}

	private bool ReadTrackCount()
	{
		byte[] array = _binReader.ReadBytes(2);
		ConvertByteArray(array);
		_tracksCount = BitConverter.ToInt16(array, 0);
		if (_tracksCount > 1 && _format == 0)
		{
			return false;
		}
		return true;
	}

	private void ReadDivision()
	{
		byte[] array = _binReader.ReadBytes(2);
		ConvertByteArray(array);
		_division = BitConverter.ToInt16(array, 0);
	}

	private void ReadTracks()
	{
		for (int i = 0; i < _tracksCount; i++)
		{
			FindNextTrack();
			ReadNextTrack(i);
			for (int j = 0; j < _bottomNoteLines.Length; j++)
			{
				if (_bottomNoteLines[j].Count > 0)
				{
					return;
				}
			}
			for (int k = 0; k < _topNoteLines.Length; k++)
			{
				if (_topNoteLines[k].Count > 0)
				{
					return;
				}
			}
		}
	}

	private void FindNextTrack()
	{
		bool flag = false;
		while (!flag)
		{
			while (_binReader.ReadByte() != _trackHeaderID[0])
			{
			}
			bool flag2 = true;
			for (int i = 1; i < _trackHeaderID.Length; i++)
			{
				if (!flag2)
				{
					break;
				}
				if (_binReader.ReadByte() != _trackHeaderID[i])
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				flag = true;
			}
		}
	}

	private void ReadNextTrack(int trackNum)
	{
		MetaType metaType = MetaType.TrackName;
		int num = 0;
		int status = 0;
		_haveFirstNote = false;
		_binReader.ReadBytes(4);
		float num2 = 0f;
		while (metaType != MetaType.EndOfTrack)
		{
			int num3 = ReadVariableLengthQuantity();
			num2 += (float)((double)num3 / (double)_division * (double)_tempo * 1E-06);
			num = _binReader.ReadByte();
			if ((num & 0x80) == 128)
			{
				if (IsChannelMessage(num))
				{
					int data = _binReader.ReadByte();
					ReadChannelMessage(trackNum, num2, num, data);
					status = num;
				}
				else if (IsMetaMessage(num))
				{
					metaType = (MetaType)_binReader.ReadByte();
					int count = ReadVariableLengthQuantity();
					byte[] data2 = _binReader.ReadBytes(count);
					if (metaType == MetaType.Tempo)
					{
						_tempo = GetTempo(data2);
					}
				}
			}
			else
			{
				ReadChannelMessage(trackNum, num2, status, num);
			}
		}
	}

	private bool IsChannelMessage(int status)
	{
		bool result = false;
		if (status >= 128 && status <= 239)
		{
			result = true;
		}
		return result;
	}

	private bool IsMetaMessage(int status)
	{
		bool result = false;
		if (status == 255)
		{
			result = true;
		}
		return result;
	}

	private int GetTempo(byte[] data)
	{
		int num = 0;
		if (BitConverter.IsLittleEndian)
		{
			int num2 = data.Length - 1;
			for (int i = 0; i < data.Length; i++)
			{
				num |= data[num2] << 8 * i;
				num2--;
			}
		}
		else
		{
			for (int j = 0; j < data.Length; j++)
			{
				num |= data[j] << 8 * j;
			}
		}
		return num;
	}

	private void ReadChannelMessage(int trackNum, float eventTime, int status, int data1)
	{
		ChannelCommand channelCommand = (ChannelCommand)(status & 0xF0);
		if (channelCommand == ChannelCommand.ChannelPressure || channelCommand == ChannelCommand.ProgramChange)
		{
			return;
		}
		int num = _binReader.ReadByte();
		if (_haveFirstNote)
		{
		}
		if (channelCommand != ChannelCommand.NoteOn && channelCommand != ChannelCommand.NoteOff)
		{
			return;
		}
		int num2 = GetWhiteKeyIndexForKey(data1) - _firstLineNoteWhiteKeyIndex;
		if (num2 >= -4 && num2 < 4)
		{
			if (channelCommand == ChannelCommand.NoteOn && num == 127)
			{
				int num3 = num2 % 4;
				if (num2 >= 0)
				{
					NoteData item = new NoteData(_nextBeatmapObjectId++, eventTime, num3, NoteLineLayer.Base, NoteLineLayer.Base, NoteType.GhostNote, NoteCutDirection.None);
					_bottomNoteLines[num3].Add(item);
				}
				else
				{
					NoteData item2 = new NoteData(_nextBeatmapObjectId++, eventTime, num3, NoteLineLayer.Top, NoteLineLayer.Top, NoteType.GhostNote, NoteCutDirection.None);
					_topNoteLines[num3].Add(item2);
				}
			}
			else if (channelCommand == ChannelCommand.NoteOn && num > 0)
			{
				NoteType noteType = ((num > 40) ? NoteType.NoteB : NoteType.NoteA);
				NoteCutDirection cutDirection = ((num - 1) / 10 % 4) switch
				{
					0 => NoteCutDirection.Left, 
					1 => NoteCutDirection.Right, 
					2 => NoteCutDirection.Up, 
					3 => NoteCutDirection.Down, 
					4 => NoteCutDirection.UpLeft, 
					5 => NoteCutDirection.UpRight, 
					6 => NoteCutDirection.DownLeft, 
					7 => NoteCutDirection.DownRight, 
					_ => NoteCutDirection.Down, 
				};
				int num4 = num2 % 4;
				if (num2 >= 0)
				{
					NoteData item3 = new NoteData(_nextBeatmapObjectId++, eventTime, num4, NoteLineLayer.Base, NoteLineLayer.Base, noteType, cutDirection);
					_bottomNoteLines[num4].Add(item3);
				}
				else
				{
					NoteData item4 = new NoteData(_nextBeatmapObjectId++, eventTime, num4, NoteLineLayer.Top, NoteLineLayer.Top, noteType, cutDirection);
					_topNoteLines[num4].Add(item4);
				}
			}
			else
			{
				int num5 = num2 % 4;
				BeatmapObjectData beatmapObjectData = ((num2 < 0) ? _topNoteLines[num5][_topNoteLines[num5].Count - 1] : _bottomNoteLines[num5][_bottomNoteLines[num5].Count - 1]);
				if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.LongNote)
				{
					LongNoteData longNoteData = (LongNoteData)beatmapObjectData;
					longNoteData.UpdateDuration(eventTime - longNoteData.time);
					_haveFirstNote = true;
				}
			}
		}
		else if (num2 >= 7 && num2 < 11)
		{
			num2 -= 7;
			if (channelCommand == ChannelCommand.NoteOn && num > 0)
			{
				ObstacleType obstacleType = ((num >= 64) ? ObstacleType.Top : ObstacleType.FullHeight);
				ObstacleData item5 = new ObstacleData(_nextBeatmapObjectId++, eventTime, num2, obstacleType, 0f, 1);
				_obstacleLines[num2].Add(item5);
			}
			else
			{
				ObstacleData obstacleData = _obstacleLines[num2][_obstacleLines[num2].Count - 1];
				obstacleData.UpdateDuration(eventTime - obstacleData.time);
			}
		}
		else if (num2 >= 14 && channelCommand == ChannelCommand.NoteOn)
		{
			BeatmapEventType type = (BeatmapEventType)(num2 - 14);
			BeatmapEventData item6 = new BeatmapEventData(eventTime, type, num);
			_beatmapEventsData.Add(item6);
		}
	}

	private int GetWhiteKeyIndexForKey(int key)
	{
		int num = key / 12 - 1;
		int octaveWhiteKeyIndexForKey = GetOctaveWhiteKeyIndexForKey(key);
		return num * 7 + octaveWhiteKeyIndexForKey;
	}

	private int GetOctaveWhiteKeyIndexForKey(int key)
	{
		return (key % 12) switch
		{
			0 => 0, 
			2 => 1, 
			4 => 2, 
			5 => 3, 
			7 => 4, 
			9 => 5, 
			11 => 6, 
			_ => -1, 
		};
	}

	private int ReadVariableLengthQuantity()
	{
		bool flag = false;
		int num = 0;
		while (!flag)
		{
			byte b = _binReader.ReadByte();
			if ((b & 0x80) == 128)
			{
				b &= 0x7F;
			}
			else
			{
				flag = true;
			}
			num <<= 7;
			num |= b;
		}
		return num;
	}

	private void ConvertByteArray(byte[] array)
	{
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse((Array)array);
		}
	}
}
