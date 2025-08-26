using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NoteDebris : MonoBehaviour
{
	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private GameObject _meshGameObject;

	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	[Space]
	[SerializeField]
	private AnimationCurve _cutoutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[SerializeField]
	private float _lifeTime = 1.5f;

	[Space]
	[SerializeField]
	private Mesh _centroidComputationMesh;

	private float _elapsedTime;

	private Rigidbody _rigidbody;

	private int _cutoutPropertyID;

	private int _colorID;

	private int _cutPlaneID;

	private int _cutoutTexOffsetID;

	private Transform _meshTranform;

	private Vector3[] _meshVertices;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_meshTranform = _meshGameObject.transform;
		_meshVertices = _centroidComputationMesh.vertices;
		_cutoutPropertyID = Shader.PropertyToID("_Cutout");
		_colorID = Shader.PropertyToID("_Color");
		_cutPlaneID = Shader.PropertyToID("_CutPlane");
		_cutoutTexOffsetID = Shader.PropertyToID("_CutoutTexOffset");
	}

	private void Update()
	{
		if (_elapsedTime < _lifeTime)
		{
			_materialPropertyBlockController.materialPropertyBlock.SetFloat(_cutoutPropertyID, _cutoutCurve.Evaluate(_elapsedTime / _lifeTime));
			_materialPropertyBlockController.ApplyChanges();
			_elapsedTime += Time.deltaTime;
		}
		else
		{
			base.gameObject.Recycle();
		}
	}

	public void Init(NoteType noteType, Transform initTransform, Vector3 cutPoint, Vector3 cutNormal, Vector3 force, Vector3 torque)
	{
		Vector3 rhs = initTransform.InverseTransformPoint(cutPoint);
		Vector3 vector = initTransform.InverseTransformVector(cutNormal);
		Vector4 vector2 = vector;
		vector2.w = 0f - Vector3.Dot(vector, rhs);
		float num = Mathf.Sqrt(Vector3.Dot(vector2, vector2));
		Vector3 zero = Vector3.zero;
		int num2 = 0;
		for (int i = 0; i < _meshVertices.Length; i++)
		{
			Vector3 vector3 = _meshVertices[i];
			float num3 = Vector3.Dot(vector2, vector3) + vector2.w;
			if (!(num3 < 0f))
			{
				float num4 = num3 / num;
				Vector3 vector4 = vector3 - (Vector3)vector2 * num4;
				zero += (vector4 + vector3) * 0.5f;
				num2++;
			}
		}
		if (num2 > 0)
		{
			zero /= (float)num2;
		}
		base.transform.SetPositionAndRotation(initTransform.TransformPoint(zero), initTransform.rotation);
		base.transform.localScale = initTransform.localScale;
		_meshTranform.localPosition = -zero;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		_rigidbody.AddForce(force, ForceMode.VelocityChange);
		_rigidbody.AddTorque(torque, ForceMode.VelocityChange);
		Color value = _colorManager.ColorForNoteType(noteType);
		MaterialPropertyBlock materialPropertyBlock = _materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.Clear();
		materialPropertyBlock.SetColor(_colorID, value);
		materialPropertyBlock.SetVector(_cutPlaneID, vector2);
		materialPropertyBlock.SetVector(_cutoutTexOffsetID, Random.insideUnitSphere);
		materialPropertyBlock.SetFloat(_cutoutPropertyID, 0f);
		_materialPropertyBlockController.ApplyChanges();
		_elapsedTime = 0f;
	}
}
