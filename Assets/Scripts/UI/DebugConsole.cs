using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    private int _lineCount = 20;

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
        var currentText = _text.text +
            type switch
            {
                LogType.Error or LogType.Exception => $"\n {type.ToString().ToUpper()}: {logString}\n\t{stackTrace}",
                _ => $"\n {type.ToString().ToUpper()}: {logString}",
            };
        _text.text = string.Join('\n', currentText.Split('\n').TakeLast(_lineCount));
    }
}
