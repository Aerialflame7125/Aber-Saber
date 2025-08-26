using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _rankText;

	[SerializeField]
	private TextMeshProUGUI _playerNameText;

	[SerializeField]
	private TextMeshProUGUI _scoreText;

	[SerializeField]
	private TextMeshProUGUI _fullComboText;

	[SerializeField]
	private Color _normalColor;

	[SerializeField]
	private TMP_FontAsset _normalFont;

	[SerializeField]
	private Color _specialScoreColor;

	[SerializeField]
	private TMP_FontAsset _specialScoreFont;

	[SerializeField]
	private UnityEngine.UI.Image _separatorImage;

	public int rank
	{
		set
		{
			_rankText.text = value.ToString();
		}
	}

	public string playerName
	{
		set
		{
			_playerNameText.text = value;
		}
	}

	public int score
	{
		set
		{
			_scoreText.text = ((value < 0) ? string.Empty : ScoreFormatter.Format(value));
		}
	}

	public bool showSeparator
	{
		set
		{
			_separatorImage.enabled = value;
		}
	}

	public bool showFullCombo
	{
		set
		{
			_fullComboText.enabled = value;
		}
	}

	public bool specialScore
	{
		set
		{
			Color color = ((!value) ? _normalColor : _specialScoreColor);
			TMP_FontAsset font = ((!value) ? _normalFont : _specialScoreFont);
			_scoreText.color = color;
			_playerNameText.color = color;
			_rankText.color = color;
			_fullComboText.color = color;
			_scoreText.font = font;
			_playerNameText.font = font;
			_rankText.font = font;
			_fullComboText.font = font;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		base.enabled = false;
	}
}
