using System;
using UnityEngine;
using VRUI;

[Serializable]
public class SettingsSubMenuInfo
{
	[SerializeField]
	private VRUIViewController _controller;

	[SerializeField]
	private string _menuName;

	public VRUIViewController controller
	{
		get
		{
			return _controller;
		}
	}

	public string menuName
	{
		get
		{
			return _menuName;
		}
	}
}
