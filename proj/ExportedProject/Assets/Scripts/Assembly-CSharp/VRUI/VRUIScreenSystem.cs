using UnityEngine;

namespace VRUI
{
	public class VRUIScreenSystem : MonoBehaviour
	{
		[SerializeField]
		private VRUIScreen _mainScreen;

		[SerializeField]
		private VRUIScreen _leftScreen;

		[SerializeField]
		private VRUIScreen _rightScreen;

		[Space]
		[SerializeField]
		private VRUIScreen _multiplayerMainScreen;
		[SerializeField]
		private VRUIScreen _multiplayerLeftScreen;
		[SerializeField]
		private VRUIScreen _multiplayerRightScreen;
		public VRUIScreen mainScreen
		{
			get
			{
				return _mainScreen;
			}
		}

		public VRUIScreen leftScreen
		{
			get
			{
				return _leftScreen;
			}
		}

		public VRUIScreen rightScreen
		{
			get
			{
				return _rightScreen;
			}
		}

		public VRUIScreen mpMainScreen
		{
			get
			{
				return _multiplayerMainScreen;
			}
		}
		public VRUIScreen mpRightScreen
		{
			get
			{
				return _multiplayerRightScreen;
			}
		}
		public VRUIScreen mpLeftScreen
		{
			get
			{
				return _multiplayerLeftScreen;
			}
		}

		private void Awake()
		{
			if (_mainScreen != null)
			{
				_mainScreen.screenSystem = this;
				_leftScreen.screenSystem = this;
				_rightScreen.screenSystem = this;
			}
			

			if (_multiplayerMainScreen != null)
			{
				_multiplayerMainScreen.screenSystem = this;
				_multiplayerLeftScreen.screenSystem = this;
				_multiplayerRightScreen.screenSystem = this;
			}
		}
	}
}
