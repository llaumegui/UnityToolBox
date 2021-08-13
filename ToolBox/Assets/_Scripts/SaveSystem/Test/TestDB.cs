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
}
