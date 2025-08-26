using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;

public class OculusPlatformLeaderboardsHandler : PlatformLeaderboardsHandler
{
	private HashSet<ulong> _oculusRequestIds;

	public OculusPlatformLeaderboardsHandler()
	{
		_oculusRequestIds = new HashSet<ulong>();
	}

	private void AddOculusRequest(Request oculusRequest, HMAsyncRequest asyncRequest)
	{
		_oculusRequestIds.Add(oculusRequest.RequestID);
		if (asyncRequest != null)
		{
			asyncRequest.CancelHandler = delegate
			{
				_oculusRequestIds.Remove(oculusRequest.RequestID);
			};
		}
	}

	private bool CheckMessageForValidRequest(Message message)
	{
		if (!_oculusRequestIds.Contains(message.RequestID))
		{
			return false;
		}
		_oculusRequestIds.Remove(message.RequestID);
		return true;
	}

	public override void GetScores(string leaderboadId, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		Message<LeaderboardEntryList>.Callback callback = delegate(Message<LeaderboardEntryList> message)
		{
			if (CheckMessageForValidRequest(message))
			{
				if (message.IsError)
				{
					if (completionHandler != null)
					{
						completionHandler(PlatformLeaderboardsModel.GetScoresResult.Failed, null, -1);
					}
				}
				else
				{
					PlatformLeaderboardsModel.LeaderboardScore[] array = new PlatformLeaderboardsModel.LeaderboardScore[message.Data.Count];
					int referencePlayerScoreIndex = -1;
					for (int i = 0; i < message.Data.Count; i++)
					{
						Oculus.Platform.Models.LeaderboardEntry leaderboardEntry = message.Data[i];
						ref PlatformLeaderboardsModel.LeaderboardScore reference = ref array[i];
						reference = new PlatformLeaderboardsModel.LeaderboardScore((int)leaderboardEntry.Score, leaderboardEntry.Rank, leaderboardEntry.User.OculusID.ToString(), leaderboardEntry.User.ID.ToString());
						if (array[i].playerId == referencePlayerId)
						{
							referencePlayerScoreIndex = i;
						}
					}
					if (completionHandler != null)
					{
						completionHandler(PlatformLeaderboardsModel.GetScoresResult.OK, array, referencePlayerScoreIndex);
					}
				}
			}
		};
		AddOculusRequest(scope switch
		{
			PlatformLeaderboardsModel.ScoresScope.AroundPlayer => Leaderboards.GetEntries(leaderboadId, count, LeaderboardFilterType.None, LeaderboardStartAt.CenteredOnViewer).OnComplete(callback), 
			PlatformLeaderboardsModel.ScoresScope.Friends => Leaderboards.GetEntries(leaderboadId, count, LeaderboardFilterType.Friends, LeaderboardStartAt.CenteredOnViewer).OnComplete(callback), 
			_ => Leaderboards.GetEntriesAfterRank(leaderboadId, count, (ulong)(fromRank - 1)).OnComplete(callback), 
		}, asyncRequest);
	}

	public override void UploadScore(string leaderboadId, int score, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		Request<bool> oculusRequest = Leaderboards.WriteEntry(leaderboadId, score).OnComplete(delegate(Message<bool> messsage)
		{
			if (CheckMessageForValidRequest(messsage) && completionHandler != null)
			{
				completionHandler((messsage.IsError || !messsage.Data) ? PlatformLeaderboardsModel.UploadScoreResult.Falied : PlatformLeaderboardsModel.UploadScoreResult.OK);
			}
		});
		AddOculusRequest(oculusRequest, asyncRequest);
	}

	public override void GetPlayerId(HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.GetPlayerIdCompletionHandler completionHandler)
	{
		Request<User> oculusRequest = Users.GetLoggedInUser().OnComplete(delegate(Message<User> message)
		{
			if (CheckMessageForValidRequest(message))
			{
				if (message.IsError)
				{
					if (completionHandler != null)
					{
						completionHandler(PlatformLeaderboardsModel.GetPlayerIdResult.Failed, null);
					}
				}
				else
				{
					string playerId = message.Data.ID.ToString();
					if (completionHandler != null)
					{
						completionHandler(PlatformLeaderboardsModel.GetPlayerIdResult.OK, playerId);
					}
				}
			}
		});
		AddOculusRequest(oculusRequest, asyncRequest);
	}
}
