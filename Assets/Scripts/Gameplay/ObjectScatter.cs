using UnityEngine;

[System.Serializable]
public class SpawnEntry
{
    public GameObject prefab;
    public int count = 20;
    public float minScale = 1f;
    public float maxScale = 1f;

    [Header("Ломание (0 = нельзя сломать)")]
    public int maxHealth = 0;
    public GameObject dropPrefab;
}

public class ObjectScatter : MonoBehaviour
{
    public SpawnEntry[] entries;
    public float areaSize = 90f;
    public float clearRadius = 5f;
    public Vector3 modelRotation = new Vector3(-90f, 0f, 0f);

    void Start()
    {
        foreach (SpawnEntry entry in entries)
        {
            if (entry.prefab == null) continue;
            for (int i = 0; i < entry.count; i++)
            {
                Spawn(entry);
            }
        }
    }

    void Spawn(SpawnEntry entry)
    {
        Vector2 point = Vector2.zero;
        for (int attempt = 0; attempt < 10; attempt++)
        {
            point = new Vector2(
                Random.Range(-areaSize / 2f, areaSize / 2f),
                Random.Range(-areaSize / 2f, areaSize / 2f));
            if (point.magnitude >= clearRadius) break;
        }

        Quaternion yaw = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        Quaternion rotation = yaw * Quaternion.Euler(modelRotation);
        GameObject item = Instantiate(entry.prefab, new Vector3(point.x, 0f, point.y), rotation, transform);

        float scale = Random.Range(entry.minScale, entry.maxScale);
        item.transform.localScale = entry.prefab.transform.localScale * scale;

        if (entry.maxHealth > 0)
        {
            // если у модели нет коллайдера, добавляем на её меш
            if (item.GetComponentInChildren<Collider>() == null)
            {
                Renderer r = item.GetComponentInChildren<Renderer>();
                if (r != null) r.gameObject.AddComponent<BoxCollider>();
            }

            Health h = item.AddComponent<Health>();
            h.maxHealth = entry.maxHealth;
            h.dropPrefab = entry.dropPrefab;
        }
    }
}