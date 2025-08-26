using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Aerial;
using HMUI;

public class BlLeaderboardTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _rankText;

	[SerializeField]
	private TextMeshProUGUI _accuracyText;

	[SerializeField]
	private TextMeshProUGUI _missesText;

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

	[SerializeField]
	private HMUI.Image _userPfp;

	[SerializeField]
	private Aerial.ImageFromURL ImageFromURL;

	public string pfpPath
    {
        set
        {
			ImageFromURL.LoadImage(value, _userPfp);
        }
    }
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

	public int misses
    {
		set
        {
			Debug.LogError("Setting misses value to " + value);
			_fullComboText.enabled = true;
			_missesText.enabled = true;
			if (value <= 0)
            {
				_missesText.text = "";
				_fullComboText.text = "FC";
            } else if (value > 0)
            {
				_missesText.text = value.ToString();
				_fullComboText.text = "";
            }
        }
    }

	public float accuracy
    {
		set
        {
			_accuracyText.text = (Mathf.Round(value * 100.0f) / 100.0f).ToString();
        }
    }

	public bool showSeparator
	{
		set
		{
			_separatorImage.enabled = value;
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
			_accuracyText.font = font;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		base.enabled = false;
	}
}
