using System;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public static event Action<float> OnPlayerHeal;

    [SerializeField] private float healAmount = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("You got a heal!");
            OnPlayerHeal?.Invoke(healAmount);

            Destroy(gameObject);
        }
    }
}
