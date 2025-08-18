using System;
using UnityEngine;

public class CoinPowerUp : MonoBehaviour
{
    public static event Action<float, float> OnCoinPowerUp;

    [SerializeField] private float coinMultiplier = 1.5f;
    [SerializeField] private float coinDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("You got a coin power up!");
            OnCoinPowerUp?.Invoke(coinMultiplier, coinDuration);

            Destroy(gameObject);
        }
    }
}
