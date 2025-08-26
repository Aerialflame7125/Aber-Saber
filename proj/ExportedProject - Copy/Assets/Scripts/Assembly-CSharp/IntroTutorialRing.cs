using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorialRing : MonoBehaviour
{
	[SerializeField]
	private Image[] _progressImages;

	[SerializeField]
	private Saber.SaberType _saberType;

	[SerializeField]
	private ParticleSystem _particleSystem;

	[SerializeField]
	private CanvasGroup _canvasGroup;

	[SerializeField]
	private float _activationDuration = 0.7f;

	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private Image[] _ringGLowImages;

	private bool _highlighted;

	private float _emitNextParticleTimer;

	private float _activationProgress;

	private HashSet<Saber.SaberType> _sabersInside = new HashSet<Saber.SaberType>();

	public float alpha
	{
		set
		{
			_canvasGroup.alpha = value;
		}
	}

	public bool fullyActivated
	{
		get
		{
			return _highlighted && _activationProgress == 1f;
		}
	}

	public Saber.SaberType saberType
	{
		get
		{
			return _saberType;
		}
		set
		{
			_saberType = value;
		}
	}

	private void Start()
	{
		Image[] ringGLowImages = _ringGLowImages;
		foreach (Image image in ringGLowImages)
		{
			image.color = _colorManager.ColorForSaberType(_saberType);
		}
	}

	private void Update()
	{
		bool flag = _sabersInside.Contains(_saberType);
		if (flag && !_highlighted)
		{
			_highlighted = true;
			_emitNextParticleTimer = 0f;
		}
		else if (!flag && _highlighted)
		{
			_highlighted = false;
		}
		if (_highlighted)
		{
			_activationProgress = Mathf.Min(_activationProgress + Time.deltaTime / _activationDuration, 1f);
			SetProgressImagesfillAmount(_activationProgress);
			if (_emitNextParticleTimer <= 0f)
			{
				_particleSystem.Emit(1);
				_emitNextParticleTimer = 0.25f;
			}
			_emitNextParticleTimer -= Time.deltaTime;
		}
		else
		{
			_activationProgress = Mathf.Max(_activationProgress - Time.deltaTime / _activationDuration, 0f);
			SetProgressImagesfillAmount(_activationProgress);
		}
	}

	private void SetProgressImagesfillAmount(float fillAmount)
	{
		Image[] progressImages = _progressImages;
		foreach (Image image in progressImages)
		{
			image.fillAmount = fillAmount;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (LayerMasks.saberLayerMask.ContainsLayer(other.gameObject.layer))
		{
			Saber component = other.gameObject.GetComponent<Saber>();
			_sabersInside.Add(component.saberType);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (LayerMasks.saberLayerMask.ContainsLayer(other.gameObject.layer))
		{
			Saber component = other.gameObject.GetComponent<Saber>();
			_sabersInside.Remove(component.saberType);
		}
	}
}
