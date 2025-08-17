using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalCoinCollected = 0;
    public static event Action<int> OnScoreUpdated;

    private void OnEnable()
    {
        Coin.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        OnScoreUpdated?.Invoke(totalCoinCollected);
    }

    private void HandleCoinCollected(int value)
    {
        totalCoinCollected += value;
        Debug.Log($"Total coins collected: {totalCoinCollected}");

        OnScoreUpdated?.Invoke(totalCoinCollected);
    }
}
