using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader _instance;
    public static SceneLoader Instance => _instance;

    [SerializeField] int _idLoadingScreen;
    int _idTarget = 1;

    public int IdTarget => _idTarget;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadTarget(int sceneTarget)
    {
        if(SceneManager.GetSceneByBuildIndex(sceneTarget).IsValid())
        {
            Debug.LogError("Scene not found");
            return;
        }

        _idTarget = sceneTarget;
        SceneManager.LoadScene(_idLoadingScreen);
    }

    public void LoadTarget(string sceneName)
    {
        if(!SceneManager.GetSceneByName(sceneName).IsValid())
        {
            Debug.LogError("Scene not found");
            return;
        }

        LoadTarget(SceneManager.GetSceneByName(sceneName).buildIndex);
    }
}
