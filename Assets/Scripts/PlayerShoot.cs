using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform lookFromPoint;
    private readonly float shootDistance = 500f;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, lookFromPoint.forward, out hit, shootDistance))
        {
            var hitPoint = hit.point;
            var hitNormal = hit.normal;
            Debug.DrawLine(transform.position, hit.point, Color.red, shootDistance);
            if(hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.HealthPoint -= 1;
            }
        }
    }
}
