using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(TypewriterText))]
public class InformationText : MonoBehaviorSingleton<InformationText>
{
    private TypewriterText _typewriter;

    private readonly CooldownCounter _showCounter = new(0);

    public static event Action FinishedShow;

    protected override void Awake()
    {
        base.Awake();

        _typewriter = GetComponent<TypewriterText>();
    }

    private void Update()
    {
        if (_showCounter.IsOver())
        {
            _typewriter.Text.enabled = false;
            FinishedShow?.Invoke();
        }
    }

    public static void ShowText(string text, float showTime, TypingSpeed typingSpeed)
    {
        if (!Instance._showCounter.IsOver())
        {
            FinishedShow?.Invoke();
        }

        Instance._typewriter.Text.enabled = true;
        Instance._showCounter.Reset();
        LocalizationManager.SetLanguageChangeListener(
            Instance._typewriter.Text,
            text,
            t =>
            {
                var charTime = typingSpeed.ToFloatValue();
                Instance._typewriter.SetText(t, charTime);
                Instance._showCounter.Cooldown = showTime + text.Length * charTime;
            });
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_typewriter.Text);
    }
}
