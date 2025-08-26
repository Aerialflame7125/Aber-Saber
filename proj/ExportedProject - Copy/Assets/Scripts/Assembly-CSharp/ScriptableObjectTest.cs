using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectTest", menuName = "BS/Others/ScriptableObjectTest")]
public class ScriptableObjectTest : ScriptableObject
{
	private class SomeClass
	{
		public void Foo()
		{
			Debug.Log("OK");
		}
	}

	private SomeClass _someClass;

	private void Awake()
	{
		_someClass = new SomeClass();
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public void Foo()
	{
		_someClass.Foo();
	}
}
