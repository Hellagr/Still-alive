using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private readonly float shootDistance = 500f;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, shootDistance))
        {
            var hitPoint = hit.point;
            var hitNormal = hit.normal;
        }
    }
}
