using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HMUI
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(HorizontalLayoutGroup))]
	public class SegmentedControlCell : UIBehaviour, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		public enum TransitionType
		{
			Instant = 0,
			Animated = 1
		}

		[SerializeField]
		[NullAllowed]
		[EventSender]
		private GameEvent _tableCellWasPressedEvent;

		private int _column;

		private SegmentedControl _segmentedControl;

		private bool _selected;

		private bool _highlighted;

		public int column
		{
			get
			{
				return _column;
			}
		}

		public bool selected
		{
			get
			{
				return _selected;
			}
			set
			{
				ChangeSelection(value, TransitionType.Animated, true, false);
			}
		}

		public bool highlighted
		{
			get
			{
				return _highlighted;
			}
		}

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
			ChangeHighlight(true, TransitionType.Animated, false);
		}

		public virtual void OnPointerExit(PointerEventData eventData)
		{
			ChangeHighlight(false, TransitionType.Animated, false);
		}
	}
}
