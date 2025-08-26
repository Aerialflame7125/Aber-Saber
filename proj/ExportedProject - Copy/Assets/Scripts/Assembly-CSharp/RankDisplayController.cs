using TMPro;
using UnityEngine;

public class RankDisplayController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    public LevelCompletionResults levelResults;

    private LevelCompletionResults.Rank lastDisplayedRank;
    private float timer = 0f;
    private const float updateInterval = 0.1f; // 100 ms

    private void Start()
    {
        if (rankText == null)
        {
            Debug.LogError("Rank TextMeshProUGUI reference is not assigned.");
        }

        if (levelResults != null)
        {
            lastDisplayedRank = levelResults.rank;
            ShowRank(lastDisplayedRank);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;

            if (levelResults != null && levelResults.rank != lastDisplayedRank)
            {
                lastDisplayedRank = levelResults.rank;
                ShowRank(lastDisplayedRank);
            }
        }
    }

    private void ShowRank(LevelCompletionResults.Rank rank)
    {
        if (rankText != null)
        {
            rankText.text = $"Rank: {LevelCompletionResults.GetRankName(rank)}";
        }
    }
}
