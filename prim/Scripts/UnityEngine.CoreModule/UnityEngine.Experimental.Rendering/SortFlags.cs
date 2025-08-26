using System;

namespace UnityEngine.Experimental.Rendering;

[Flags]
public enum SortFlags
{
	None = 0,
	SortingLayer = 1,
	RenderQueue = 2,
	BackToFront = 4,
	QuantizedFrontToBack = 8,
	OptimizeStateChanges = 0x10,
	CanvasOrder = 0x20,
	CommonOpaque = 0x3B,
	CommonTransparent = 0x17
}
