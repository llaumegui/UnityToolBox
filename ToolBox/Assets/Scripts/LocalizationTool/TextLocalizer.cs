using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TextLocalizer : MonoBehaviour
{
    TextMeshProUGUI _textMeshGUIField;
    TextMeshPro _textMeshField;
    Text _textField;

    public string Key;

    bool _checkComponents;

    private void Start()
    {
        Register();
        RefreshValue();

        CheckComponents();
    }

    private void CheckComponents() //try to detect any part of text
    {
        TryGetComponent(out _textMeshGUIField);
        TryGetComponent(out _textField);
        TryGetComponent(out _textMeshField);

        _checkComponents = true;
    }

    public void RefreshValue()
    {
        if(string.IsNullOrEmpty(Key))
        {
            Debug.LogWarning("Missing key " + gameObject);
            return;
        }

        string value = LocalizationSystem.GetLocalisedValue(Key);

        if (!_checkComponents)
            CheckComponents();

        if (_textMeshGUIField)
            _textMeshGUIField.text = value;

        if (_textField)
            _textField.text = value;

        if (_textMeshField)
            _textMeshField.text = value;
    }

    void Register()
    {
        if (LocalizationManager.Instance == null)
        {
            Debug.LogError("LocalizationManager Not Found");
            return;
        }

        LocalizationManager.Instance.Register(this);
    }

#if UNITY_EDITOR
    [ContextMenu("AddToCSV")]
    public void AddToCSV()
    {
        string value = "";

        if (!_checkComponents)
            CheckComponents();

        if (_textMeshGUIField)
            value = _textMeshGUIField.text;

        if (_textField)
            value = _textField.text;

        if (_textMeshField)
            value = _textMeshField.text;

        LocalizationSystem.Add(Key, value);
    }
#endif

}
