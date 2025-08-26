using UnityEngine;

[CreateAssetMenu(fileName = "MultipliedColorSO", menuName = "BS/Others/MultipliedColorSO")]
public class MultipliedColorSO : ColorSO
{
	[SerializeField]
	private SimpleColorSO _baseColor;

	[SerializeField]
	private Color _multiplierColor;

	public override Color color => _multiplierColor * _baseColor.color;
}
