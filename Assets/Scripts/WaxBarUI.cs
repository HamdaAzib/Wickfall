using UnityEngine;
using UnityEngine.UI;

public class WaxBarUI : MonoBehaviour
{
    public PlayerWax playerWax;
    public float smooth = 8f;
    public Color fullColor = new Color(0.91f, 0.64f, 0.24f);  // orange
    public Color lowColor = new Color(0.8f, 0.15f, 0.1f);     // red

    Image fill;
    float shown = 1f;

    void Awake() { fill = GetComponent<Image>(); }

    void Update()
    {
        float target = playerWax.currentWax / playerWax.maxWax;
        shown = Mathf.Lerp(shown, target, smooth * Time.deltaTime);
        fill.fillAmount = shown;
        fill.color = Color.Lerp(lowColor, fullColor, shown);
    }
}