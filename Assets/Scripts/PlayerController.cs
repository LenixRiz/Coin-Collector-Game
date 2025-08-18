using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float health = 100f;
    private Vector2 moveInput;

    public static event Action<float> OnHealthChanged;
    public static event Action OnPlayerTookDamaged;
    public static Func<float> GetPlayerHealth;
    public static event Action<string, float> OnPowerUpStatusUpdated;

    private void OnEnable()
    {
        SpeedPowerUp.OnSpeedPowerUp += OnSpeedPowerUp;
        Heal.OnPlayerHeal += HealPlayerHealth;

        GetPlayerHealth = () => health;
    }

    private void OnDisable()
    {
        SpeedPowerUp.OnSpeedPowerUp -= OnSpeedPowerUp;
        Heal.OnPlayerHeal -= HealPlayerHealth;

        GetPlayerHealth = null;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Announce our starting health to any listeners (like the UI) 
        // So there will be no null error if player got deleted before the game played
        OnHealthChanged?.Invoke(health);
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; //* Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out DangerZone dangerZone))
        {
            TakeDamage(dangerZone.GetDamage());
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log($"Player got injuried! Health {health}");
        OnHealthChanged?.Invoke(health);
        OnPlayerTookDamaged?.Invoke();

        if (health <= 0)
        {
            Debug.Log("Player died!");
            health = 0;
            Destroy(gameObject);
        }
    }

    private IEnumerator SpeedPowerUpCoroutine(float multiplier, float duration)
    {
        float originalMoveSpeed = moveSpeed;
        moveSpeed *= multiplier;

        // Countdown from the full duration down to 1
        for (float i = duration; i > 0; i--)
        {
            OnPowerUpStatusUpdated?.Invoke("Speed", i);
            yield return new WaitForSeconds(1f);
        }

        // Power-up has ended
        moveSpeed = originalMoveSpeed;
        OnPowerUpStatusUpdated?.Invoke("Speed", 0f); // Send 0 to hide the UI
        Debug.Log("Speed power up ended!");
    }

    private void OnSpeedPowerUp(float moveSpeedMultiplier, float moveSpeedDuration)
    {
        StartCoroutine(SpeedPowerUpCoroutine(moveSpeedMultiplier, moveSpeedDuration));
    }

    private void HealPlayerHealth(float healAmount)
    {
        health += healAmount;
        OnHealthChanged?.Invoke(health);
    }

}
