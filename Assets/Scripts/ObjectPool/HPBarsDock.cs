using UnityEngine;

class HPBarsDock : ObjectPool<BarController, DestroyableWorldObject>
{
    [SerializeField]
    private Transform _hpBarsParent;

    protected override void Initialize(BarController barController, DestroyableWorldObject param)
    {
        barController.transform.SetParent(transform, false);
        barController.gameObject.SetActive(true);
    }

    protected override void Deinitialize(BarController barController)
    {

    }
}
