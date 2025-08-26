using UnityEngine;

public abstract class MixedRealityCompositor : Object
{
	public bool renderEveryFrame;

	public abstract void Cleanup();

	public abstract void DoComposition(int frameNum);
}
