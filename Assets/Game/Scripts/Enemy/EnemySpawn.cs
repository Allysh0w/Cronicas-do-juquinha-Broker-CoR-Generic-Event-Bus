using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 10;
    public Collider2D area;

    private GameObject[] enemies;

    void Start()
    {
        enemies = new GameObject[maxEnemies];

        for (int i = 0; i < maxEnemies; i++)
            SpawnEnemy(i);
    }

    void Update()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (enemies[i] == null)
                SpawnEnemy(i);
        }
    }

    Vector3 GetRandomPointInArea()
    {
        Bounds b = area.bounds;

        return new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            0
        );
    }

    void SpawnEnemy(int index)
    {
        enemies[index] = Instantiate(enemyPrefab, GetRandomPointInArea(), Quaternion.identity);
    }
}
