using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelBetweenPlayerLevels : MonoBehaviour
{
    [SerializeField] Button button1, button2, button3;
    [SerializeField] Player player;
    [SerializeField] PlayerShoot playerShoot;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI firstCard, secondCard, thirdCard;
    [SerializeField] List<Buffs> listOfBuffs;
    System.Random random = new System.Random();

    //DebugStats
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;

    void OnEnable()
    {
        listOfBuffs = listOfBuffs.OrderBy(x => random.Next()).ToList();

        if (listOfBuffs.Count < 3)
        {
            Debug.LogError("List of buffs less than 3 elements");
        }

        firstCard.text = GetDescription(listOfBuffs[0].typeOfBuff, listOfBuffs[0].amountOfBuff);
        secondCard.text = GetDescription(listOfBuffs[1].typeOfBuff, listOfBuffs[1].amountOfBuff);
        thirdCard.text = GetDescription(listOfBuffs[2].typeOfBuff, listOfBuffs[2].amountOfBuff);
    }

    void Start()
    {
        button1.onClick.AddListener(() => ApplyBuff(listOfBuffs[0]));
        button2.onClick.AddListener(() => ApplyBuff(listOfBuffs[1]));
        button3.onClick.AddListener(() => ApplyBuff(listOfBuffs[2]));


    }

    void ApplyBuff(Buffs buff)
    {
        switch (buff.typeOfBuff)
        {
            case TypeOfBuffs.Damage:
                playerShoot.SetCurrentDamage(buff.amountOfBuff);
                break;
            case TypeOfBuffs.Health:
                player.SetGeneralHealth(buff.amountOfBuff);
                break;
            case TypeOfBuffs.Speed:
                playerMovement.AddSpeed(buff.amountOfBuff);
                break;
            default:
                Debug.LogError($"Can't handle that type of buff: {buff.typeOfBuff}");
                break;
        }

        //Debug line
        m_TextMeshProUGUI.text =
            "Stats:\n" +
            $"Attack {playerShoot.currentGunDamage}\n" +
            $"M.Speed {playerMovement.GetSpeed()}\n" +
            $"T.Health {player.ganeralHealthPoints}\n" +
            $"Dash {playerMovement.currentAmountOfDashes}";

        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        button1.onClick.RemoveListener(() => ApplyBuff(listOfBuffs[0]));
        button2.onClick.RemoveListener(() => ApplyBuff(listOfBuffs[1]));
        button3.onClick.RemoveListener(() => ApplyBuff(listOfBuffs[2]));
    }

    public string GetDescription(TypeOfBuffs type, int amountOfBuff)
    {
        string nameOfBuff = type.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("Add ").Append(nameOfBuff).Append(" + ").Append(amountOfBuff);
        return sb.ToString();
    }
}
