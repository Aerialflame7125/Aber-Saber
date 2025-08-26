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

		public Sprite image => _image;

		public Color color => _color;

		public Color eventActiveColor => _eventActiveColor;

		public int eventValue => _eventValue;
	}

	[SerializeField]
	private string _eventId;

	[SerializeField]
	private Sprite _image;

	[SerializeField]
	private SubEventDrawStyle[] _subEvents;

	public string eventId => _eventId;

	public Sprite image => _image;

	public SubEventDrawStyle[] subEvents => _subEvents;
}
