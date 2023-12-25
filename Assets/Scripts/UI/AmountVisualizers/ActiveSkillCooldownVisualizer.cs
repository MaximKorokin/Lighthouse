using UnityEngine;

[RequireComponent(typeof(PlayerInputActor))]
public class ActiveSkillCooldownVisualizer : BarAmountVisualizer
{
    [SerializeField]
    private Transform _barParent;
    private CooldownCounter _cooldownCounter;

    protected override void Start()
    {
        base.Start();
        var activeSkill = GetComponent<PlayerInputActor>().ActiveSkill;
        _cooldownCounter = activeSkill.CooldownCounter;
        BarController.transform.SetParent(_barParent, false);
    }

    private void Update()
    {
        if (_cooldownCounter.Cooldown >= _cooldownCounter.TimeSinceReset)
        {
            VisualizeAmount(_cooldownCounter.TimeSinceReset, _cooldownCounter.Cooldown);
        }
    }
}
