using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Brazier : MonoBehaviour
{
    public Light2D flameLight;
    public bool isFinal;
    bool lit;

    void Start() { flameLight.enabled = false; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerWax wax)) return;

        wax.Heal(wax.maxWax); // full refill every time you reach it
        GameManager.Instance.SetCheckpoint(transform.position + Vector3.down);

        if (!lit)
        {
            lit = true;
            flameLight.enabled = true;
            GameManager.Instance.BrazierLit();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isFinal && other.TryGetComponent(out PlayerWax _) && GameManager.Instance.enemiesAlive <= 0)
            GameManager.Instance.LevelComplete();
    }
}