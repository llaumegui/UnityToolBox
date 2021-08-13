using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public abstract class Database : MonoBehaviour
{
    //insert singleton

    public TextAsset TextDefinition = null;

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

    public abstract XElement Serialize();

}
