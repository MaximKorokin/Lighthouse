using UnityEngine;

public class GlobalPositionFixer : MonoBehaviour
{
    [SerializeField]
    private float _fixedGlobalZPosition;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _fixedGlobalZPosition);
    }
}
