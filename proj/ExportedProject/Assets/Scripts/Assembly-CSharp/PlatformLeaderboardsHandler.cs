public abstract class PlatformLeaderboardsHandler
{
	public abstract void GetScores(string leaderboadId, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler);

	public abstract void UploadScore(string leaderboadId, int score, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler);

	public abstract void GetPlayerId(HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.GetPlayerIdCompletionHandler completionHandler);
}
