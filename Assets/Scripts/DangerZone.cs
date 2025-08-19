using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pursuingDistance = 8;
    [SerializeField] private float wanderingInterval = 2;
    private float distance;
    private float timer;
    private bool isRunning = false;
    private bool hasLogged = false;
    private bool hasUpdateWander = false;
    private Vector2 wanderPosition;
    private Transform playerTransform;
    private Transform coinTransform;

    public static event Action<string> OnPursuing;

    public float GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        GameObject coinObject = GameObject.FindGameObjectWithTag("Coin");

        coinTransform = coinObject.transform;

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            Debug.Log("Player found and cached!");
        }
        else
        {
            this.enabled = false;
            Debug.Log("Player not found! Turning off enemy.");
        }
    }

    private void Update()
    {
        //if player died do nothing
        if (playerTransform == null)
        {
            return;
        }

        EnemyMovement(wanderPosition);

        timer += Time.deltaTime;
        if (timer >= wanderingInterval )
        {
            SetEnemyPosition();
            timer = 0f;
            EnemyMovement(wanderPosition);
        }
    }


    private void EnemyMovement(Vector2 wanderPosition)
    {
        distance = Vector2.Distance(transform.position, playerTransform.position);
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (distance > pursuingDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, wanderPosition, speed * Time.deltaTime);
            isRunning = false;
            if (!isRunning && hasLogged)
            {
                OnPursuing?.Invoke("You don't see any enemy...");
                Debug.Log("Enemy don't see player and start walking...");
                hasLogged = false;
            }
        }
        else if (distance < pursuingDistance)
        {
            float multiplier = 1.5f;
            transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.position, speed * multiplier * Time.deltaTime);
            isRunning = true;
            if (isRunning && !hasLogged)
            {
                OnPursuing?.Invoke("Enemy is chasing you!");
                Debug.Log("Enemy sees player and start chasing!");
                hasLogged = true;
            }
        }
    }

    private void SetEnemyPosition()
    {
        if (!hasUpdateWander)
        {
            float wanderPositionX = UnityEngine.Random.Range(1f, 50f);
            float wanderPositionY = UnityEngine.Random.Range(1f, 50f);
            Debug.Log($"Creating new wander position at {wanderPositionX:F0},{wanderPositionY:F0}.");
            wanderPosition = new Vector2(wanderPositionX, wanderPositionY);
            hasUpdateWander = true;
        }
        else
        {
           hasUpdateWander = false;
        }
    }
}