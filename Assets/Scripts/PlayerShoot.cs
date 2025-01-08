using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform FP_Camera;
    private readonly float shootDistance = 500f;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, FP_Camera.forward, out hit, shootDistance))
        {
            var hitPoint = hit.point;
            var hitNormal = hit.normal;
            //Debug.DrawLine(transform.position, hit.point, Color.red, shootDistance);
            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.ReceiveDamage(1);
            }
        }
    }
}
