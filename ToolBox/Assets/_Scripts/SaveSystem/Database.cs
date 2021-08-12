using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Database : MonoBehaviour
{

    public TextAsset TextDefinition = null;

    public List<Data> Datas { get; private set; }

    public virtual void Awake()
    {
        Deserialize();
    }

    public virtual void Deserialize()
    {
        if (TextDefinition == null)
        {
            Debug.LogError($"Failed to find TextAsset of {this.GetType().Name}");
            return;
        }
    }

}
