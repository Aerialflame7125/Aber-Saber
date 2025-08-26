using UnityEngine;

public class ObstacleSaberSparkleEffect : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _sparkleParticleSystem;

	[SerializeField]
	private ParticleSystem _smokeParticleSystem;

	[SerializeField]
	private ParticleSystem _burnParticleSystem;

	private ParticleSystem.EmissionModule _sparkleParticleSystemEmmisionModule;

	private ParticleSystem.EmissionModule _smokeParticleSystemEmmisionModule;

	private ParticleSystem.EmissionModule _burnParticleSystemEmmisionModule;

	private void Awake()
	{
		_sparkleParticleSystemEmmisionModule = _sparkleParticleSystem.emission;
		_smokeParticleSystemEmmisionModule = _smokeParticleSystem.emission;
		_burnParticleSystemEmmisionModule = _burnParticleSystem.emission;
		_sparkleParticleSystemEmmisionModule.enabled = false;
		_smokeParticleSystemEmmisionModule.enabled = false;
		_burnParticleSystemEmmisionModule.enabled = false;
	}

	public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
	{
		base.transform.SetPositionAndRotation(pos, rot);
	}

	public void StartEmission()
	{
		if (!_burnParticleSystemEmmisionModule.enabled)
		{
			_sparkleParticleSystemEmmisionModule.enabled = true;
			_smokeParticleSystemEmmisionModule.enabled = true;
			_burnParticleSystemEmmisionModule.enabled = true;
		}
	}

	public void StopEmission()
	{
		if (_burnParticleSystemEmmisionModule.enabled)
		{
			_burnParticleSystem.Clear();
			_sparkleParticleSystemEmmisionModule.enabled = false;
			_smokeParticleSystemEmmisionModule.enabled = false;
			_burnParticleSystemEmmisionModule.enabled = false;
		}
	}
}
