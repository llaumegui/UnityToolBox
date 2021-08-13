using System.Collections;
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
}
