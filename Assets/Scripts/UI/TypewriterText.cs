using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterText : MonoBehaviour
{
    private string _currentTextString;
    private Coroutine _coroutine;

    public TMP_Text Text { get; private set; }
    public bool IsTyping => _coroutine != null;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
    }

    public void SetText(string textString, float charTime)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        _currentTextString = textString;

        if (charTime > 0)
        {
            Text.text = "";
        }
        else
        {
            Text.text = textString;
            return;
        }

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
        Text.text = _currentTextString;
    }

    private IEnumerator Typewrite(float charTime)
    {
        foreach (var c in _currentTextString)
        {
            Text.text += c;
            yield return new WaitForSecondsRealtime(charTime);
        }
        _coroutine = null;
    }
}
