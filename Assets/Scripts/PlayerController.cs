using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public Vector2 LastDirection { get; private set; } = Vector2.down;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 1f) input.Normalize();

        bool moving = input != Vector2.zero;
        anim.SetBool("IsMoving", moving);

        if (moving)
        {
            LastDirection = input;
            anim.SetFloat("MoveX", input.x); // remove these 2 lines if you have no directional blend tree
            anim.SetFloat("MoveY", input.y);
            // if you only have a side sprite: sr.flipX = input.x < 0;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}