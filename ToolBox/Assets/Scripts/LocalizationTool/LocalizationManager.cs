using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    static LocalizationManager _instance;
    public static LocalizationManager Instance => _instance;

    [SerializeField] Language _language;

    List<TextLocalizer> _localizers = new List<TextLocalizer>();
    public Language Language
    {
        get => _language;
        set
        {
            SetLanguage(value);
        }
    }

    [Header("Debug")]
    [SerializeField] bool _changeLanguage;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SetLanguage(_language);
    }

    private void Update()
    {
        if(_changeLanguage)
        {
            _changeLanguage = false;
            SetLanguage(_language);
        }
    }


    private void SetLanguage(Language value)
    {
        if (LocalizationSystem.Language == value)
            return;

        _language = value;
        LocalizationSystem.SetLanguage(value);
        RefreshTexts();
    }

    private void RefreshTexts()
    {
        foreach(TextLocalizer text in _localizers)
        {
            text.RefreshValue();
        }
    }

    public void Register(TextLocalizer text)
    {
        _localizers.Add(text);
    }
}
