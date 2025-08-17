using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    [SerializeField] private int coinValue = 10;
    public static event Action<int> OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("You got a coin!");
            OnCoinCollected?.Invoke(coinValue);

            Destroy(gameObject);
        }
    }
}
