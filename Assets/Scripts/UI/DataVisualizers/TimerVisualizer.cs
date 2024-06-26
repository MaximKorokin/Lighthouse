using UnityEngine;

class TimerVisualizer : BarAmountVisualizer
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
            _timer.Ticked -= OnTimerTicked;
        }
        _timer = timer;
        _timer.Ticked -= OnTimerTicked;
        _timer.Ticked += OnTimerTicked;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_timer != null)
        {
            _timer.Ticked -= OnTimerTicked;
        }
    }

    private void OnTimerTicked(float cur, float max)
    {
        VisualizeAmount(0, cur, max);
    }
}
