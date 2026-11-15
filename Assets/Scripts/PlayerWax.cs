using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerWax : MonoBehaviour
{
    [Header("Health (wax)")]
    public float maxWax = 100f;
    public float currentWax;

    [Header("Light")]
    public Light2D playerLight;
    public float maxRadius = 6f;       // at 100/100
    public float minRadius = 1.5f;     // at 0/100
    public float maxIntensity = 1.3f;  // strong at 100/100
    public float minIntensity = 0.1f;  // very soft near 0
    public float lightSmooth = 6f;     // how fast the light follows changes
    public float flickerAmount = 0.08f;

    float shownRadius, shownIntensity;

    void Start()
    {
        currentWax = maxWax;
        shownRadius = maxRadius;
        shownIntensity = maxIntensity;
    }

    void Update()
    {
        float t = Mathf.Clamp01(currentWax / maxWax); // 1 = full, 0.2 = 20/100

        float targetRadius = Mathf.Lerp(minRadius, maxRadius, t);
        float targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        // smooth transition when taking damage or healing
        shownRadius = Mathf.Lerp(shownRadius, targetRadius, lightSmooth * Time.deltaTime);
        shownIntensity = Mathf.Lerp(shownIntensity, targetIntensity, lightSmooth * Time.deltaTime);

        // candle flicker
        float flicker = (Mathf.PerlinNoise(Time.time * 3f, 0f) - 0.5f) * 2f * flickerAmount;

        playerLight.pointLightOuterRadius = shownRadius * (1f + flicker);
        playerLight.intensity = shownIntensity * (1f + flicker);
    }

    public void TakeDamage(float amount)
    {
        currentWax = Mathf.Max(0f, currentWax - amount);
        if (currentWax <= 0f) GameManager.Instance.Respawn();
    }

    public void Heal(float amount)
    {
        currentWax = Mathf.Min(maxWax, currentWax + amount);
    }
}