using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Slimy,
        Scoopy
    }

    public EnemyType enemyType;

    public float maxHealth;
    public float currentHealth;

    public HealthBar enemyHealthBar;
    protected GameObject player;
    private PlayerScript playerScript;

    public ParticleSystem dieParticle;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    public virtual void OnDamaged(float amount)
    {
        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
            OnKilled();
            return; 
        }

        else currentHealth -= amount;

        enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public virtual void OnHealed(float amount)
    {
        if (currentHealth + amount > maxHealth) currentHealth = maxHealth;
        else currentHealth += amount;

        enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public virtual void OnKilled()
    {
        Destroy(gameObject);
        if (dieParticle != null) Instantiate(dieParticle, transform.position, Quaternion.identity);
    }
}
