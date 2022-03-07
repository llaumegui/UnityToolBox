﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public abstract class Data
{
    public string Id { get; protected set; }

    public Data(XContainer container)
    {
        Deserialize(container);
    }
    public abstract void Deserialize(XContainer container);
    public abstract XElement Serialize();

    #region Templates
    /*public override void Deserialize(XContainer container)
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

        return new XElement("Data", new XAttribute("Id", Id),
            new XElement("Num", Num),
            new XElement("Txt", Txt));
    }*/
    #endregion
}