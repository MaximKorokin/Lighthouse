using UnityEngine;

class TimerVisualizer : AmountVisualizer
{
    [SerializeField]
    private Transform _barParent;
    private Timer _timer;

    protected override void Start()
    {
        base.Start();
        BarController.transform.SetParent(_barParent, false);
    }

    public void SetTimer(Timer timer)
    {
        if (_timer != null)
        {
            _timer.Ticked -= VisualizeAmount;
        }
        _timer = timer;
        _timer.Ticked -= VisualizeAmount;
        _timer.Ticked += VisualizeAmount;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_timer != null)
        {
            _timer.Ticked -= VisualizeAmount;
        }
    }
}
