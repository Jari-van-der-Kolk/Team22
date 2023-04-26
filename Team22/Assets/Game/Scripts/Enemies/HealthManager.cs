using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    public float maxHealth;
    public float Health { get; set; }

    [SerializeField] private float DEBUGHEALTH;
    private void Awake()
    {
        Health = maxHealth;
    }

    private void Update()
    {
        DEBUGHEALTH = Health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (!gameObject.CompareTag("Player"))
        {
            HandleDeath();
        }
        if (gameObject.CompareTag("Player"))
        {

        }
    }

    private void HandleDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
