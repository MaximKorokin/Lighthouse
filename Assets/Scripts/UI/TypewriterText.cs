using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterText : MonoBehaviour
{
    private readonly char[] _charsToShowImmediately = new char[] { ' ', '\n' };

    private string _currentTextString;
    private Coroutine _coroutine;

    public TMP_Text Text { get; private set; }
    public bool IsTyping => _coroutine != null;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
    }

    public float SetText(string textString, TypingSpeed typingSpeed)
    {
        if (!gameObject.activeInHierarchy)
        {
            return 0;
        }

        _currentTextString = textString;

        var charTime = typingSpeed.ToFloatValue();
        if (charTime > 0)
        {
            Text.text = "";
        }
        else
        {
            Text.text = textString;
            return 0;
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(Typewrite(charTime));

        return (textString.Length - textString.Count(x => ShouldShowImmediately(x))) * charTime;
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

            if (!ShouldShowImmediately(c))
            {
                yield return new WaitForSecondsRealtime(charTime);
            }
        }
        _coroutine = null;
    }

    private bool ShouldShowImmediately(char c) => Array.IndexOf(_charsToShowImmediately, c) != -1;
}

public enum TypingSpeed
{
    Normal = 0,
    Slow = 1,
    Fast = 2,
    Instant = 9,
}
