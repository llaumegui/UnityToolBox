using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TestDB : Database
{
    public static TestDB Instance;

    public List<TestData> Datas;

    public override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public override XElement Serialize()
    {
        if (Datas.Count > 0)
        {
            XElement e = new XElement("Root");

            for (int i = 0; i < Datas.Count; i++)
            {
                e.Add(Datas[i].Serialize());
            }

            return e;
        }

        return null;
    }

    public override void Deserialize()
    {
        base.Deserialize();

        Datas = new List<TestData>();

        XDocument doc = XDocument.Parse(TextDefinition.text);

        XElement dataElements = doc.Element("Root");

        foreach(XElement element in dataElements.Elements("Data"))
        {
            TestData data = new TestData(element);
            Datas.Add(data);
        }

        Debug.Log($"Deserialized {Datas.Count} in {this.GetType().Name}");
    }
}
