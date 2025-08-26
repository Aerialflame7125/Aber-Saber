public class MissionSuccessChecker
{
	private LevelMissionParser _levelMissionParser;

	private LevelCompletionResults _levelCompetionResults;

	public MissionSuccessChecker()
	{
		_levelMissionParser = new LevelMissionParser();
		_levelMissionParser.AddFunction("score>", ScoreGreaterThan);
		_levelMissionParser.AddFunction("score<", ScoreLessThan);
		_levelMissionParser.AddFunction("combo>", ComboGreaterThan);
		_levelMissionParser.AddFunction("combo<", ComboLessThan);
		_levelMissionParser.AddFunction("energy>", EnergyGreaterThan);
		_levelMissionParser.AddFunction("energy<", EnergyLessThan);
		_levelMissionParser.AddFunction("rank>", RankGreaterThan);
		_levelMissionParser.AddFunction("rank<", RankLessThan);
		_levelMissionParser.AddFunction("rank=", RankEqual);
	}

	private bool ScoreGreaterThan(float[] functionParams, int paramCount)
	{
		return (float)_levelCompetionResults.score > functionParams[0];
	}

	private bool ScoreLessThan(float[] functionParams, int paramCount)
	{
		return (float)_levelCompetionResults.score < functionParams[0];
	}

	private bool ComboGreaterThan(float[] functionParams, int paramCount)
	{
		return (float)_levelCompetionResults.maxCombo > functionParams[0];
	}

	private bool ComboLessThan(float[] functionParams, int paramCount)
	{
		return (float)_levelCompetionResults.maxCombo < functionParams[0];
	}

	private bool EnergyGreaterThan(float[] functionParams, int paramCount)
	{
		return _levelCompetionResults.energy > functionParams[0];
	}

	private bool EnergyLessThan(float[] functionParams, int paramCount)
	{
		return _levelCompetionResults.energy < functionParams[0];
	}

	private bool RankGreaterThan(float[] functionParams, int paramCount)
	{
		return false;
	}

	private bool RankLessThan(float[] functionParams, int paramCount)
	{
		return false;
	}

	private bool RankEqual(float[] functionParams, int paramCount)
	{
		return false;
	}

	public bool GetMissionSuccess(string missionFormula, LevelCompletionResults levelCompetionResults)
	{
		_levelCompetionResults = levelCompetionResults;
		return _levelMissionParser.Parse(missionFormula);
	}
}
