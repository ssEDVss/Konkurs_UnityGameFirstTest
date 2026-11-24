using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public GameObject dropPrefab;
    public int current;            // видно в Inspector

    Vector3 baseScale;
    float shakeTimer;

    void Start()
    {
        current = maxHealth;
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            float k = 1f + Mathf.Sin(shakeTimer * 60f) * 0.08f;
            transform.localScale = baseScale * k;
            if (shakeTimer <= 0f) transform.localScale = baseScale;
        }
    }

    public void TakeDamage(int amount)
    {
        current -= amount;
        shakeTimer = 0.2f;
        Debug.Log(name + ": здоровье " + current + "/" + maxHealth);
        if (current <= 0) Die();
    }

    void Die()
    {
        if (dropPrefab != null)
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}