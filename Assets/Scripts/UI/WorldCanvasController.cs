using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldCanvasController : MonoBehaviour
{
    [field: SerializeField]
    public Transform SpeechBubbleParent { get; private set; }
    [field: SerializeField]
    public Transform HPChangeParent { get; private set; }
    [field: SerializeField]
    public Transform HPViewParent { get; private set; }

    public Canvas Canvas { get; private set; }

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
    }
}
