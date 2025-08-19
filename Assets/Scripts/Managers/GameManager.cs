using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    private float totalCoinCollected = 0;
    private int totalCoin;
    private int finalScore;
    private int highscore;
    public static event Action<int> TotalCoinCount;
    public static event Action<float> OnScoreUpdated;
    public static event Action<int> OnGameOver;
    public static event Action<int> OnGameWon;
    public static event Action<int> OnTotalCoinUpdated;
    public static event Action<string, int> OnHighscoreUpdated;

    private void OnEnable()
    {
        PlayerController.OnHealthChanged += HandleHealthChanged;
        CoinManager.OnCoinsChanged += ReceiveCoinFromManager;
        Coin.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= HandleHealthChanged;
        CoinManager.OnCoinsChanged -= ReceiveCoinFromManager;
        Coin.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        // Load the highscore from PlayerPrefs when the game starts. Default to 0 if it doesn't exist.
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        InitializeCoinCount();
        OnScoreUpdated?.Invoke(totalCoinCollected);
        OnTotalCoinUpdated?.Invoke(totalCoin);
        OnHighscoreUpdated?.Invoke("Highscore: ", highscore); // call and announce the saved highscore so it will get updated on ui
    }

    private void InitializeCoinCount()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        totalCoin = coins.Length;
        Debug.Log($"Total coins: {totalCoin}");
        TotalCoinCount?.Invoke(totalCoin);
    }

    private void ReceiveCoinFromManager(int value)
    {
        totalCoin = value;
        OnTotalCoinUpdated?.Invoke(totalCoin);
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

    private void HandleCoinCollected(float value)
    {
        totalCoinCollected += value;
        Debug.Log($"Total coins collected: {totalCoinCollected}");

        totalCoin--;
        Debug.Log($"Total coins left: {totalCoin}");
        OnTotalCoinUpdated?.Invoke(totalCoin);

        if (totalCoin <= 0)
        {
            Debug.Log("You won!");
            FinalScore();
            OnGameWon?.Invoke(finalScore);
            SaveHighScore(finalScore);
        }

        OnScoreUpdated?.Invoke(totalCoinCollected);
    }

    private void SaveHighScore(int value)
    {
        if (finalScore > highscore)
        {
            Debug.Log("Saving new highscore!");
            highscore = finalScore;
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
        }

        int updateHighScore = PlayerPrefs.GetInt("HighScore");
        OnHighscoreUpdated?.Invoke("Highscore: ", updateHighScore);
    }
}