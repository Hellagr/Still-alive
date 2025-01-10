using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sphereAround = 2f;
    [SerializeField] Countdown countdown;
    float chekingExpirienceAroundTime = 0.1f;
    LayerMask layerMaskExperience;

    void Start()
    {
        layerMaskExperience = LayerMask.GetMask("Experience");
        StartCoroutine(GrabExpirience());
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereAround);
    }
}
