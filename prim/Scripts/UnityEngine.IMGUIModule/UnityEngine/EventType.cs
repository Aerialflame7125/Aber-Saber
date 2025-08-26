using System;

namespace UnityEngine;

public enum EventType
{
	MouseDown = 0,
	MouseUp = 1,
	MouseMove = 2,
	MouseDrag = 3,
	KeyDown = 4,
	KeyUp = 5,
	ScrollWheel = 6,
	Repaint = 7,
	Layout = 8,
	DragUpdated = 9,
	DragPerform = 10,
	DragExited = 15,
	Ignore = 11,
	Used = 12,
	ValidateCommand = 13,
	ExecuteCommand = 14,
	ContextClick = 16,
	MouseEnterWindow = 20,
	MouseLeaveWindow = 21,
	[Obsolete("Use MouseDown instead (UnityUpgradable) -> MouseDown", true)]
	mouseDown = 0,
	[Obsolete("Use MouseUp instead (UnityUpgradable) -> MouseUp", true)]
	mouseUp = 1,
	[Obsolete("Use MouseMove instead (UnityUpgradable) -> MouseMove", true)]
	mouseMove = 2,
	[Obsolete("Use MouseDrag instead (UnityUpgradable) -> MouseDrag", true)]
	mouseDrag = 3,
	[Obsolete("Use KeyDown instead (UnityUpgradable) -> KeyDown", true)]
	keyDown = 4,
	[Obsolete("Use KeyUp instead (UnityUpgradable) -> KeyUp", true)]
	keyUp = 5,
	[Obsolete("Use ScrollWheel instead (UnityUpgradable) -> ScrollWheel", true)]
	scrollWheel = 6,
	[Obsolete("Use Repaint instead (UnityUpgradable) -> Repaint", true)]
	repaint = 7,
	[Obsolete("Use Layout instead (UnityUpgradable) -> Layout", true)]
	layout = 8,
	[Obsolete("Use DragUpdated instead (UnityUpgradable) -> DragUpdated", true)]
	dragUpdated = 9,
	[Obsolete("Use DragPerform instead (UnityUpgradable) -> DragPerform", true)]
	dragPerform = 10,
	[Obsolete("Use Ignore instead (UnityUpgradable) -> Ignore", true)]
	ignore = 11,
	[Obsolete("Use Used instead (UnityUpgradable) -> Used", true)]
	used = 12
}
