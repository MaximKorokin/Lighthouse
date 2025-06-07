using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
    private readonly char[] _charsToSkipCharTypingEvent = new char[] { ' ', '\n', ',', '.', '\'', '\"', '-' };
    private static readonly Dictionary<char, float> _charsShowMultipliers = new()
    {
        { '\n', 0 },
        { '\'', 0 },
        { '\"', 0 },
        { ' ', 2 },
        { ',', 3 },
        { '.', 3 },
        { '?', 3 },
        { '!', 3 },
        { '-', 0 },
    };

    private string _currentTextString;
    private Coroutine _coroutine;

    public TMP_Text Text { get; private set; }
    public bool IsTyping => _coroutine != null;

    public event Action CharTyping;

    private void Awake()
    {
        Text = this.GetRequiredComponent<TMP_Text>();
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

        return textString.Sum(c => _charsShowMultipliers.GetOrDefault(c, 1)) * charTime;
    }

    private IEnumerator Typewrite(float charTime)
    {
        for (int i = 0; i < _currentTextString.Length; i++)
        {
            var c = _currentTextString[i];

            if (!_charsToSkipCharTypingEvent.Contains(c))
            {
                CharTyping?.Invoke();
            }
            var currentCharTime = charTime * _charsShowMultipliers.GetOrDefault(c, 1);
            if (currentCharTime > 0)
            {
                yield return new WaitForSecondsRealtime(currentCharTime);
            }

            Text.text += c;
        }
        _coroutine = null;
    }
}

public enum TypingSpeed
{
    Normal = 0,
    Slow = 1,
    Fast = 2,
    SuperSlow = 3,
    Instant = 9,
}
