using System.Linq;
using UnityEngine;

public class SkillCooldownVisualizer : BarAmountVisualizer
{
    [SerializeField]
    private int _skillIndex;
    private CooldownCounter _cooldownCounter;

    protected override void Start()
    {
        base.Start();

        var skilledActor = this.GetRequiredComponent<SkilledActor>();
        var canvasProvider = this.GetRequiredComponent<WorldCanvasProvider>();

        var skill = skilledActor.Skills.Skip(_skillIndex).FirstOrDefault();
        skill ??= skilledActor.Skills.Last();

        _cooldownCounter = skill.CooldownCounter;

        canvasProvider.CanvasController.SkillsCDChildrenSorter.SetChild(BarController.transform, 1);
    }

    private void Update()
    {
        if (_cooldownCounter != null && _cooldownCounter.Cooldown >= _cooldownCounter.TimeSinceReset)
        {
            VisualizeAmount(_cooldownCounter.TimeSinceReset, _cooldownCounter.TimeSinceReset, _cooldownCounter.Cooldown / _cooldownCounter.CooldownDivider);
        }
    }
}
