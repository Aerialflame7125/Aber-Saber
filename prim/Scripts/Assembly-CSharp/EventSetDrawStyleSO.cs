using UnityEngine;

public class EventSetDrawStyleSO : ScriptableObject
{
	[SerializeField]
	private EventDrawStyleSO[] _events;

	[SerializeField]
	private Sprite[] _overrideImages;

	public EventDrawStyleSO[] events => _events;

	public Sprite[] overrideImages => _overrideImages;
}
