using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 30f;

    [Header("Phantom cloning")]
    public bool canClone = false;       // tick this for phantoms only
    public bool splitOnHit = true;      // clone when hit
    public float cloneInterval = 6f;    // seconds between automatic clones (0 = off)
    public int maxClonesPerPhantom = 2; // how many clones ONE phantom can make
    public int generation = 0;          // 0 = original
    public int maxGeneration = 2;       // clones of clones stop after this
    public int maxAlive = 12;           // global cap on phantoms alive
    public float cloneHealthFactor = 0.7f;
    public float cloneScaleFactor = 0.85f;

    float health;
    int clonesMade;
    float nextClone;
    SpriteRenderer sr;
    Color baseColor;

    void Start()
    {
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        if (sr) baseColor = sr.color;
        nextClone = Time.time + cloneInterval;
        GameManager.Instance.EnemyRegistered();
    }

    void Update()
    {
        if (canClone && cloneInterval > 0f && Time.time >= nextClone)
        {
            nextClone = Time.time + cloneInterval;
            Clone();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            GameManager.Instance.EnemyDefeated();
            Destroy(gameObject);
            return;
        }

        if (sr) StartCoroutine(Flash());
        if (canClone && splitOnHit) Clone();
    }

    void Clone()
    {
        if (clonesMade >= maxClonesPerPhantom) return;
        if (generation >= maxGeneration) return;
        if (GameManager.Instance.enemiesAlive >= maxAlive) return;

        clonesMade++;
        Vector2 offset = Random.insideUnitCircle.normalized * 1f;
        GameObject c = Instantiate(gameObject, (Vector2)transform.position + offset, Quaternion.identity);
        c.transform.localScale = transform.localScale * cloneScaleFactor;

        EnemyHealth h = c.GetComponent<EnemyHealth>();
        h.generation = generation + 1;
        h.maxHealth = Mathf.Max(10f, maxHealth * cloneHealthFactor);
        h.clonesMade = 0;
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (sr) sr.color = baseColor;
    }
}