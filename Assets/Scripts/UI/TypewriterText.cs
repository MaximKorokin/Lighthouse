using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterText : MonoBehaviour
{
    private TMP_Text _text;
    private string _currentTextString;

    public bool IsTyping { get; private set; }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string textString, float charTime)
    {
        _currentTextString = textString;
        _text.text = "";
        IsTyping = true;
        StartCoroutine(Typewrite(charTime));
    }

    public void ForceCurrentText()
    {
        IsTyping = false;
        _text.text = _currentTextString;
    }

    private IEnumerator Typewrite(float charTime)
    {
        foreach (var c in _currentTextString)
        {
            if (!IsTyping)
            {
                yield break;
            }
            _text.text += c;
            yield return new WaitForSecondsRealtime(charTime);
        }
        IsTyping = false;
    }
}
