using UnityEngine;

public abstract class TabletListApplicationDisplay<T> : MonoBehaviour
{
    public abstract void SetModel(T model);
}
