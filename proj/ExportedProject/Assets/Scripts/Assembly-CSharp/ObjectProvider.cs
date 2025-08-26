using UnityEngine;

public class ObjectProvider : ScriptableObject
{
	protected MonoBehaviour _providedObject;

	public T GetProvidedObject<T>() where T : MonoBehaviour
	{
		return _providedObject as T;
	}

	public void Setup(MonoBehaviour providedObject)
	{
		_providedObject = providedObject;
	}

	public void Reset()
	{
		_providedObject = null;
	}
}
