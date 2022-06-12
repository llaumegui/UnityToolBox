using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] float _minimumTime = 3;
    float _clock;

    private void Start()
    {
        StartCoroutine(Load());

    }

    private void Update()
    {
        _clock += Time.deltaTime;
    }

    IEnumerator Load()
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(SceneLoader.Instance.IdTarget);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            if(operation.progress >= 0.9f && _clock >= _minimumTime)
                operation.allowSceneActivation = true;

            yield return null;
        }

    }
}
