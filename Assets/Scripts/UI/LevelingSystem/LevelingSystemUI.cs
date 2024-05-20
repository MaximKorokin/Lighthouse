using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelingSystemUI : MonoBehaviorSingleton<LevelingSystemUI>
{
    [SerializeField]
    private Transform _effectsParent;
    [SerializeField]
    private BarController _expBarController;
    [SerializeField]
    private TMP_Text _levelText;

    private readonly List<EffectView> _currentViews = new();

    public event Action<EffectPreview> EffectChosen;

    public void DisplayEffects(IEnumerable<EffectPreview> effectPreviews)
    {
        GameManager.Pause();

        _effectsParent.gameObject.SetActive(true);
        foreach (var effectPreview in effectPreviews)
        {
            var view = EffectViewPool.Take(effectPreview);
            _currentViews.Add(view);
            view.transform.SetParent(_effectsParent, false);
            view.Clicked -= OnViewClicked;
            view.Clicked += OnViewClicked;
        }
    }

    public void VisualizeExperienceAmount(float current, float max)
    {
        if (_expBarController != null)
        {
            _expBarController.SetFillRatio(current / max);
        }
    }

    public void VisualizeLevel(int level)
    {
        if (_levelText != null)
        {
            _levelText.text = level.ToString();
        }
    }

    private void OnViewClicked(EffectView view, EffectPreview effectPreview)
    {
        GameManager.Resume();

        _effectsParent.gameObject.SetActive(false);
        _currentViews.ForEach(EffectViewPool.Return);
        _currentViews.Clear();
        EffectChosen?.Invoke(effectPreview);
    }
}
