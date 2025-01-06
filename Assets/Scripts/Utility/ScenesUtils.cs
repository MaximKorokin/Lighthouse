using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesUtils
{
    public static void Load(Constants.Scene sceneName, MonoBehaviour behaviour, Action finalAction = null)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
        behaviour.StartCoroutineSafe(CoroutinesUtils.WaitForAsyncOperation(operation), finalAction);
    }
}
