using UnityEngine;

public class EmitParticles : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem _particleSystem;

	public void Emit(int count)
	{
		_particleSystem.Emit(count);
	}
}
