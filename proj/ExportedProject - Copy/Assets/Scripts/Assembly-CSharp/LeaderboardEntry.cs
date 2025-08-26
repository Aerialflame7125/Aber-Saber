using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	[SerializeField]
	private TextMeshProUGUI _playerNameText;

	[SerializeField]
	private TextMeshProUGUI _rankText;

	[SerializeField]
	private Color _color = Color.black;

	public void SetScore(int score, string playerName, int rank, bool highlighted, bool showSeparator)
	{
		_scoreText.text = ScoreFormatter.Format(score);
		_playerNameText.text = playerName;
		_rankText.text = rank.ToString();
		Color color = ((!highlighted) ? (_color * 0.5f) : _color);
		_scoreText.color = color;
		_playerNameText.color = color;
		_rankText.color = color;
	}
}
