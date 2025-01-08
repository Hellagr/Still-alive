using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float roundDuration = 60f;
    public float currentRoundTime { get; private set; } = 0;
    TextMeshProUGUI timerText;

    void OnEnable()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = roundDuration.ToString();
        currentRoundTime = roundDuration;
    }

    void Update()
    {
        currentRoundTime -= Time.deltaTime;

        int seconds = Mathf.FloorToInt(currentRoundTime);

        float fractionalPart = currentRoundTime - seconds;
        int milliseconds = Mathf.FloorToInt(fractionalPart * 100);

        timerText.text = $"{seconds}:{milliseconds:D2}";
    }

    void OnDisable()
    {
        currentRoundTime = roundDuration;
    }
}
