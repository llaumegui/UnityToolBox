﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class TestData : Data
{
    public string Hi;
    public int Num;


    public override void Deserialize(XContainer container)
    {
        throw new System.NotImplementedException();
    }
}
