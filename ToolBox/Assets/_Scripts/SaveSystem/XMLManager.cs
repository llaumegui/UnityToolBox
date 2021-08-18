using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO; //file management
using System;
using System.Xml.Linq;

using UnityEditor;

[DefaultExecutionOrder(-1)]
public class XMLManager : MonoBehaviour
{
    public bool DebugSerialize;

    string _dataPath;
    string _localPath;

    #region Singleton
    public static XMLManager Instance { get; private set; }
    #endregion

    public List<Database> Databases;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _dataPath = $"{Application.persistentDataPath}/XMLSaves/";
        _localPath = $"{Application.dataPath}/Saves/";

        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
        if (!Directory.Exists(_localPath))
            Directory.CreateDirectory(_localPath);
    }

    private void Update()
    {
        if(DebugSerialize)
        {
            DebugSerialize = false;

        }
    }

    public void Serialize(Data data, bool dataPath = false)
    {
        XDocument doc = new XDocument(new XComment("TestComment"));

        doc.Add(data.Serialize());

        string newPath = string.Empty;

        if (dataPath)
            newPath = _dataPath + data.GetType().Name + ".xml";
        else
            newPath = _localPath + data.GetType().Name + ".xml";

        doc.Save(newPath);

        Refresh();
    }

    public void Serialize(Database db, bool dataPath = false)
    {
        XDocument doc = new XDocument(new XComment("TestComment"));

        doc.Add(db.Serialize());

        string newPath = string.Empty;

        if (dataPath)
            newPath = _dataPath + db.GetType().Name + ".xml";
        else
            newPath = _localPath + db.GetType().Name + ".xml";

        doc.Save(newPath);

        Refresh();
    }

    void Refresh()
    {
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
