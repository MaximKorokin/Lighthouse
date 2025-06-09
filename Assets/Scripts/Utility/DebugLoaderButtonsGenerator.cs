using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugLoaderButtonsGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonPrefab;

    private void Start()
    {
        foreach (var sceneName in ((Constants.Scene[])Enum.GetValues(typeof(Constants.Scene))).Except(Constants.Scene.DebugLoader.Yield()))
        {
            var button = Instantiate(_buttonPrefab);

            button.GetComponentInChildren<TMP_Text>().text = sceneName.ToString();
            button.GetComponentInChildren<Button>().onClick.AddListener(() => GameManager.LoadScene(sceneName, this));

            button.transform.SetParent(transform, false);
        }
    }
}
