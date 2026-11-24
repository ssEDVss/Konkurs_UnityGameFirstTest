using UnityEngine;

public class Boar : MonoBehaviour
{
    public float speed = 3f;
    public float detectRange = 12f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 1f;

    Transform player;
    PlayerHealth playerHealth;
    float nextAttack;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            playerHealth = p.GetComponent<PlayerHealth>();
        }
        else Debug.LogWarning("Кабан: не найден объект с тегом Player");
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float dist = toPlayer.magnitude;

        if (dist > detectRange) return;

        transform.rotation = Quaternion.LookRotation(toPlayer);

        if (dist > attackRange)
        {
            transform.position += toPlayer.normalized * speed * Time.deltaTime;
        }
        else if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            Attack();
        }
    }

    void Attack()
    {
        if (playerHealth != null) playerHealth.TakeDamage(damage);
    }
}