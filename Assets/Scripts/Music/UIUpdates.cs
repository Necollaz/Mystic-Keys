using UnityEngine;
using UnityEngine.UI;

public class UIUpdates
{
    private Graphic _targetGraphic;
    private Graphic _muteGraphic;

    public UIUpdates(Graphic targetGraphic, Graphic muteGraphic)
    {
        _targetGraphic = targetGraphic;
        _muteGraphic = muteGraphic;
    }

    public void UpdateGraphic(bool isOn, Toggle mute)
    {
        if (isOn)
        {
            mute.targetGraphic = _targetGraphic;
            _targetGraphic.gameObject.SetActive(true);
            _muteGraphic.gameObject.SetActive(false);
        }
        else
        {
            mute.targetGraphic = _muteGraphic;
            _targetGraphic.gameObject.SetActive(false);
            _muteGraphic.gameObject.SetActive(true);
        }
    }
}
