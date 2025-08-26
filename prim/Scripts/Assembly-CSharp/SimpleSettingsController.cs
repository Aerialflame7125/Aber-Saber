using UnityEngine;

public abstract class SimpleSettingsController : MonoBehaviour
{
	public abstract void Init();

	public abstract void ApplySettings();

	public abstract void CancelSettings();
}
