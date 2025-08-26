using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using BL.Leaderboards;
using HMUI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using VRUI;
using BL.Auth;

public class BlLeaderboardViewController : VRUIViewController
{

    BL.Authentication.CurrentToken _currentToken = new BL.Authentication.CurrentToken();

    [Serializable]
    public class PlayerInfo
    {
        public int mapperId;
        public bool banned;
        public bool inactive;
        public string banDescription;
        public string externalProfileUrl;
        public int richBioTimeset;
        public int speedrunStart;
        public object linkedIds; // Can be null, so use object
        public object history; // Can be null, so use object
        public object badges; // Can be null, so use object
        public object pinnedScores; // Can be null, so use object
        public object changes; // Can be null, so use object
        public float accPp;
        public float passPp;
        public float techPp;
        public int allContextsPp;
        public ScoreStats scoreStats;
        public float lastWeekPp;
        public int lastWeekRank;
        public int lastWeekCountryRank;
        public int extensionId;
        public string id; // This is the ID you are looking for
        public string name;
        public string platform;
        public string avatar;
        public string country;
        public string alias;
        public bool bot;
        public float pp;
        public int rank;
        public int countryRank;
        public int level;
        public int experience;
        public int prestige;
        public string role;
        public Social[] socials;
        public ContextExtension[] contextExtensions;
        public object patreonFeatures; // Can be null, so use object
        public ProfileSettings profileSettings;
        public string clanOrder;
        public ClanInfo[] clans;
    }

    [Serializable]
    public class ScoreStats
    {
        public int id;
        public long totalScore;
        public long totalUnrankedScore;
        public long totalRankedScore;
        public long firstScoreTime;
        public long firstUnrankedScoreTime;
        public long firstRankedScoreTime;
        public long lastScoreTime;
        public long lastUnrankedScoreTime;
        public long lastRankedScoreTime;
        public float averageRankedAccuracy;
        public float averageWeightedRankedAccuracy;
        public float averageUnrankedAccuracy;
        public float averageAccuracy;
        public float medianRankedAccuracy;
        public float medianAccuracy;
        public float topRankedAccuracy;
        public float topUnrankedAccuracy;
        public float topAccuracy;
        public float topPp;
        public float topBonusPP;
        public float topPassPP;
        public float topAccPP;
        public float topTechPP;
        public int peakRank;
        public int rankedMaxStreak;
        public int unrankedMaxStreak;
        public int maxStreak;
        public float averageLeftTiming;
        public float averageRightTiming;
        public int steamPlaytime2Weeks;
        public int steamPlaytimeForever;
        public float scorePlaytime;
        public int rankedPlayCount;
        public int unrankedPlayCount;
        public int totalPlayCount;
        public int rankedImprovementsCount;
        public int unrankedImprovementsCount;
        public int totalImprovementsCount;
        public int rankedTop1Count;
        public int unrankedTop1Count;
        public int top1Count;
        public int rankedTop1Score;
        public int unrankedTop1Score;
        public int top1Score;
        public float averageRankedRank;
        public float averageWeightedRankedRank;
        public float averageUnrankedRank;
        public float averageRank;
        public int sspPlays;
        public int ssPlays;
        public int spPlays;
        public int sPlays;
        public int aPlays;
        public string topPlatform;
        public int topHMD;
        public string allHMDs;
        public float topPercentile;
        public float countryTopPercentile;
        public int dailyImprovements;
        public int authorizedReplayWatched;
        public int anonimusReplayWatched;
        public int watchedReplays;
    }

    [Serializable]
    public class Social
    {
        public int id;
        public string service;
        public string link;
        public string user;
        public string userId;
        public string playerId;
        public bool hidden;
    }

    [Serializable]
    public class ContextExtension
    {
        public int id;
        public int context;
        public float pp;
        public float accPp;
        public float techPp;
        public float passPp;
        public int rank;
        public string country;
        public int countryRank;
        public int level;
        public int experience;
        public int prestige;
        public float lastWeekPp;
        public int lastWeekRank;
        public int lastWeekCountryRank;
        public string playerId;
        public object scoreStats; // Can be null, so use object
        public bool banned;
    }

