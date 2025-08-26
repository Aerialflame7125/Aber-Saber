using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class EventSystemHelper
{
	public static bool IsInputFieldSelected()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return false;
		}
		if (currentSelectedGameObject.GetComponent<InputField>() == null)
		{
			return false;
		}
		return true;
	}
}
