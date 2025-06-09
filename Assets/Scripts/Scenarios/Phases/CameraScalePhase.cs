using UnityEngine;

class CameraScalePhase : ActPhase
{
    [SerializeField]
    private PixelPerfectCameraReferenceResolution _resolution;

    public override void Invoke()
    {
        MainCameraController.SetReferenceResolution(_resolution);
        InvokeFinished();
    }

    public override string IconName => "CameraScale.png";
}