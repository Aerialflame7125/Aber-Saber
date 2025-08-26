using UnityEngine;
using UnityEngine.UI;

public class FillIndicator : MonoBehaviour
{
	[SerializeField]
	private Image _bgImage;

	[SerializeField]
	private Image _image;

	public float fillAmount
	{
		get
		{
			return _image.fillAmount;
		}
		set
		{
			_image.fillAmount = value;
			_bgImage.fillAmount = 1f - value;
		}
	}
}
