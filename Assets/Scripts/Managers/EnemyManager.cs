using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int maxEnemies = 10;
    private WaitForSeconds wait;
    private Vector3 spawnPosition;

    private static int currentEnemiesCount;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            wait = new WaitForSeconds(spawnRate);

            if (currentEnemiesCount <= maxEnemies)
            {
                SpawnSingleEnemy();
            }

            yield return wait;
        }
    }

    private void SpawnSingleEnemy()
    {
        float spawnX = Random.Range(-50f, 50f);
        float spawnY = Random.Range(-50f, 50f);
        spawnPosition = new Vector3(spawnX, spawnY, 0);
        Debug.Log($"Creating new enemy at {spawnPosition}");

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        currentEnemiesCount++;
        Debug.Log($"Enemy spawned! Total enemy {currentEnemiesCount}");
    }

    public void EnemyDestroyed()
    {
        if (currentEnemiesCount > 0)
        { 
            currentEnemiesCount--;
        }
    }
}
