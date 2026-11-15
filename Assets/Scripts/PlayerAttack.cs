using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float range = 0.8f, offset = 0.9f, damage = 10f, cooldown = 0.4f;
    public float hitDelay = 0.12f; // time until the swing connects

    PlayerController pc;
    Animator anim;
    float nextAttack;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // snap the attack point to 4 directions
        Vector2 d = pc.LastDirection;
        Vector2 dir = Mathf.Abs(d.x) > Mathf.Abs(d.y)
            ? new Vector2(Mathf.Sign(d.x), 0)
            : new Vector2(0, Mathf.Sign(d.y));
        attackPoint.localPosition = dir * offset;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time >= nextAttack)
        {
            nextAttack = Time.time + cooldown;
            anim.SetTrigger("Attack");
            StartCoroutine(DoHit());
        }
    }

    IEnumerator DoHit()
    {
        yield return new WaitForSeconds(hitDelay);

        foreach (var hit in Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer))
        {
            if (hit.TryGetComponent(out EnemyHealth eh)) eh.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint) Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}