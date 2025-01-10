using UnityEngine;

public class ParticleExperience : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    public int amountOfExpirince { get; private set; }

    public void SetAmountOfExpirience(int amountOfExp)
    {
        amountOfExpirince = amountOfExp;
    }

    public bool isMovingToPlayer { get; private set; }

    public void IsMovingToPlayer()
    {
        isMovingToPlayer = true;
    }

    void OnEnable()
    {
        isMovingToPlayer = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.activeSelf)
        {
            ParticlePool.Instance.particleExperiencePool.Release(this);
            GameManager.Instance.GotExperience(amountOfExpirince);
        }
    }

    void Update()
    {
        if (isMovingToPlayer)
        {
            transform.Translate((GameManager.Instance.PlayerPosition.position - transform.position) * Time.deltaTime * speed);
        }
    }
}

