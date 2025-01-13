using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashingEffect : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    Image image;
    Color color;
    float currentTransparent;
    float dashEffectTime;
    float halfDashEffectTime;

    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
        currentTransparent = color.a;
        dashEffectTime = playerMovement.GetDashTime();
        halfDashEffectTime = dashEffectTime / 2;
        playerMovement.dashing += StartDashing;
    }

    void StartDashing()
    {
        StartCoroutine(StartDashEffect(halfDashEffectTime));
    }

    IEnumerator StartDashEffect(float timeForEffect)
    {
        var time = 0f;
        var updateTime = 0.01f;
        var amountOfPaces = timeForEffect / updateTime;
        var pace = timeForEffect / amountOfPaces;
        while (time < timeForEffect)
        {
            time += updateTime;
            currentTransparent += pace;
            color.a = currentTransparent;
            image.color = color;
            yield return new WaitForSeconds(updateTime);
        }
        StartCoroutine(FinishDashEffect(halfDashEffectTime, updateTime, pace));
    }

    IEnumerator FinishDashEffect(float timeForEffect, float updateTime, float pace)
    {
        var time = 0f;
        while (time < timeForEffect)
        {
            time += updateTime;
            currentTransparent -= pace;
            color.a = currentTransparent;
            image.color = color;
            yield return new WaitForSeconds(updateTime);
        }
        currentTransparent = 0f;
    }
}
