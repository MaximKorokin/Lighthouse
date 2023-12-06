using System;
using System.Collections;
using UnityEngine;

class Timer
{
    private readonly MonoBehaviour _behaviour;
    private float _startTime;
    private float _currentTime;
    private Coroutine _coroutine;

    public event Action<float, float> Ticked;
    public event Action Finished;

    public bool Started { get; private set; }

    public Timer(MonoBehaviour behaviour)
    {
        _behaviour = behaviour;
    }

    public void Start(float startTime)
    {
        Started = true;
        _startTime = startTime;
        _currentTime = startTime;
        _coroutine = _behaviour.StartCoroutine(TimerCoroutine());
    }

    public void Stop()
    {
        Started = false;
        if (_coroutine != null)
        {
            _behaviour.StopCoroutine(_coroutine);
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (_currentTime > 0)
        {
            yield return new WaitForEndOfFrame();
            _currentTime -= Time.deltaTime;
            Ticked?.Invoke(_currentTime, _startTime);
        }
        Started = false;
        Finished?.Invoke();
    }
}
