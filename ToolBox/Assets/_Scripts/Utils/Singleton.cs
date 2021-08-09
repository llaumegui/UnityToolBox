using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _instance;
    public static T Instance
    { get
        {
            if(_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                    Debug.LogError($"There is more than one {typeof(T).Name} in the Scene");

                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    static T _instance;
    public static T Instance { get => _instance; }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
