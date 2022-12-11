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

    [Header("Enemy Settings")]
    public EnemyType enemyType;

    public HealthBar enemyHealthBar;
    public float maxHealth;
    public float currentHealth;


    public AudioClip dieSound;
    public ParticleSystem dieParticle;

    protected GameObject player;
    private PlayerScript playerScript;
    private AudioSource sfxSource;

    public float damage;


    public virtual void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
      //  sfxSource = GameObject.FindGameObjectWithTag("SfxSource").GetComponent<AudioSource>();
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
       // if (dieSound != null) sfxSource.PlayOneShot(dieSound);
    }
}
