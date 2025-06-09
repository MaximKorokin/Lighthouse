using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManualInteractionController : TriggerController, IPointerDownHandler, IPointerUpHandler
{
    private int _initialLayer;
    private bool _isPointerDown;

    public event Action Interacted;

    protected override void Awake()
    {
        base.Awake();

        _initialLayer = gameObject.layer;
    }

    protected override void Control()
    {
        if (TriggeredWorldObjects.Any() && _isPointerDown)
        {
            InvokeActors(new PrioritizedTargets(TriggeredWorldObjects));
            Interacted?.Invoke();
        }
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        base.Trigger(worldObject, entered);

        if (!entered && !TriggeredWorldObjects.Any())
            gameObject.layer = _initialLayer;
        else
            gameObject.layer = LayerMask.NameToLayer(Constants.RaycastTarget2DLayerName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }
}
