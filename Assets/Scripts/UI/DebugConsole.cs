using System.Text;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DebugConsole : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        _text.text += type switch
        {
            LogType.Error => $"\n {type.ToString().ToUpper()}: {logString}\n\t{stackTrace}",
            _ => $"\n {type.ToString().ToUpper()}: {logString}",
        };
    }
}
