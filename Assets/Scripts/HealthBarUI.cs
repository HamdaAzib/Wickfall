using UnityEngine;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    public PlayerWax playerWax;
    public RectTransform cover;   // BarCover
    public TMP_Text healthText;   // HealthText
    public float barWidth = 333f; // width of the orange bar in the frame's units
    public float smooth = 8f;

    float shown = 1f;

    void Update()
    {
        float target = Mathf.Clamp01(playerWax.currentWax / playerWax.maxWax);
        shown = Mathf.Lerp(shown, target, smooth * Time.deltaTime);

        cover.sizeDelta = new Vector2(barWidth * (1f - shown), cover.sizeDelta.y);
        healthText.text = Mathf.CeilToInt(playerWax.currentWax) + " / " + Mathf.CeilToInt(playerWax.maxWax);
    }
}