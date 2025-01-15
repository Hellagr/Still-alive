using TMPro;
using UnityEngine;

public class DebugStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] Player player;
    [SerializeField] PlayerShoot playerShoot;
    [SerializeField] PlayerMovement playerMovement;

    void Awake()
    {
        m_TextMeshProUGUI.text =
            "Stats:\n" +
            $"Attack {playerShoot.currentGunDamage}\n" +
            $"M.Speed {playerMovement.GetSpeed()}\n" +
            $"T.Health {player.ganeralHealthPoints}\n" +
            $"Dash 2";
    }

    public void DebugStatsUpdate()
    {
        m_TextMeshProUGUI.text =
            "Stats:\n" +
            $"Attack {playerShoot.currentGunDamage}\n" +
            $"M.Speed {playerMovement.GetSpeed()}\n" +
            $"T.Health {player.ganeralHealthPoints}\n" +
            $"Dash {playerMovement.currentAmountOfDashes}";
    }
}
