using System;
using UnityEngine;

public class LevelCollectionsForGameplayModes : ScriptableObject
{
	[Serializable]
	public class LevelCollectionForGameplayMode
	{
		[SerializeField]
		private GameplayMode _gameplayMode;

		[SerializeField]
		private StandardLevelCollectionSO _levelCollection;

		public GameplayMode gameplayMode => _gameplayMode;

		public StandardLevelCollectionSO levelCollection => _levelCollection;
	}

	[SerializeField]
	private LevelCollectionForGameplayMode[] _collections;

	public StandardLevelSO[] GetLevels(GameplayMode gameplayMode)
	{
		LevelCollectionForGameplayMode[] collections = _collections;
		foreach (LevelCollectionForGameplayMode levelCollectionForGameplayMode in collections)
		{
			if (levelCollectionForGameplayMode.gameplayMode == gameplayMode)
			{
				return levelCollectionForGameplayMode.levelCollection.levels;
			}
		}
		return null;
	}

	public StandardLevelCollectionSO GetCollection(GameplayMode gameplayMode)
	{
		LevelCollectionForGameplayMode[] collections = _collections;
		foreach (LevelCollectionForGameplayMode levelCollectionForGameplayMode in collections)
		{
			if (levelCollectionForGameplayMode.gameplayMode == gameplayMode)
			{
				return levelCollectionForGameplayMode.levelCollection;
			}
		}
		return null;
	}
}
