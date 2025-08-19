using System;
using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] private int maxCoins;
    [SerializeField] private int minCoins;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject coinPrefab;
    public static int totalCoins;

    public static event Action<int> OnCoinsChanged;

    private void OnEnable()
    {
        GameManager.TotalCoinCount += TotalCoin;
    }

    private void OnDisable()
    {
        GameManager.TotalCoinCount -= TotalCoin;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnCoinRoutine());
    }

    private IEnumerator SpawnCoinRoutine()
    {
        WaitForSeconds wait = new (spawnRate);

        while (true)
        {
            if (totalCoins >= minCoins && totalCoins <= maxCoins)
            {
                SpawnSingleCoin();

                totalCoins++;
                OnCoinsChanged?.Invoke(totalCoins);
            }
            yield return wait;
        }
    }

    private void SpawnSingleCoin()
    {
        float spawnX = UnityEngine.Random.Range(-50f, 50f);
        float spawnY = UnityEngine.Random.Range(-50f, 50f);
        Vector2 spawnPosition = new (spawnX, spawnY);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Spawning new coin at {spawnPosition}");
    }

    private void TotalCoin (int totalCoin)
    {
        totalCoins = totalCoin;
    }

    public void CoinDestroyed()
    {
        totalCoins--;
        OnCoinsChanged?.Invoke(totalCoins);
    }
}
