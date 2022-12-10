using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public HealthBar enemyHealthBar;
    protected GameObject player;
    private PlayerScript playerScript;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    public virtual void Damage(float amount)
    {
        if (currentHealth - amount < 0) currentHealth = 0;
        else currentHealth -= amount;

        enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public virtual void Heal(float amount)
    {
        if (currentHealth + amount > maxHealth) currentHealth = maxHealth;
        else currentHealth += amount;

        enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public virtual void Kill()
    {
        currentHealth = 0;
        enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
