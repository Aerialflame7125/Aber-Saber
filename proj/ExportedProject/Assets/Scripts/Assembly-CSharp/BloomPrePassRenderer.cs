using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BloomPrePassRenderer : ScriptableObject
{
	[SerializeField]
	private KawaseBlurRenderer _kawaseBlurRenderer;

	[SerializeField]
	private Shader _linesShader;

	[SerializeField]
	private Shader _linesHDRToLDRShader;

	[SerializeField]
	private Shader _additiveShader;

	private Material _linesMaterial;

	private Material _linesHDRToLDRMaterial;

	private Material _additiveMaterial;

	private int _prePassLinesFogDensityID;

	private int _fullIndensityOffsetID;

	private int _alphaID;

	private int _transformHDRToLDRParamID;

	private int _lineIntensityMultiplierID;

	private int _vertexTransfromMatrixID;

	private Mesh _mesh;

	private Vector3[] _vertices;

	private Color32[] _colors32;

	private Vector2[] _uv2;

	private Vector2[] _uv3;

	private CommandBuffer _commandBuffer;

	private void OnEnable()
	{
		_linesMaterial = new Material(_linesShader);
		_linesMaterial.hideFlags = HideFlags.HideAndDontSave;
		_linesHDRToLDRMaterial = new Material(_linesHDRToLDRShader);
		_linesHDRToLDRMaterial.hideFlags = HideFlags.HideAndDontSave;
		_additiveMaterial = new Material(_additiveShader);
		_additiveMaterial.hideFlags = HideFlags.HideAndDontSave;
		_prePassLinesFogDensityID = Shader.PropertyToID("_PrePassLinesFogDensity");
		_fullIndensityOffsetID = Shader.PropertyToID("_FullIntensityOffset");
		_alphaID = Shader.PropertyToID("_Alpha");
		_transformHDRToLDRParamID = Shader.PropertyToID("_TransformHDRToLDRParam");
		_lineIntensityMultiplierID = Shader.PropertyToID("_LineIntensityMultiplier");
		_vertexTransfromMatrixID = Shader.PropertyToID("_VertexTransfromMatrix");
	}

	private void OnDisable()
	{
		EssentialHelpers.SafeDestroy(_linesMaterial);
		EssentialHelpers.SafeDestroy(_linesHDRToLDRMaterial);
		EssentialHelpers.SafeDestroy(_additiveMaterial);
	}

	public void Render(Camera camera, BloomPrePassParams bloomPrePassParams, RenderTexture dest)
	{
		bool sRGBWrite = GL.sRGBWrite;
		GL.sRGBWrite = false;
		Matrix4x4 projectionMatrix;
		if (camera.stereoEnabled)
		{
			Matrix4x4 stereoProjectionMatrix = camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
			Matrix4x4 stereoProjectionMatrix2 = camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
			projectionMatrix = MatrixLerp(stereoProjectionMatrix, stereoProjectionMatrix2, 0.5f);
		}
		else
		{
			projectionMatrix = camera.projectionMatrix;
		}
		projectionMatrix.m00 *= bloomPrePassParams.textureToScreenRatio;
		projectionMatrix.m02 *= bloomPrePassParams.textureToScreenRatio;
		projectionMatrix.m11 *= bloomPrePassParams.textureToScreenRatio;
		projectionMatrix.m12 *= bloomPrePassParams.textureToScreenRatio;
		Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
		RenderTexture temporary = RenderTexture.GetTemporary(bloomPrePassParams.textureWidth, bloomPrePassParams.textureHeight, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
		Graphics.SetRenderTarget(temporary);
		GL.Clear(true, true, Color.black);
		RenderAllLights(worldToCameraMatrix, projectionMatrix, bloomPrePassParams.linesTexture, bloomPrePassParams.linesWidth, bloomPrePassParams.linesFogDensity, bloomPrePassParams.fullIntensityOffset, bloomPrePassParams.lineIntensityMultiplier);
		RenderTexture temporary2 = RenderTexture.GetTemporary(bloomPrePassParams.textureWidth, bloomPrePassParams.textureHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
		_linesHDRToLDRMaterial.SetFloat(_transformHDRToLDRParamID, bloomPrePassParams.transformHDRToLDRParam);
		Graphics.Blit(temporary, temporary2, _linesHDRToLDRMaterial);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture temporary3 = RenderTexture.GetTemporary(bloomPrePassParams.textureWidth, bloomPrePassParams.textureHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
		_kawaseBlurRenderer.DoubleBlur(temporary2, dest, bloomPrePassParams.bloom1KernelSize, bloomPrePassParams.bloom1Boost, temporary3, bloomPrePassParams.bloom2KernelSize, bloomPrePassParams.bloom2Boost, bloomPrePassParams.downsample);
		RenderTexture.ReleaseTemporary(temporary2);
		_additiveMaterial.SetFloat(_alphaID, bloomPrePassParams.bloom2Alpha);
		Graphics.Blit(temporary3, dest, _additiveMaterial);
		RenderTexture.ReleaseTemporary(temporary3);
		GL.sRGBWrite = sRGBWrite;
	}

	private void RenderAllLights(Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, Texture2D linesTexture, float linesWidth, float linesFogDensity, float fullIntensityOffset, float lineIntensityMultiplier)
	{
		List<BloomPrePassLight> lightList = BloomPrePassLight.lightList;
		if (lightList != null && lightList.Count != 0)
		{
			int count = lightList.Count;
			PrepareLightsMeshRendering(count);
			_linesMaterial.SetPass(0);
			_linesMaterial.SetFloat(_prePassLinesFogDensityID, linesFogDensity);
			_linesMaterial.SetFloat(_fullIndensityOffsetID, fullIntensityOffset);
			_linesMaterial.SetFloat(_lineIntensityMultiplierID, lineIntensityMultiplier);
			_linesMaterial.SetMatrix(_vertexTransfromMatrixID, Matrix4x4.Ortho(0f, 1f, 1f, 0f, -1f, 1f));
			_linesMaterial.mainTexture = linesTexture;
			for (int i = 0; i < count; i++)
			{
				lightList[i].FillMeshData(i, _vertices, _colors32, _uv2, _uv3, viewMatrix, projectionMatrix, linesWidth);
			}
			_mesh.vertices = _vertices;
			_mesh.colors32 = _colors32;
			_mesh.uv2 = _uv2;
			_mesh.uv3 = _uv3;

			//Shuts up the compiler
			_commandBuffer = new CommandBuffer();
			_commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.Ortho(0f, 1f, 1f, 0f, -1f, 1f));
			_commandBuffer.DrawMesh(_mesh, Matrix4x4.identity, _linesMaterial);

			//Originally commented, broke bloom.
			Graphics.ExecuteCommandBuffer(_commandBuffer);
		}
	}

	private void PrepareLightsMeshRendering(int numberOfLights)
	{
		if (_vertices == null || _vertices.Length != numberOfLights * 4)
		{
			_vertices = new Vector3[numberOfLights * 4];
		}
		if (_colors32 == null || _colors32.Length != numberOfLights * 4)
		{
			_colors32 = new Color32[numberOfLights * 4];
		}
		if (_uv2 == null || _uv2.Length != numberOfLights * 4)
		{
			_uv2 = new Vector2[numberOfLights * 4];
		}
		if (_uv3 == null || _uv3.Length != numberOfLights * 4)
		{
			_uv3 = new Vector2[numberOfLights * 4];
		}
		if (!_mesh || _mesh.vertexCount != numberOfLights * 4)
		{
			if ((bool)_mesh)
			{
				_mesh.Clear();
			}
			else
			{
				_mesh = new Mesh();
				_mesh.MarkDynamic();
			}
			_mesh.vertices = _vertices;
			int[] array = new int[numberOfLights * 2 * 3];
			Vector2[] array2 = new Vector2[numberOfLights * 4];
			for (int i = 0; i < numberOfLights; i++)
			{
				array[i * 6] = i * 4;
				array[i * 6 + 1] = i * 4 + 1;
				array[i * 6 + 2] = i * 4 + 2;
				array[i * 6 + 3] = i * 4 + 2;
				array[i * 6 + 4] = i * 4 + 3;
				array[i * 6 + 5] = i * 4;
				array2[i * 4] = new Vector2(0f, 0f);
				array2[i * 4 + 1] = new Vector2(1f, 0f);
				array2[i * 4 + 2] = new Vector2(1f, 1f);
				array2[i * 4 + 3] = new Vector2(0f, 1f);
			}
			_mesh.triangles = array;
			_mesh.uv = array2;
			_commandBuffer = new CommandBuffer();
			_commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.Ortho(0f, 1f, 1f, 0f, -1f, 1f));
			_commandBuffer.DrawMesh(_mesh, Matrix4x4.identity, _linesMaterial);
		}
	}

	private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
	{
		Matrix4x4 result = default(Matrix4x4);
		for (int i = 0; i < 16; i++)
		{
			result[i] = Mathf.Lerp(from[i], to[i], t);
		}
		return result;
	}
}
