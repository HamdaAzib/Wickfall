using UnityEngine;

public class WaxPickup : MonoBehaviour
{
    public float healAmount = 25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerWax wax))
        {
            wax.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}