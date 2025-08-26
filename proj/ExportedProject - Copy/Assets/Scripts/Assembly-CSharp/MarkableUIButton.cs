using UnityEngine;

public class MarkableUIButton : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	private bool _marked;

	private int _markedTriggerId;

	public bool marked
	{
		get
		{
			return _marked;
		}
		set
		{
			_marked = value;
			_animator.SetBool(_markedTriggerId, value);
		}
	}

	protected void Awake()
	{
		_markedTriggerId = Animator.StringToHash("Marked");
	}

	public void ToggleMarked()
	{
		marked = !marked;
	}
}
