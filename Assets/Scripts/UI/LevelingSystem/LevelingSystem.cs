using System;
using System.Linq;

public class LevelingSystem
{
    private readonly LevelingSystemSettings _settings;

    private int _currentExperience;

    public int Level { get; private set; }

    public event Action<Effect> LevelIncreased;

    public LevelingSystem(LevelingSystemSettings settings)
    {
        _settings = settings;
        VisualizeExperienceAmount();
        LevelingSystemUI.Instance.VisualizeLevel(Level);
        LevelingSystemUI.Instance.EffectChosen += OnEffectChosen;
    }

    public void AddExperience(float expValue)
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
            _currentExperience += (int)expValue;
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
        var randomEffects = _settings.Effects.OrderBy(x => random.Next()).Take(_settings.AlternativesAmount).ToArray();
        LevelingSystemUI.Instance.VisualizeLevel(Level);
        LevelingSystemUI.Instance.DisplayEffects(randomEffects);
    }

    private void VisualizeExperienceAmount() => LevelingSystemUI.Instance.VisualizeExperienceAmount(_currentExperience, _settings.LevelsExperience[Level]);

    private void OnEffectChosen(Effect effect)
    {
        LevelIncreased?.Invoke(effect);
    }
}
