using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NoteMeshCutDebris : MonoBehaviour
{
	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private Transform _meshTranform;

	[SerializeField]
	private MeshFilter _meshFilter;

	[SerializeField]
	private MeshRenderer _meshRenderer;

	[Space]
	[SerializeField]
	private Material _baseMaterial;

	[SerializeField]
	private Material _redCutMaterial;

	[SerializeField]
	private Material _blueCutMaterial;

	[Space]
	[SerializeField]
	private AnimationCurve _cutoutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[SerializeField]
	private float _lifeTime = 1.5f;

	private float _elapsedTime;

	private Rigidbody _rigidbody;

	private int _cutoutPropertyID;

	private Mesh _mesh;

	private static MaterialPropertyBlock _materialPropertyBlock;

	public Mesh mesh => _mesh;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if (_materialPropertyBlock == null)
		{
			_materialPropertyBlock = new MaterialPropertyBlock();
		}
		_cutoutPropertyID = Shader.PropertyToID("_Cutout");
		_mesh = new Mesh();
		_mesh.name = "DebrisMesh";
		_mesh.hideFlags = HideFlags.HideAndDontSave;
		_meshFilter.mesh = _mesh;
	}

	private void Update()
	{
		if (_elapsedTime < _lifeTime)
		{
			_materialPropertyBlock.SetFloat(_cutoutPropertyID, _cutoutCurve.Evaluate(_elapsedTime / _lifeTime));
			_meshRenderer.SetPropertyBlock(_materialPropertyBlock);
			_elapsedTime += Time.deltaTime;
		}
		else
		{
			_materialPropertyBlock.SetFloat(_cutoutPropertyID, 1f);
			base.gameObject.Recycle();
		}
	}

	public void Init(NoteType noteType, Transform initTransform, Vector3 force, Vector3 torque)
	{
		Vector3[] vertices = mesh.vertices;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < vertices.Length; i++)
		{
			zero += vertices[i];
		}
		if (vertices.Length > 0)
		{
			zero /= (float)vertices.Length;
		}
		base.transform.position = initTransform.TransformPoint(zero);
		base.transform.rotation = initTransform.rotation;
		base.transform.localScale = initTransform.localScale;
		_meshTranform.localPosition = -zero;
		Material[] materials = new Material[2]
		{
			_baseMaterial,
			(noteType != 0) ? _blueCutMaterial : _redCutMaterial
		};
		_meshRenderer.materials = materials;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		_rigidbody.AddForce(force, ForceMode.VelocityChange);
		_rigidbody.AddTorque(torque, ForceMode.VelocityChange);
		Color value = _colorManager.ColorForNoteType(noteType);
		_materialPropertyBlock.SetColor("_CutoutEdgeColor", value);
		_materialPropertyBlock.SetFloat(_cutoutPropertyID, 0f);
		_meshRenderer.SetPropertyBlock(_materialPropertyBlock);
		_elapsedTime = 0f;
	}
}
