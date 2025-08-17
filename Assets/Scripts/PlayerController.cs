using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float health = 100f;
    private Vector2 moveInput;

    public static event Action<float> OnPlayerDamaged;
    public static Func<float> GetPlayerHealth;

    private void OnEnable()
    {
        GetPlayerHealth = () => health;
    }

    private void OnDisable()
    {
        GetPlayerHealth = null;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (health <= 0)
        {
            Debug.Log("Player died!");
            health = 0;
            Destroy(gameObject);
        }

        Debug.Log($"Player got injuried! Health {health}");
        OnPlayerDamaged?.Invoke(health);
    }

}
