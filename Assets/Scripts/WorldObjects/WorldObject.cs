using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField]
    private int _test1;
    [SerializeField]
    private Stats _stats;
    protected Stats Stats => _stats;
    [SerializeField]
    private string _test2;
    [SerializeField]
    private int[] _test3;

    protected virtual void Awake()
    {
        Debug.Log("Awake");
        Stats.Init();
    }

    public virtual void ModifyStats(Stats otherStats)
    {
        var previousSizeScale = Stats[StatName.SizeScale];
        Stats.Modify(otherStats);
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != previousSizeScale)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
    }

    private void Update()
    {
        Stats.Modify(new Stats());
    }
}
