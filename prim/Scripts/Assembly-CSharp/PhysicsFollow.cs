using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsFollow : MonoBehaviour
{
	public Transform _targetTransform;

	public Vector3 _offset;

	public float _friction = 0.9f;

	public float _elasticity = 10f;

	private Rigidbody2D _rigidBody2D;

	private void Start()
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
		base.transform.position = _targetTransform.position + _offset;
	}

	private void FixedUpdate()
	{
		Vector3 vector = _rigidBody2D.velocity;
		Vector3 vector2 = _targetTransform.position + _offset;
		vector += (vector2 - (Vector3)_rigidBody2D.position) * _elasticity * Time.fixedDeltaTime;
		vector *= _friction;
		_rigidBody2D.velocity = vector;
	}
}