    [Serializable]
    public class ProfileSettings
    {
        public int id;
        public object bio; // Can be null, so use object
        public object message; // Can be null, so use object
        public string effectName;
        public string profileAppearance;
        public float hue;
        public float saturation;
        public object leftSaberColor; // Can be null, so use object
        public object rightSaberColor; // Can be null, so use object
        public string profileCover;
        public string starredFriends;
        public bool horizontalRichBio;
        public string rankedMapperSort;
        public bool showBots;
        public bool showAllRatings;
        public bool showExplicitCovers;
        public bool showStatsPublic;
        public bool showStatsPublicPinned;
    }

    [Serializable]
    public class ClanInfo
    {
        public int id;
        public string tag;
        public string color;
        public string name; // Can be null, so use string
    }

    [Serializable]
    public class ClanRoot
    {
        public int id;
        public string name;
        public string color;
        public string icon;
        public string tag;
        public string leaderID;
        public int playersCount;
        public float averageRank;
        public float averageAccuracy;
        public int rankedPoolPercentCaptured;
        public int captureLeaderboardsCount;
        public string[] players;
        public string[] pendingInvites;
    }

    [Serializable]
    public class ClanRequest
    {
        public int id;
        public string name;
        public string color;
        public string icon;
        public string tag;
        public string leaderID;
        public string description;
        public string bio;
        public int richBioTimeset;
        public int playersCount;
        public int mainPlayersCount;
        public float pp;
        public int rank;
        public float averageRank;
        public float averageAccuracy;
        public object featuredPlaylists; // Can be null
        public int rankedPoolPercentCaptured;
        public int captureLeaderboardsCount;
        public object capturedLeaderboards; // Can be null
        public int globalMapX;
        public int globalMapY;
    }

    [Serializable]
    public class FriendInfo
    {
        public int mapperId;
        public bool banned;
        public bool inactive;
        public object banDescription; // Can be null
        public string externalProfileUrl;
        public int richBioTimeset;
        public int speedrunStart;
        public object linkedIds; // Can be null
        public object history; // Can be null
        public object badges; // Can be null
        public object pinnedScores; // Can be null
        public object changes; // Can be null
        public float accPp;
        public float passPp;
        public float techPp;
        public int allContextsPp;
        public object scoreStats; // Can be null
        public float lastWeekPp;
        public int lastWeekRank;
        public int lastWeekCountryRank;
        public int extensionId;
        public string id; // This is the ID for friends
        public string name;
        public string platform;
        public string avatar;
        public string country;
        public string alias;
        public bool bot;
        public float pp;
        public int rank;
        public int countryRank;
        public int level;
        public int experience;
        public int prestige;
        public string role;
        public object socials; // Can be null
        public object contextExtensions; // Can be null
        public object patreonFeatures; // Can be null
        public ProfileSettings profileSettings;
        public string clanOrder;
        public object clans; // Can be null
    }


    // This is the root class that matches the entire JSON structure
    [Serializable]
    public class RootObject
    {
        public PlayerInfo player;
        public ClanRoot clan;
        public object ban; // Can be null
        public ClanRequest[] clanRequest;
        public ClanInfo[] bannedClans;
        public object playlists; // Can be null
        public FriendInfo[] friends;
    }

    [SerializeField]
    private BlLeaderboardTableView _leaderboardTableView; // Your UI component

    [SerializeField]
    private SimpleSegmentedControl _scopeSegmentedControl;

    [SerializeField]
    private GameObject _loadingIndicator;

    [SerializeField]
    private GetLeaderboard _getLeaderboardFetcher;

    private static PlatformLeaderboardsModel.ScoresScope _scoresScope;

    [SerializeField]
    private GameObject _playerNotSetText;

    [Space]
    [SerializeField]
    private Button UpButton;

    [SerializeField]
    private Button DownButton;

    private IStandardLevelDifficultyBeatmap _difficultyLevel;

    private GameplayMode _gameplayMode;

    private string _difficulty;
    private int _currentPage = 1;

    private int[] _playerScorePos; // Still used by SetScores, but logic for custom index will be simple or default

    // Ensure this list uses the correct ScoreData type from LeaderboardTableView
    private List<BlLeaderboardTableView.ScoreData> _scores = new List<BlLeaderboardTableView.ScoreData>(10);

    private bool _forcedLoadingIndicator;

    public bool forcedLoadingIndicator
    {
        get
        {
            return _forcedLoadingIndicator;
        }
        set
        {
            _forcedLoadingIndicator = value;
            _loadingIndicator.SetActive(_forcedLoadingIndicator);
            if (_forcedLoadingIndicator)
            {
                Clear();
            }
        }
    }

