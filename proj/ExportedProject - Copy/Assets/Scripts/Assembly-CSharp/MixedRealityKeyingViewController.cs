using UnityEngine;
using UnityEngine.UI;

public class MixedRealityKeyingViewController : MonoBehaviour
{
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[Space]
	[SerializeField]
	private Slider _colorHSlider;

	[SerializeField]
	private Slider _colorSSlider;

	[SerializeField]
	private Slider _colorLSlider;

	[SerializeField]
	private Slider _thresholdHSlider;

	[SerializeField]
	private Slider _thresholdSSlider;

	[SerializeField]
	private Slider _thresholdLSlider;

	[SerializeField]
	private Slider _hardnessHSlider;

	[SerializeField]
	private Slider _hardnessSSlider;

	[SerializeField]
	private Slider _hardnessLSlider;
}
