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

    protected override Color Color
    {
        get
        {
            return _colorManager.ColorForSaberType(_saberType.saberType) * _multiplierSaberColor;
        }
    }
}
