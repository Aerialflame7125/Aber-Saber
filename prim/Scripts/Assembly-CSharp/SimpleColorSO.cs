using UnityEngine;

[CreateAssetMenu(fileName = "SimpleColorSO", menuName = "BS/Others/SimpleColorSO")]
public class SimpleColorSO : ColorSO
{
	[SerializeField]
	protected Color _color;

	public override Color color => _color;

	public void SetColor(Color c)
	{
		_color = c;
	}
}
