using UnityEngine;

public class OptionalMonoBehaviorSingleton<T> : MonoBehaviorSingleton<T> where T : OptionalMonoBehaviorSingleton<T>
{
    [field: SerializeField]
    public bool IsMain { get; protected set; } = false;

    protected override void Awake()
    {
        if (IsMain)
        {
            if (Instance != null)
            {
                Logger.Warn($"More than one {GetType()} with {nameof(IsMain)} flag detected");
                return;
            }
            base.Awake();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
