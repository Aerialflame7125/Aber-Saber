using TMPro;
using UnityEngine;

public class TextFadeTransitions : MonoBehaviour
{
	private enum State
	{
		NotInTransition,
		FadingOut,
		FadingIn
	}

	[SerializeField]
	private TextMeshProUGUI _textLabel;

	[Tooltip("If Canvas Group is specified, it is used for fadeing instead of Text Label color.")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[SerializeField]
	private float _fadeDuration = 0.4f;

	private State _state;

	private string _nextText;

	private float _fade;

	private void Awake()
	{
		base.enabled = false;
		_state = State.NotInTransition;
		_fade = 0f;
		_textLabel.text = string.Empty;
		RefreshTextAlpha();
	}

	private void Update()
	{
		RefreshState();
	}

	private void RefreshState()
	{
		switch (_state)
		{
		case State.NotInTransition:
			if (_nextText != null)
			{
				if (_fade == 0f)
				{
					_textLabel.text = _nextText;
					_nextText = null;
					_state = State.FadingIn;
				}
				else
				{
					_state = State.FadingOut;
				}
			}
			else
			{
				base.enabled = false;
			}
			break;
		case State.FadingIn:
			_fade = Mathf.Min(_fade + Time.deltaTime / _fadeDuration, 1f);
			RefreshTextAlpha();
			if (_fade == 1f)
			{
				_state = State.NotInTransition;
			}
			break;
		case State.FadingOut:
			RefreshTextAlpha();
			_fade = Mathf.Max(_fade - Time.deltaTime / _fadeDuration, 0f);
			if (_fade == 0f)
			{
				_state = State.NotInTransition;
			}
			break;
		}
	}

	private void RefreshTextAlpha()
	{
		if (_canvasGroup != null)
		{
			_canvasGroup.alpha = _fade;
		}
		else
		{
			Color color = _textLabel.color;
			color.a = _fade;
			_textLabel.color = color;
		}
		_textLabel.enabled = _fade != 0f;
	}

	public void ShowText(string text)
	{
		if (!(_nextText == text) && !(_textLabel.text == text))
		{
			_nextText = text;
			base.enabled = true;
		}
	}
}
