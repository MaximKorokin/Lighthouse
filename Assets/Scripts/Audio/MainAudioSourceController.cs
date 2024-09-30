using System.Collections.Generic;
using UnityEngine;

public class MainAudioSourceController : MonoBehaviorSingleton<MainAudioSourceController>
{
    private AudioSourceProvider _audioSourceProvider1;
    private AudioSourceProvider _audioSourceProvider2;

    private bool _areSourcesSwitched;
    private int _currentAudioClipItemIndex = 0;

    private readonly List<AudioClipItem> _audioClipsQueue = new();

    public AudioSourceProvider ActiveAudioSourceProvider => _areSourcesSwitched ? _audioSourceProvider2 : _audioSourceProvider1;
    public AudioSourceProvider InactiveAudioSourceProvider => _areSourcesSwitched ? _audioSourceProvider1 : _audioSourceProvider2;

    private AudioClipItem CurrentAudioClipItem => _audioClipsQueue.Count > _currentAudioClipItemIndex ? _audioClipsQueue[_currentAudioClipItemIndex] : null;
    private AudioClipItem NextAudioClipItem => _audioClipsQueue.Count > (_currentAudioClipItemIndex + 1) ? _audioClipsQueue[_currentAudioClipItemIndex + 1] : null;
    private AudioClipItem LastAudioClipItem => _audioClipsQueue.Count > 0 && _audioClipsQueue.Count > _currentAudioClipItemIndex ? _audioClipsQueue[^1] : null;

    private double DspTime => AudioSettings.dspTime;

    private void Start()
    {
        _audioSourceProvider1 = AudioSourceProviderPool.Take(null);
        _audioSourceProvider2 = AudioSourceProviderPool.Take(null);
    }

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
    public void SetAudioClip(AudioClip audioClip, AudioClipType type, bool loop, AudioClipOperation operation)
    {
        var item = new AudioClipItem(audioClip, type, loop, -1);
        switch (operation)
        {
            case AudioClipOperation.Assign:
                AssignAudioClip(item);
                break;
            case AudioClipOperation.AssignFadeOut:
                AssignAudioClipFadeOut(item);
                break;
            case AudioClipOperation.AssignFadeIn:
                AssignAudioClipFadeIn(item);
                break;
            case AudioClipOperation.AssignCrossFade:
                AssignAudioClipFadeOutFadeIn(item);
                break;
            case AudioClipOperation.Enqueue:
                Enqueue(item);
                break;
            default:
                Logger.Error($"Invalid {nameof(AudioClipOperation)} value");
                break;
        }
    }

    private void Enqueue(AudioClipItem audioClipItem)
    {
        _audioClipsQueue.Add(audioClipItem);
    }

    private void AssignAudioClip(AudioClipItem audioClipItem)
    {
        _audioClipsQueue.Clear();
        _currentAudioClipItemIndex = 0;

        Enqueue(audioClipItem);
    }

    private void AssignAudioClipFadeOut(AudioClipItem audioClipItem)
    {
        var initialVolume = ActiveAudioSourceProvider.AudioSource.volume;
        this.StartCoroutineSafe(ActiveAudioSourceProvider.FadeOut(2.5f), () =>
        {
            ActiveAudioSourceProvider.AudioSource.volume = initialVolume;
            AssignAudioClip(audioClipItem);
        });
    }

    private void AssignAudioClipFadeIn(AudioClipItem audioClipItem)
    {
        AssignAudioClip(audioClipItem);
        this.StartCoroutineSafe(ActiveAudioSourceProvider.FadeIn(2.5f));
    }

    private void AssignAudioClipFadeOutFadeIn(AudioClipItem audioClipItem)
    {
        var initialVolume = ActiveAudioSourceProvider.AudioSource.volume;
        this.StartCoroutineSafe(ActiveAudioSourceProvider.FadeOut(2.5f), () =>
        {
            ActiveAudioSourceProvider.AudioSource.volume = initialVolume;
            AssignAudioClipFadeIn(audioClipItem);
        });
    }

    private void CycleQueue()
    {
        // Queue is empty
        if (CurrentAudioClipItem == null)
        {
            return;
        }

        ActiveAudioSourceProvider.AudioSource.loop = CurrentAudioClipItem.Loop && CurrentAudioClipItem == LastAudioClipItem;

        // Start sequence
        if (CurrentAudioClipItem.EndTime == -1)
        {
            // Adding a time offset to prevent initial lag
            SchedulePlay(ActiveAudioSourceProvider, CurrentAudioClipItem, DspTime + 0.25);
        }
        // Cycle sequence
        else if(CurrentAudioClipItem.EndTime < DspTime)
        {
            // Update end time if clip is lat in queue and is looped
            if (NextAudioClipItem == null)
            {
                if (ActiveAudioSourceProvider.AudioSource.loop)
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
        if (NextAudioClipItem != null && NextAudioClipItem.EndTime == -1 && DspTime >= CurrentAudioClipItem.EndTime - 1)
        {
            SchedulePlay(InactiveAudioSourceProvider, NextAudioClipItem, CurrentAudioClipItem.EndTime);
        }
    }

    private void SchedulePlay(AudioSourceProvider provider, AudioClipItem item, double time)
    {
        provider.AudioSource.clip = item.Clip;
        provider.AudioSource.PlayScheduled(time);
        item.EndTime = time + item.Clip.length;
        provider.SetAudioClipType(item.Type);
    }

    private void OnDestroy()
    {
        if (_audioSourceProvider1 != null && _audioSourceProvider1.gameObject.activeInHierarchy)
        {
            AudioSourceProviderPool.Return(_audioSourceProvider1);
        }
        if (_audioSourceProvider2 != null && _audioSourceProvider2.gameObject.activeInHierarchy)
        {
            AudioSourceProviderPool.Return(_audioSourceProvider2);
        }
    }

    private class AudioClipItem
    {
        public AudioClip Clip;
        public AudioClipType Type;
        public bool Loop;
        public double EndTime;

        public AudioClipItem(AudioClip audioClip, AudioClipType type, bool loop, double endTime)
        {
            Clip = audioClip;
            Type = type;
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
    AssignCrossFade = 8,
    Enqueue = 11
}
