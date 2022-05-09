using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    English, French
}

public class LocalizationSystem
{
    public static Language Language = Language.English;

    static Dictionary<string, string> _localisedEN;
    static Dictionary<string, string> _localisedFR;
    //add more dictionaries if you want more languages

    static CSVLoader _csvLoader;

    public static bool IsInit;

    public static void Init()
    {
        _csvLoader = new CSVLoader();
        _csvLoader.LoadCSV();

        UpdateDictionaries();

        IsInit = true;
    }

    public static void UpdateDictionaries()
    {
        _localisedEN = _csvLoader.GetDictionnaryValues("en");
        _localisedFR = _csvLoader.GetDictionnaryValues("fr");
        //add extra lines if you want more dictionaries
    }

    public static string GetLocalisedValue(string key)
    {
        if (!IsInit) { Init(); }

        string value = key;

        switch(Language)
        {
            case Language.English:
                _localisedEN.TryGetValue(key, out value);
                break;
            case Language.French:
                _localisedFR.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public static void SetLanguage(Language value) => Language = value;

#if UNITY_EDITOR

    public static void Add(string key,string value)
    {

        if (value.Contains("\""))
            value.Replace('"', '\"');

        if (_csvLoader == null)
            _csvLoader = new CSVLoader();

        _csvLoader.LoadCSV();

        if (_csvLoader.Exists(key))
            return;

        _csvLoader.Add(key, value);
        _csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Remove(string key)
    {

        if (_csvLoader == null)
            _csvLoader = new CSVLoader();

        _csvLoader.LoadCSV();
        _csvLoader.Remove(key);
        _csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Sort()
    {
        if (_csvLoader == null)
            _csvLoader = new CSVLoader();

        _csvLoader.LoadCSV();
        _csvLoader.Sort();
        _csvLoader.LoadCSV();

        UpdateDictionaries();
    }

#endif
}
