using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public Transform[] patrolPoints; // optional

    Rigidbody2D rb;
    Transform player;
    PlayerWax playerWax;
    float nextAttack;
    int patrolIndex;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        player = p.transform;
        playerWax = p.GetComponent<PlayerWax>();
    }

    void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            if (Time.time >= nextAttack)
            {
                nextAttack = Time.time + attackCooldown;
                playerWax.TakeDamage(attackDamage);
            }
        }
        else if (dist <= detectionRange)
        {
            MoveTowards(player.position);
        }
        else if (patrolPoints != null && patrolPoints.Length > 0)
        {
            MoveTowards(patrolPoints[patrolIndex].position);
            if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.2f)
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}