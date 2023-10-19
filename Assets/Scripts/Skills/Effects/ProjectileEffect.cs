using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileEffect", menuName = "ScriptableObjects/Effects/ProjectileEffect", order = 1)]
public class ProjectileEffect : ComplexEffect
{
    [field: SerializeField]
    public ProjectileManipulator Projectile { get; private set; }
    [field: SerializeField]
    public int Amount { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }
    [field: SerializeField]
    public float Spread { get; private set; }

    private bool _isInvoked = false;

    public override void Invoke(WorldObject source, WorldObject target)
    {
        if (_isInvoked)
        {
            base.Invoke(source, target);
        }
        else
        {
            _isInvoked = true;
            var spreadStep = Spread / (Amount - 1);
            var currentYaw = -Spread / 2;
            for (int i = 0; i < Amount; i++)
            {
                currentYaw += spreadStep;
                CreateProjectile(source.transform.position, target, currentYaw);
            }
        }
    }

    private void CreateProjectile(Vector2 position, WorldObject target, float yaw)
    {
        Projectile.gameObject.SetActive(false);

        var projectile = Instantiate(Projectile, position, Quaternion.identity);
        projectile.SetDestination(target, yaw);
        projectile.gameObject.SetActive(true);

        Projectile.gameObject.SetActive(true);
    }
}
