using System.Linq;
using UnityEngine.EventSystems;

public class ManualInteractionController : TriggerController, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPointerDown;

    protected override void Control()
    {
        if (TriggeredWorldObjects.Any() && _isPointerDown)
        {
            InvokeActors(new PrioritizedTargets(TriggeredWorldObjects));
        }
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
