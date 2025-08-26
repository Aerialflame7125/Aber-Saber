using UnityEngine;

public class NoteCutParticlesEffect : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _sparklesPS;

	[SerializeField]
	private ParticleSystem _explosionPS;

	private ParticleSystem.EmitParams _sparklesPSEmitParams;

	private ParticleSystem.ShapeModule _sparklesPSShapeModule;

	private ParticleSystem.EmitParams _explosionPSEmitParams;

	private void Awake()
	{
		_sparklesPSEmitParams = default(ParticleSystem.EmitParams);
		_sparklesPSShapeModule = _sparklesPS.shape;
		_explosionPSEmitParams = default(ParticleSystem.EmitParams);
		_explosionPSEmitParams.applyShapeToPosition = true;
	}

	public void SpawnParticles(Vector3 pos, Vector3 cutNormal, Vector3 saberDir, Color32 color, int sparkleParticlesCount, int explosionParticlesCount)
	{
		Quaternion quaternion = default(Quaternion);
		quaternion.eulerAngles = new Vector3(0f, 0f, 60f);
		Quaternion quaternion2 = default(Quaternion);
		quaternion2.SetLookRotation(cutNormal, saberDir);
		quaternion2 *= quaternion;
		_sparklesPSEmitParams.startColor = color;
		_sparklesPSShapeModule.position = pos;
		_sparklesPSShapeModule.rotation = quaternion2.eulerAngles;
		_sparklesPS.Emit(_sparklesPSEmitParams, sparkleParticlesCount);
		_explosionPSEmitParams.startColor = color;
		_explosionPSEmitParams.position = pos;
		_explosionPS.Emit(_explosionPSEmitParams, explosionParticlesCount);
	}
}
