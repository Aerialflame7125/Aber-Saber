using UnityEngine;

public class ShockwaveEffect : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _shockwavePS;

	private ParticleSystem.EmitParams _shockwavePSEmitParams;

	private ParticleSystem.Particle[] _shockwaveParticles;

	private float _prevShockwaveParticleSpawnTime;

	private void Awake()
	{
		_shockwaveParticles = new ParticleSystem.Particle[_shockwavePS.main.maxParticles];
		_shockwavePSEmitParams = default(ParticleSystem.EmitParams);
	}

	public void SpawnShockwave(Vector3 pos)
	{
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		if (!(timeSinceLevelLoad - _prevShockwaveParticleSpawnTime > 0.06f))
		{
			return;
		}
		int particles = _shockwavePS.GetParticles(_shockwaveParticles);
		if (particles >= _shockwavePS.main.maxParticles - 1)
		{
			float num = float.MaxValue;
			int num2 = 0;
			for (int i = 0; i < particles; i++)
			{
				float remainingLifetime = _shockwaveParticles[i].remainingLifetime;
				if (remainingLifetime < num)
				{
					num = remainingLifetime;
					num2 = i;
				}
			}
			_shockwaveParticles[num2].remainingLifetime = 0f;
			_shockwavePS.SetParticles(_shockwaveParticles, particles);
		}
		pos.y = 0.01f;
		_shockwavePSEmitParams.position = pos;
		_shockwavePS.Emit(_shockwavePSEmitParams, 1);
		_prevShockwaveParticleSpawnTime = timeSinceLevelLoad;
	}
}
