using UnityEngine.UI;

public class Touchable : Graphic
{
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}
