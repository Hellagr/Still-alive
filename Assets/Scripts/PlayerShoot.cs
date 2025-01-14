using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform FP_Camera;
    private readonly float shootDistance = 500f;
    public int currentGunDamage { get; private set; } = 1;
    LayerMask excludeLayer;

    public void SetCurrentDamage(int addedDamage)
    {
        currentGunDamage += addedDamage;
    }

    void Awake()
    {
        excludeLayer = ~LayerMask.GetMask("Experience", "Projectile");
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, FP_Camera.forward, out hit, shootDistance, excludeLayer))
        {
            var hitPoint = hit.point;
            var hitNormal = hit.normal;
            //Debug.DrawLine(transform.position, hit.point, Color.red, shootDistance);
            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.ReceiveDamage(currentGunDamage);
            }
        }
    }
}
