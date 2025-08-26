using UnityEngine;

public class EnableEmmisionOnVisible : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem[] _particleSystems;

	private ParticleSystem.EmissionModule[] _emmisionModules;

	private void Awake()
	{
		_emmisionModules = new ParticleSystem.EmissionModule[_particleSystems.Length];
		for (int i = 0; i < _particleSystems.Length; i++)
		{
			_emmisionModules[i] = _particleSystems[i].emission;
			_emmisionModules[i].enabled = false;
		}
	}

	private void OnBecameVisible()
	{
		for (int i = 0; i < _particleSystems.Length; i++)
		{
			_emmisionModules[i].enabled = true;
		}
	}

	private void OnBecameInvisible()
	{
		for (int i = 0; i < _particleSystems.Length; i++)
		{
			_emmisionModules[i].enabled = false;
		}
	}
}
