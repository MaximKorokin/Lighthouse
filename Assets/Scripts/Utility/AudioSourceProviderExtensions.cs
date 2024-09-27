using System;
using System.Collections;
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
        provider.AudioSource.volume = 0;
        while (provider.AudioSource.volume < (targetVolume = provider.GetTargetVolume()))
        {
            provider.AudioSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }
    }

    public static void PlayAudioClip(this AudioSourceProvider provider, AudioClip clip, bool loop, AudioClipType type, Action finalAction)
    {
        provider.SetAudioClipType(type);
        provider.AudioSource.loop = loop;
        provider.AudioSource.clip = clip;
        provider.AudioSource.Play();

        provider.StartCoroutineSafe(CoroutinesUtils.WaitForSeconds(clip.length), finalAction);
    }
}
