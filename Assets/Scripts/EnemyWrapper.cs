using UnityEngine;
using System;

public class EnemyWrapper : MonoBehaviour
{
    private int health;
    private Action<GameObject> onDeath;

    public void Initialize(int maxHealth, Action<GameObject> deathCallback)
    {
        health = maxHealth;
        onDeath = deathCallback;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {health}");
        if (health <= 0)
        {
            onDeath?.Invoke(gameObject);
        }
    }

    public int GetHealth() => health;
}