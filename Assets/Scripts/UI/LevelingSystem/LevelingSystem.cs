using System;
using System.Collections.Generic;
using System.Linq;

public class LevelingSystem
{
    private readonly LevelingSystemSettings _settings;
    private readonly Dictionary<EffectPreview, Effect[]> _effects;
    private int _currentExperience;

    public int Level { get; private set; }

    public event Action<Effect[]> LevelIncreased;

    public LevelingSystem(LevelingSystemSettings settings)
    {
        _settings = settings;
        _effects = _settings.EffectsSettings.ToDictionary(x => x.Preview, x => x.GetEffects());
        VisualizeExperienceAmount();
        LevelingSystemUI.Instance.VisualizeLevel(Level);
        LevelingSystemUI.Instance.EffectChosen += OnEffectChosen;
    }

    public void AddExperience(int expValue)
    {
        var expToUp = GetExpereinceNeeded(Level, _currentExperience);
        var expDelta = expToUp - expValue;
        if (expDelta <= 0)
        {
            IncreaseLevel();
            _currentExperience = 0;
            AddExperience(-expDelta);
        }
        else
        {
            _currentExperience += expValue;
            VisualizeExperienceAmount();
        }
    }

    private int GetExpereinceNeeded(int level, int exp)
    {
        if (level >= _settings.LevelsExperience.Length)
        {
            return int.MaxValue;
        }
        return _settings.LevelsExperience[level] - exp;
    }

    private void IncreaseLevel()
    {
        Level++;
        var random = new Random();
        var randomEffects = _effects.Keys.OrderBy(x => random.Next()).Take(_settings.AlternativesAmount);
        LevelingSystemUI.Instance.VisualizeLevel(Level);
        LevelingSystemUI.Instance.DisplayEffects(randomEffects);
    }

    private void VisualizeExperienceAmount() => LevelingSystemUI.Instance.VisualizeExperienceAmount(_currentExperience, _settings.LevelsExperience[Level]);

    private void OnEffectChosen(EffectPreview effect)
    {
        LevelIncreased?.Invoke(_effects[effect]);
    }
}
