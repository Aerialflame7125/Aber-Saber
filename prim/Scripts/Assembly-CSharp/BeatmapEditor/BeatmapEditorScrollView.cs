using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BeatmapEditor;

public class BeatmapEditorScrollView : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IPlayheadBeatIndex, IEventSystemHandler
{
	[SerializeField]
	private ScrollRect _scrollRect;

	[SerializeField]
	private RectTransform _playHead;

	[Space]
	[SerializeField]
	private float _rowHeight = 20f;

	[SerializeField]
	private float _playHeadPointsOffset = 160f;

	public Action scrollPosDidChangeEvent;

	public Action scrollDragDidBeginEvent;

	public Action scrollDragDidEndEvent;

	private const string kSavedBarPosition = "BeatmapEditor.BarPosition";

	private float _songDuration;

	private float _beatDuration;

	private float _barDuration;

	private float _lastUsedBarPosition;

	public ScrollRect scrollRect => _scrollRect;

	public float rowHeight => _rowHeight;

	public float playHeadPointsOffset => _playHeadPointsOffset;

	public float contentHeight
	{
		get
		{
			RectTransform content = _scrollRect.content;
			return content.sizeDelta.y - playHeadPointsOffset;
		}
		private set
		{
			RectTransform content = _scrollRect.content;
			content.sizeDelta = new Vector3(0f, value + playHeadPointsOffset);
		}
	}

	public float visibleAreaTimeDuration { get; private set; }

	public float playHeadSongTimeOffset { get; private set; }

	public int playheadBeatIndex => (int)(scrollPositionSongTime / _beatDuration + 0.5f);

	public float scrollPositionSongTime => scrollRect.verticalNormalizedPosition * _songDuration;

	private void Awake()
	{
		contentHeight = 20000f;
		_scrollRect.verticalNormalizedPosition = 0f;
		_playHead.anchorMax = new Vector2(1f, 0f);
		_playHead.anchorMin = new Vector2(0f, 0f);
		_playHead.anchoredPosition = new Vector2(0f, _playHeadPointsOffset);
		_scrollRect.onValueChanged.AddListener(ScrollViewDidScroll);
		ChangeParams(60f, 4, 600f);
	}

	private void OnDestroy()
	{
		PlayerPrefs.SetFloat("BeatmapEditor.BarPosition", _lastUsedBarPosition);
	}

	public void ChangeParams(float songBPM, int beatsPerBar, float songDuration)
	{
		float num = ((!(_barDuration > 0f)) ? 0f : (scrollPositionSongTime / _barDuration));
		_songDuration = songDuration;
		_barDuration = 60f / songBPM * 4f;
		_beatDuration = _barDuration / (float)beatsPerBar;
		float num2 = (float)beatsPerBar * _rowHeight;
		contentHeight = songDuration / _barDuration * num2 + _scrollRect.viewport.rect.height - _rowHeight * 8f;
		visibleAreaTimeDuration = _scrollRect.viewport.rect.height / num2 * 4f / songBPM * 60f;
		playHeadSongTimeOffset = _playHeadPointsOffset / num2 * 4f / songBPM * 60f;
		SetPositionToSongTime(num * _barDuration);
	}

	public void MoveToSavedBarPosition()
	{
		float @float = PlayerPrefs.GetFloat("BeatmapEditor.BarPosition", 0f);
		SetPositionToSongTime(@float * _barDuration);
	}

	public void SetPositionToSongTime(float songTime, bool snapToBeat = false)
	{
		if (snapToBeat)
		{
			songTime = (float)Mathf.RoundToInt(songTime / _beatDuration) * _beatDuration;
		}
		float verticalNormalizedPosition = Mathf.Min(Mathf.Max(0f, songTime / _songDuration), 1f);
		_scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
		_lastUsedBarPosition = scrollPositionSongTime / _barDuration;
	}

	public void SnapPositionToBeat()
	{
		SetPositionToSongTime(scrollPositionSongTime, snapToBeat: true);
	}

	private void ScrollViewDidScroll(Vector2 normalizedPos)
	{
		_lastUsedBarPosition = scrollPositionSongTime / _barDuration;
		if (scrollPosDidChangeEvent != null)
		{
			scrollPosDidChangeEvent();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (scrollDragDidBeginEvent != null)
		{
			scrollDragDidBeginEvent();
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		SnapPositionToBeat();
		if (scrollDragDidEndEvent != null)
		{
			scrollDragDidEndEvent();
		}
	}

	public int GetRowForWorldPos(Vector3 worldPos)
	{
		Vector3 vector = _scrollRect.content.transform.InverseTransformPoint(worldPos);
		int num = Mathf.FloorToInt((contentHeight + vector.y) / rowHeight);
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	public float GetRowWorldPos(int row)
	{
		return _scrollRect.content.TransformPoint(new Vector2(0f, (float)row * rowHeight - contentHeight)).y;
	}
}