    private void OnDestroy()
    {
        if (_scopeSegmentedControl != null)
        {
            _scopeSegmentedControl.didSelectCellEvent -= HandleScopeSegmentedControlDidSelectCell;
        }
    }

    protected override void DidActivate(bool firstActivation, ActivationType activationType)
    {
        if (firstActivation)
        {
            _currentPage = 1;
            _playerScorePos = new int[3] { -1, -1, -1 };
            _scopeSegmentedControl.didSelectCellEvent += HandleScopeSegmentedControlDidSelectCell;
            base.rectTransform.anchorMin = Vector2.zero;
            base.rectTransform.anchorMax = Vector2.one;
            base.rectTransform.offsetMin = Vector2.zero;
            base.rectTransform.offsetMax = Vector2.zero;
            _scopeSegmentedControl.SetTexts(new string[3] { "Global", "Player", "Friends" });
            //UpButton.onClick.Addlistener(OnClicked);
            //DownButton.onClick.Addlistener(OnClicked);
        }
        if (activationType == ActivationType.AddedToHierarchy)
        {
            _scopeSegmentedControl.SelectColumn((int)_scoresScope);
        }
    }

    protected override void DidDeactivate(DeactivationType deactivationType)
    {
        // No _asyncRequest to cancel here anymore
    }

    private void LeaderboardsResultsReturned(string jsonResult)
    {
        _loadingIndicator.SetActive(false);

        if (string.IsNullOrEmpty(jsonResult) || jsonResult == "[]" || jsonResult == "{\"Items\":[]}")
        {
            Debug.LogWarning("Leaderboard: No data received or JSON is empty.");
            _scores.Clear();
            _leaderboardTableView.SetScores(_scores, -1);
            return;
        } 
        else if (jsonResult.ToString() == "No Score")
        {
            _playerNotSetText.SetActive(true);
            return;
        }

        BL.Leaderboards.Wrapper<BL.Leaderboards.GetLeaderboard.CustomBeatLeaderScore> wrapper;
        try
        {
            wrapper = JsonUtility.FromJson<BL.Leaderboards.Wrapper<BL.Leaderboards.GetLeaderboard.CustomBeatLeaderScore>>(jsonResult);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error parsing leaderboard JSON: {e.Message}\nJSON: {jsonResult}");
            _scores.Clear();
            _leaderboardTableView.SetScores(_scores, -1);
            return;
        }

        _scores.Clear();

        if (wrapper != null && wrapper.Items != null)
        {
            foreach (var customScore in wrapper.Items)
            {
                float displayAccuracy = (float)customScore.Accuracy / 100.0f;

                _scores.Add(new BlLeaderboardTableView.ScoreData(
                    displayAccuracy,
                    customScore.Score,
                    customScore.Player,
                    customScore.Place,
                    customScore.fullCombo,
                    customScore.missedNotes,
                    customScore.avatar
                ));
            }
        }

        int customPlayerScorePos = -1;
        _leaderboardTableView.SetScores(_scores, customPlayerScorePos);
    }

