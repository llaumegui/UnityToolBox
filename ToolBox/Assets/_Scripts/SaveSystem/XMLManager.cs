using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO; //file management
using System;
using System.Xml.Linq;

[DefaultExecutionOrder(1)]
public class XMLManager : MonoBehaviour
{
    public bool DebugSerialize;

    string _xmlPath;

    #region Singleton
    public static XMLManager Instance { get; private set; }

    public List<Database> Databases;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

         _xmlPath = $"{Application.persistentDataPath}/XMLSaves/";

        if (!Directory.Exists(_xmlPath))
            Directory.CreateDirectory(_xmlPath);
    }
    #endregion


    private void Start()
    {

    }

    private void Update()
    {
        if(DebugSerialize)
        {
            DebugSerialize = false;

            Serialize(new TestData(new XDocument()));

            if (Databases.Count > 0)
                SerializeDB(Databases[0]);
        }
    }

    public void Serialize(Data data)
    {
        XDocument doc = new XDocument(new XComment("TestComment"));

        doc.Add(data.Serialize());

        string newPath = _xmlPath + data.GetType().Name+".xml";

        doc.Save(newPath);
    }

    public void SerializeDB(Database db)
    {
        XDocument doc = new XDocument(new XComment("TestComment"));

        doc.Add(db.Serialize());

        string newPath = _xmlPath + db.GetType().Name + ".xml";

        doc.Save(newPath);
    }
}
