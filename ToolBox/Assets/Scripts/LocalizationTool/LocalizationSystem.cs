using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English,French
    }
    public static Language language = Language.English;

    static Dictionary<string, string> _localisedEN;
    static Dictionary<string, string> _localisedFR;
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
    }

    public static string GetLocalisedValue(string key)
    {
        if (!IsInit) { Init(); }

        string value = key;

        switch(language)
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

    public static void Add(string key,string value)
    {
        if (value.Contains("\""))
            value.Replace('"', '\"');

        if (_csvLoader == null)
            _csvLoader = new CSVLoader();

        _csvLoader.LoadCSV();
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
}
