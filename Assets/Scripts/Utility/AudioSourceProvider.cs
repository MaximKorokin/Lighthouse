using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceProvider : MonoBehaviour
{
    public static AudioSourceProvider Main { get; private set; }

    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        if (CompareTag(Constants.MainAudioSourceTag))
        {
            if (Main == null)
            {
                Main = this;
            }
            else
            {
                Logger.Error($"There is more than one {nameof(AudioSourceProvider)} tagged as main.");
            }
        }

        AudioSource = GetComponent<AudioSource>();
    }
}
