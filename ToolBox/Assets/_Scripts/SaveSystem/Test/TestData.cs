using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class TestData : Data
{
    public int Num = 0;
    public string Txt = "slt";

    public TestData(XContainer container) : base(container)
    {
    }

    public override void Deserialize(XContainer container)
    {
        Id = "Test";
        //throw new System.NotImplementedException();
    }

    public override XElement Serialize()
    {
        if (Id == null)
            Id = "SampleName";

        return new XElement(Id,
            new XElement("Num", Num),
            new XElement("Txt", Txt));
    }
}
