using System;
using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float speed = 5f;
    private float distance;
    [SerializeField] private GameObject player;

    public float GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Player not found!");
        }
    }
}