    private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int column)
    {
        switch (column)
        {
            case 0:
                Debug.LogError("Getting Global");
                _scoresScope = PlatformLeaderboardsModel.ScoresScope.Global;
                break;
            case 1:
                Debug.LogError("Getting Around Player");
                _scoresScope = PlatformLeaderboardsModel.ScoresScope.AroundPlayer;
                break;
            case 2:
                Debug.LogError("Getting Friends");
                _scoresScope = PlatformLeaderboardsModel.ScoresScope.Friends;
                break;
        }
        Refresh();
    }

    public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode, int context, int scope, int page = 1)
    {
        // Store difficulty and page as class members
        _difficulty = difficultyLevel.difficulty.ToString();
        _currentPage = page;

        _difficultyLevel = difficultyLevel;
        _gameplayMode = GameplayMode.SoloStandard;

        if (_getLeaderboardFetcher == null)
        {
            Debug.LogError("GetLeaderboard fetcher not assigned in BlLeaderboardViewController!");
            return;
        }

        string mapIDForBeatSaver = difficultyLevel.level.levelID;
        Clear();

        // Corrected call with all required parameters for GetLeaderboard.ByID
        if (transform.parent.gameObject.activeInHierarchy)
        {
            Refresh();
        }
        else
        {
            Debug.LogError("Beatleader not active in Heirarchy.");
            StartCoroutine(sleepfortime(2f));
            Init(_difficultyLevel, gameplayMode, context, scope, page);
        }
    }

    IEnumerator sleepfortime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

    public IEnumerator Refresh()
    {
        Debug.LogError("Refreshed leaderboard");
        if (!_forcedLoadingIndicator)
        {
            _playerNotSetText.SetActive(false);
            _loadingIndicator.SetActive(true);
            Clear();
            if (_scoresScope == PlatformLeaderboardsModel.ScoresScope.Global)
            {
                if (_getLeaderboardFetcher == null)
                {
                    Debug.LogError("GetLeaderboard fetcher not assigned in BlLeaderboardViewController for Refresh!");
                    _loadingIndicator.SetActive(false);
                    yield break;
                }

                if (_difficultyLevel == null)
                {
                    Debug.LogWarning("Cannot refresh leaderboard: DifficultyLevel not set (call Init first).");
                    _loadingIndicator.SetActive(false);
                    yield break;
                }

                string mapIDForBeatSaver = _difficultyLevel.level.levelID;
                // Corrected call with all required parameters for GetLeaderboard.ByID
                // Assuming _difficulty and _currentPage are set during Init or managed elsewhere
                Debug.LogError("Grabbing global leaderboard for map " + mapIDForBeatSaver);
                StartCoroutine(_getLeaderboardFetcher.ByIDGlobal(mapIDForBeatSaver, _difficulty, _currentPage.ToString(), LeaderboardsResultsReturned));
            }
            else if (_scoresScope == PlatformLeaderboardsModel.ScoresScope.Friends)
            {
                _playerNotSetText.SetActive(true);
                _loadingIndicator.SetActive(false);
                Debug.LogError("ScoresScope set to friends");
            }
            else
            {
                //https://api.beatleader.net/v3/scores/{hash}/{diff}/{characteristic}/1/1/around?player={userID}&count=10
                
                if (_getLeaderboardFetcher == null)
                {
                    Debug.LogError("GetLeaderboard fetcher not assigned in BlLeaderboardViewController for Refresh!");
                    _loadingIndicator.SetActive(false);
                    yield break;
                }

                if (_difficultyLevel == null)
                {
                    Debug.LogWarning("Cannot refresh leaderboard: DifficultyLevel not set (call Init first).");
                    _loadingIndicator.SetActive(false);
                    yield break;
                }

                string mapIDForBeatSaver = _difficultyLevel.level.levelID;

                string userID;

                string token = _currentToken.token;

                UnityWebRequest request = UnityWebRequest.Get("https://api.beatleader.net/user");
                request.SetRequestHeader("Accept", "*/*");
                request.SetRequestHeader("User-Agent", "Aber Saber/0.11.1(BeatLeader/0.0.1)");

                UnityEngine.Debug.Log($"Attempting login to BeatLeader...");
                Authentication.CookieManager.ApplyCookies(request);
                yield return request.SendWebRequest();

                string responseBody = request.downloadHandler?.text;
                long responseCode = request.responseCode;

                if (request.isNetworkError || request.isHttpError) // Use isNetworkError and isHttpError for Unity 2019
                {
                    // Handle network or HTTP errors
                    UnityEngine.Debug.LogError($"Getting UserID failed." +
                                               $"URL: {request.url}, " +
                                               $"Response Code: {responseCode}, " +
                                               $"Error: {request.error}, " +
                                               $"Response Body: {responseBody}");
                }
                else
                {
                    RootObject data = JsonUtility.FromJson<RootObject>(responseBody);

                    if (data != null && data.player != null)
                    {
                        Debug.Log("Player ID: " + data.player.id);
                        userID = data.player.id;
                        StartCoroutine(_getLeaderboardFetcher.ByIDAround(mapIDForBeatSaver, _difficulty, _currentPage.ToString(), userID, LeaderboardsResultsReturned));
                    }
                    else
                    {
                        Debug.LogError("Could not parse player data or player object is null.");
                    }
                }
            }

        }
        else
        {
            Debug.LogError("Not forced indicator");
        }
    }

    public void Clear()
    {
        _scores.Clear();
        // Corrected typo: _BlLeaderboardTableView -> _leaderboardTableView
        _leaderboardTableView.SetScores(_scores, -1);
    }
}