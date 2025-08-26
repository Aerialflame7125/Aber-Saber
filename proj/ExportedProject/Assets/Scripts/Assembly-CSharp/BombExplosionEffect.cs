using UnityEngine;

public class BombExplosionEffect : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _debrisPS;

	[SerializeField]
	private ParticleSystem _explosionPS;

	[SerializeField]
	private int _debrisCount = 40;

	[SerializeField]
	private int _explosionParticlesCount = 70;

	private ParticleSystem.EmitParams _emitParams;

	private ParticleSystem.EmitParams _explosionPSEmitParams;

	private void Awake()
	{
		_emitParams = default(ParticleSystem.EmitParams);
		_emitParams.applyShapeToPosition = true;
	}

	public void SpawnExplosion(Vector3 pos)
	{
		_emitParams.position = pos;
		_debrisPS.Emit(_emitParams, _debrisCount);
		_explosionPS.Emit(_emitParams, _explosionParticlesCount);
	}
}
