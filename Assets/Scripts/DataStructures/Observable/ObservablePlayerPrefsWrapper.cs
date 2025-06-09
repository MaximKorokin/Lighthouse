using UnityEngine;

public class ObservablePlayerPrefsWrapper<T> : ObservableKeyValueStoreWrapper<T, string>
{
    protected override void OnSet(T key, string value)
    {
        PlayerPrefs.SetString(key.ToString(), value);
    }

    protected override string OnGet(T key)
    {
        return PlayerPrefs.GetString(key.ToString());
    }

    protected override bool HasKey(T key)
    {
        return PlayerPrefs.HasKey(key.ToString());
    }
}