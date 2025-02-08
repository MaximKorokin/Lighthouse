using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class TabletListApplicationPreview<T> : MonoBehaviour
{
    protected T Model { get; private set; }

    public event Action<T> Clicked;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public virtual void SetModel(T model)
    {
        Model = model;
    }

    protected virtual void OnClick()
    {
        Clicked?.Invoke(Model);
    }
}
