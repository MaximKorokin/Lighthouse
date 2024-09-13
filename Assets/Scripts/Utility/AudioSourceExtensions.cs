using System.Collections;
using UnityEngine;

public static class AudioSourceExtensions
{
    /// <summary>
    /// Continuously reduses volume until its value is equal to 0
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator FadeOut(this AudioSource audioSource, float duration)
    {
        var initialVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= initialVolume * Time.deltaTime / duration;
            yield return null;
        }
    }

    /// <summary>
    /// Continuously increases volume until its value is equal to <paramref name="targetVolume"/>
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator FadeIn(this AudioSource audioSource, float targetVolume, float duration)
    {
        audioSource.volume = 0;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }
    }
}