using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Transform playerPosition;
    [SerializeField] Slider sliderExperience;
    [SerializeField] int requredExperienceForLevelUp = 10;
    [SerializeField] int additionToNextLevel = 5;
    [SerializeField] TextMeshProUGUI currentLevel;
    public int playerLevel { get; private set; } = 0;
    int currentExperience = 0;

    public Transform PlayerPosition
    {
        get
        {
            return playerPosition;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sliderExperience.maxValue = requredExperienceForLevelUp;
    }

    public void GotExperience(int expPoints)
    {
        if (requredExperienceForLevelUp > currentExperience + expPoints)
        {
            currentExperience += expPoints;
            sliderExperience.value = currentExperience;
        }
        else
        {
            var rest = currentExperience + expPoints - requredExperienceForLevelUp;
            currentExperience = 0;
            requredExperienceForLevelUp += additionToNextLevel;
            sliderExperience.maxValue = requredExperienceForLevelUp;
            sliderExperience.value = rest;
            playerLevel++;
            currentLevel.text = playerLevel.ToString();
        }
    }
}
