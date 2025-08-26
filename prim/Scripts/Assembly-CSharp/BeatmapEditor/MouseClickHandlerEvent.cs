using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace BeatmapEditor;

[Serializable]
public class MouseClickHandlerEvent : UnityEvent<PointerEventData.InputButton>
{
}
