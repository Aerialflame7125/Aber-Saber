using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class ObstacleTypePanelController : MonoBehaviour, IBeatmapEditorObstacleLength, IBeatmapEditorObstacleType
{
	[SerializeField]
	private Text _lengthText;

	public ObstacleType obstacleType { get; private set; }

	public int obstacleLength { get; private set; }

	private void Awake()
	{
		obstacleLength = 8;
		_lengthText.text = obstacleLength.ToString();
	}

	public void SetObstacleType(int index)
	{
		obstacleType = (ObstacleType)index;
	}

	public void IncreaseLength(bool doubleLength)
	{
		if (doubleLength)
		{
			if (obstacleLength < 64)
			{
				obstacleLength *= 2;
			}
		}
		else
		{
			obstacleLength /= 2;
		}
		if (obstacleLength < 1)
		{
			obstacleLength = 1;
		}
		_lengthText.text = obstacleLength.ToString();
	}
}
