using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO; //file management
using System;

[DefaultExecutionOrder(1)]
public class XMLManager : MonoBehaviour
{
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
        SerializeDB(Databases[0]);
    }

    public void Serialize(Data data)
    {
        string newPath = _xmlPath + data.GetType().Name + ".xml";

        XmlSerializer serializer = new XmlSerializer(data.GetType());
        if (File.Exists(newPath))
            File.Delete(newPath);
        FileStream stream = new FileStream(newPath, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }

    public void Serialize<T>() where T:Data
    {
        Type type = typeof(T);

        switch(type)
        {
            //write different data to write and link them to their method
        }
    }

    public void SerializeDB(Database db)
    {
        if (db.Datas.Count == 0)
            return;

        string newPath = _xmlPath + db.GetType().Name + ".xml";

        XmlSerializer serializer = new XmlSerializer(db.GetType());

        XmlWriter writer = XmlWriter.Create(newPath);

/*        writer.WriteStartElement(db.GetType().Name);

        foreach (Data data in db.Datas)
        {
            writer.WriteStartElement("data");
            serializer.Serialize(writer, data);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        writer.Close();*/
    }

    /* public void SerializeDB(Database db)      //DICTIONNARIES
 {
     Dictionary<string, Data> datas;
     if (datas.Count == 0)
         return;

     string newPath = _xmlPath + db.GetType().Name + ".xml";

     XmlSerializer keySerializer = new XmlSerializer(datas.Keys.GetType());
     XmlSerializer valueSerializer = new XmlSerializer(datas.Values.GetType());

     XmlWriter writer = XmlWriter.Create(newPath);

     foreach(KeyValuePair<string,Data> data in datas)
     {
         writer.WriteStartElement(db.name);

         writer.WriteStartElement("key");
         keySerializer.Serialize(writer, data.Key);
         writer.WriteEndElement();

         writer.WriteStartElement("value");
         valueSerializer.Serialize(writer, data.Value);
         writer.WriteEndElement();

         writer.WriteEndElement();
     }

     writer.Close();
 }*/
}
