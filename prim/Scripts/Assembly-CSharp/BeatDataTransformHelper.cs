public class BeatDataTransformHelper
{
	public static BeatmapData CreateTransformedBeatmapData(BeatmapData beatmapData, GameplayOptions gameplayOptions, GameplayMode gameplayMode)
	{
		BeatmapData beatmapData2 = beatmapData;
		if (gameplayOptions.mirror)
		{
			beatmapData2 = BeatDataMirrorTransform.CreateTransformedData(beatmapData2);
		}
		if (gameplayMode == GameplayMode.SoloNoArrows)
		{
			beatmapData2 = BeatmapDataNoArrowsTransform.CreateTransformedData(beatmapData2);
		}
		if (gameplayOptions.obstaclesOption != 0)
		{
			beatmapData2 = BeatmapDataObstaclesTransform.CreateTransformedData(beatmapData2, gameplayOptions.obstaclesOption);
		}
		if (beatmapData2 == beatmapData)
		{
			beatmapData2 = beatmapData.GetCopy();
		}
		if (gameplayOptions.staticLights)
		{
			beatmapData2 = new BeatmapData(beatmapEventData: new BeatmapEventData[2]
			{
				new BeatmapEventData(0f, BeatmapEventType.Event0, 1),
				new BeatmapEventData(0f, BeatmapEventType.Event4, 1)
			}, beatmapLinesData: beatmapData2.beatmapLinesData);
		}
		return beatmapData2;
	}
}
