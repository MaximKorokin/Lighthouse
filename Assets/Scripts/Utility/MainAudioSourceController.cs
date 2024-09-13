using System.Collections.Generic;
using UnityEngine;

public class MainAudioSourceController : MonoBehaviorSingleton<MainAudioSourceController>
{
    [SerializeField]
    private AudioSource _audioSource1;
    [SerializeField]
    private AudioSource _audioSource2;

    private bool _areSourcesSwitched;
    private int _currentAudioClipItemIndex = 0;

    private readonly List<AudioClipItem> _audioClipsQueue = new();

    private AudioSource ActiveAudioSource => _areSourcesSwitched ? _audioSource2 : _audioSource1;
    private AudioSource InactiveAudioSource => _areSourcesSwitched ? _audioSource1 : _audioSource2;

    private AudioClipItem CurrentAudioClipItem => _audioClipsQueue.Count > _currentAudioClipItemIndex ? _audioClipsQueue[_currentAudioClipItemIndex] : null;
    private AudioClipItem NextAudioClipItem => _audioClipsQueue.Count > (_currentAudioClipItemIndex + 1) ? _audioClipsQueue[_currentAudioClipItemIndex + 1] : null;
    private AudioClipItem LastAudioClipItem => _audioClipsQueue.Count > 0 && _audioClipsQueue.Count > _currentAudioClipItemIndex ? _audioClipsQueue[^1] : null;

    private double DspTime => AudioSettings.dspTime;

    private void Update()
    {
        CycleQueue();
    }

    /// <summary>
    /// Clip will loop only if it is last in queue.
    /// Assign operations clear queue.
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="loop"></param>
    /// <param name="operation"></param>
    public void SetAudioClip(AudioClip audioClip, bool loop, AudioClipOperation operation)
    {
        switch (operation)
        {
            case AudioClipOperation.Assign:
                AssignAudioClip(audioClip, loop);
                break;
            case AudioClipOperation.AssignFadeOut:
                AssignAudioClipFadeOut(audioClip, loop);
                break;
            case AudioClipOperation.AssignFadeIn:
                AssignAudioClipFadeIn(audioClip, loop);
                break;
            case AudioClipOperation.AssignFadeOutFadeIn:
                AssignAudioClipFadeOutFadeIn(audioClip, loop);
                break;
            case AudioClipOperation.Enqueue:
                Enqueue(audioClip, loop);
                break;
            default:
                Logger.Error($"Invalid {nameof(AudioClipOperation)} value");
                break;
        }
    }

    private void AssignAudioClip(AudioClip audioClip, bool loop)
    {
        _audioClipsQueue.Clear();
        _currentAudioClipItemIndex = 0;

        _audioClipsQueue.Add(new(audioClip, loop, -1));
    }

    private void AssignAudioClipFadeOut(AudioClip audioClip, bool loop)
    {
        var initialVolume = ActiveAudioSource.volume;
        this.StartCoroutineSafe(ActiveAudioSource.FadeOut(2.5f), () =>
        {
            ActiveAudioSource.volume = initialVolume;
            AssignAudioClip(audioClip, loop);
        });
    }

    private void AssignAudioClipFadeIn(AudioClip audioClip, bool loop)
    {
        AssignAudioClip(audioClip, loop);
        this.StartCoroutineSafe(ActiveAudioSource.FadeIn(ActiveAudioSource.volume, 2.5f));
    }

    private void AssignAudioClipFadeOutFadeIn(AudioClip audioClip, bool loop)
    {
        var initialVolume = ActiveAudioSource.volume;
        this.StartCoroutineSafe(ActiveAudioSource.FadeOut(2.5f), () =>
        {
            ActiveAudioSource.volume = initialVolume;
            AssignAudioClipFadeIn(audioClip, loop);
        });
    }

    private void Enqueue(AudioClip audioClip, bool loop)
    {
        _audioClipsQueue.Add(new(audioClip, loop, -1));
    }

    private void CycleQueue()
    {
        // Queue is empty
        if (CurrentAudioClipItem == null)
        {
            return;
        }

        ActiveAudioSource.loop = CurrentAudioClipItem.Loop && CurrentAudioClipItem == LastAudioClipItem;

        // Start sequence
        if (CurrentAudioClipItem.EndTime == -1)
        {
            ActiveAudioSource.clip = CurrentAudioClipItem.Clip;
            ActiveAudioSource.PlayScheduled(DspTime);
            CurrentAudioClipItem.EndTime = DspTime + CurrentAudioClipItem.Clip.length;
        }
        // Cycle sequence
        else if(CurrentAudioClipItem.EndTime < DspTime)
        {
            // Update end time if clip is lat in queue and is looped
            if (NextAudioClipItem == null)
            {
                if (ActiveAudioSource.loop)
                {
                    CurrentAudioClipItem.EndTime += CurrentAudioClipItem.Clip.length;
                    return;
                }
            }
            else
            {
                // Move queue if current audio clip ended and have next clip
                _currentAudioClipItemIndex++;
                _areSourcesSwitched = !_areSourcesSwitched;
            }
        }

        // Schedule next clip if have one
        // Scheduling when it is 1 second left because of buffer size (I guess)
        if (NextAudioClipItem != null && NextAudioClipItem.EndTime == -1 && CurrentAudioClipItem.EndTime <= DspTime + 1)
        {
            InactiveAudioSource.clip = NextAudioClipItem.Clip;
            InactiveAudioSource.PlayScheduled(CurrentAudioClipItem.EndTime);
            NextAudioClipItem.EndTime = CurrentAudioClipItem.EndTime + NextAudioClipItem.Clip.length;
        }
    }

    private class AudioClipItem
    {
        public AudioClip Clip;
        public bool Loop;
        public double EndTime;

        public AudioClipItem(AudioClip audioClip, bool loop, double endTime)
        {
            Clip = audioClip;
            Loop = loop;
            EndTime = endTime;
        }
    }
}

public enum AudioClipOperation
{
    Assign = 1,
    AssignFadeOut = 2,
    AssignFadeIn = 5,
    AssignFadeOutFadeIn = 8,
    Enqueue = 11
}
