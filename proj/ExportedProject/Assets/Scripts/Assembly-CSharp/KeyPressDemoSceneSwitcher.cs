using UnityEngine;

public class KeyPressDemoSceneSwitcher : MonoBehaviour
{
	[SerializeField]
	public KeyCode _menuSceneKeyCode = KeyCode.F1;

	[SerializeField]
	public KeyCode _gameSceneKeyCode = KeyCode.F2;

	[SerializeField]
	public KeyCode _tutorialSceneKeyCode = KeyCode.F3;

	[SerializeField]
	public KeyCode _mrSetupSceneKeyCode = KeyCode.F12;

	[SerializeField]
	public KeyCode _beatmapEditorSceneKeyCode = KeyCode.E;

	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	[SerializeField]
	private TutorialSceneSetupData _tutorialSceneSetupData;

	[SerializeField]
	private SceneInfo _mixedRealitySetupSceneInfo;

	[SerializeField]
	private SceneInfo _beatmapEditorSceneInfo;

	[SerializeField]
	private StandardLevelSO _standardLevel;
}
