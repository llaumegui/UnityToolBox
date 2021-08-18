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
        XElement element = container as XElement;

        XAttribute id = element.Attribute("Id");
        Id = id.Value;

        XElement num = element.Element("Num");

        if (int.TryParse(num.Value, out int result))
            Num = result;
        else
            return;

        XElement txt = element.Element("Txt");
        Txt = txt.Value;
    }

    public override XElement Serialize()
    {
        if (string.IsNullOrEmpty(Id))
            Id = "SampleId";

        return new XElement("Data",new XAttribute("Id",Id),
            new XElement("Num", Num),
            new XElement("Txt", Txt));
    }
}
