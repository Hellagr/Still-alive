using UnityEngine;

public class ProjectileRangeCreep : MonoBehaviour
{
    [SerializeField] float speedProjectileMultiplier = 10f;

    void OnEnable()
    {
        Invoke(nameof(ReleaseProjectile), 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedProjectileMultiplier * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground")) && gameObject.activeSelf)
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

    private void OnDisable()
    {
        CancelInvoke(nameof(ReleaseProjectile));
    }
}
