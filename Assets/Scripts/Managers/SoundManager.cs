using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinCollectedSound;
    [SerializeField] private AudioClip playerTakeDamageSound;
    [SerializeField] private AudioClip speedPowerUpSound;
    [SerializeField] private AudioClip coinPowerUpSound;
    [SerializeField] private AudioClip playerHealSound;
    private AudioSource audioSource;

    private void OnEnable()
    {
        Coin.OnCoinCollected += CoinCollectedPlay;
        PlayerController.OnPlayerTookDamaged += PlayerTakeDamagePlay;
        SpeedPowerUp.OnSpeedPowerUp += SpeedPowerUpPlay;
        CoinPowerUp.OnCoinPowerUp += CoinPowerUpPlay;
        Heal.OnPlayerHeal += PlayerHealPlay;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCollectedPlay;
        PlayerController.OnPlayerTookDamaged -= PlayerTakeDamagePlay;
        SpeedPowerUp.OnSpeedPowerUp -= SpeedPowerUpPlay;
        CoinPowerUp.OnCoinPowerUp -= CoinPowerUpPlay;
        Heal.OnPlayerHeal -= PlayerHealPlay;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void CoinCollectedPlay(float _)
    {
        if (coinCollectedSound != null)
        {
            audioSource.PlayOneShot(coinCollectedSound);
        }
    }

    private void PlayerTakeDamagePlay()
    {
        if (playerTakeDamageSound != null)
        {
            audioSource.PlayOneShot(playerTakeDamageSound);
        }
    }

    private void SpeedPowerUpPlay(float _, float __)
    {
        if (speedPowerUpSound != null)
        {
            audioSource.PlayOneShot(speedPowerUpSound);
        }
    }

    private void CoinPowerUpPlay(float _, float __)
    {
        if (coinPowerUpSound != null)
        {
            audioSource.PlayOneShot(coinPowerUpSound);
        }
    }

    private void PlayerHealPlay(float _)
    {
        if (playerHealSound != null)
        {
            audioSource.PlayOneShot(playerHealSound);
        }
    }
}
