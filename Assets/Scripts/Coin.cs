using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float coinValue = 10;

    public static event Action<float> OnCoinCollected;
    public static event Action<string, float> OnPowerUpStatusUpdated;

    private void OnEnable()
    {
        CoinPowerUp.OnCoinPowerUp += OnCoinPowerUp;
    }

    private void OnDisable()
    {
        CoinPowerUp.OnCoinPowerUp -= OnCoinPowerUp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("You got a coin!");
            OnCoinCollected?.Invoke(coinValue);

            Destroy(gameObject);
        }
    }

    private IEnumerator CoinBoostPowerUpCoroutine(float multiplier, float duration)
    {
        float originalCoinValue = coinValue;
        coinValue *= multiplier;

        for (float i = duration; i > 0; i--)
        {
            OnPowerUpStatusUpdated?.Invoke("Coin", i);
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Coin Power Up Ended!");
        OnPowerUpStatusUpdated?.Invoke("Coin", 0f);
        coinValue = originalCoinValue;
    }

    private void OnCoinPowerUp(float coinMultiplier, float coinDuration)
    {
        StartCoroutine(CoinBoostPowerUpCoroutine(coinMultiplier, coinDuration));
    }
}
