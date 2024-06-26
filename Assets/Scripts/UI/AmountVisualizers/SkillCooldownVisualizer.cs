using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SkilledActor))]
public class SkillCooldownVisualizer : BarAmountVisualizer
{
    [SerializeField]
    private int _skillIndex;
    [SerializeField]
    private Transform _barParent;
    private CooldownCounter _cooldownCounter;

    protected override void Start()
    {
        base.Start();
        var skill = GetComponent<SkilledActor>().Skills.Skip(_skillIndex).FirstOrDefault();
        if (skill == null)
        {
            return;
        }
        _cooldownCounter = skill.CooldownCounter;
        BarController.transform.SetParent(_barParent, false);
    }

    private void Update()
    {
        if (_cooldownCounter != null && _cooldownCounter.Cooldown >= _cooldownCounter.TimeSinceReset)
        {
            VisualizeAmount(_cooldownCounter.TimeSinceReset, _cooldownCounter.TimeSinceReset, _cooldownCounter.Cooldown);
        }
    }
}
