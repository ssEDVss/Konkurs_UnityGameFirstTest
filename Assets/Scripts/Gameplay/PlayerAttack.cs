using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float range = 3f;
    public int damage = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Attack();
    }

    void Attack()
    {
        Debug.Log("Клик: атака");
        Collider[] hits = Physics.OverlapSphere(transform.position, range);

        Health best = null;
        float bestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            Health h = hit.GetComponentInParent<Health>();
            if (h == null || h.gameObject == gameObject) continue;

            float d = (h.transform.position - transform.position).sqrMagnitude;
            if (d < bestDist) { bestDist = d; best = h; }
        }

        if (best != null) best.TakeDamage(damage);
        else Debug.Log("Рядом нет объектов с Health");
    }
}