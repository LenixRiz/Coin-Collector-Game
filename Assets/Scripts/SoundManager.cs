using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioClip coinCollectedSound;
    [SerializeField] private AudioClip playerTakeDamageSound;
    private AudioSource audioSource;

    private void OnEnable()
    {
        Coin.OnCoinCollected += CoinCollectedPlay;
        PlayerController.OnPlayerDamaged += PlayerTakeDamagePlay;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCollectedPlay;
        PlayerController.OnPlayerDamaged -= PlayerTakeDamagePlay;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void CoinCollectedPlay(int value)
    {
        if (coinCollectedSound != null)
        {
            audioSource.PlayOneShot(coinCollectedSound);
        }
    }

    private void PlayerTakeDamagePlay(float health)
    {
        if (playerTakeDamageSound != null)
        {
            audioSource.PlayOneShot(playerTakeDamageSound);
        }
    }
}
