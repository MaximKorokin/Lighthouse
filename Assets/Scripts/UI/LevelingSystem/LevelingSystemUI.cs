using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelingSystemUI : MonoBehaviour
{
    public static LevelingSystemUI Instance { get; private set; }

    [SerializeField]
    private Transform _effectsParent;
    [SerializeField]
    private BarController _expBarController;
    [SerializeField]
    private TMP_Text _levelText;

    private readonly List<EffectView> _currentViews = new();

    public event Action<Effect> EffectChosen;

    public void Initialize()
    {
        Instance = this;
    }

    public void DisplayEffects(Effect[] effects)
    {
        Game.Pause();

        _effectsParent.gameObject.SetActive(true);
        foreach (var effect in effects)
        {
            var view = EffectViewPool.Take(effect);
            _currentViews.Add(view);
            view.transform.SetParent(_effectsParent, false);
            view.Clicked -= OnViewClicked;
            view.Clicked += OnViewClicked;
        }
    }

    public void VisualizeExperienceAmount(float current, float max)
    {
        _expBarController.SetFillRatio(current / max);
    }

    public void VisualizeLevel(int level)
    {
        _levelText.text = level.ToString();
    }

    private void OnViewClicked(EffectView view, Effect effect)
    {
        Game.Resume();

        _effectsParent.gameObject.SetActive(false);
        _currentViews.ForEach(EffectViewPool.Return);
        _currentViews.Clear();
        EffectChosen?.Invoke(effect);
    }
}
