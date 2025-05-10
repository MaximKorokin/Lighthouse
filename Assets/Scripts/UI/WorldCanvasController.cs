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
    [field: SerializeField]
    public Transform SkillsCDParent { get; private set; }

    private TransformChildrenSorter _hpChildrenSorter;
    public TransformChildrenSorter HPChildrenSorter => this.LazyInitialize(ref _hpChildrenSorter, () => new(HPViewParent));

    private TransformChildrenSorter _skillsCDChildrenSorter;
    public TransformChildrenSorter SkillsCDChildrenSorter => this.LazyInitialize(ref _skillsCDChildrenSorter, () => new(SkillsCDParent));

    public Canvas Canvas { get; private set; }

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
    }
}
