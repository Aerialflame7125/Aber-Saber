using System;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
	public Action TriggerCheckerOnEnterEvent;

	public Action TriggerCheckerOnExitEvent;

	public Action TriggerCheckerOnStayEvent;

	private void OnTriggerEnter(Collider other)
	{
		if (TriggerCheckerOnEnterEvent != null)
		{
			TriggerCheckerOnEnterEvent();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (TriggerCheckerOnExitEvent != null)
		{
			TriggerCheckerOnExitEvent();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (TriggerCheckerOnStayEvent != null)
		{
			TriggerCheckerOnStayEvent();
		}
	}
}
