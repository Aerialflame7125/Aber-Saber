using System;
using UnityEngine;

public class EventDrawStyleSO : ScriptableObject
{
	[Serializable]
	public class SubEventDrawStyle
	{
		[SerializeField]
		private Sprite _image;

		[SerializeField]
		private Color _color;

		[SerializeField]
		private Color _eventActiveColor;

		[SerializeField]
		private int _eventValue;

		public Sprite image
		{
			get
			{
				return _image;
			}
		}

		public Color color
		{
			get
			{
				return _color;
			}
		}

		public Color eventActiveColor
		{
			get
			{
				return _eventActiveColor;
			}
		}

		public int eventValue
		{
			get
			{
				return _eventValue;
			}
		}
	}

	[SerializeField]
	private string _eventId;

	[SerializeField]
	private Sprite _image;

	[SerializeField]
	private SubEventDrawStyle[] _subEvents;

	public string eventId
	{
		get
		{
			return _eventId;
		}
	}

	public Sprite image
	{
		get
		{
			return _image;
		}
	}

	public SubEventDrawStyle[] subEvents
	{
		get
		{
			return _subEvents;
		}
	}
}
