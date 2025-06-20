using System.Collections.Generic;
using UnityEngine;

public class MainAudioSourceController : MonoBehaviorSingleton<MainAudioSourceController>
{
    private const double UnsetAudioClipEndTime = -1;

    private AudioSourceProvider _audioSourceProvider1;
    private AudioSourceProvider AudioSourceProvider1 => this.LazyInitialize(ref _audioSourceProvider1, () => AudioSourceProviderPool.Take(new(false)));
    private AudioSourceProvider _audioSourceProvider2;
    private AudioSourceProvider AudioSourceProvider2 => this.LazyInitialize(ref _audioSourceProvider2, () => AudioSourceProviderPool.Take(new(false)));

    private bool _areSourcesSwitched;
    private int _currentAudioClipItemIndex = 0;

    private readonly List<AudioClipItem> _audioClipsQueue = new();

    public AudioSourceProvider ActiveAudioSourceProvider => _areSourcesSwitched ? AudioSourceProvider2 : AudioSourceProvider1;
    public AudioSourceProvider InactiveAudioSourceProvider => _areSourcesSwitched ? AudioSourceProvider1 : AudioSourceProvider2;

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
    public void SetAudioClip(AudioClip audioClip, AudioClipType type, bool loop, AudioClipOperation operation)
    {
        var item = new AudioClipItem(audioClip, type, loop, UnsetAudioClipEndTime);
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
        if (CurrentAudioClipItem.EndTime == UnsetAudioClipEndTime)
        {
            // Adding a time offset to prevent initial lag
            SchedulePlay(ActiveAudioSourceProvider, CurrentAudioClipItem, DspTime + 0.25);
        }
        // Cycle sequence
        else if(CurrentAudioClipItem.EndTime < DspTime)
        {
            // Update end time if clip is last in queue and is looped
            if (NextAudioClipItem == null)
            {
                if (ActiveAudioSourceProvider.AudioSource.loop && CurrentAudioClipItem.Clip != null)
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
        if (NextAudioClipItem != null && NextAudioClipItem.EndTime == UnsetAudioClipEndTime && DspTime >= CurrentAudioClipItem.EndTime - 1)
        {
            SchedulePlay(InactiveAudioSourceProvider, NextAudioClipItem, CurrentAudioClipItem.EndTime);
        }
    }

    private void SchedulePlay(AudioSourceProvider provider, AudioClipItem item, double time)
    {
        provider.AudioSource.clip = item.Clip;
        if (item.Clip != null) provider.AudioSource.PlayScheduled(time);
        item.EndTime = time + (item.Clip != null ? item.Clip.length : 0);
        provider.SetAudioClipType(item.Type);
    }

    private void OnDestroy()
    {
        if (_audioSourceProvider1 != null)
        {
            AudioSourceProviderPool.Return(_audioSourceProvider1);
        }
        if (_audioSourceProvider2 != null)
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
