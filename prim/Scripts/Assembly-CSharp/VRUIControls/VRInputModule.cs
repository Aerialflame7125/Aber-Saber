using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRUIControls;

[RequireComponent(typeof(VRPointer))]
public class VRInputModule : BaseInputModule
{
	private VRPointer _vrPointer;

	public const int kMouseLeftId = -1;

	public const float kMinPressValue = 0.9f;

	protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();

	private List<Component> _componentList = new List<Component>(20);

	private readonly MouseState m_MouseState = new MouseState();

	private VRPointer vrPointer
	{
		get
		{
			if (_vrPointer == null)
			{
				_vrPointer = GetComponent<VRPointer>();
			}
			return _vrPointer;
		}
	}

	protected bool GetPointerData(int id, out PointerEventData data, bool create)
	{
		if (!m_PointerData.TryGetValue(id, out data) && create)
		{
			data = new PointerEventData(base.eventSystem)
			{
				pointerId = id
			};
			m_PointerData.Add(id, data);
			return true;
		}
		return false;
	}

	protected virtual MouseState GetMousePointerEventData(int id)
	{
		PointerEventData data;
		bool pointerData = GetPointerData(-1, out data, create: true);
		data.Reset();
		data.button = PointerEventData.InputButton.Left;
		VRController vrController = vrPointer.vrController;
		if (vrController.active)
		{
			data.pointerCurrentRaycast = new RaycastResult
			{
				worldPosition = vrController.position,
				worldNormal = vrController.forward
			};
			Vector2 scrollDelta = new Vector2(vrController.horizontalAxisValue, vrController.verticalAxisValue) * 1f;
			scrollDelta.x *= -1f;
			data.scrollDelta = scrollDelta;
		}
		base.eventSystem.RaycastAll(data, m_RaycastResultCache);
		RaycastResult raycastResult2 = (data.pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(m_RaycastResultCache));
		m_RaycastResultCache.Clear();
		Vector2 screenPosition = raycastResult2.screenPosition;
		if (pointerData)
		{
			data.delta = new Vector2(0f, 0f);
		}
		else
		{
			data.delta = screenPosition - data.position;
		}
		data.position = screenPosition;
		float num = 0f;
		PointerEventData.FramePressState stateForMouseButton = PointerEventData.FramePressState.NotChanged;
		if (vrController.active)
		{
			num = vrController.triggerValue;
			ButtonState buttonState = m_MouseState.GetButtonState(PointerEventData.InputButton.Left);
			if (buttonState.pressedValue < 0.9f && num >= 0.9f)
			{
				stateForMouseButton = PointerEventData.FramePressState.Pressed;
			}
			else if (buttonState.pressedValue >= 0.9f && num < 0.9f)
			{
				stateForMouseButton = PointerEventData.FramePressState.Released;
			}
			buttonState.pressedValue = num;
		}
		m_MouseState.SetButtonState(PointerEventData.InputButton.Left, stateForMouseButton, data);
		return m_MouseState;
	}

	protected PointerEventData GetLastPointerEventData(int id)
	{
		GetPointerData(id, out var data, create: false);
		return data;
	}

	private bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
	{
		if (!useDragThreshold)
		{
			return true;
		}
		return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
	}

	protected virtual void ProcessMove(PointerEventData pointerEvent)
	{
		GameObject newEnterTarget = pointerEvent.pointerCurrentRaycast.gameObject;
		HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
	}

	protected virtual void ProcessDrag(PointerEventData pointerEvent)
	{
		bool flag = pointerEvent.IsPointerMoving();
		if (flag && pointerEvent.pointerDrag != null && !pointerEvent.dragging && ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
		{
			ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
			pointerEvent.dragging = true;
		}
		if (pointerEvent.dragging && flag && pointerEvent.pointerDrag != null)
		{
			if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
			{
				ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
			}
			ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
		}
	}

	public override bool IsPointerOverGameObject(int pointerId)
	{
		PointerEventData lastPointerEventData = GetLastPointerEventData(pointerId);
		if (lastPointerEventData != null)
		{
			return lastPointerEventData.pointerEnter != null;
		}
		return false;
	}

	protected void ClearSelection()
	{
		BaseEventData baseEventData = GetBaseEventData();
		foreach (PointerEventData value in m_PointerData.Values)
		{
			HandlePointerExitAndEnter(value, null);
		}
		m_PointerData.Clear();
		base.eventSystem.SetSelectedGameObject(null, baseEventData);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + GetType());
		stringBuilder.AppendLine();
		foreach (KeyValuePair<int, PointerEventData> pointerDatum in m_PointerData)
		{
			if (pointerDatum.Value != null)
			{
				stringBuilder.AppendLine("<B>Pointer:</b> " + pointerDatum.Key);
				stringBuilder.AppendLine(pointerDatum.Value.ToString());
			}
		}
		return stringBuilder.ToString();
	}

	protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
	{
		GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
		if (eventHandler != base.eventSystem.currentSelectedGameObject)
		{
			base.eventSystem.SetSelectedGameObject(null, pointerEvent);
		}
	}

	public override void Process()
	{
		MouseState mousePointerEventData = GetMousePointerEventData(0);
		MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
		ProcessMousePress(eventData);
		ProcessMove(eventData.buttonData);
		ProcessDrag(eventData.buttonData);
		if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
			ExecuteEvents.ExecuteHierarchy(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
		}
		vrPointer.Process(eventData.buttonData);
	}

	protected bool SendUpdateEventToSelectedObject()
	{
		if (base.eventSystem.currentSelectedGameObject == null)
		{
			return false;
		}
		BaseEventData baseEventData = GetBaseEventData();
		ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
		return baseEventData.used;
	}

	protected void ProcessMousePress(MouseButtonEventData data)
	{
		PointerEventData buttonData = data.buttonData;
		GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
		if (data.PressedThisFrame())
		{
			buttonData.eligibleForClick = true;
			buttonData.delta = Vector2.zero;
			buttonData.dragging = false;
			buttonData.useDragThreshold = true;
			buttonData.pressPosition = buttonData.position;
			buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
			DeselectIfSelectionChanged(gameObject, buttonData);
			GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
			if (gameObject2 == null)
			{
				gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			}
			float unscaledTime = Time.unscaledTime;
			if (gameObject2 == buttonData.lastPress)
			{
				float num = unscaledTime - buttonData.clickTime;
				if (num < 0.3f)
				{
					buttonData.clickCount++;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.clickTime = unscaledTime;
			}
			else
			{
				buttonData.clickCount = 1;
			}
			buttonData.pointerPress = gameObject2;
			buttonData.rawPointerPress = gameObject;
			buttonData.clickTime = unscaledTime;
			buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
			if (buttonData.pointerDrag != null)
			{
				ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
			}
			ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
			buttonData.eligibleForClick = false;
		}
		if (data.ReleasedThisFrame())
		{
			ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
			{
				ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
			}
			else if (buttonData.pointerDrag != null && buttonData.dragging)
			{
				ExecuteEvents.ExecuteHierarchy(gameObject, buttonData, ExecuteEvents.dropHandler);
			}
			else
			{
				base.eventSystem.SetSelectedGameObject(null, null);
			}
			buttonData.eligibleForClick = false;
			buttonData.pointerPress = null;
			buttonData.rawPointerPress = null;
			if (buttonData.pointerDrag != null && buttonData.dragging)
			{
				ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
			}
			buttonData.dragging = false;
			buttonData.pointerDrag = null;
			if (gameObject != buttonData.pointerEnter)
			{
				HandlePointerExitAndEnter(buttonData, null);
				HandlePointerExitAndEnter(buttonData, gameObject);
			}
		}
	}

	protected new void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
	{
		if (newEnterTarget == null || currentPointerData.pointerEnter == null)
		{
			for (int i = 0; i < currentPointerData.hovered.Count; i++)
			{
				ExecuteEvents.Execute(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
			}
			currentPointerData.hovered.Clear();
			if (newEnterTarget == null)
			{
				currentPointerData.pointerEnter = newEnterTarget;
				return;
			}
		}
		if (currentPointerData.pointerEnter == newEnterTarget && (bool)newEnterTarget)
		{
			return;
		}
		GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
		if (currentPointerData.pointerEnter != null)
		{
			Transform parent = currentPointerData.pointerEnter.transform;
			while (parent != null && (!(gameObject != null) || !(gameObject.transform == parent)))
			{
				ExecuteEvents.Execute(parent.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
				currentPointerData.hovered.Remove(parent.gameObject);
				parent = parent.parent;
			}
		}
		currentPointerData.pointerEnter = newEnterTarget;
		if (!(newEnterTarget != null))
		{
			return;
		}
		Transform parent2 = newEnterTarget.transform;
		bool flag = false;
		while (parent2 != null && parent2.gameObject != gameObject)
		{
			_componentList.Clear();
			parent2.gameObject.GetComponents(_componentList);
			for (int j = 0; j < _componentList.Count; j++)
			{
				Selectable selectable = _componentList[j] as Selectable;
				VRInteractable vRInteractable = _componentList[j] as VRInteractable;
				if ((selectable != null && selectable.isActiveAndEnabled && selectable.interactable) || (vRInteractable != null && vRInteractable.isActiveAndEnabled && vRInteractable.interactable && !flag))
				{
					flag = true;
					PersistentSingleton<VRPlatformHelper>.instance.TriggerHapticPulse(vrPointer.vrController.node, 0.25f);
					break;
				}
			}
			ExecuteEvents.Execute(parent2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
			currentPointerData.hovered.Add(parent2.gameObject);
			parent2 = parent2.parent;
		}
	}
}
