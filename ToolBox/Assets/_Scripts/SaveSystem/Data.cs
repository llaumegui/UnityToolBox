using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public abstract class Data
{
    public abstract void Deserialize(XContainer container);
}
