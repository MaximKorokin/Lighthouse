using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterText : MonoBehaviour
{
    private TMP_Text _text;
    private string _currentTextString;
    private Coroutine _coroutine;

    public bool IsTyping => _coroutine != null;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string textString, float charTime)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        _currentTextString = textString;
        _text.text = "";
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(Typewrite(charTime));
    }

    public void ForceCurrentText()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _text.text = _currentTextString;
    }

    private IEnumerator Typewrite(float charTime)
    {
        foreach (var c in _currentTextString)
        {
            _text.text += c;
            yield return new WaitForSecondsRealtime(charTime);
        }
        _coroutine = null;
    }
}
