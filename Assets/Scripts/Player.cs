using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sphereAround = 2f;
    [SerializeField] Countdown countdown;
    [SerializeField] TextMeshProUGUI HP_visulizer;
    public int ganeralHealthPoints { get; private set; } = 100;
    public int currentHeathPoints { get; private set; }
    float chekingExpirienceAroundTime = 0.1f;
    LayerMask layerMaskExperience;

    void Start()
    {
        layerMaskExperience = LayerMask.GetMask("Experience");
        StartCoroutine(GrabExpirience());
    }

    void OnEnable()
    {
        currentHeathPoints = ganeralHealthPoints;
        SetHP();
    }

    IEnumerator GrabExpirience()
    {
        Collider[] sphereAroundPlayer;
        while (countdown.currentRoundTime > 0)
        {
            sphereAroundPlayer = Physics.OverlapSphere(transform.position, sphereAround, layerMaskExperience);
            if (sphereAroundPlayer.Length > 0)
            {
                foreach (Collider collider in sphereAroundPlayer)
                {
                    collider.gameObject.GetComponent<ParticleExperience>().IsMovingToPlayer();
                }
            }
            yield return new WaitForSeconds(chekingExpirienceAroundTime);
        };
    }

    void TakeDamage(int damagePoints)
    {
        if (currentHeathPoints - damagePoints <= 0)
        {
            Die();
        }
        else
        {
            currentHeathPoints -= damagePoints;
            SetHP();
        }
    }

    private void SetHP()
    {
        HP_visulizer.text = currentHeathPoints.ToString();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereAround);
    }
}
