using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;

    [SerializeField] private int _numberOfEnemies = 5;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float _spawnHeight = 0f;

    private Collider2D playerCollider;
    private LayerMask _layer;

    private void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject enemyPrefab = GetRandomEnemyPrefab();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);
            bool isBlocked = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider == playerCollider || collider.CompareTag("Enemy"))
                {
                    isBlocked = true;
                    break;
                }
            }

            if (!isBlocked && IsPositionValid(spawnPosition))
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                SetEnemySpawnHeight(enemy);
            }
            else
            {
                i--;
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(0f, spawnRadius);
        float randomY = Random.Range(-spawnRadius, spawnRadius);
        Vector2 spawnPosition = transform.position + new Vector3(randomX, _spawnHeight, randomY);

        Vector2 spawnerPosition = transform.position;
        Vector2 playerPosition = playerCollider.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(spawnerPosition, playerPosition - spawnerPosition, Vector2.Distance(spawnerPosition, playerPosition), _layer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            spawnPosition = playerPosition + (spawnPosition - playerPosition).normalized * spawnRadius;
        }

        return spawnPosition;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[randomIndex];
    }

    private bool IsPositionValid(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1f, _layer);
        return colliders.Length == 0;
    }

    private void SetEnemySpawnHeight(GameObject enemy)
    {
        Vector3 enemyPosition = enemy.transform.position;
        enemyPosition.y = _spawnHeight;
        enemy.transform.position = enemyPosition;
    }
}
