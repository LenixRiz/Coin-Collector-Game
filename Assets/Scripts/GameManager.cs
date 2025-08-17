using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalCoinCollected = 0;
    private int totalCoin;
    private int finalScore;
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> CountAllCoinsInScene;
    public static event Action<int> OnGameOver;
    public static event Action<int> OnGameWon;

    private void OnEnable()
    {
        PlayerController.OnHealthChanged += HandleHealthChanged;
        Coin.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= HandleHealthChanged;
        Coin.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        InitializeCoinCount();
        OnScoreUpdated?.Invoke(totalCoinCollected);
        CountAllCoinsInScene?.Invoke(totalCoin);
    }

    private void InitializeCoinCount()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        totalCoin = coins.Length;
        Debug.Log($"Total coins: {totalCoin}");
    }

    private void FinalScore()
    {
        float playerHealth = PlayerController.GetPlayerHealth();
        finalScore = (int)Math.Round(totalCoinCollected * playerHealth);
        Debug.Log($"Final score: {finalScore}");
    }

    private void HandleHealthChanged(float health)
    {
        if (health <= 0)
        {
            Debug.Log("You lost!");
            FinalScore();
            OnGameOver?.Invoke(finalScore);
        }
    }

    private void HandleCoinCollected(int value)
    {
        totalCoinCollected += value;
        Debug.Log($"Total coins collected: {totalCoinCollected}");

        totalCoin--;
        CountAllCoinsInScene?.Invoke(totalCoin);
        Debug.Log($"Total coins left: {totalCoin}");

        if (totalCoin <= 0)
        {
            Debug.Log("You won!");
            FinalScore();
            OnGameWon?.Invoke(finalScore);
        }

        OnScoreUpdated?.Invoke(totalCoinCollected);
    }
}
