using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDB : Database
{
    public new List<TestData> Datas = new List<TestData>();

    public override void Awake()
    {
        base.Awake();
    }
}
