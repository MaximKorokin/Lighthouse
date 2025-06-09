using UnityEngine;

class DockedHPBarVisualizer : HPBarVisualizer
{
    [SerializeField]
    private float _onDestroyingShowTime;

    protected override void Start()
    {
        base.Start();
        BarController.transform.SetParent(HPBarsDock.Instance.transform, false);

        WorldObject.OnDestroying(() => 
            WorldObject.StartCoroutineSafe(
                CoroutinesUtils.WaitForSeconds(_onDestroyingShowTime),
                () => BarController.gameObject.SetActive(false)));
    }
}
