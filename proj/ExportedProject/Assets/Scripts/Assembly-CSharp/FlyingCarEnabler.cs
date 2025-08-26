using UnityEngine;

public class FlyingCarEnabler : MonoBehaviour
{
	[SerializeField]
	private ResultsFlowCoordinator _resultsFlowCoordinator;

	private void Awake()
	{
		_resultsFlowCoordinator.didEnterPlayerNameEvent += HandleResultsFlowCoordinatorDidEnterPlayerName;
	}

	private void HandleResultsFlowCoordinatorDidEnterPlayerName(string playerName, LevelDifficulty levelDifficulty, string levelID)
	{
		if (playerName == "FLYING CAR" && levelID == "Level9" && levelDifficulty == LevelDifficulty.Hard)
		{
			FlyingCar.startflyingCars = true;
		}
	}
}
