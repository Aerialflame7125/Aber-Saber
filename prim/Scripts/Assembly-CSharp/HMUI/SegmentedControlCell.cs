using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HMUI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class SegmentedControlCell : UIBehaviour, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	public enum TransitionType
	{
		Instant,
		Animated
	}

	[SerializeField]
	[NullAllowed]
	[EventSender]
	private GameEvent _tableCellWasPressedEvent;

	private int _column;

	private SegmentedControl _segmentedControl;

	private bool _selected;

	private bool _highlighted;

	public int column => _column;

	public bool selected
	{
		get
		{
			return _selected;
		}
		set
		{
			ChangeSelection(value, TransitionType.Animated, useCallback: true, ignoreCurrentValue: false);
		}
	}

	public bool highlighted => _highlighted;

	protected override void Start()
	{
		base.Start();
		SelectionDidChange(TransitionType.Instant);
		HighlightDidChange(TransitionType.Instant);
	}

	public void SegmentedControlSetup(SegmentedControl segmentedControl, int column)
	{
		_segmentedControl = segmentedControl;
		_column = column;
	}

	public void ChangeSelection(bool value, TransitionType transitionType, bool useCallback, bool ignoreCurrentValue)
	{
		if (ignoreCurrentValue || _selected != value)
		{
			_selected = value;
			SelectionDidChange(transitionType);
			if (useCallback)
			{
				_segmentedControl.CellSelectionStateDidChange(this);
			}
		}
	}

	public void ChangeHighlight(bool value, TransitionType transitionType, bool ignoreCurrentValue)
	{
		if (ignoreCurrentValue || _highlighted != value)
		{
			_highlighted = value;
			HighlightDidChange(transitionType);
		}
	}

	private void InternalToggle()
	{
		if (IsActive() && !selected)
		{
			selected = true;
		}
	}

	protected virtual void SelectionDidChange(TransitionType transitionType)
	{
	}

	protected virtual void HighlightDidChange(TransitionType transitionType)
	{
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			InternalToggle();
			if (_tableCellWasPressedEvent != null)
			{
				_tableCellWasPressedEvent.Raise();
			}
		}
	}

	public virtual void OnSubmit(BaseEventData eventData)
	{
		InternalToggle();
		if (_tableCellWasPressedEvent != null)
		{
			_tableCellWasPressedEvent.Raise();
		}
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		ChangeHighlight(value: true, TransitionType.Animated, ignoreCurrentValue: false);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		ChangeHighlight(value: false, TransitionType.Animated, ignoreCurrentValue: false);
	}
}
