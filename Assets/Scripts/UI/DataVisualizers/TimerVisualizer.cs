using UnityEngine;

class TimerVisualizer : BarAmountVisualizer
{
    private Timer _timer;

    protected override void Start()
    {
        base.Start();
        var canvasProvider = this.GetRequiredComponent<WorldCanvasProvider>();
        BarController.transform.SetParent(canvasProvider.CanvasController.Canvas.transform, false);
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
