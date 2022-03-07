using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextLocalizer : MonoBehaviour
{
    TextMeshProUGUI _textMeshField;
    Text _textField;

    public string Key;

    private void Start()
    {
        string value = LocalizationSystem.GetLocalisedValue(Key);

        if(TryGetComponent(out _textMeshField))
            _textMeshField.text = value;

        if (TryGetComponent(out _textField))
            _textField.text = value;
    }

    [ContextMenu("AddToCSV")]
    public void AddToCSV()
    {
        string value = "";

        if (TryGetComponent(out _textMeshField))
            value = _textMeshField.text;

        if (TryGetComponent(out _textField))
            value = _textField.text;

        LocalizationSystem.Add(Key, value);
    }
}
