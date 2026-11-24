using UnityEngine;

public class BoarSpawner : MonoBehaviour
{
    public GameObject boarPrefab;
    public int startCount = 5;        // сколько кабанов в начале
    public int maxAlive = 8;          // больше этого не появится
    public float respawnTime = 20f;   // раз в сколько секунд добавляется новый
    public float areaSize = 90f;      // как у ObjectScatter
    public float minDistanceFromPlayer = 15f;

    Transform player;
    float nextRespawn;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        for (int i = 0; i < startCount; i++) SpawnOne();
        nextRespawn = Time.time + respawnTime;
    }

    void Update()
    {
        if (Time.time < nextRespawn) return;
        nextRespawn = Time.time + respawnTime;

        if (transform.childCount < maxAlive) SpawnOne();
    }

    void SpawnOne()
    {
        if (boarPrefab == null) return;

        Vector3 pos = Vector3.zero;
        for (int attempt = 0; attempt < 10; attempt++)
        {
            pos = new Vector3(
                Random.Range(-areaSize / 2f, areaSize / 2f),
                boarPrefab.transform.position.y,
                Random.Range(-areaSize / 2f, areaSize / 2f));

            // не спавним слишком близко к игроку
            if (player == null || Vector3.Distance(pos, player.position) >= minDistanceFromPlayer)
                break;
        }

        Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        Instantiate(boarPrefab, pos, rot, transform);
    }
}