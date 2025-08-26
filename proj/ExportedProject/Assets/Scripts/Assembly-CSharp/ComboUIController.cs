using TMPro;
using UnityEngine;

public class ComboUIController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private TextMeshProUGUI _comboText;

	private ScoreController _scoreController;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_scoreController.comboDidChangeEvent += HandleComboDidChange;
		HandleComboDidChange(0);
	}

	private void OnDestroy()
	{
		if ((bool)_scoreController)
		{
			_scoreController.comboDidChangeEvent -= HandleComboDidChange;
		}
	}

	private void HandleComboDidChange(int combo)
	{
		_comboText.text = combo.ToString();
	}
}
