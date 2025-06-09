using UnityEngine;

public class GraphicRaycasterPhase : ActPhase
{
    [SerializeField]
    private bool _block2D;

    public override void Invoke()
    {
        MainGraphicRaycaster.Instance.Set2DBlocking(_block2D);
        InvokeFinished();
    }
}
