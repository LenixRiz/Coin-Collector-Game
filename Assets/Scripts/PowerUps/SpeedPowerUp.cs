using System;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public static event Action<float, float> OnSpeedPowerUp;

    [SerializeField] private float moveSpeedMultiplier = 1.5f;
    [SerializeField] private float moveSpeedDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("You got a speed power up!");
            OnSpeedPowerUp?.Invoke(moveSpeedMultiplier, moveSpeedDuration);

            Destroy(gameObject);
        }
    }

}
