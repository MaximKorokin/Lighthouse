using UnityEngine.UI;

public class MainGraphicRaycaster : MonoBehaviorSingleton<MainGraphicRaycaster>
{
    private GraphicRaycaster _raycaster;

    protected override void Awake()
    {
        base.Awake();
        _raycaster = this.GetRequiredComponent<GraphicRaycaster>();
    }

    public void Set2DBlocking(bool value)
    {
        _raycaster.blockingObjects = value ? GraphicRaycaster.BlockingObjects.None : GraphicRaycaster.BlockingObjects.TwoD;
    }
}
