using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLocalizationExample : MonoBehaviour
{
    bool _french = true;

    private void SetLanguage()
    {
        if (TryGetComponent(out LocalizationManager script))
            if (_french)
                script.Language = Language.French;
            else
                script.Language = Language.English;
    }

    public void SwitchLanguage()
    {
        _french = !_french;
        SetLanguage();
    }
}
