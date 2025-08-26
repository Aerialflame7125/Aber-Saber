using System;

namespace UnityEngine.Experimental.UIElements;

[Flags]
public enum ChangeType
{
	PersistentData = 0x40,
	PersistentDataPath = 0x20,
	Layout = 0x10,
	Styles = 8,
	Transform = 4,
	StylesPath = 2,
	Repaint = 1,
	All = 0x7F
}
