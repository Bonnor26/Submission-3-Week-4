using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int count = 8;
    public float minSpawnDistance = 3f;
    public float maxSpawnDistance = 8f;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            // Random angle (radians) and distance -> position around the player
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float dist = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * dist;

            GameObject enemy = Instantiate(enemyPrefab, player.position + offset, Quaternion.identity);
            enemy.GetComponent<EnemyController>().player = player;
        }
    }
}

