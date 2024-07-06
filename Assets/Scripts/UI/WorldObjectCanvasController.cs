using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldObjectCanvasController : MonoBehaviour
{
    [field: SerializeField]
    public Transform UpperElementsParent { get; private set; }
    [field: SerializeField]
    public Transform LowerElementsParent { get; private set; }

    public Canvas Canvas { get; private set; }

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
    }
}
