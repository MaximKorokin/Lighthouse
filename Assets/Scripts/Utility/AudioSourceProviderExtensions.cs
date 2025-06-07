using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceProviderExtensions
{
    /// <summary>
    /// Continuously reduses volume until its value is equal to 0
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator FadeOut(this AudioSourceProvider provider, float duration)
    {
        var initialVolume = provider.AudioSource.volume;
        while (provider.AudioSource.volume > 0)
        {
            provider.AudioSource.volume -= initialVolume * Time.deltaTime / duration;
            yield return null;
        }
    }

    /// <summary>
    /// Continuously increases volume until its value is equal to <see href="provider.GetTargetVolume()"/>
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator FadeIn(this AudioSourceProvider provider, float duration)
    {
        float targetVolume;
        float currentVolume = 0;
        while (currentVolume < (targetVolume = provider.GetTargetVolume()))
        {
            currentVolume += targetVolume * Time.deltaTime / duration;
            provider.AudioSource.volume = currentVolume;
            yield return null;
        }
    }

    private static readonly Dictionary<AudioClip, float> _clipsStartTime = new();

    /// <summary>
    /// More preferrable way to play audio clips than directly through AudioSource
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    /// <param name="type"></param>
    /// <param name="finalAction"></param>
    public static void PlayAudioClip(this AudioSourceProvider provider, AudioClip clip, bool loop, AudioClipType type, Action finalAction = null, float interferenceBlockingTime = 0.05f)
    {
        // Prevents sounds interferencing
        if (_clipsStartTime.TryGetValue(clip, out var previousStartTime) && Time.unscaledTime - previousStartTime < interferenceBlockingTime)
        {
            finalAction?.Invoke();
            return;
        }
        _clipsStartTime[clip] = Time.unscaledTime;

        provider.SetAudioClipType(type);
        provider.AudioSource.loop = loop;
        provider.AudioSource.clip = clip;
        provider.AudioSource.Play();

        provider.StartCoroutineSafe(CoroutinesUtils.WaitForSeconds(clip.length), finalAction);
    }
}
