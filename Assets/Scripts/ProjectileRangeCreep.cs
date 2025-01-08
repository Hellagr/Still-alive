using UnityEngine;

public class ProjectileRangeCreep : MonoBehaviour
{
    [SerializeField] float speedProjectileMultiplier = 10f;

    void OnEnable()
    {
        Invoke("ReleaseProjectile", 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedProjectileMultiplier * Time.deltaTime);
    }

    //Make it work, doesn't work with terrain!!!
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ProjectilePool.Instance.rangeCreepProjectilePool.Release(this);
        }
    }

    void ReleaseProjectile()
    {
        if (gameObject.activeSelf)
        {
            ProjectilePool.Instance.rangeCreepProjectilePool.Release(this);
        }
    }
}
