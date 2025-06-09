using System;
using UnityEngine;

public abstract class TextViewer : MonoBehaviour
{
    [SerializeField]
    protected TypewriterText Typewriter;

    private readonly CooldownCounter _showCounter = new(0);
    private readonly ReadOnceValue<bool> _isViewing = new(false);

    public event Action ViewFinished;

    protected virtual void Awake()
    {
        ViewFinished += OnViewFinished;
    }

    private void Update()
    {
        if (_showCounter.IsOver())
        {
            FinishViewText();
        }
    }

    // todo: mb change text with a collection of strings to have ability to show series of them in a row
    public void ViewText(string text, float showTime, TypingSpeed typingSpeed)
    {
        FinishViewText();
        OnViewStarted();

        _isViewing.Set(true);
        _showCounter.Cooldown = float.MaxValue;
        _showCounter.Reset();

        LocalizationManager.SetLanguageChangeListener(
            Typewriter,
            text,
            t =>
            {
                var timeToShow = Typewriter.SetText(t, typingSpeed);
                _showCounter.Cooldown = showTime + timeToShow;
            });
    }

    /// <summary>
    /// Immediately finishes viewing text if not finished.
    /// </summary>
    public void FinishViewText()
    {
        if (_isViewing)
        {
            Typewriter.SetText("", TypingSpeed.Instant);
            ViewFinished?.Invoke();
        }
    }

    protected abstract void OnViewStarted();
    protected abstract void OnViewFinished();

    protected virtual void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(Typewriter);
    }
}
