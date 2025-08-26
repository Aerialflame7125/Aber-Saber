using UnityEngine;

public class EventSetDrawStyleSO : ScriptableObject
{
	[SerializeField]
	private EventDrawStyleSO[] _events;

	[SerializeField]
	private Sprite[] _overrideImages;

	public EventDrawStyleSO[] events
	{
		get
		{
			return _events;
		}
	}

	public Sprite[] overrideImages
	{
		get
		{
			return _overrideImages;
		}
	}
}
