using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public abstract class Database : MonoBehaviour
{
    //insert singleton

    public TextAsset TextDefinition = null;

    [SerializeField]protected bool _serialize = false;

    public List<Data> Datas;

    public virtual void Awake()
    {
        if (!_serialize)
            Deserialize();
        else
        {
            _serialize = false;

            XMLManager.Instance.Serialize(this);
        }

    }

    public virtual void Deserialize()
    {
        if (TextDefinition == null)
        {
            Debug.LogError($"Unable to find TextAsset of {this.GetType().Name}");
            return;
        }
    }

    public abstract XElement Serialize();

    [ContextMenu("Serialize List")]
    public virtual void CallSerializer()
    {
        if(Application.isEditor)
        {
            Debug.LogError("You need to be in Play Mode in Order to Serialize");
            return;
        }

        XMLManager.Instance.Serialize(this);
    }


    #region Templates
    /*public override XElement Serialize()
    {
        if (List.Count > 0)
        {
            XElement e = new XElement("Root");

            for (int i = 0; i < List.Count; i++)
            {
                e.Add(Datas[i].Serialize());
            }

            return e;
        }

        return null;
    }*/

    /*
    public override void Deserialize()
    {
        base.Deserialize();

        Datas = new List<TestData>();

        XDocument doc = XDocument.Parse(TextDefinition.text);

        XElement dataElements = doc.Element("Root");

        foreach(XElement element in dataElements.Elements("Data"))
        {
            TestData data = new TestData(element);
            Datas.Add(data);
        }

        Debug.Log($"Deserialized {Datas.Count} in {this.GetType().Name}");
    }*/
    #endregion

}
