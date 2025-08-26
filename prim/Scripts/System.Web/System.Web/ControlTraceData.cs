namespace System.Web;

internal sealed class ControlTraceData
{
	public string ControlId;

	public Type Type;

	public int RenderSize;

	public int ViewstateSize;

	public int Depth;

	public int ControlstateSize;

	public ControlTraceData(string controlId, Type type, int renderSize, int viewstateSize, int controlstateSize, int depth)
	{
		ControlId = controlId;
		Type = type;
		RenderSize = renderSize;
		ViewstateSize = viewstateSize;
		Depth = depth;
		ControlstateSize = controlstateSize;
	}
}
