using UnityEngine;
using Xft;

public class SaberWeaponTrail : XWeaponTrail
{
    [Space]
    [SerializeField]
    private SaberTypeObject _saberType;

    [SerializeField]
    private ColorManager _colorManager;

    [SerializeField]
    private Color _multiplierSaberColor = new Color(1f, 1f, 1f, 1f);

    public void SetMF(int value) { _maxFrame = value; }

    public void SetGran(int value) { _granularity = value; }

    public void SetSFF(int value) { _skipFirstFrames = value; }

    public void SetPointStart(Transform value) { _pointStart = value; }

    public void SetPointEnd(Transform value) { _pointEnd = value; }

    public void SetTrailRender(XWeaponTrailRenderer value) { _trailRendererPrefab = value; }

    public void SetSaberType(SaberTypeObject value) { _saberType = value; }

    public void SetColorManager(ColorManager value) { _colorManager = value; }

    public void SetColor(Color value) { _multiplierSaberColor = value; }

    protected override Color Color
    {
        get
        {
            return _colorManager.ColorForSaberType(_saberType.saberType) * _multiplierSaberColor;
        }
    }
}
