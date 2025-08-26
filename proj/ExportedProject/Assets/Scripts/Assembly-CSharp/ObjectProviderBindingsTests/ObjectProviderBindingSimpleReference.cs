using UnityEngine;

namespace ObjectProviderBindingsTests
{
	public class ObjectProviderBindingSimpleReference : MonoBehaviour
	{
		[SerializeField]
		[Provider(typeof(MainCamera))]
		private ObjectProvider _objectProvider;
	}
}
