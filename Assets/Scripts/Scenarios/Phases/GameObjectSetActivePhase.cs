using UnityEngine;

public class GameObjectSetActivePhase : ActPhase
{
    [field: SerializeField]
    public GameObject GameObject { get; private set; }
    [SerializeField]
    private bool _setActive;

    public override void Invoke()
    {
        if (GameObject != null) GameObject.SetActive(_setActive);
        InvokeFinished();
    }

    public override string IconName => "Switch.png";
    public override Color IconColor => _setActive ? MyColors.Green : MyColors.Red;
}