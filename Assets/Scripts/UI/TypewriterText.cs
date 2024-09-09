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

    public event Action CharTyping;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Returns estimated time needed to completely show the provided text
    /// </summary>
    /// <param name="textString"></param>
    /// <param name="typingSpeed"></param>
    /// <returns></returns>
    public float SetText(string textString, TypingSpeed typingSpeed)
    {
        if (!gameObject.activeInHierarchy)
        {
            return 0;
        }

        _currentTextString = textString;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

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

        _coroutine = StartCoroutine(Typewrite(charTime));

        return (textString.Length - textString.Count(x => ShouldShowImmediately(x))) * charTime;
    }

    private IEnumerator Typewrite(float charTime)
    {
        for (int i = 0; i < _currentTextString.Length; i++)
        {
            var c = _currentTextString[i];

            if (charTime > 0 && !ShouldShowImmediately(c))
            {
                CharTyping?.Invoke();
                yield return new WaitForSecondsRealtime(charTime);
            }

            Text.text += c;
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
    SuperSlow = 3,
    Instant = 9,
}
